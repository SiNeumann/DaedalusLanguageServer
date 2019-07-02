using OmniSharp.Extensions.LanguageServer.Protocol.Models;

namespace DaedalusLanguageServerLib.Services
{
    public static class DaedalusDefaults
    {
        public static readonly DocumentSelector DocumentSelector = new DocumentSelector(
            new DocumentFilter { Language = "daedalus" },
            new DocumentFilter { Pattern = "**/*.d" },
            new DocumentFilter { Pattern = "**/*.D" }
        );
        public static readonly TextDocumentRegistrationOptions RegistrationOptions = new TextDocumentRegistrationOptions
        {
            DocumentSelector = DocumentSelector
        };

        public static string Language => "daedalus";
    }
}
