using DaedalusLanguageServerLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DaedalusLanguageServer.Tests
{
    [TestClass]
    public class BufferedDocumentTests
    {
        [TestMethod]
        public void GetMethodCall()
        {
            var test = @"const string NINJA_MANAREG_VERSION = ""ManaReg 1.1.0"";
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
};";
            var line = 15; var column = 32;
            var doc = new BufferedDocument(test.ToCharArray());
            var mc = doc.GetMethodCall(new OmniSharp.Extensions.LanguageServer.Protocol.Models.Position(line, column));
            Assert.AreEqual("Npc_ChangeAttribute(", mc);

            column = 47;
            mc = doc.GetMethodCall(new OmniSharp.Extensions.LanguageServer.Protocol.Models.Position(line, column));
            Assert.AreEqual("Npc_ChangeAttribute(hero, ATR_MANA,", mc);
        }

        [TestMethod]
        public void GetMethodCallWithNewlines()
        {
            var test = @"func void Ninja_ManaReg_ApplyINI() {
	
	var string ini_threshold; ini_threshold = MEM_GetGothOpt(NINJA_MANAREG_INI, NINJA_MANAREG_INI_THRESHOLD);
	var string ini_divisor; ini_divisor = MEM_GetGothOpt(NINJA_MANAREG_INI, NINJA_MANAREG_INI_DIVISOR);

	Ninja_ManaReg_Mana_Threshold = DEFAULT_NINJA_MANAREG_MANA_THRESHOLD;
	Ninja_ManaReg_Max_Mana_Divisor = DEFAULT_NINJA_MANAREG_MAX_MANA_DIVISOR;
	
	MEM_Info(
		ConcatStrings(
			ConcatStrings(NINJA_MANAREG_VERSION, // Some comments
			"": THRESHOLD FROM INI = ""), ini_threshold));
	MEM_Info(ConcatStrings(ConcatStrings(NINJA_MANAREG_VERSION, "": DIVISOR FROM INI = ""), ini_divisor));
	
	if (!Hlp_StrCmp(ini_threshold, """")) {
		Ninja_ManaReg_Mana_Threshold = STR_ToInt(ini_threshold);
	};
	
	
	if (!Hlp_StrCmp(ini_divisor, """")) {
		Ninja_ManaReg_Max_Mana_Divisor = STR_ToInt(ini_divisor);
	};
};";
            var line = 11; var column = 15;
            var doc = new BufferedDocument(test.ToCharArray());
            var mc = doc.GetMethodCall(new OmniSharp.Extensions.LanguageServer.Protocol.Models.Position(line, column));
            Assert.AreEqual(@"ConcatStrings(NINJA_MANAREG_VERSION, // Some comments
			"": THRESHOLD", mc);
        }
    }
}
