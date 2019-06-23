using DaedalusCompiler.Compilation.Symbols;
using System;
using System.Collections.Generic;

namespace DaedalusCompiler.Compilation
{
    public class ParseResult
    {
        public List<SyntaxError> SyntaxErrors { get; set; }
        public List<Variable> GlobalVariables { get; set; }
        public List<Constant> GlobalConstants { get; set; }
        public List<Function> GlobalFunctions { get; set; }
        public List<Class> GlobalClasses { get; set; }
        public List<Prototype> GlobalPrototypes { get; set; }
        public List<Instance> GlobalInstances { get; set; }
        public Uri Source { get; set; }
    }
}
