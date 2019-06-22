using DaedalusLanguageServer.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OmniSharp.Extensions.LanguageServer.Protocol.Models;
using OmniSharp.Extensions.LanguageServer.Server;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace DaedalusLanguageServer
{
    internal static class Program
    {
        private static async Task Main(string[] args)
        {
            var logLevel = args.Any(x => x == "-debug") ? LogLevel.Trace : LogLevel.Warning;

            var server = await LanguageServer.From(options =>
                options
                    .WithInput(Console.OpenStandardInput())
                    .WithOutput(Console.OpenStandardOutput())
                    .WithLoggerFactory(new LoggerFactory())
                    .AddDefaultLoggingProvider()
                    .WithMinimumLogLevel(logLevel)
                    .WithServices(ConfigureServices)
                    .WithHandler<TextDocumentSyncHandler>()
                    .WithHandler<DidChangeWatchedFilesHandler>()
                    .WithHandler<DidChangeWorkspaceFoldersHandler>()
                    .WithHandler<DocumentSymbolHandler>()
                    .WithHandler<CompletionHandler>()
                    .WithHandler<HoverHandler>()
                    .WithHandler<InitializeHandler>()
                 );

            await server.WaitForExit;
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<BufferManager>();
            services.AddSingleton<ParsedDocumentsManager>();
        }
    }
    public class ParsedDocumentsManager
    {
        private readonly ConcurrentDictionary<Uri, DaedalusCompiler.Compilation.Compiler.ParseResult> LastParseResults
            = new ConcurrentDictionary<Uri, DaedalusCompiler.Compilation.Compiler.ParseResult>();

        public ParsedDocumentsManager()
        {

        }

        public DaedalusCompiler.Compilation.Compiler.ParseResult GetParseResult(Uri uri)
        {
            if (LastParseResults.TryGetValue(uri, out var val))
            {
                return val;
            }
            return null;
        }

        public void UpdateParseResult(Uri uri, DaedalusCompiler.Compilation.Compiler.ParseResult parserResult)
        {
            LastParseResults.AddOrUpdate(uri, parserResult, (u, oldParse) => parserResult);
        }

        public IReadOnlyDictionary<Uri, DaedalusCompiler.Compilation.Compiler.ParseResult> GetDocuments()
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

            DaedalusCompiler.Compilation.Compiler.ParseResult parserResult = null;
            if (string.IsNullOrWhiteSpace(text))
            {
                parserResult = DaedalusCompiler.Compilation.Compiler.Load(path);
            }
            else
            {
                parserResult = DaedalusCompiler.Compilation.Compiler.Parse(text);
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


