using OmniSharp.Extensions.Embedded.MediatR;
using OmniSharp.Extensions.LanguageServer.Protocol;
using OmniSharp.Extensions.LanguageServer.Protocol.Client.Capabilities;
using OmniSharp.Extensions.LanguageServer.Protocol.Models;
using OmniSharp.Extensions.LanguageServer.Protocol.Server;
using OmniSharp.Extensions.LanguageServer.Protocol.Server.Capabilities;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DaedalusLanguageServer.Services
{
    public class TextDocumentSyncHandler : ITextDocumentSyncHandler
    {
        private readonly ILanguageServer _router;
        private readonly BufferManager _bufferManager;
        private readonly ParsedDocumentsManager documentsManager;

        private SynchronizationCapability _capability;

        public TextDocumentSyncKind Change => TextDocumentSyncKind.Full;

        public Task<Unit> Handle(DidChangeTextDocumentParams request, CancellationToken cancellationToken)
        {
            var documentPath = request.TextDocument.Uri.ToString();
            var text = request.ContentChanges.FirstOrDefault()?.Text;
            _bufferManager.UpdateBuffer(documentPath, text.ToCharArray());

            _router.Window.LogInfo($"Updated buffer for document: {documentPath} ({text.Length} chars)");

            return Unit.Task;
        }
        public TextDocumentSyncHandler(ILanguageServer router, BufferManager bufferManager, ParsedDocumentsManager documentsManager)
        {
            this._router = router;
            this._bufferManager = bufferManager;
            this.documentsManager = documentsManager;
        }
        public TextDocumentChangeRegistrationOptions GetRegistrationOptions()
        {
            return new TextDocumentChangeRegistrationOptions()
            {
                DocumentSelector = DaedalusDefaults.DocumentSelector,
                SyncKind = Change
            };
        }

        public Task<Unit> Handle(DidOpenTextDocumentParams request, CancellationToken cancellationToken)
        {
            var documentPath = request.TextDocument.Uri.ToString();
            var text = request.TextDocument.Text;
            _bufferManager.UpdateBuffer(documentPath, text.ToCharArray());

            Parse(request.TextDocument.Uri, request.TextDocument.Text, cancellationToken);
            return Unit.Task;
        }

        TextDocumentRegistrationOptions IRegistration<TextDocumentRegistrationOptions>.GetRegistrationOptions() => DaedalusDefaults.RegistrationOptions;

        public Task<Unit> Handle(DidCloseTextDocumentParams request, CancellationToken cancellationToken) => Unit.Task;

        public void SetCapability(SynchronizationCapability capability)
        {
            _capability = capability;
        }
        public void Parse(Uri uri, string text, CancellationToken cancellation)
        {
            _bufferManager.UpdateBuffer(uri.AbsolutePath, text.ToCharArray());
            var diags = documentsManager.Parse(uri, text, cancellation);
            if (diags != null)
            {
                _router.Document.PublishDiagnostics(diags);
            }
            else
            {
                _router.Document.PublishDiagnostics(new PublishDiagnosticsParams { Uri = uri, Diagnostics = new Container<Diagnostic>() });
            }
        }

        public Task<Unit> Handle(DidSaveTextDocumentParams request, CancellationToken cancellationToken)
        {
            Parse(request.TextDocument.Uri, request.Text, cancellationToken);
            return Unit.Task;
        }

        TextDocumentSaveRegistrationOptions IRegistration<TextDocumentSaveRegistrationOptions>.GetRegistrationOptions()
        {
            return new TextDocumentSaveRegistrationOptions()
            {
                DocumentSelector = DaedalusDefaults.DocumentSelector,
                IncludeText = true,
            };
        }

        public TextDocumentAttributes GetTextDocumentAttributes(Uri uri)
        {
            return new TextDocumentAttributes(uri, "daedalus");
        }
    }
}
