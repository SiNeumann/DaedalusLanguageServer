using DaedalusLanguageServer.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OmniSharp.Extensions.LanguageServer.Protocol.Server;
using OmniSharp.Extensions.LanguageServer.Server;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DaedalusLanguageServer
{
    public static class Program
    {
        public static readonly string AppDir = Path.GetDirectoryName(typeof(Program).Assembly.Location);
        private static async Task Main(string[] args)
        {
            // Add support for Windows codepages like 1250
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

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
                    .WithHandler<GoToDefinitionHandler>()
                    .WithHandler<SignatureInfoHandler>()
                    .WithHandler<InitializeHandler>()
                 );
            var docManager = server.Services.GetRequiredService<ParsedDocumentsManager>();
            ParseBuiltIns(docManager);
            if (File.Exists("Gothic.src"))
            {
                var router = server.Services.GetRequiredService<OmniSharp.Extensions.LanguageServer.Protocol.Server.ILanguageServer>();
                var srcPath = Path.GetFullPath("Gothic.src");
                await Task.Run(() => ParseSrc(srcPath, router, docManager));
            }
            await server.WaitForExit;
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<BufferManager>();
            services.AddSingleton<ParsedDocumentsManager>();
        }

        private static void ParseBuiltIns(ParsedDocumentsManager parsedDocumentsManager)
        {
            var symbolInfoPath = Path.Combine(AppDir, "DaedalusBuiltins", "symbols.json");
            Dictionary<string, string> documentations = null;
            if (File.Exists(symbolInfoPath))
            {
                var symbolInfos = JsonSerializer.Parse<List<SymbolDocumentation>>(File.ReadAllText(symbolInfoPath, Encoding.UTF8));
                documentations = symbolInfos.ToDictionary(x => x.Name, x => x.Documentation, StringComparer.OrdinalIgnoreCase);
            }

            var buildInsPath = Path.Combine(AppDir, "DaedalusBuiltins");
            foreach (var builtIn in Directory.EnumerateFiles(buildInsPath, "*.d"))
            {
                var builtInUri = new Uri(builtIn);
                parsedDocumentsManager.Parse(builtInUri, File.ReadAllText(builtIn, Encoding.GetEncoding(1250)), default);
                var parsedResult = parsedDocumentsManager.GetParseResult(builtInUri);
                if (documentations != null)
                {
                    foreach (var symbol in parsedResult.EnumerateSymbols())
                    {
                        if (documentations.TryGetValue(symbol.Name, out var d))
                        {
                            symbol.Documentation = d;
                        }
                    }
                }
            }
        }

        private static void ParseSrc(string srcPath, OmniSharp.Extensions.LanguageServer.Protocol.Server.ILanguageServer router, ParsedDocumentsManager parsedDocumentsManager)
        {
            var cpus = Environment.ProcessorCount;
            if (cpus > 1) cpus--;
            router.Window.LogInfo($"Parsing Gothic.Src using {cpus} threads. This might take a while.");
            var parseResults = DaedalusCompiler.Compilation.Compiler.ParseSrc(srcPath, cpus);

            foreach (var parseResult in parseResults)
            {
                parsedDocumentsManager.UpdateParseResult(parseResult.Key, parseResult.Value);
            }
            foreach (var kvp in parseResults)
            {
                if (kvp.Value.SyntaxErrors.Count > 0)
                {
                    router.Document.PublishDiagnostics(new OmniSharp.Extensions.LanguageServer.Protocol.Models.PublishDiagnosticsParams
                    {
                        Uri = kvp.Key,
                        Diagnostics = new OmniSharp.Extensions.LanguageServer.Protocol.Models.Container<OmniSharp.Extensions.LanguageServer.Protocol.Models.Diagnostic>(
                            kvp.Value.SyntaxErrors.Select(x => new OmniSharp.Extensions.LanguageServer.Protocol.Models.Diagnostic
                            {
                                Severity = OmniSharp.Extensions.LanguageServer.Protocol.Models.DiagnosticSeverity.Error,
                                Message = x.Message,
                                Range = new OmniSharp.Extensions.LanguageServer.Protocol.Models.Range(
                                            new OmniSharp.Extensions.LanguageServer.Protocol.Models.Position(x.Line - 1, x.Column),
                                            new OmniSharp.Extensions.LanguageServer.Protocol.Models.Position(x.Line - 1, x.Column)
                                        )
                            }))
                    });
                }
            }
            router.Window.LogInfo($"Parsed {parseResults.Count} scripts");
        }
    }
}


