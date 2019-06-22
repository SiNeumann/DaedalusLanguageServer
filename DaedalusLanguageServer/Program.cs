using DaedalusCompiler.Compilation;
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
using System.Text;
using System.Threading;
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
            ParseBuiltIns(server.Services.GetRequiredService<ParsedDocumentsManager>());

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
    }
}


