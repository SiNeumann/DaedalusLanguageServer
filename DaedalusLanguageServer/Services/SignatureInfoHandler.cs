using OmniSharp.Extensions.LanguageServer.Protocol.Client.Capabilities;
using OmniSharp.Extensions.LanguageServer.Protocol.Models;
using OmniSharp.Extensions.LanguageServer.Protocol.Server;
using System.Threading;
using System.Threading.Tasks;

namespace DaedalusLanguageServer.Services
{
    public class SignatureInfoHandler : ISignatureHelpHandler
    {
        private readonly ParsedDocumentsManager documentsManager;
        private readonly BufferManager bufferManager;

        public SignatureInfoHandler(ParsedDocumentsManager documentsManager, BufferManager bufferManager)
        {
            this.documentsManager = documentsManager;
            this.bufferManager = bufferManager;
        }

        public SignatureHelpRegistrationOptions GetRegistrationOptions()
        {
            return new SignatureHelpRegistrationOptions
            {
                DocumentSelector = DaedalusDefaults.DocumentSelector,
                TriggerCharacters = new Container<string>("(", ",")
            };
        }

        public Task<SignatureHelp> Handle(SignatureHelpParams request, CancellationToken cancellationToken)
        {
            var doc = bufferManager.GetBuffer(request.TextDocument.Uri.AbsolutePath);
            if (doc == null) return null;
            string line = doc.GetLine(request.Position);

            var signatureHelp = new SignatureHelp();
            return Task.FromResult(signatureHelp);
        }

        public void SetCapability(SignatureHelpCapability capability)
        {
        }
    }
}
