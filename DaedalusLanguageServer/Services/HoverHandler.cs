using DaedalusCompiler.Compilation;
using OmniSharp.Extensions.LanguageServer.Protocol.Client.Capabilities;
using OmniSharp.Extensions.LanguageServer.Protocol.Models;
using OmniSharp.Extensions.LanguageServer.Protocol.Server;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DaedalusLanguageServer.Services
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
            var symbol = doc.GetWordRangeAtPosition(request.Position).ToUpper();

            var v = documentsManager.LookupSymbol(symbol);

            if (v == null)
            {
                return Task.FromResult<Hover>(null);
            }

            string marked;
            if (v is Function fv)
            {
                marked = $"func {fv.ReturnType} {fv.Name}({string.Join(", ", fv.Parameters.Select(x => $"var {x.Type} {x.Name}"))})";
            }
            else if (v is Class clv)
            {
                if (clv.Fields.Count > 0)
                {
                    marked = $"class {clv.Name} {{\n\t{string.Join("\t", clv.Fields.Select(x => $"var {x.Type} {x.Name};\n"))}}};";
                }
                else
                {
                    marked = $"class {clv.Name}";
                }
            }
            else if (v is Prototype pv)
            {
                marked = $"prototype {pv.Name} ({pv.Parent})";
            }
            else if (v is Constant cv)
            {
                marked = $"const {cv.Type} {cv.Name} = {cv.Value}";
            }
            else if (v is Variable sv)
            {
                marked = $"var {sv.Type} {sv.Name}";
            }
            else
            {
                marked = v.Name;
            }
            return Task.FromResult(new Hover
            {
                Contents = new MarkedStringsOrMarkupContent(new MarkedString(DaedalusDefaults.Language, marked))
            });

        }

        public void SetCapability(HoverCapability capability)
        {
            this.capability = capability;
        }
    }

    public static class CharArrayExtensions
    {
        public static string GetWordRangeAtPosition(this char[] ca, Position position)
        {
            Span<char> c = ca;
            var currentLine = 0;
            var offset = 0;
            var wordLine = position.Line;

            while (currentLine < wordLine)
            {
                currentLine++;
                var lineEnd = c.IndexOf('\n');
                if (lineEnd != -1)
                {
                    offset += lineEnd;
                    if (c.Length < lineEnd + 1) break;
                    offset++;
                    c = c.Slice(lineEnd + 1);
                }
            }

            var center = offset + (int)position.Character - 1;
            var start = center;
            var end = center;

            while (IsIdentifier(ca[start])) start--;
            while (IsIdentifier(ca[end])) end++;

            if (start < center)
            {
                start++; // Skip the first bad char
            }

            return string.Create(end - start, (ca, start, end), (c, state) =>
              {
                  var len = state.end - state.start;
                  for (var i = 0; i < len; i++)
                  {
                      c[i] = state.ca[state.start + i];
                  }
              });
        }
        private static bool IsIdentifier(char c)
        {
            return char.IsLetterOrDigit(c) || c == '_' || c == '@' || c == '^';
        }
    }
}
