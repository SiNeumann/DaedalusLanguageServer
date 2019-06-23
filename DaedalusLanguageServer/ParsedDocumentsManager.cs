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

namespace DaedalusLanguageServer
{
    public class ParsedDocumentsManager
    {
        private readonly ConcurrentDictionary<Uri, ParseResult> LastParseResults
            = new ConcurrentDictionary<Uri, ParseResult>();

        private readonly ConcurrentDictionary<Uri, ILookup<string, Symbol>> symbolsLookup
            = new ConcurrentDictionary<Uri, ILookup<string, Symbol>>();

        public ParsedDocumentsManager()
        {}

        public ParseResult GetParseResult(Uri uri)
        {
            if (LastParseResults.TryGetValue(uri, out var val))
            {
                return val;
            }
            return null;
        }

        public void UpdateParseResult(Uri uri, ParseResult parserResult)
        {
            LastParseResults.AddOrUpdate(uri, parserResult, (u, oldParse) => parserResult);
            var lookup = parserResult.GlobalClasses.Cast<Symbol>()
                .Concat(parserResult.GlobalConstants)
                .Concat(parserResult.GlobalFunctions)
                .Concat(parserResult.GlobalPrototypes)
                .Concat(parserResult.GlobalVariables)
                .Concat(parserResult.GlobalInstances)
                .ToLookup(c => c.Name.ToUpper());

            symbolsLookup.AddOrUpdate(uri, lookup, (uri, old) => lookup);
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
    }
}


