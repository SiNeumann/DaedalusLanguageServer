using System;
using System.Windows.Forms;
using Peter.CSParser;

namespace Peter.CParser
{
    public class Parser
    {
        private const int maxT = 80;
        private const bool T = true;
        private const bool x = false;
        private const int minErrDist = 2;

        public Scanner scanner;
        public Errors errors;

        public Token t;    // last recognized token
        public Token la;   // lookahead token
        private int errDist = minErrDist;
        private string m_DataType = "";
        private string m_Current = "";
        private int m_Count = 0;

        public CCodeInfo CodeInfo { get; } = new CCodeInfo();

        public Parser(Scanner scanner)
        {
            this.scanner = scanner;
            errors = new Errors();
        }

        private void SynErr(int n)
        {
            if (errDist >= minErrDist) errors.SynErr(la.line, la.col, n);
            errDist = 0;
        }

        public void SemErr(string msg)
        {
            if (errDist >= minErrDist) errors.SemErr(t.line, t.col, msg);
            errDist = 0;
        }

        private void Get()
        {
            for (; ; )
            {
                t = la;
                la = scanner.Scan();
                if (la.kind <= maxT) { ++errDist; break; }
                if (la.kind == 81)
                {
                    var def = la.val.Replace("\t", " ");
                    def = def.Replace("#define", "");
                    var count = 0;
                    while (def[count] == ' ')
                    {
                        count++;
                    }
                    def = def.Trim();
                    var space = false;
                    var temp = "";
                    for (var a = 0; a < def.Length; a++)
                    {
                        if (space && def[a] == ' ')
                        {
                        }
                        else if (def[a] == ' ')
                        {
                            space = true;
                            temp += def[a];
                        }
                        else
                        {
                            space = false;
                            temp += def[a];
                        }
                    }
                    while (temp.Substring(temp.Length - 1).Equals("\\"))
                    {
                        Get();
                        temp += " " + la.val;
                        var line = la.line;
                        while (scanner.Peek().line == line)
                        {
                            Get();
                            temp += " " + la.val;
                        }
                    }
                    var tm = new TokenMatch(temp, la.pos + 8);
                    this.CodeInfo.Defines.Add(tm);
                }

                la = t;
            }
        }

        private void Expect(int n)
        {
            if (la.kind == n) Get(); else { SynErr(n); }
        }

        private bool StartOf(int s)
        {
            return set[s, la.kind];
        }

        private void ExpectWeak(int n, int follow)
        {
            if (la.kind == n) Get();
            else
            {
                SynErr(n);
                while (!StartOf(follow)) Get();
            }
        }

        private bool WeakSeparator(int n, int syFol, int repFol)
        {
            var kind = la.kind;
            if (kind == n) { Get(); return true; }
            else if (StartOf(repFol)) { return false; }
            else
            {
                SynErr(n);
                while (!(set[syFol, kind] || set[repFol, kind] || set[0, kind]))
                {
                    Get();
                    kind = la.kind;
                }
                return StartOf(syFol);
            }
        }

        private void C()
        {
            while (la.kind == 75)
            {
                IncludeDirective();
            }
            while (StartOf(1) || la.kind == 33 || la.kind == 37)
            {
                TypeDeclaration();
            }
        }

        private void IncludeDirective()
        {
            Application.DoEvents();
            var tm = new TokenMatch();
            Expect(75);
            tm.Position = la.pos;
            tm.Value = la.val;
            while (scanner.Peek().line == la.line)
            {
                Get();
                tm.Value += la.val;
            }
            Get();
            this.CodeInfo.Includes.Add(tm);
        }

