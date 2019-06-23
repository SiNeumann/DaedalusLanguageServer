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
            var ca = Document;
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


