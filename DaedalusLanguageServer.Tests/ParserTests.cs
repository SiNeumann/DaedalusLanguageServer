using DaedalusCompiler.Compilation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;

namespace DaedalusLanguageServer.Tests
{
    [TestClass]
    public class ParserTests
    {
        [TestMethod]
        public void ParseGothicSrc()
        {
            var parsed = Compiler.ParseSrc(@"A:\Spiele\Gothic II_Mods\_work\Data\Scripts\Content\Gothic.src", Environment.ProcessorCount);
            foreach (var kvp in parsed)
            {
                if (kvp.Value.SyntaxErrors.Count > 0)
                {
                    Console.WriteLine($"Errors in {kvp.Key.LocalPath}");
                    foreach (var err in kvp.Value.SyntaxErrors)
                    {
                        Console.WriteLine($"{err.Line}:{err.Column} {err.Message}");
                    }
                }
            }
            Assert.IsTrue(!parsed.Values.Any(p => p.SyntaxErrors.Count > 0));
        }

        [TestMethod]
        public void ParseInstance()
        {
            var parsed = Compiler.Parse(@"instance PC_Hero (NPC_DEFAULT) {};");
        }

        [TestMethod]
        public void ParseFileWithVariables()
        {
            var parsed = Compiler.Parse(@"const string NINJA_MANAREG_VERSION = ""ManaReg 1.1.0"";
func void Ninja_ManaReg_Regeneration() {
    // Not during loading
    if (!Hlp_IsValidNpc(hero)) { return; };
    // Only in-game
    if (!MEM_Game.timestep) { return; };
    // Only in a certain interval
    var int delayTimer; delayTimer += MEM_Timer.frameTime;
    if (delayTimer < DEFAULT_NINJA_MANAREG_TICKRATE) { return; };
	
    delayTimer -= DEFAULT_NINJA_MANAREG_TICKRATE;
    
    if (hero.attribute[ATR_MANA_MAX] >= Ninja_ManaReg_Mana_Threshold) {
        if (hero.attribute[ATR_MANA] < hero.attribute[ATR_MANA_MAX]) {
            var int menge; menge = (hero.attribute[ATR_MANA_MAX] + (Ninja_ManaReg_Max_Mana_Divisor/2)) / Ninja_ManaReg_Max_Mana_Divisor;
            Npc_ChangeAttribute(hero, ATR_MANA, menge);
		};
    };
};", detailed: true);
        }

        [TestMethod]
        public void CreateSymbolDocumentationLookup()
        {
            var symbolInfoPath = Path.Combine("DaedalusBuiltins", "symbols.json");
            Dictionary<string, string> documentations = null;
            if (File.Exists(symbolInfoPath))
            {
                var symbolInfos = JsonSerializer.Parse<List<SymbolDocumentation>>(File.ReadAllText(symbolInfoPath, Encoding.UTF8));
                documentations = symbolInfos.ToDictionary(x => x.Name, x => x.Documentation, StringComparer.OrdinalIgnoreCase);
            }
        }

        public class SymbolDocumentation
        {
            [JsonPropertyName("name")]
            public string Name { get; set; }
            [JsonPropertyName("desc")]
            public string Documentation { get; set; }
        }
    }
}
