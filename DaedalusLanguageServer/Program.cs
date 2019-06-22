using DemoLanguageServer.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OmniSharp.Extensions.LanguageServer.Server;
using System;
using System.Linq;
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
                 );

            await server.WaitForExit;
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<BufferManager>();
        }
    }
}


