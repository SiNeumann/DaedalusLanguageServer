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
    public abstract class Symbol : IEqualityComparer<Symbol>
    {
        public static readonly IEqualityComparer<Symbol> SameNameComparer = new SameNameEqualityComparer();
        public string Name { get; set; }
        public int Line { get; set; }
        public int Column { get; set; }
        public Uri Source { get; set; }
        public string Documentation { get; set; }

        /// <summary>Returns the daedalus representation of the symbol</summary>
        public abstract override string ToString();

        #region IEqualityComparer<Symbol>

        public override int GetHashCode() => HashCode.Combine(this.Name, this.Line, this.Column, this.Source);
        public int GetHashCode(Symbol obj) => obj.GetHashCode();
        public override bool Equals(object obj)
        {
            return obj is Symbol symbol && Equals(this, symbol);
        }

        public bool Equals(Symbol x, Symbol y)
        {
            if (x is null || y is null) return false;
            return this.Name == y.Name &&
                   this.Line == y.Line &&
                   this.Column == y.Column &&
                   EqualityComparer<Uri>.Default.Equals(this.Source, y.Source);
        }

        private class SameNameEqualityComparer : IEqualityComparer<Symbol>
        {
            public bool Equals(Symbol x, Symbol y)
            {
                if (x is null || y is null) return false;
                return x.Name.Equals(y.Name, StringComparison.OrdinalIgnoreCase);
            }
            public int GetHashCode(Symbol obj) => HashCode.Combine(obj.Name);
        }

        #endregion
    }
    public class Class : Symbol
    {
        public List<Variable> Fields { get; set; }
        public override string ToString() => $"class {Name}";
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
