using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DaedalusCompiler.Compilation;
namespace DaedalusCompiler.Compilation{
internal class ExtendedSyntaxErrorListener : IAntlrErrorListener<IToken>
        {

            public List<SyntaxError> SyntaxErrors { get; } = new List<SyntaxError>();
            public void SyntaxError(TextWriter output, IRecognizer recognizer, IToken offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
            {
                SyntaxErrorCode errorCode;
                if (e != null)
                {
                    if (e.Data.Contains(Compilation.SyntaxError.DataKey_ErrorCode))
                    {
                        errorCode = (SyntaxErrorCode)e.Data[Compilation.SyntaxError.DataKey_ErrorCode];
                    }
                    else
                    {
                        errorCode = new SyntaxErrorCode("D0000", e.Message);
                    }
                }
                else
                {
                    errorCode = new SyntaxErrorCode("D0000", msg);
                }
                SyntaxErrors.Add(new SyntaxError { ErrorCode = errorCode, Column = charPositionInLine, Line = line });
            }
        }
    }
