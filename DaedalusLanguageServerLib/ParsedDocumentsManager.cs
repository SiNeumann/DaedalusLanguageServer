using DaedalusCompiler.Compilation;
using DaedalusCompiler.Compilation.Symbols;
using OmniSharp.Extensions.LanguageServer.Protocol.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace DaedalusLanguageServerLib
{
    public class ParsedDocumentsManager
    {
        private readonly ConcurrentDictionary<Uri, ParseResult> DocumentLastParseResults
            = new ConcurrentDictionary<Uri, ParseResult>();

        private readonly ConcurrentDictionary<Uri, ILookup<string, Symbol>> documentSymbolsLookup
            = new ConcurrentDictionary<Uri, ILookup<string, Symbol>>();

        private readonly ConcurrentDictionary<Uri, List<CompletionItem>> documentSymbolCompletions
            = new ConcurrentDictionary<Uri, List<CompletionItem>>();

        public ParsedDocumentsManager()
        { }

        public ParseResult GetParseResult(Uri uri)
        {
            if (DocumentLastParseResults.TryGetValue(uri, out var val))
            {
                return val;
            }
            return null;
        }

        private void UpdateSymbolCompletions(Uri uri = null)
        {
            if (uri != null)
            {
                var parsedResult = GetParseResult(uri);
                if (parsedResult is null) return;

                var completionItems = parsedResult.EnumerateSymbols().Select(s => new CompletionItem
                {
                    Kind = KindFromSymbol(s),
                    Label = s.Name,
                    Detail = s.ToString(),
                });
                documentSymbolCompletions[uri] = completionItems.ToList();
            }
            else
            {
                foreach (var kvp in DocumentLastParseResults)
                {
                    UpdateSymbolCompletions(kvp.Key);
                }
            }
        }

        public void UpdateParseResult(Uri uri, ParseResult parserResult)
        {
            DocumentLastParseResults.AddOrUpdate(uri, parserResult, (u, oldParse) => parserResult);
            var lookup = parserResult.EnumerateSymbols()
                .ToLookup(c => c.Name, StringComparer.OrdinalIgnoreCase);

            documentSymbolsLookup.AddOrUpdate(uri, lookup, (key, old) => lookup);
            UpdateSymbolCompletions(uri);
        }

        public void Delete(Uri uri)
        {
            DocumentLastParseResults.Remove(uri, out _);
            documentSymbolsLookup.Remove(uri, out _);
        }
        public ICollection<CompletionItem> GetCompletionSymbols(Uri uri = null, Position position = null)
        {
            if (uri != null)
            {
                var symbols = new List<CompletionItem>();

                foreach (var kvp in documentSymbolCompletions)
                {
                    if (kvp.Key == uri) continue;

                    foreach (var s in kvp.Value)
                    {
                        symbols.Add(s);
                    }
                }
                return symbols;
            }
            else
            {
                var symbols = new HashSet<CompletionItem>();
                foreach (var kvp in documentSymbolCompletions)
                {
                    foreach (var s in kvp.Value)
                    {
                        symbols.Add(s);
                    }
                }
                return symbols;
            }
        }
        public List<Symbol> GetGlobalSymbols()
        {
            var symbols = new List<Symbol>();
            foreach (var kvp in DocumentLastParseResults)
            {
                foreach (var s in kvp.Value.EnumerateSymbols())
                {
                    symbols.Add(s);
                }
            }
            return symbols;
        }
        public Symbol LookupInFunctionSymbol(Uri documentUri, string functionName, string identifier)
        {
            var fn = this.DocumentLastParseResults[documentUri].GlobalFunctions.FirstOrDefault(x => string.Equals(x.Name, functionName, StringComparison.OrdinalIgnoreCase));
            if (fn is null)
            {
                return null;
            }

            Symbol sym = fn.Parameters.FirstOrDefault(x => string.Equals(x.Name, identifier, StringComparison.OrdinalIgnoreCase));
            if (sym is null)
            {
                sym = fn.LocalVariables.FirstOrDefault(x => string.Equals(x.Name, identifier, StringComparison.OrdinalIgnoreCase));
            }
            return sym;
        }

        public Symbol LookupSymbol(string identifier)
        {
            foreach (var item in documentSymbolsLookup)
            {
                var symbols = item.Value;
                if (symbols.Contains(identifier))
                {
                    return symbols[identifier].First();
                }
            }
            return null;
        }

        public IReadOnlyDictionary<Uri, ParseResult> GetDocuments()
        {
            return DocumentLastParseResults;
        }

        public PublishDiagnosticsParams ParseDetailed(Uri uri, string text, CancellationToken cancellation)
        {
            return ParseInternal(uri, text, true, cancellation);
        }
        public PublishDiagnosticsParams Parse(Uri uri, string text, CancellationToken cancellation)
        {
            return ParseInternal(uri, text, false, cancellation);
        }

        private PublishDiagnosticsParams ParseInternal(Uri uri, string text, bool detailed, CancellationToken cancellation)
        {
            var newUri = new Uri(Uri.UnescapeDataString(uri.AbsoluteUri));
            var localPath = newUri.LocalPath;

            // Workaround: Skip externals. Too many wrong function definitions 
            if (localPath.Contains("AI_Intern", StringComparison.OrdinalIgnoreCase) && localPath.EndsWith("Externals.d", StringComparison.OrdinalIgnoreCase)) return null;
            
            ParseResult parserResult = null;
            if (string.IsNullOrWhiteSpace(text))
            {
                parserResult = FastCompiler.Load(localPath, detailed, GetDocuments().Values);
            }
            else
            {
                parserResult = FastCompiler.Parse(text, newUri, detailed, GetDocuments().Values);
            }
            PublishDiagnosticsParams result = null;
            if (parserResult.SyntaxErrors.Count > 0)
            {
                result = new PublishDiagnosticsParams
                {
                    Uri = newUri,
                    Diagnostics = new Container<Diagnostic>(parserResult.SyntaxErrors
                        .Select(x => new Diagnostic
                        {
                            Message = x.ErrorCode.Description,
                            Code = x.ErrorCode.Code,
                            //Source = localPath,
                            Severity = DiagnosticSeverityFromSyntaxError(x),
                            Range = new OmniSharp.Extensions.LanguageServer.Protocol.Models.Range(new Position(x.Line - 1, x.Column), new Position(x.Line - 1, x.Column)),
                        }))
                };
            }
            UpdateParseResult(uri, parserResult);
            return result;
        }

        public static DiagnosticSeverity DiagnosticSeverityFromSyntaxError(SyntaxError syntaxError)
        {
            switch (syntaxError.ErrorCode.Severity)
            {
                case ErrorSeverity.Info:
                    return DiagnosticSeverity.Information;
                case ErrorSeverity.Warning:
                    return DiagnosticSeverity.Warning;
                case ErrorSeverity.Error:                    
                default:
                    return DiagnosticSeverity.Error;
            }
        }
        private CompletionItemKind KindFromSymbol(Symbol s)
        {
            switch (s)
            {
                case Function _: return CompletionItemKind.Function;
                case Class _: return CompletionItemKind.Class;
                case Prototype _: return CompletionItemKind.Class;
                case Constant _: return CompletionItemKind.Constant;
                case Variable _: return CompletionItemKind.Variable;
            }
            return CompletionItemKind.Value;
        }
    }
}


