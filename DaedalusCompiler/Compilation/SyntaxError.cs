namespace DaedalusCompiler.Compilation
{
    public class SyntaxError
    {
        public string Message { get; set; }
        public int Line { get; set; }
        public int Column { get; set; }
    }
}