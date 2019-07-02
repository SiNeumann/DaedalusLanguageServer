using DaedalusLanguageServerLib;
using OmniSharp.Extensions.LanguageServer.Protocol.Client.Capabilities;
using OmniSharp.Extensions.LanguageServer.Protocol.Models;
using OmniSharp.Extensions.LanguageServer.Protocol.Server;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DaedalusLanguageServerLib.Services
{
    public class DocumentSymbolHandler : IDocumentSymbolHandler
    {
        private readonly ParsedDocumentsManager documentsManager;

        public DocumentSymbolHandler(ParsedDocumentsManager documentsManager)
        {
            this.documentsManager = documentsManager;
        }
        public TextDocumentRegistrationOptions GetRegistrationOptions() => DaedalusDefaults.RegistrationOptions;

        public Task<SymbolInformationOrDocumentSymbolContainer> Handle(DocumentSymbolParams request, CancellationToken cancellationToken)
        {
            var symbols = new List<SymbolInformationOrDocumentSymbol>();
            foreach (var doc in documentsManager.GetDocuments())
            {
                foreach (var c in doc.Value.GlobalConstants)
                {
                    var pos = new Position(c.Line-1, c.Column);
                    var symbol = new SymbolInformationOrDocumentSymbol(new DocumentSymbol
                    {
                        Kind = SymbolKind.Constant,
                        Name = c.Name,
                        Range = new Range(pos, pos),
                    });
                    symbols.Add(symbol);
                }
                foreach (var v in doc.Value.GlobalVariables)
                {
                    var pos = new Position(v.Line - 1, v.Column);
                    var symbol = new SymbolInformationOrDocumentSymbol(new DocumentSymbol
                    {
                        Kind = SymbolKind.Variable,
                        Name = v.Name,
                        Range = new Range(pos, pos),
                    });
                    symbols.Add(symbol);
                }
            }
            return Task.FromResult(new SymbolInformationOrDocumentSymbolContainer(symbols));
        }
        private DocumentSymbolCapability capability;
        public void SetCapability(DocumentSymbolCapability capability) => this.capability = capability;
    }
}
