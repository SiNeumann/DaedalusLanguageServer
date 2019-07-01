using DaedalusCompiler.Compilation.Symbols;
using OmniSharp.Extensions.LanguageServer.Protocol.Client.Capabilities;
using OmniSharp.Extensions.LanguageServer.Protocol.Models;
using OmniSharp.Extensions.LanguageServer.Protocol.Server;
using System;
using System.Linq;
using System.Text.RegularExpressions;
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
        private Regex rxFuncDef = new Regex(@"^\s*func\s+", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Multiline);
        private Regex rxStringValues = new Regex(@"("".*? ""|'.*?')", RegexOptions.Compiled);
        private Regex rxFuncCall = new Regex(@"\([\w@^_,:""'=\s]*\)", RegexOptions.Compiled);

        public Task<SignatureHelp> Handle(SignatureHelpParams request, CancellationToken cancellationToken)
        {
            var doc = bufferManager.GetBuffer(request.TextDocument.Uri.AbsolutePath);
            if (doc == null) return null;

            var line = doc.GetMethodCall(request.Position).Trim();
            if (rxFuncDef.IsMatch(line)) return null;

            var contextLine = line;

            contextLine = rxStringValues.Replace(contextLine, "");

            var oldLen = -1;
            while (contextLine.Length != oldLen)
            {
                oldLen = contextLine.Length;
                contextLine = rxFuncCall.Replace(contextLine, "");
            }

            var idxOfParen = contextLine.LastIndexOf('(');
            if (idxOfParen < 0) return null;

            string word = null;
            for (var i = idxOfParen - 1; i > 0; i--)
            {
                var c = contextLine[i];
                if (!BufferedDocument.IsIdentifier(c))
                {
                    var start = i + 1;
                    word = contextLine.Substring(start, idxOfParen - start);
                    break;
                }
            }
            if (word == null)
            {
                word = contextLine.Substring(0, idxOfParen);
            }
            word = word.Trim();
            var sigCtx = contextLine.Substring(idxOfParen + 1);
            var func = (Function)documentsManager.GetGlobalSymbols()
                .FirstOrDefault(x => x is Function && x.Name.Equals(word, StringComparison.OrdinalIgnoreCase));
            if (func != null)
            {
                var signatureHelp = new SignatureHelp
                {
                    Signatures = new Container<SignatureInformation>(new SignatureInformation
                    {
                        Label = func.ToString(),
                        Parameters = new Container<ParameterInformation>(func.Parameters.Select(x => new ParameterInformation
                        {
                            Label = x.ToString(),
                        })),
                        Documentation = func.Documentation,
                    }),
                    ActiveParameter = sigCtx.Count(x => x == ','),
                    ActiveSignature = 0
                };
                return Task.FromResult(signatureHelp);

            }
            return null;
        }

        public void SetCapability(SignatureHelpCapability capability)
        {
        }
    }
}