        private void TypeDeclaration()
        {
            Application.DoEvents();
            this.m_DataType = "";
            while ((StartOf(2) || la.kind == 33 || la.kind == 37) && la.kind != 0)
            {
                this.m_DataType += la.val + " ";
                Get();
            }

            var tempPos = la.pos;
            if (StartOf(3))
            {
                this.m_Current = "";
                Type();
                this.m_DataType += this.m_Current;
            }
            else if (la.kind == 1)
            {
                this.m_DataType += la.val;
                Get();
            }
            else SynErr(83);

            if (StartOf(3))
            {
                this.m_Current = "";
                Type();
                this.m_DataType += " " + this.m_Current;
            }

            var tm = new TokenMatch(la.val, la.pos);
            if (la.val == "{" && this.m_DataType.Trim().ToLower().IndexOf("struct") == 0)
            {
                Block();
                Expect(70);
                tm.Position = tempPos;
                tm.Value = this.m_DataType.Substring(6).Trim();
                this.CodeInfo.Structs.Add(tm);
            }
            else if (this.m_DataType.ToLower().IndexOf("typedef") == 0)
            {
                // typedef...
                if (la.val != "{")
                {
                    tm.Position = la.pos;
                    tm.Value = la.val;
                    Get();
                }
                if (la.val == "{")
                {
                    Block();
                    tm.Value = la.val;
                    tm.Position = la.pos;
                    Get();
                    while (la.kind != 70 && la.kind != 0)
                    {
                        tm.Value += " " + la.val;
                        Get();
                    }
                    tm.Value += ":" + this.m_DataType.Substring(7).Trim();
                    Expect(70);
                }
                else
                {
                    while (tm.Value == "*" && la.kind != 0)
                    {
                        tm.Value = la.val;
                        tm.Position = la.pos;
                        this.m_DataType += "*";
                        Get();
                    }
                    tm.Value += ":" + this.m_DataType.Substring(7).Trim();
                    Expression();
                    Expect(70);
                }
                this.CodeInfo.TypeDefs.Add(tm);
            }
            else
            {
                Application.DoEvents();
                while (la.kind == 72 && la.kind != 0)
                {
                    this.m_DataType += "*";
                    Get();
                }
                if (tm.Value == "*")
                {
                    tm.Position = la.pos;
                    tm.Value = la.val;
                }
                Expect(1);
                while (la.kind == 1 && la.kind != 0)
                {
                    tm.Value += " " + la.val;
                    Get();
                }
                while (la.kind == 53 && la.kind != 0)
                {
                    while (la.kind != 68)
                    {
                        Get();
                    }
                    Expect(68);
                    this.m_DataType += "[]";
                }

                if (la.kind == 70)
                {
                    tm.Value += ":" + this.m_DataType;
                    this.CodeInfo.GlobalVariables.Add(tm);
                    Get();
                    return;
                }

                if (la.val.Equals("("))
                {
                    Function(tm);
                }
                else
                {
                    Assignment(tm);
                }
            }
        }

        #region -= Type =-

        private void Type()
        {
            Application.DoEvents();
            this.m_Current += la.val;
            switch (la.kind)
            {
                case 27:
                    {
                        Get();
                        if (la.kind == 23)
                        {
                            this.m_Current += " " + la.val;
                            Get();
                        }
                        break;
                    }
                case 24:
                    {
                        Get();
                        if (la.kind == 19 || la.kind == 23)
                        {
                            this.m_Current += " " + la.val;
                            Get();
                        }
                        break;
                    }
                case 78:
                    {
                        Get();
                        if (la.kind == 10 || la.kind == 23 || la.kind == 24 || la.kind == 27)
                        {
                            this.m_Current += " " + la.val;
                            Get();
                        }
                        break;
                    }
                case 10:
                    {
                        Get();
                        break;
                    }
                case 23:
                    {
                        Get();
                        break;
                    }
                case 19:
                    {
                        Get();
                        break;
                    }
                case 15:
                    {
                        Get();
                        break;
                    }
                case 36:
                    {
                        Get();
                        break;
                    }
                default: SynErr(85); break;
            }
        }

        #endregion

        private void Expression()
        {
            Application.DoEvents();
            while (!la.val.Equals(";"))
            {
                Get();
            }
        }

        #region -= Function =-

        private void Function(TokenMatch tm)
        {
            Application.DoEvents();
            Expect(54);
            tm.Value += "(";
            if (StartOf(5) || StartOf(2))
            {
                this.m_Current = "";
                Parameters();
            }
            //tm.Value += ")";
            //Expect(69);
            if (this.m_Count == 2 && la.kind != 70)
            {
                this.m_Current = "";
                while (!la.val.Equals("{") && la.kind != 0)
                {
                    if (la.kind == 70)
                    {
                        this.m_Current += ",";
                    }
                    else
                    {
                        this.m_Current += " " + la.val;
                    }
                    Get();
                }
                this.m_Current = this.m_Current.TrimEnd(new char[] { ',' }) + " )";
            }

            tm.Value += this.m_Current;

            if (la.val.Equals("{"))
            {
                Block();
                tm.Value += ":" + this.m_DataType;
                this.CodeInfo.Functions.Add(tm);
            }
            else
            {
                while (la.kind != 70 && la.kind != 0)
                {
                    Get();
                }
                tm.Value += ":" + this.m_DataType;
                this.CodeInfo.Prototypes.Add(tm);
                Expect(70);
            }
        }

        #endregion

