using Antlr4.Runtime;
using System.IO;
using System.Text;

namespace DaedalusCompiler.Compilation
{
    public partial class Compiler
    {

        public Compiler()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        public string GetBuiltinsPath()
        {
            var programStartPath = System.Reflection.Assembly.GetExecutingAssembly().Location;

            return Path.Combine(Path.GetDirectoryName(programStartPath), "DaedalusBuiltins");
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