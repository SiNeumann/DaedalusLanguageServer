using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using DaedalusCompiler.Dat;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DaedalusCompiler.Compilation
{
    public partial class Compiler
    {

        private readonly AssemblyBuilder _assemblyBuilder;
        private readonly OutputUnitsBuilder _ouBuilder;
        private readonly string _outputDirPath;


        public Compiler(string outputDirPath = "output", bool verbose = true)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            _assemblyBuilder = new AssemblyBuilder(verbose);
            _ouBuilder = new OutputUnitsBuilder(verbose);
            _outputDirPath = outputDirPath;
        }

        public string GetBuiltinsPath()
        {
            var programStartPath = System.Reflection.Assembly.GetExecutingAssembly().Location;

            return Path.Combine(Path.GetDirectoryName(programStartPath), "DaedalusBuiltins");
        }

        public bool CompileFromSrc(
            string srcFilePath,
            bool compileToAssembly,
            bool verbose = true,
            bool generateOutputUnits = true
        )
        {
            var absoluteSrcFilePath = Path.GetFullPath(srcFilePath);

            try
            {
                var paths = SrcFileHelper.LoadScriptsFilePaths(absoluteSrcFilePath).ToArray();
                var srcFileName = Path.GetFileNameWithoutExtension(absoluteSrcFilePath).ToLower();

                var runtimePath = Path.Combine(GetBuiltinsPath(), srcFileName + ".d");
                if (File.Exists(runtimePath))
                {
                    _assemblyBuilder.IsCurrentlyParsingExternals = true;
                    if (verbose) Console.WriteLine($"[0/{paths.Length}]Compiling runtime: {runtimePath}");
                    var parser = GetParserForScriptsFile(runtimePath);
                    ParseTreeWalker.Default.Walk(new DaedalusListener(_assemblyBuilder, 0), parser.daedalusFile());
                    _assemblyBuilder.IsCurrentlyParsingExternals = false;
                }

                for (var i = 0; i < paths.Length; i++)
                {
                    if (verbose) Console.WriteLine($"[{i + 1}/{paths.Length}]Compiling: {paths[i]}");

                    var fileContent = GetFileContent(paths[i]);
                    var parser = GetParserForText(fileContent);

                    _assemblyBuilder.ErrorContext.FileContentLines = fileContent.Split(Environment.NewLine);
                    _assemblyBuilder.ErrorContext.FilePath = paths[i];
                    _assemblyBuilder.ErrorContext.FileIndex = i;
                    ParseTreeWalker.Default.Walk(new DaedalusListener(_assemblyBuilder, i), parser.daedalusFile());
                    if (generateOutputUnits)
                    {
                        _ouBuilder.ParseText(fileContent);
                    }
                }

                if (!compileToAssembly)
                {
                    Directory.CreateDirectory(_outputDirPath);
                }

                if (generateOutputUnits)
                {
                    _ouBuilder.SaveOutputUnits(_outputDirPath);
                }

                _assemblyBuilder.Finish();
                if (_assemblyBuilder.Errors.Any())
                {
                    _assemblyBuilder.Errors.Sort((x, y) => x.CompareTo(y));

                    var lastErrorFilePath = "";
                    string lastErrorBlockName = null;
                    var logger = new StdErrorLogger();
                    foreach (var error in _assemblyBuilder.Errors)
                    {
                        if (lastErrorFilePath != error.FilePath)
                        {
                            lastErrorFilePath = error.FilePath;
                            Console.WriteLine(error.FilePath);
                        }

                        if (lastErrorBlockName != error.ExecBlockName)
                        {
                            lastErrorBlockName = error.ExecBlockName;
                            if (error.ExecBlockName == null)
                            {
                                Console.WriteLine($"{error.FileName}: In global scope:");
                            }
                            else
                            {
                                Console.WriteLine($"{error.FileName}: In {error.ExecBlockType} ‘{error.ExecBlockName}’:");
                            }

                        }

                        error.Print(logger);
                    }
                    return false;
                }

                if (compileToAssembly)
                {
                    Console.WriteLine(_assemblyBuilder.GetAssembler());
                }
                else
                {
                    Directory.CreateDirectory(_outputDirPath);
                    var datPath = Path.Combine(_outputDirPath, srcFileName + ".dat");
                    _assemblyBuilder.SaveToDat(datPath);
                }

                return true;
            }
            catch (Exception exc)
            {
                Console.WriteLine("SRC compilation failed");
                Console.WriteLine($"{exc}");
                return false;
            }
        }

        public List<DatSymbol> GetSymbols()
        {
            return _assemblyBuilder.GetSymbols();
        }

        public List<BaseExecBlockContext> GetExecBlocks()
        {
            return _assemblyBuilder.GetExecBlocks();
        }

        public void SetCompilationDateTimeText(string compilationDateTimeText)
        {
            _ouBuilder.SetGenerationDateTimeText(compilationDateTimeText);
        }

        public void SetCompilationUserName(string userName)
        {
            _ouBuilder.SetGenerationUserName(userName);
        }

        private string GetFileContent(string filePath)
        {
            return File.ReadAllText(filePath, Encoding.GetEncoding(1250));
        }

        private DaedalusParser GetParserForText(string input)
        {
            var inputStream = new AntlrInputStream(input);
            var lexer = new DaedalusLexer(inputStream);
            var commonTokenStream = new CommonTokenStream(lexer);
            return new DaedalusParser(commonTokenStream);
        }

        private DaedalusParser GetParserForScriptsFile(string scriptFilePath)
        {
            var fileContent = GetFileContent(scriptFilePath);
            return GetParserForText(fileContent);
        }
    }
}