        private void Assignment(TokenMatch tm)
        {
            Application.DoEvents();
            if (StartOf(4))
            {
                Expression();
                tm.Value += ":" + this.m_DataType;
                this.CodeInfo.GlobalVariables.Add(tm);
            }
            else if (la.kind == 43)
            {
                // Comma(,)
                tm.Value += ":" + this.m_DataType;
                this.CodeInfo.GlobalVariables.Add(tm);
                var comma = true;
                while (la.kind != 70 && la.kind != 0)
                {
                    Get();
                    if (comma)
                    {
                        this.CodeInfo.GlobalVariables.Add(new TokenMatch(la.val + ":" + this.m_DataType, la.pos));
                        comma = false;
                    }
                    else if (la.kind == 43)
                    {
                        comma = true;
                    }
                }
            }
            Expect(70);
        }

        #region -= Assignment Operator =-

        private void AssignmentOperator()
        {
            switch (la.kind)
            {
                case 41:
                    {
                        Get();
                        break;
                    }
                case 73:
                    {
                        Get();
                        break;
                    }
                case 45:
                    {
                        Get();
                        break;
                    }
                case 60:
                    {
                        Get();
                        break;
                    }
                case 65:
                    {
                        Get();
                        break;
                    }
                case 59:
                    {
                        Get();
                        break;
                    }
                case 40:
                    {
                        Get();
                        break;
                    }
                case 74:
                    {
                        Get();
                        break;
                    }
                case 63:
                    {
                        Get();
                        break;
                    }
                case 55:
                    {
                        Get();
                        break;
                    }
                case 79:
                    {
                        Get();
                        break;
                    }
                default: SynErr(86); break;
            }
        }

        #endregion

        #region -= Block =-

        private void Block()
        {
            Application.DoEvents();
            Expect(52);
            var oBrackCount = 1;
            var cBrackCount = 0;
            while (oBrackCount != cBrackCount && la.kind != 0)
            {
                if (la.val.Equals("{"))
                {
                    oBrackCount++;
                }
                if (la.val.Equals("}"))
                {
                    cBrackCount++;
                }
                Get();
            }
            //Expect(67);
        }

        #endregion

        #region -= Parmeters =-

        private void Parameters()
        {
            Application.DoEvents();
            this.m_Count = 0;
            var oPar = 1;
            var cPar = 0;
            while (cPar != oPar && la.kind != 0)
            {
                if (la.kind == 43)
                {
                    this.m_Current += la.val;
                    this.m_Count = 0;
                }
                else
                {
                    this.m_Current += " " + la.val;
                    this.m_Count++;
                }

                if (la.val.Equals("void"))
                {
                    this.m_Count++;
                }

                if (la.kind == 54)
                {
                    oPar++;
                }
                if (la.kind == 69)
                {
                    cPar++;
                }
                Get();
            }
        }

        #endregion

        public void Parse()
        {
            la = new Token
            {
                val = ""
            };
            Get();
            C();

            Expect(0);
        }

        #region -= Set =-

        private readonly bool[,] set = {
        {T,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x},
        {x,T,x,x, x,x,T,x, x,x,T,T, x,x,x,T, x,x,T,T, x,x,x,T, T,x,x,T, x,x,T,T, x,x,x,x, T,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,T,x, x,x},
        {x,x,x,x, x,x,T,x, x,x,x,T, x,x,x,x, x,x,T,x, x,x,x,x, x,x,x,x, x,x,T,T, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x},
        {x,x,x,x, x,x,x,x, x,x,T,x, x,x,x,T, x,x,x,T, x,x,x,T, T,x,x,T, x,x,x,x, x,x,x,x, T,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,T,x, x,x},
        {x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, T,T,x,x, x,T,x,x, x,x,x,x, x,x,x,T, x,x,x,T, T,x,x,T, x,T,x,x, x,x,x,x, x,T,T,x, x,x,x,T, x,x},
        {x,T,x,x, x,x,x,x, x,x,T,x, x,x,x,T, x,x,x,T, x,x,x,T, T,x,x,T, x,x,x,x, x,x,x,x, T,x,x,x, x,x,x,T, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,T,x,x, x,x,x,x, x,x,T,x, x,x},
        {x,T,x,x, x,x,x,x, x,x,T,x, x,x,x,T, x,x,x,T, x,x,x,T, T,x,x,T, x,x,x,x, x,x,x,x, T,x,x,x, x,x,x,T, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,T,x, x,x}

        };

        #endregion

    } // end Parser

    #region -= Errors =-

    public class Errors
    {
        public int count = 0;                                    // number of errors detected
        public System.IO.TextWriter errorStream = Console.Out;   // error messages go to this stream
        public string errMsgFormat = "-- line {0} col {1}: {2}"; // 0=line, 1=column, 2=text

