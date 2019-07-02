using OmniSharp.Extensions.LanguageServer.Protocol.Models;
using OmniSharp.Extensions.LanguageServer.Protocol.Server;
using OmniSharp.Extensions.LanguageServer.Protocol.Server.Capabilities;
using System.Threading;
using System.Threading.Tasks;

namespace DaedalusLanguageServerLib.Services
{
    public class InitializeHandler : IInitializeHandler
    {
        public Task<InitializeResult> Handle(InitializeParams request, CancellationToken cancellationToken)
        {
            var result = new InitializeResult
            {
                Capabilities = new ServerCapabilities
                {
                    TextDocumentSync = new TextDocumentSync(new TextDocumentSyncOptions
                    {
                        Change = TextDocumentSyncKind.Full,
                        Save = new SaveOptions
                        {
                            IncludeText = true,
                        },
                    }),
                    DocumentSymbolProvider = true,
                    HoverProvider = true,
                }
            };
            return Task.FromResult(result);
        }
    }
}
