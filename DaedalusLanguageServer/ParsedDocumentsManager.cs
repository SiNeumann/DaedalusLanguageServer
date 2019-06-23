﻿using DaedalusCompiler.Compilation;
using DaedalusCompiler.Compilation.Symbols;
using OmniSharp.Extensions.LanguageServer.Protocol.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;

namespace DaedalusLanguageServer
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

            symbolsLookup.AddOrUpdate(uri, lookup, (uri, old) => lookup);
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

        public PublishDiagnosticsParams Parse(Uri uri, string text, CancellationToken cancellation)
        {
            var path = uri.LocalPath;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows) && path.StartsWith("/"))
            {
                path = Path.GetFullPath(path.Substring(1));
            }
            // Workaround: Skip externals. Too many wrong function definitions 
            if (path.Contains("AI_Intern", StringComparison.OrdinalIgnoreCase) && path.EndsWith("Externals.d", StringComparison.OrdinalIgnoreCase)) return null;

            ParseResult parserResult = null;
            if (string.IsNullOrWhiteSpace(text))
            {
                parserResult = Compiler.Load(path);
            }
            else
            {
                parserResult = Compiler.Parse(text, uri);
            }
            if (parserResult.SyntaxErrors.Count > 0)
            {
                return new PublishDiagnosticsParams
                {
                    Uri = uri,
                    Diagnostics = new Container<Diagnostic>(parserResult.SyntaxErrors
                        .Select(x => new Diagnostic
                        {
                            Message = x.Message,
                            Range = new OmniSharp.Extensions.LanguageServer.Protocol.Models.Range(new Position(x.Line - 1, x.Column), new Position(x.Line - 1, x.Column)),
                        }))
                };
            }
            UpdateParseResult(uri, parserResult);
            return null;
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


