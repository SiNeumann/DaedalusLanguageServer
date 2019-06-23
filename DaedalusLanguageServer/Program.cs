using DaedalusLanguageServer.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OmniSharp.Extensions.LanguageServer.Protocol.Server;
using OmniSharp.Extensions.LanguageServer.Server;
using System;
using System.IO;
using System.Linq;
using System.Text;
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
            var buildInsPath = Path.Combine(AppDir, "DaedalusBuiltins");
            foreach (var builtIn in Directory.EnumerateFiles(buildInsPath, "*.d"))
            {
                parsedDocumentsManager.Parse(new Uri(builtIn), File.ReadAllText(builtIn, Encoding.GetEncoding(1250)), default);
            }
        }
        private static void ParseSrc(string srcPath, OmniSharp.Extensions.LanguageServer.Protocol.Server.ILanguageServer router, ParsedDocumentsManager parsedDocumentsManager)
        {
            router.Window.LogInfo("Parsing Gothic.Src (this might take a while)");
            var parseResults = DaedalusCompiler.Compilation.Compiler.ParseSrc(srcPath);
            foreach (var parseResult in parseResults)
            {
                parsedDocumentsManager.UpdateParseResult(parseResult.Key, parseResult.Value);
            }
            router.Window.LogInfo($"Parsed {parseResults.Count} scripts");
        }
    }
}


