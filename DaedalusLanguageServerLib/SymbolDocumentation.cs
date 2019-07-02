using System.Text.Json.Serialization;

namespace DaedalusLanguageServerLib
{
    public class SymbolDocumentation
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("desc")]
        public string Documentation { get; set; }
    }
}