        public void SynErr(int line, int col, int n)
        {
            string s;
            switch (n)
            {
                case 0: s = "EOF expected"; break;
                case 1: s = "ident expected"; break;
                case 2: s = "intCon expected"; break;
                case 3: s = "realCon expected"; break;
                case 4: s = "charCon expected"; break;
                case 5: s = "stringCon expected"; break;
                case 6: s = "auto expected"; break;
                case 7: s = "break expected"; break;
                case 8: s = "case expected"; break;
                case 9: s = "catch expected"; break;
                case 10: s = "char expected"; break;
                case 11: s = "const expected"; break;
                case 12: s = "continue expected"; break;
                case 13: s = "default expected"; break;
                case 14: s = "do expected"; break;
                case 15: s = "double expected"; break;
                case 16: s = "else expected"; break;
                case 17: s = "enum expected"; break;
                case 18: s = "extern expected"; break;
                case 19: s = "float expected"; break;
                case 20: s = "for expected"; break;
                case 21: s = "goto expected"; break;
                case 22: s = "if expected"; break;
                case 23: s = "int expected"; break;
                case 24: s = "long expected"; break;
                case 25: s = "register expected"; break;
                case 26: s = "return expected"; break;
                case 27: s = "short expected"; break;
                case 28: s = "signed expected"; break;
                case 29: s = "sizeof expected"; break;
                case 30: s = "static expected"; break;
                case 31: s = "struct expected"; break;
                case 32: s = "switch expected"; break;
                case 33: s = "typedef expected"; break;
                case 34: s = "union expected"; break;
                case 35: s = "unsigned expected"; break;
                case 36: s = "void expected"; break;
                case 37: s = "volatile expected"; break;
                case 38: s = "while expected"; break;
                case 39: s = "and expected"; break;
                case 40: s = "andassgn expected"; break;
                case 41: s = "assgn expected"; break;
                case 42: s = "colon expected"; break;
                case 43: s = "comma expected"; break;
                case 44: s = "dec expected"; break;
                case 45: s = "divassgn expected"; break;
                case 46: s = "dot expected"; break;
                case 47: s = "dblcolon expected"; break;
                case 48: s = "eq expected"; break;
                case 49: s = "gt expected"; break;
                case 50: s = "gteq expected"; break;
                case 51: s = "inc expected"; break;
                case 52: s = "lbrace expected"; break;
                case 53: s = "lbrack expected"; break;
                case 54: s = "lpar expected"; break;
                case 55: s = "lshassgn expected"; break;
                case 56: s = "lt expected"; break;
                case 57: s = "ltlt expected"; break;
                case 58: s = "minus expected"; break;
                case 59: s = "minusassgn expected"; break;
                case 60: s = "modassgn expected"; break;
                case 61: s = "neq expected"; break;
                case 62: s = "not expected"; break;
                case 63: s = "orassgn expected"; break;
                case 64: s = "plus expected"; break;
                case 65: s = "plusassgn expected"; break;
                case 66: s = "question expected"; break;
                case 67: s = "rbrace expected"; break;
                case 68: s = "rbrack expected"; break;
                case 69: s = "rpar expected"; break;
                case 70: s = "scolon expected"; break;
                case 71: s = "tilde expected"; break;
                case 72: s = "times expected"; break;
                case 73: s = "timesassgn expected"; break;
                case 74: s = "xorassgn expected"; break;
                case 75: s = "\"#include\" expected"; break;
                case 76: s = "\"\"\" expected"; break;
                case 77: s = "\"/\" expected"; break;
                case 78: s = "\"unsigned\" expected"; break;
                case 79: s = "\">>=\" expected"; break;
                case 80: s = "??? expected"; break;
                case 81: s = "invalid IncludeDirective"; break;
                case 82: s = "invalid IncludeDirective"; break;
                case 83: s = "invalid TypeDeclaration"; break;
                case 84: s = "invalid TypeDeclaration"; break;
                case 85: s = "invalid Type"; break;
                case 86: s = "invalid AssignmentOperator"; break;
                case 87: s = "invalid Parameters"; break;

                default: s = "error " + n; break;
            }
            errorStream.WriteLine(errMsgFormat, line, col, s);
            count++;
        }

        public void SemErr(int line, int col, string s)
        {
            errorStream.WriteLine(errMsgFormat, line, col, s);
            count++;
        }

        public void SemErr(string s)
        {
            errorStream.WriteLine(s);
            count++;
        }

        public void Warning(int line, int col, string s)
        {
            errorStream.WriteLine(errMsgFormat, line, col, s);
        }

        public void Warning(string s)
        {
            errorStream.WriteLine(s);
        }
    } // Errors


    public class FatalError : Exception
    {
        public FatalError(string m) : base(m) { }
    }

    #endregion
}
