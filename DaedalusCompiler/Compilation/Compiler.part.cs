using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using DaedalusCompiler.Compilation.Symbols;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DaedalusCompiler.Compilation
{
    public partial class Compiler
    {
        public static ParseResult Load(string filename)
        {
            var compiler = new Compiler();
            return compiler.LoadFile(filename);
        }
        public static ParseResult Parse(string code, Uri src = null)
        {
            var compiler = new Compiler();
            var result = compiler.ParseText(code);
            if (src != null)
            {
                result.Source = src;
                compiler.UpdateParserSymbolSource(result);
            }
            return result;
        }
        public static Dictionary<Uri, ParseResult> ParseSrc(string srcPath)
        {
            var compiler = new Compiler();
            return compiler.ParseSrcInternal(srcPath, 1);
        }

        public static Dictionary<Uri, ParseResult> ParseSrc(string srcPath, int maxConcurrency)
        {
            var compiler = new Compiler();
            return compiler.ParseSrcInternal(srcPath, maxConcurrency);
        }

        private ParseResult LoadFile(string filename)
        {
            var fileContent = GetFileContent(filename);
            var result = ParseText(fileContent);
            result.Source = new Uri(filename);
            return result;
        }

        private ParseResult LoadFile(Uri filename)
        {
            var fileContent = GetFileContent(filename.LocalPath);
            var result = ParseText(fileContent);
            result.Source = filename;
            UpdateParserSymbolSource(result);
            return result;
        }

        private class SyntaxErrorListener : IAntlrErrorListener<IToken>
        {
            public List<SyntaxError> SyntaxErrors { get; } = new List<SyntaxError>();
            public void SyntaxError(TextWriter output, IRecognizer recognizer, IToken offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
                => SyntaxErrors.Add(new SyntaxError { Message = msg, Column = charPositionInLine, Line = line });
        }

        private ParseResult ParseText(string fileContent)
        {
            using (var sw = new StringWriter())
            {
                var errors = new List<SyntaxError>();
                var parser = GetParserForText(fileContent, TextWriter.Null, sw);
                var errListener = new SyntaxErrorListener();
                parser.AddErrorListener(errListener);
                var listener = new DaedalusStatefulParseTreeListener();
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

        private Dictionary<Uri, ParseResult> ParseSrcInternal(string srcPath, int maxConcurrency = 1)
        {
            var parseResults = new Dictionary<Uri, ParseResult>();
            var absoluteSrcFilePath = Path.GetFullPath(srcPath);
            if (maxConcurrency == 1)
            {
                foreach (var f in SrcFileHelper.LoadScriptsFilePaths(absoluteSrcFilePath))
                {
                    var result = LoadFile(new Uri(f));
                    parseResults.Add(new Uri(f), result);
                }
            }
            else
            {
                Parallel.ForEach(SrcFileHelper.LoadScriptsFilePaths(absoluteSrcFilePath),
                    new ParallelOptions { MaxDegreeOfParallelism = maxConcurrency },
                    (f, state, index) =>
                    {
                        var result = LoadFile(new Uri(f));
                        lock (parseResults)
                        {
                            parseResults.Add(new Uri(f), result);
                        }
                    });
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

        private void UpdateParserSymbolSource(ParseResult result)
        {
            IEnumerable<Symbol> symbols = result.EnumerateSymbols();
            var src = result.Source;
            foreach (var symbol in symbols)
            {
                symbol.Source = src;
            }
        }
    }
}
