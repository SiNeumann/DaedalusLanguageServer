using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaedalusCompiler.Compilation
{
    public partial class Compiler
    {
        private Dictionary<Uri, ParseResult> externals = new Dictionary<Uri, ParseResult>();

        public static ParseResult Load(string filename, bool detailed = false, IEnumerable<ParseResult> previouslyParsedFiles = null)
        {
            var compiler = new Compiler();
            return compiler.LoadFile(filename, detailed, previouslyParsedFiles);
        }
        public static ParseResult Parse(string code, Uri src = null, bool detailed = false, IEnumerable<ParseResult> previouslyParsedFiles = null)
        {
            var compiler = new Compiler();
            var result = compiler.ParseText(code, detailed, previouslyParsedFiles);
            if (src != null)
            {
                result.Source = src;
                compiler.UpdateParserSymbolSource(result);
            }
            return result;
        }
        public static Dictionary<Uri, ParseResult> ParseSrc(string srcPath, Dictionary<Uri, ParseResult> externals = null)
        {
            var compiler = new Compiler();
            compiler.externals = externals;
            return compiler.ParseSrcInternal(srcPath, 1);
        }

        public static Dictionary<Uri, ParseResult> ParseSrc(string srcPath, int maxConcurrency)
        {
            var compiler = new Compiler();
            return compiler.ParseSrcInternal(srcPath, maxConcurrency);
        }

        private ParseResult LoadFile(string filename, bool detailed = false, IEnumerable<ParseResult> currentParseResults = null)
        {
            return LoadFile(new Uri(filename), detailed, currentParseResults);
        }

        private ParseResult LoadFile(Uri filename, bool detailed = false, IEnumerable<ParseResult> currentParseResults = null)
        {
            var result = ParseFile(filename.LocalPath, Encoding.GetEncoding(1250), detailed, currentParseResults);
            result.Source = filename;
            UpdateParserSymbolSource(result);
            return result;
        }

        private class SyntaxErrorListener : IAntlrErrorListener<IToken>
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

        private ParseResult ParseText(string fileContent, bool detailed = false, IEnumerable<ParseResult> currentParseResults = null)
        {
            using (var sw = new StringWriter())
            {
                var errors = new List<SyntaxError>();
                var parser = GetParserForText(fileContent, TextWriter.Null, sw);
                var errListener = new SyntaxErrorListener();
                parser.AddErrorListener(errListener);
                var listener = detailed ? new DaedalusStatefulDetailedParseTreeListener(parser, currentParseResults) : new DaedalusStatefulParseTreeListener(parser, currentParseResults);
                ParseTreeWalker.Default.Walk(listener, parser.daedalusFile());

                return new ParseResult
                {
                    SyntaxErrors = errListener.SyntaxErrors,
                    GlobalConstants = listener.GlobalConsts,
                    GlobalVariables = listener.GlobalVars,
                    GlobalFunctions = listener.GlobalFunctions,
                    GlobalClasses = listener.GlobalClasses,
                    GlobalPrototypes = listener.GlobalPrototypes,
                    GlobalInstances = listener.GlobalInstances,
                };
            }
        }

        private ParseResult ParseFile(string filename, Encoding encoding, bool detailed = false, IEnumerable<ParseResult> currentParseResults = null)
        {
            using (var sw = new StringWriter())
            using (var sr = new StreamReader(filename, encoding))
            {
                var errors = new List<SyntaxError>();
                var parser = GetParserForStream(sr, TextWriter.Null, sw);
                parser.Interpreter.PredictionMode = Antlr4.Runtime.Atn.PredictionMode.SLL;
                var errListener = new SyntaxErrorListener();
                parser.AddErrorListener(errListener);
                var listener = detailed ?
                                new DaedalusStatefulDetailedParseTreeListener(parser, currentParseResults) 
                                : new DaedalusStatefulParseTreeListener(parser, currentParseResults);
                DaedalusParser.DaedalusFileContext fileCtx;
                try
                {
                    fileCtx = parser.daedalusFile();
                }
                catch (Exception)
                {
                    parser.Interpreter.PredictionMode = Antlr4.Runtime.Atn.PredictionMode.LL;
                    fileCtx = parser.daedalusFile();
                }

                ParseTreeWalker.Default.Walk(listener, fileCtx);

                return new ParseResult
                {
                    SyntaxErrors = errListener.SyntaxErrors,
                    GlobalConstants = listener.GlobalConsts,
                    GlobalVariables = listener.GlobalVars,
                    GlobalFunctions = listener.GlobalFunctions,
                    GlobalClasses = listener.GlobalClasses,
                    GlobalPrototypes = listener.GlobalPrototypes,
                    GlobalInstances = listener.GlobalInstances,
                };
            }
        }

        private Dictionary<Uri, ParseResult> ParseSrcInternal(string srcPath, int maxConcurrency = 1)
        {
            var parseResults = new Dictionary<Uri, ParseResult>();
            foreach (var kvp in externals)
            {
                parseResults[kvp.Key] = kvp.Value;
            }
            var absoluteSrcFilePath = Path.GetFullPath(srcPath);

            if (maxConcurrency <= 1)
            {
                foreach (var f in SrcFileHelper.LoadScriptsFilePaths(absoluteSrcFilePath))
                {
                    if (!File.Exists(f)) continue;
                    //var fileUri = new Uri(Uri.EscapeDataString(f));
                    var result = LoadFile(f, false, parseResults.Values);
                    var fileUriWithColonEncoded = new Uri(f).AbsoluteUri.Replace(":", "%3A").Replace("file%3A///", "file:///");
                    parseResults.Add(new Uri(fileUriWithColonEncoded), result);
                }
            }
            return parseResults;
        }

        private DaedalusParser GetParserForText(string input, TextWriter output, TextWriter errorOutput)
        {
            var inputStream = new AntlrInputStream(input);
            var lexer = new DaedalusLexer(inputStream, output, errorOutput);
            var commonTokenStream = new CommonTokenStream(lexer);
            return new DaedalusParser(commonTokenStream, output, errorOutput);
        }

        private DaedalusParser GetParserForStream(TextReader input, TextWriter output, TextWriter errorOutput)
        {
            var inputStream = new AntlrInputStream(input);
            var lexer = new DaedalusLexer(inputStream, output, errorOutput);
            var commonTokenStream = new CommonTokenStream(lexer);
            return new DaedalusParser(commonTokenStream, output, errorOutput);
        }

        private void UpdateParserSymbolSource(ParseResult result)
        {
            var src = result.Source;
            if (src is null) return;

            var symbols = result.EnumerateSymbols();
            foreach (var symbol in symbols)
            {
                symbol.Source = src;
            }
            foreach (var symbol in result.GlobalFunctions)
            {
                foreach (var varDef in symbol.LocalVariables)
                {
                    varDef.Source = src;
                }
            }
        }
    }
}
