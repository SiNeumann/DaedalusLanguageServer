using DaedalusCompiler.Compilation;
using DaedalusLanguageServerLib;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OmniSharp.Extensions.LanguageServer.Protocol.Models;
using OmniSharp.Extensions.LanguageServer.Protocol.Server;
using OmniSharp.Extensions.LanguageServer.Server;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
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
                    .WithHandler<DaedalusLanguageServerLib.Services.TextDocumentSyncHandler>()
                    .WithHandler<DaedalusLanguageServerLib.Services.DidChangeWatchedFilesHandler>()
                    .WithHandler<DaedalusLanguageServerLib.Services.DidChangeWorkspaceFoldersHandler>()
                    .WithHandler<DaedalusLanguageServerLib.Services.DocumentSymbolHandler>()
                    .WithHandler<DaedalusLanguageServerLib.Services.CompletionHandler>()
                    .WithHandler<DaedalusLanguageServerLib.Services.HoverHandler>()
                    .WithHandler<DaedalusLanguageServerLib.Services.GoToDefinitionHandler>()
                    .WithHandler<DaedalusLanguageServerLib.Services.SignatureInfoHandler>()
                    .WithHandler<DaedalusLanguageServerLib.Services.InitializeHandler>()
                 );
            var router = server.Services.GetRequiredService<OmniSharp.Extensions.LanguageServer.Protocol.Server.ILanguageServer>();

            Action<string> logError = text =>
            {
                if (text.Contains("WARNING", StringComparison.OrdinalIgnoreCase))
                {
                    router.Window.LogWarning(text);
                }
                else if (text.Contains("ERROR", StringComparison.OrdinalIgnoreCase))
                {
                    router.Window.LogError(text);
                }
            };
            System.Diagnostics.Trace.Listeners.Add(new DelegateTraceListener(logError, logError));

            var docManager = server.Services.GetRequiredService<ParsedDocumentsManager>();
            var externals = ParseBuiltIns(docManager);
            if (externals?.Count == 0)
            {
                router.Window.LogError("ERROR Could not find Gothic externals");
                router.Window.ShowError("Could not find Gothic externals");
            } else
            {
                router.Window.LogInfo($"INFO Loaded {externals.Count} externals");
            }
            if (File.Exists("Gothic.src"))
            {
                var srcPath = Path.GetFullPath("Gothic.src");
                try
                {
                    await Task.Run(() => ParseSrc(srcPath, externals, router, docManager));
                }
                catch (Exception ex)
                {
                    router.Window.LogError("ERROR while parsing Gothic.src: " + ex.ToString());
                    router.Window.ShowError("Gothic.src: " + ex.Message);
                }
            }
            await server.WaitForExit;
        }

        private class DelegateTraceListener : System.Diagnostics.TraceListener
        {
            private readonly Action<string> write;
            private readonly Action<string> writeLine;

            public DelegateTraceListener(Action<string> write, Action<string> writeLine)
            {
                this.write = write;
                this.writeLine = writeLine;
            }
            public override void Write(string message) => write(message);

            public override void WriteLine(string message) => writeLine(message);
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<BufferManager>();
            services.AddSingleton<ParsedDocumentsManager>();
        }

        private static Dictionary<Uri, ParseResult> ParseBuiltIns(ParsedDocumentsManager parsedDocumentsManager)
        {
            var symbolInfoPath = Path.Combine(AppDir, "DaedalusBuiltins", "symbols.json");
            Dictionary<string, string> documentations = null;
            if (File.Exists(symbolInfoPath))
            {
                var symbolInfos = JsonSerializer.Deserialize<List<SymbolDocumentation>>(File.ReadAllText(symbolInfoPath, Encoding.UTF8));
                documentations = symbolInfos.ToDictionary(x => x.Name, x => x.Documentation, StringComparer.OrdinalIgnoreCase);
            }
            var externals = new Dictionary<Uri, ParseResult>();
            var buildInsPath = Path.Combine(AppDir, "DaedalusBuiltins");

            foreach (var builtIn in Directory.EnumerateFiles(buildInsPath, "*.d"))
            {
                var builtInUri = new Uri(builtIn);
                parsedDocumentsManager.Parse(builtInUri, File.ReadAllText(builtIn, Encoding.GetEncoding(1250)), default);
                var parsedResult = parsedDocumentsManager.GetParseResult(builtInUri);
                externals[builtInUri] = parsedResult;
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

            return externals;
        }

        private static void ParseSrc(
            string srcPath, 
            Dictionary<Uri, ParseResult> externals, 
            OmniSharp.Extensions.LanguageServer.Protocol.Server.ILanguageServer router, 
            ParsedDocumentsManager parsedDocumentsManager)
        {
            router.Window.LogInfo($"Parsing Gothic.Src. This might take a while.");
            var parseResults = Compiler.ParseSrc(srcPath, externals);

            foreach (var parseResult in parseResults)
            {
                parsedDocumentsManager.UpdateParseResult(parseResult.Key, parseResult.Value);
            }
            router.Window.LogInfo($"Parsed {parseResults.Count} scripts");

            var diagnostics = new List<PublishDiagnosticsParams>();
            foreach (var kvp in parseResults)
            {
                if (kvp.Value.SyntaxErrors.Count > 0)
                {
                    diagnostics.Add(new PublishDiagnosticsParams
                    {
                        Uri = kvp.Key,
                        Diagnostics = new Container<Diagnostic>(
                            kvp.Value.SyntaxErrors.Select(x => new Diagnostic
                            {
                                Severity = ParsedDocumentsManager.DiagnosticSeverityFromSyntaxError(x),
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
            foreach (var dp in diagnostics)
            {
                router.Document.PublishDiagnostics(dp);
            }
        }
    }
}


