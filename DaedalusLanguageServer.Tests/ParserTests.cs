using DaedalusCompiler.Compilation;
using DaedalusLanguageServerLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DaedalusLanguageServer.Tests
{
    [TestClass]
    public class ParserTests
    {
        [TestMethod]
        public void ParseGothicSrc()
        {
            var AppDir = AppDomain.CurrentDomain.BaseDirectory;
            var externals = new Dictionary<Uri, ParseResult>();
            var buildInsPath = Path.Combine(AppDir, "DaedalusBuiltins");

            foreach (var builtIn in Directory.EnumerateFiles(buildInsPath, "*.d"))
            {
                var builtInUri = new Uri(builtIn);
                var parsedResult = FastCompiler.Load(builtIn, true);
                externals[builtInUri] = parsedResult;
            }

            string srcPath = @"A:\Spiele\Gothic II_Mods\_work\Data\Scripts\Content\Gothic.src";
            var parseResults = FastCompiler.ParseSrc(srcPath, externals);
        }

        [TestMethod]
        public void ParseInstance()
        {
            var parsed = FastCompiler.Parse(@"instance PC_Hero (NPC_DEFAULT) {};");
        }
        [TestMethod]
        public void ParseMultiVarsAndNumericIdentifiers()
        {
            var parsed = FastCompiler.Parse(@"///Does magical stuff
func int learn1hSkill(var int skill) {
	var string 1hSkillText, var int meinInt;
	1hSkillText = ConcatStrings (""Lerne: Einhand Stufe "", IntToString (skill));

    decreaseLearnPoints(B_GetLearnCostWeaponSkill(NPC_TALENT_1H, skill));
            PrintScreen_Ext(1hSkillText, -1, -1, FONT_SCREEN, 2);
            B_RaiseFightTalent(hero, NPC_TALENT_1H, B_GetRaiseAmountOfWeaponSkill(NPC_TALENT_1H, skill));
            onehandedSkillLevel = skill;

    MEM_SwapBytes(n0+60,                       n1+60,                      64);                          // trafo

    };");
            if (parsed.SyntaxErrors.Count > 0)
            {
                foreach (var err in parsed.SyntaxErrors)
                {
                    Console.WriteLine($"{err.Line}:{err.Column} {err.ErrorCode.Code}: {err.ErrorCode.Description}");
                }
            }
        }
        [TestMethod]
        public void ParseFileWithVariables()
        {
            var parsed = FastCompiler.Parse(@"const string NINJA_MANAREG_VERSION = ""ManaReg 1.1.0""
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
                var symbolInfos = JsonSerializer.Deserialize<List<SymbolDocumentation>>(File.ReadAllText(symbolInfoPath, Encoding.UTF8));
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
