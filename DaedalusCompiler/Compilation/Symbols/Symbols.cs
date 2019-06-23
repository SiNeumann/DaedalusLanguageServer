using System;
using System.Collections.Generic;

namespace DaedalusCompiler.Compilation.Symbols
{
    public class Constant : Variable
    {
        public string Value { get; set; }
        public override string ToString() => $"const {Type} {Name} = {Value}";
    }
    public class Function : Symbol
    {
        public string ReturnType { get; set; }
        public List<Variable> Parameters { get; set; }
        public override string ToString() => $"func {ReturnType} {Name}({string.Join(", ", Parameters)})";
    }
    public class Variable : Symbol
    {
        public string Type { get; set; }
        public override string ToString() => $"var {Type} {Name}";
    }
    public abstract class Symbol
    {
        public string Name { get; set; }
        public int Line { get; set; }
        public int Column { get; set; }
        public Uri Source { get; set; }
        /// <summary>Returns the daedalus representation of the symbol</summary>
        public abstract override string ToString();
    }
    public class Class : Symbol
    {
        public List<Variable> Fields { get; set; }
        public override string ToString()
        {
            if (Fields?.Count > 0)
            {
                return $"class {Name} {{\n\t{string.Join("\t", Fields)}}};";
            }
            return $"class {Name}";
        }
    }
    public class Prototype : Symbol
    {
        public string Parent { get; set; }
        public override string ToString() => $"prototype {Name} ({Parent})";
    }

    public class Instance : Prototype
    {
        public override string ToString() => $"instance {Name} ({Parent})";
    }
}
