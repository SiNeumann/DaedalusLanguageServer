using Antlr4.Runtime.Misc;
using DaedalusCompiler.Compilation.Symbols;
using System.Collections.Generic;

namespace DaedalusCompiler.Compilation
{
    public class DaedalusStatefulParseTreeListener : DaedalusBaseListener
    {
        public List<Variable> GlobalVars { get; } = new List<Variable>();
        public List<Constant> GlobalConsts { get; } = new List<Constant>();
        public List<Function> GlobalFunctions { get; } = new List<Function>();
        public List<Class> GlobalClasses { get; } = new List<Class>();
        public List<Prototype> GlobalPrototypes { get; } = new List<Prototype>();
        public List<Instance> GlobalInstances { get; } = new List<Instance>();
        

        public override void EnterInlineDef([NotNull] DaedalusParser.InlineDefContext context)
        {
            AddGlobalsInlineDef(context);
        }

        public override void EnterBlockDef([NotNull] DaedalusParser.BlockDefContext context)
        {
            AddGlobalsBlockDef(context);
        }

        private void AddGlobalsBlockDef(Antlr4.Runtime.ParserRuleContext ruleContext)
        {
            // Functions

            var allFuncDefs = ruleContext.GetRuleContexts<DaedalusParser.FunctionDefContext>();
            foreach (var fd in allFuncDefs)
            {
                var p = new List<Variable>();
                var funcParameters = fd.parameterList().parameterDecl();
                if (funcParameters != null)
                {
                    foreach (var pdef in funcParameters)
                    {
                        p.Add(new Variable
                        {
                            Name = pdef.nameNode().GetText(),
                            Type = pdef.typeReference().GetText(),
                            Column = pdef.nameNode().Start.Column,
                            Line = pdef.nameNode().Start.Line,
                        });
                    }
                }
                GlobalFunctions.Add(new Function
                {
                    Name = fd.nameNode().GetText(),
                    ReturnType = fd.typeReference().GetText(),
                    Line = fd.nameNode().Start.Line,
                    Column = fd.nameNode().Start.Column,
                    Parameters = p,
                });
            }

            // Classes

            var allClassDefs = ruleContext.GetRuleContexts<DaedalusParser.ClassDefContext>();
            foreach (var cd in allClassDefs)
            {
                var classVars = new List<Variable>();
                var allVarDecls = cd.varDecl();
                if (allVarDecls != null)
                {
                    foreach (var v in allVarDecls)
                    {
                        foreach (var vals in v.varValueDecl())
                        {
                            classVars.Add(new Variable
                            {
                                Name = vals.nameNode().GetText(),
                                Type = v.typeReference().GetText(),
                                Line = vals.nameNode().Start.Line,
                                Column = vals.nameNode().Start.Column
                            });
                        }
                    }
                }
                GlobalClasses.Add(new Class
                {
                    Fields = classVars,
                    Name = cd.nameNode().GetText(),
                    Line = cd.nameNode().Start.Line,
                    Column = cd.nameNode().Start.Column,
                });
            }

            // Prototypes

            var allPrototypeDefs = ruleContext.GetRuleContexts<DaedalusParser.PrototypeDefContext>();
            foreach (var pd in allPrototypeDefs)
            {
                GlobalPrototypes.Add(new Prototype
                {
                    Name = pd.nameNode().GetText(),
                    Column = pd.nameNode().Start.Column,
                    Line = pd.nameNode().Start.Line,
                    Parent = pd.parentReference().Identifier().GetText(),
                });
            }

            // INSTANCES

            var allInsts = ruleContext.GetRuleContexts<DaedalusParser.InstanceDefContext>();
            foreach (var inst in allInsts)
            {
                GlobalInstances.Add(new Instance
                {
                    Column = inst.nameNode().Start.Column,
                    Line = inst.nameNode().Start.Line,
                    Name = inst.nameNode().GetText(),
                    Parent = inst.parentReference().GetText(),
                });
            }
        }

        private void AddGlobalsInlineDef(Antlr4.Runtime.ParserRuleContext ruleContext)
        {
            // CONSTANTS 

            var allConsts = ruleContext.GetRuleContexts<DaedalusParser.ConstDefContext>();
            foreach (var c in allConsts)
            {
                foreach (var cv in c.constValueDef())
                {
                    GlobalConsts.Add(new Constant
                    {
                        Column = cv.Start.Column,
                        Line = cv.Start.Line,
                        Name = cv.nameNode().GetText(),
                        Type = c.typeReference().GetText(),
                        Value = cv.constValueAssignment().expressionBlock().GetText(),
                    });
                }
            }

            // VARIABLES 

            var allVarDecls = ruleContext.GetRuleContexts<DaedalusParser.VarDeclContext>();
            foreach (var v in allVarDecls)
            {
                foreach (var vals in v.varValueDecl())
                {
                    GlobalVars.Add(new Variable
                    {
                        Name = vals.nameNode().GetText(),
                        Type = v.typeReference().GetText(),
                        Line = vals.nameNode().Start.Line,
                        Column = vals.nameNode().Start.Column
                    });
                }
                foreach (var innerVarDecl in v.varDecl())
                {
                    foreach (var vals in v.varValueDecl())
                    {
                        GlobalVars.Add(new Variable
                        {
                            Name = vals.nameNode().GetText(),
                            Type = v.typeReference().GetText(),
                            Line = vals.nameNode().Start.Line,
                            Column = vals.nameNode().Start.Column
                        });
                    }
                }
            }
        }
    }
}
