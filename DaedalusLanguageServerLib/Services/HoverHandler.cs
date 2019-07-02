using OmniSharp.Extensions.LanguageServer.Protocol.Client.Capabilities;
using OmniSharp.Extensions.LanguageServer.Protocol.Models;
using OmniSharp.Extensions.LanguageServer.Protocol.Server;
using System.Threading;
using System.Threading.Tasks;

namespace DaedalusLanguageServerLib.Services
{
    public class HoverHandler : IHoverHandler
    {
        public TextDocumentRegistrationOptions GetRegistrationOptions() => DaedalusDefaults.RegistrationOptions;

        private HoverCapability capability;
        private readonly ParsedDocumentsManager documentsManager;
        private readonly BufferManager bufferManager;

        public HoverHandler(ParsedDocumentsManager documentsManager, BufferManager bufferManager)
        {
            this.documentsManager = documentsManager;
            this.bufferManager = bufferManager;
        }
        public Task<Hover> Handle(HoverParams request, CancellationToken cancellationToken)
        {
            var doc = bufferManager.GetBuffer(request.TextDocument.Uri.AbsolutePath);
            if (doc == null)
            {
                return Task.FromResult<Hover>(null);
            }
            var symbol = doc.GetWordRangeAtPosition(request.Position);
            var v = documentsManager.LookupSymbol(symbol);

            if (v == null)
            {
                return Task.FromResult<Hover>(null);
            }

            return Task.FromResult(new Hover
            {
                Contents = new MarkedStringsOrMarkupContent(new MarkedString(DaedalusDefaults.Language, v.ToString()))
            });

        }

        public void SetCapability(HoverCapability capability)
        {
            this.capability = capability;
        }
    }
}
