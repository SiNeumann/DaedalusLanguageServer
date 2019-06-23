using OmniSharp.Extensions.LanguageServer.Protocol.Models;
using System;

namespace DaedalusLanguageServer
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
            Span<char> c = doc;
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

        private static bool IsIdentifier(char c)
        {
            return char.IsLetterOrDigit(c) || c == '_' || c == '@' || c == '^';
        }
    }
}


