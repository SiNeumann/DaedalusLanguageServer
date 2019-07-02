using OmniSharp.Extensions.LanguageServer.Protocol.Models;
using System;
using System.Text;

namespace DaedalusLanguageServerLib
{
    public class BufferedDocument
    {
        public char[] Document { get; private set; }

        public BufferedDocument(char[] document)
        {
            this.Document = document;
        }

        public void UpdateDocument(char[] document) => Document = document;

        public string GetWordRangeAtPosition(Position position)
        {
            var doc = Document;
            Span<char> docSlice = doc;
            var currentLine = 0;
            var offset = 0;
            var wordLine = position.Line;

            while (currentLine < wordLine)
            {
                currentLine++;
                var lineEnd = docSlice.IndexOf('\n');
                if (lineEnd != -1)
                {
                    offset += lineEnd;
                    if (docSlice.Length < lineEnd + 1) break;
                    offset++;
                    docSlice = docSlice.Slice(lineEnd + 1);
                }
            }

            var center = offset + (int)position.Character - 1;
            var start = center;
            var end = center;

            while (IsIdentifier(doc[start])) start--;
            while (IsIdentifier(doc[end])) end++;

            if (start < center)
            {
                start++; // Skip the first bad char
            }

            return string.Create(end - start, (doc, offset: start), (c, state) =>
            {
                for (var i = 0; i < c.Length; i++)
                {
                    c[i] = state.doc[state.offset + i];
                }
            });
        }
        public string GetMethodCall(Position center)
        {
            Span<char> doc = Document;
            var c = doc;
            var currentLine = 0;
            var offset = 0;
            var line = center.Line;
            while (currentLine < line && offset < doc.Length)
            {
                currentLine++;
                var lineEnd = c.IndexOf('\n');
                if (lineEnd == -1) break;

                offset += lineEnd;
                if (c.Length < lineEnd + 1) break;
                offset++;
                c = c.Slice(lineEnd + 1);
            }
            offset += center.Character > 0 ? (int)center.Character : 0;
            // combine all text that precede the center position
            int o = offset;
            int skipOpen = 1;
            while (o >= 0)
            {
                var token = doc[o--];
                if (token == ';' || token == '}') break;
                if (token == '(' && skipOpen <= 0) break;
                if (token == ')')
                {
                    skipOpen++; continue;
                }
                if (token == '(')
                {
                    skipOpen--; continue;
                }
                if (token == '\n' || token == '\r') continue;
            }
            if (o + 1 > doc.Length)
            {
                return new string(doc.Slice(o));
            }
            var result = doc.Slice(o + 2, offset - o - 2);
            for (int i = 0; i < result.Length; i++)
            {
                if (!char.IsWhiteSpace(result[i]))
                {
                    result = result.Slice(i);
                    break;
                }
            }
            return new string(result);
        }
        public string GetLine(Position center)
        {
            var doc = Document;
            Span<char> c = doc;
            var currentLine = 0;
            var offset = 0;
            var line = center.Line;
            while (currentLine < line)
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
            var end = c.IndexOf('\n');
            if (end == -1)
            {
                return new string(c);
            }
            var rIdx = c.IndexOf('\r');
            if (rIdx != -1 && rIdx < end)
            {
                end = rIdx;
            }
            return new string(c.Slice(0, end));
        }

        public static bool IsIdentifier(char c)
        {
            return char.IsLetterOrDigit(c) || c == '_' || c == '@' || c == '^';
        }
    }
}


