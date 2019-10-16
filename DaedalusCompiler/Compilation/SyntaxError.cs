namespace DaedalusCompiler.Compilation
{
    public class SyntaxError
    {
        public const string DataKey_ErrorCode = "ErrorCode";

        public SyntaxErrorCode ErrorCode { get; set; }
        public int Line { get; set; }
        public int Column { get; set; }
    }
    public enum ErrorSeverity
    {
        Info = 0,
        Warning = 1,
        Error = 2,
    }
    public static class SyntaxErrorCodes
    {
        public static readonly SyntaxErrorCode D0001_No_Identifier_With_Starting_Digits
            = new SyntaxErrorCode("D0001", "Do not start identifiers with digits", ErrorSeverity.Warning);
        public static readonly SyntaxErrorCode D0002_Split_Multiple_Var_Decl
            = new SyntaxErrorCode("D0002", "Split multiple 'var TYPE ..., var TYPE ...' into separate statements.", ErrorSeverity.Warning);
    }

    public class SyntaxErrorCode
    {
        public string Code { get; }
        public string Description { get; }
        public ErrorSeverity Severity { get; }

        public SyntaxErrorCode(string code, string description, ErrorSeverity errorSeverity = ErrorSeverity.Error)
        {
            this.Code = code;
            this.Description = description;
            this.Severity = errorSeverity;
        }
    }
}