using OmniSharp.Extensions.LanguageServer.Protocol.Client.Capabilities;
using OmniSharp.Extensions.LanguageServer.Protocol.Models;
using OmniSharp.Extensions.LanguageServer.Protocol.Server;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DaedalusLanguageServerLib.Services
{
    public class GoToDefinitionHandler : IDefinitionHandler
    {
        private readonly ParsedDocumentsManager documentsManager;
        private readonly BufferManager bufferManager;

        public GoToDefinitionHandler(ParsedDocumentsManager documentsManager, BufferManager bufferManager)
        {
            this.documentsManager = documentsManager;
            this.bufferManager = bufferManager;
        }

        public TextDocumentRegistrationOptions GetRegistrationOptions() => DaedalusDefaults.RegistrationOptions;

        public Task<LocationOrLocationLinks> Handle(DefinitionParams request, CancellationToken cancellationToken)
        {
            var doc = bufferManager.GetBuffer(request.TextDocument.Uri.AbsolutePath);
            if (doc == null)
            {
                return Task.FromResult<LocationOrLocationLinks>(null);
            }
            var identifier = doc.GetWordRangeAtPosition(request.Position);
            var symbol = documentsManager.LookupSymbol(identifier);

            if (symbol == null || symbol.Source == null || symbol.Source.LocalPath.Contains("DaedalusBuiltins"))
                return Task.FromResult<LocationOrLocationLinks>(null);

            var symbolStart = new Position(symbol.Line - 1, symbol.Column);
            var symbolEnd = new Position(symbol.Line - 1, symbol.Column + symbol.Name.Length);

            var result = new LocationOrLocationLinks(new LocationOrLocationLink(new LocationLink
            {
                TargetRange = new OmniSharp.Extensions.LanguageServer.Protocol.Models.Range(symbolStart, symbolEnd),
                TargetSelectionRange = new OmniSharp.Extensions.LanguageServer.Protocol.Models.Range(symbolStart, symbolEnd),
                TargetUri = new Uri(symbol.Source.AbsoluteUri),
            }));
            return Task.FromResult(result);
        }

        public void SetCapability(DefinitionCapability capability) { }
    }
}
