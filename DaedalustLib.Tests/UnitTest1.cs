using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    }
}
