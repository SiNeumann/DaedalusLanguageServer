using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;

namespace DaedalustLib.Tests
{
    [TestClass]
    public class ParserTest
    {
        [TestMethod]
        public void Parse()
        {
            const string code = @"class _PM_SaveObject_Cls {
    var int type;
    var string name;
    var int content; // zCArray<_PM_SaveObject*>*
    var string class;
};
func zCView View_Get(var int hndl) { 
    get(hndl);
};";
            var parserResult = DaedalusLib.Parser.DaedalusParserHelper.Parse(code);
        }

        [TestMethod]
        public void Special_Identifiers_As_Fields()
        {
            const string code = @"func void Test() {
    oc.class = """";
};";
            var parserResult = DaedalusLib.Parser.DaedalusParserHelper.Parse(code);
            Assert.AreEqual(parserResult.ErrorMessages.Count, 0);
        }

        [TestMethod]
        public void Multiple_Vars_Per_Line()
        {
            const string code = @"func void Test() {
    var int l0, var int l1, var int l2;
};";
            var parserResult = DaedalusLib.Parser.DaedalusParserHelper.Parse(code);
            Assert.AreEqual(parserResult.ErrorMessages.Count, 0, parserResult.ErrorMessages.FirstOrDefault()?.Message);
        }

        [TestMethod]
        public void Special_Identifiers_As_Variable_Identifiers()
        {
            const string code = @"class _PM_SaveObject_Cls {
    var int type;
    var string name;
    var int content; // zCArray<_PM_SaveObject*>*
    var string class;
};";
            var parserResult = DaedalusLib.Parser.DaedalusParserHelper.Parse(code);
            Assert.AreEqual(parserResult.ErrorMessages.Count, 0);
        }

        [TestMethod]
        public void InvalidOperator_FIXME()
        {
            byte[] code = Convert.FromBase64String("ZnVuYyBWT0lEIEJfVmF0cmFzX0lORkxVRU5DRV9SRVBFQVQoKSB7DQoMCUFJX091dHB1dCAoc2VsZiwgb3RoZXIsICJESUFfVmF0cmFzX0lORkxVRU5DRV9SRVBFQVRfMDVfMDEiKTsgLy9HdXQsIGZhc3NlbiB3aXIgbWFsIHp1c2FtbWVuOg0KfTs=");
            var mem = new MemoryStream(code);
            var parserResult = DaedalusLib.Parser.DaedalusParserHelper.Load(mem);
            if (parserResult.ErrorMessages.Count > 0)
            {
                foreach (var err in parserResult.ErrorMessages)
                {
                    Console.WriteLine($"{err.Message} [{err.Line}, {err.Column}]");
                }
            }
            Assert.AreEqual(parserResult.ErrorMessages.Count, 0);
        }

        [TestMethod]
        public void G2_Scripts()
        {
            var path = @"A:\Spiele\Gothic II_Mods\_work\Data\Scripts\Content";
            var scripts = Directory.EnumerateFiles(path, "*.*", SearchOption.AllDirectories)
                .Where(x => x.EndsWith("D", StringComparison.OrdinalIgnoreCase))
                .ToList();
            foreach (var script in scripts)
            {
                if (script.Contains("AI_Intern", StringComparison.OrdinalIgnoreCase) && script.EndsWith("Externals.d", StringComparison.OrdinalIgnoreCase)) continue;

                var parserResult = DaedalusLib.Parser.DaedalusParserHelper.Load(script);
                if (parserResult.ErrorMessages.Count > 0)
                {
                    Console.WriteLine($"File: {script}");
                    foreach (var err in parserResult.ErrorMessages)
                    {
                        Console.WriteLine($"{err.Message} [{err.Line}, {err.Column}]");
                    }
                }
                //Assert.AreEqual(parserResult.ErrorMessages.Count, 0);
            }
        }
    }
}
