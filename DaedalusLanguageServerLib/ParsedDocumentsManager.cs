using DaedalusCompiler.Compilation;
using DaedalusCompiler.Compilation.Symbols;
using OmniSharp.Extensions.LanguageServer.Protocol.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;

namespace DaedalusLanguageServerLib
{
    public class ParsedDocumentsManager
    {
        private readonly ConcurrentDictionary<Uri, ParseResult> LastParseResults
            = new ConcurrentDictionary<Uri, ParseResult>();

        private readonly ConcurrentDictionary<Uri, ILookup<string, Symbol>> symbolsLookup
            = new ConcurrentDictionary<Uri, ILookup<string, Symbol>>();

        private readonly ConcurrentDictionary<Uri, List<CompletionItem>> symbolCompletions
            = new ConcurrentDictionary<Uri, List<CompletionItem>>();

        public ParsedDocumentsManager()
        { }

        public ParseResult GetParseResult(Uri uri)
        {
            if (LastParseResults.TryGetValue(uri, out var val))
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
                symbolCompletions[uri] = completionItems.ToList();
            }
            else
            {
                foreach (var kvp in LastParseResults)
                {
                    UpdateSymbolCompletions(kvp.Key);
                }
            }
        }

        public void UpdateParseResult(Uri uri, ParseResult parserResult)
        {
            LastParseResults.AddOrUpdate(uri, parserResult, (u, oldParse) => parserResult);
            var lookup = parserResult.EnumerateSymbols()
                .ToLookup(c => c.Name.ToUpper());

            symbolsLookup.AddOrUpdate(uri, lookup, (key, old) => lookup);
            UpdateSymbolCompletions(uri);
        }

        public void Delete(Uri uri)
        {
            LastParseResults.Remove(uri, out _);
            symbolsLookup.Remove(uri, out _);
        }
        public ICollection<CompletionItem> GetCompletionSymbols()
        {
            var symbols = new HashSet<CompletionItem>();
            foreach (var kvp in symbolCompletions)
            {
                foreach (var s in kvp.Value)
                {
                    symbols.Add(s);
                }
            }
            return symbols;
        }
        public List<Symbol> GetGlobalSymbols()
        {
            var symbols = new List<Symbol>();
            foreach (var kvp in LastParseResults)
            {
                foreach (var s in kvp.Value.EnumerateSymbols())
                {
                    symbols.Add(s);
                }
            }
            return symbols;
        }
        public Symbol LookupSymbol(string identifier)
        {
            identifier = identifier.ToUpper();
            foreach (var item in symbolsLookup)
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
            return LastParseResults;
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
                parserResult = Compiler.Load(localPath, detailed);
            }
            else
            {
                parserResult = Compiler.Parse(text, newUri, detailed);
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

        public List<PublishDiagnosticsParams> ParseSrc(string srcPath, int numThreads = 1)
        {
            var parseResults = Compiler.ParseSrc(srcPath, numThreads);
            foreach (var parseResult in parseResults)
            {
                UpdateParseResult(parseResult.Key, parseResult.Value);
            }
            var dp = new List<PublishDiagnosticsParams>();
            foreach (var kvp in parseResults)
            {
                if (kvp.Value.SyntaxErrors.Count > 0)
                {
                    dp.Add(new PublishDiagnosticsParams
                    {
                        Uri = kvp.Key,
                        Diagnostics = new Container<Diagnostic>(
                            kvp.Value.SyntaxErrors.Select(x => new Diagnostic
                            {
                                Severity = DiagnosticSeverityFromSyntaxError(x),
                                Message = x.ErrorCode.Description,
                                Code = x.ErrorCode.Code,
                                Range = new OmniSharp.Extensions.LanguageServer.Protocol.Models.Range(
                                            new Position(x.Line - 1, x.Column),
                                            new Position(x.Line - 1, x.Column)
                                        )
                            }))
                    });
                }
            }
            return dp;
        }
        private static DiagnosticSeverity DiagnosticSeverityFromSyntaxError(SyntaxError syntaxError)
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


