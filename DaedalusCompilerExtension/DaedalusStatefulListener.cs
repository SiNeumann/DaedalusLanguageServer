using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using DaedalusCompiler.Compilation.Symbols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DaedalusCompiler.Compilation
{
    public class DaedalusStatefulDetailedParseTreeListener : DaedalusStatefulParseTreeListener
    {
        public DaedalusStatefulDetailedParseTreeListener(DaedalusParser parser, IEnumerable<ParseResult> currentParseResults = null)
            : base(parser, currentParseResults)
        {
        }

        public override void EnterFunctionDef([NotNull] DaedalusParser.FunctionDefContext context)
        {
            base.EnterFunctionDef(context);
        }

        private void AddStatementsFromIf(List<Statement> target, DaedalusParser.IfBlockStatementContext context)
        {
            var ifStmt = context.ifBlock().statementBlock();
            if (ifStmt != null)
            {
                AddStatementsFromBlock(target, ifStmt);
            }
            foreach (var elseIfStmt in context.elseIfBlock())
            {
                AddStatementsFromBlock(target, elseIfStmt.statementBlock());
            }

            var elseStmt = context.elseBlock()?.statementBlock();
            if (elseStmt != null)
            {
                AddStatementsFromBlock(target, elseStmt);
            }
        }
        private void AddStatementsFromBlock(List<Statement> target, DaedalusParser.StatementBlockContext statementBlock)
        {
            if (statementBlock.statement() != null)
            {
                AddStatements(target, statementBlock.statement());
            }
            if (statementBlock.ifBlockStatement() != null)
            {
                foreach (var ifStmt in statementBlock.ifBlockStatement())
                {
                    AddStatementsFromIf(target, ifStmt);
                }
            }
        }
        private void AddStatements(List<Statement> target, IEnumerable<DaedalusParser.StatementContext> statements)
        {
            foreach (var stmt in statements)
            {
                target.Add(new Statement
                {
                    Definition = new Defintion
                    {
                        Start = new DefinitionIndex(stmt.Start.Line, stmt.Start.Column),
                        End = new DefinitionIndex(stmt.Stop.Line, stmt.Stop.Column),
                    },
                    Text = stmt.GetText()
                });
            }
        }
        public override void ExitFunctionDef([NotNull] DaedalusParser.FunctionDefContext context)
        {
            var currentFunc = CurrentFunctionDef;
            CurrentFunctionDef = null;

            if (currentFunc != null)
            {
                currentFunc.Definition.End = new DefinitionIndex(context.Stop.Line, context.Stop.Column);
            }
        }
    }

    public class DaedalusStatefulParseTreeListener : DaedalusBaseListener
    {
        public List<Variable> GlobalVars { get; } = new List<Variable>();
        public List<Constant> GlobalConsts { get; } = new List<Constant>();
        public List<Function> GlobalFunctions { get; } = new List<Function>();
        public List<Class> GlobalClasses { get; } = new List<Class>();
        public List<Prototype> GlobalPrototypes { get; } = new List<Prototype>();
        public List<Instance> GlobalInstances { get; } = new List<Instance>();

        protected Function CurrentFunctionDef;
        protected readonly DaedalusParser parser;
        protected readonly IEnumerable<ParseResult> currentParseResults;
        protected bool isInsideStatement;

        public DaedalusStatefulParseTreeListener(DaedalusParser parser, IEnumerable<ParseResult> currentParseResults)
        {
            this.parser = parser;
            this.currentParseResults = currentParseResults ?? Enumerable.Empty<ParseResult>(); ;
        }
        private RecognitionException CreateRecognitionException(ParserRuleContext context, SyntaxErrorCode errorCode)
        {
            var error = new RecognitionException(parser, parser.InputStream, context);
            error.Data.Add(SyntaxError.DataKey_ErrorCode, errorCode);
            return error;
        }
        public override void EnterInlineDef([NotNull] DaedalusParser.InlineDefContext context)
        {
            AddGlobalsInlineDef(context);
        }

        public override void EnterBlockDef([NotNull] DaedalusParser.BlockDefContext context)
        {
            AddGlobalsBlockDef(context);
        }

        public override void EnterAnyIdentifier([NotNull] DaedalusParser.AnyIdentifierContext context)
        {
            var id = context.Identifier()?.GetText();
            if (id?.Length > 0 && char.IsDigit(id[0]))
            {
                var error = CreateRecognitionException(context, SyntaxErrorCodes.D0001_No_Identifier_With_Starting_Digits);
                parser.NotifyErrorListeners(context.Identifier().Symbol, error.Message, error);
            }

            // TODO: Implement checking if an identifier is defined.
            return;

            if (string.Equals(id, "hero", StringComparison.OrdinalIgnoreCase)) return;
            if (string.Equals(id, "self", StringComparison.OrdinalIgnoreCase)) return;

            if (!isInsideStatement) return;
            if (context.Parent?.Parent is DaedalusParser.VarDeclContext) return;

            // Variable definition, exists after this.
            if (context.Parent?.Parent is DaedalusParser.VarValueDeclContext) return;
            // Const definition, exists after this.
            if (context.Parent?.Parent is DaedalusParser.ConstDefContext) return;
            // Const Value definition, exists after this.
            if (context.Parent?.Parent is DaedalusParser.ConstValueDefContext) return;
            // Inside VarArray definition, exists after this.
            if (context.Parent?.Parent is DaedalusParser.VarArrayDeclContext) return;

            // After a "." or as a parameter.
            if (context.Parent?.Parent is DaedalusParser.ReferenceAtomContext) {
                if (context.Parent?.Parent?.Parent?.Parent is DaedalusParser.AssignmentContext) return;
                if (context.Parent?.Parent?.Parent is DaedalusParser.ReferenceContext refCtx && refCtx.GetText().Contains('.')) return;
            }
            
            // Inside Instance or Prototype definition.
            if (context.Parent?.Parent?.Parent?.Parent?.Parent?.Parent?.Parent is DaedalusParser.InstanceDefContext) return;
            if (context.Parent?.Parent?.Parent?.Parent?.Parent?.Parent?.Parent is DaedalusParser.PrototypeDefContext) return;

            var isDefined = GetIsIdentifierDefined(id, context.Start.Column, context.Start.Line);
            if (!isDefined)
            {
                var error = CreateRecognitionException(context, new SyntaxErrorCode("D0003", $"Identifier is not defined '{id}'"));
                parser.NotifyErrorListeners(context.Start, error.Message, error);
            }
        }

        public override void EnterStatement([NotNull] DaedalusParser.StatementContext context)
        {
            isInsideStatement = true;
        }
        public override void ExitStatement([NotNull] DaedalusParser.StatementContext context)
        {
            isInsideStatement = false;
        }

        private bool GetIsIdentifierDefined(string identifier, int col, int line)
        {
            bool isDefined = false;
            var start = col;
            var startLine = line;
            if (CurrentFunctionDef != null)
            {
                foreach (var item in CurrentFunctionDef.Parameters)
                {
                    if (item.Name == identifier && ((item.Line < startLine) || (item.Column < start && item.Line == startLine)))
                    {
                        return true;
                    }
                }
                foreach (var item in CurrentFunctionDef.LocalVariables)
                {
                    if (item.Name == identifier && ((item.Line < startLine) || (item.Column < start && item.Line == startLine)))
                    {
                        return true;
                    }
                }
            }
            if (!isDefined)
            {
                isDefined = FindGlobalIdentifier(identifier);
            }
            return isDefined;
        }

        private bool FindGlobalIdentifier(string name)
        {
            foreach (var c in GlobalConsts)
                if (string.Equals(c.Name, name, System.StringComparison.OrdinalIgnoreCase))
                    return true;
            foreach (var c in GlobalVars)
                if (string.Equals(c.Name, name, System.StringComparison.OrdinalIgnoreCase))
                    return true;
            foreach (var c in GlobalFunctions)
                if (string.Equals(c.Name, name, System.StringComparison.OrdinalIgnoreCase))
                    return true;
            foreach (var res in currentParseResults)
            {
                foreach (var c in res.GlobalConstants)
                    if (string.Equals(c.Name, name, System.StringComparison.OrdinalIgnoreCase))
                        return true;
                foreach (var c in res.GlobalVariables)
                    if (string.Equals(c.Name, name, System.StringComparison.OrdinalIgnoreCase))
                        return true;
                foreach (var c in res.GlobalFunctions)
                    if (string.Equals(c.Name, name, System.StringComparison.OrdinalIgnoreCase))
                        return true;
                //foreach (var c in res.GlobalConstants)
                //    if (string.Equals(c.Name, name, System.StringComparison.OrdinalIgnoreCase))
                //        return true;
            }
            return false;
        }
        public override void EnterVarDecl([NotNull] DaedalusParser.VarDeclContext context)
        {
            var varDecls = context.varDecl();
            if (varDecls != null && varDecls.Length > 0)
            {
                var error = new RecognitionException(parser, parser.InputStream, context);
                error.Data.Add(SyntaxError.DataKey_ErrorCode, SyntaxErrorCodes.D0002_Split_Multiple_Var_Decl);
                parser.NotifyErrorListeners(varDecls[0].Start, error.Message, error);
            }
            base.EnterVarDecl(context);
        }
        private readonly StringBuilder _docCommentBuilder = new StringBuilder();
        private bool _docCommentBuilderCleaned = true;
        public override void EnterFunctionDef([NotNull] DaedalusParser.FunctionDefContext context)
        {
            var fd = context;

            var statements = fd.statementBlock()?.statement();
            var localVars = new List<Variable>();
            if (statements != null)
            {
                for (int i = 0; i < statements.Length; i++)
                {
                    for (int j = 0; j < statements[i].ChildCount; j++)
                    {
                        if (statements[i].children[j] is DaedalusParser.VarDeclContext varDecl)
                        {
                            var varType = varDecl.typeReference().GetText();
                            var varDecls = varDecl.varDecl();
                            for (int k = 0; k < varDecls.Length; k++)
                            {
                                var innerVal = varDecls[k].varValueDecl();
                                for (int v = 0; v < innerVal.Length; v++)
                                {
                                    localVars.Add(new Variable
                                    {
                                        Type = varDecls[k].typeReference().GetText(),
                                        Column = innerVal[v].Start.Column,
                                        Line = innerVal[v].Start.Line,
                                        Name = innerVal[v].nameNode().GetText(),
                                        Definition = new Defintion
                                        {
                                            Start = new DefinitionIndex(innerVal[v].Start.Line, innerVal[v].Start.Column),
                                            End = new DefinitionIndex(innerVal[v].Start.Line + innerVal[v].GetText().Length, innerVal[v].Stop.Column)
                                        }
                                    });
                                }
                            }
                            var varValues = varDecl.varValueDecl();
                            for (int k = 0; k < varValues.Length; k++)
                            {
                                localVars.Add(new Variable
                                {
                                    Type = varType,
                                    Column = varValues[k].Start.Column,
                                    Line = varValues[k].Start.Line,
                                    Name = varValues[k].nameNode().GetText(),
                                    Definition = new Defintion
                                    {
                                        Start = new DefinitionIndex(varValues[k].Start.Line, varValues[k].Start.Column),
                                        End = new DefinitionIndex(varValues[k].Start.Line + varValues[k].GetText().Length, varValues[k].Stop.Column)
                                    }
                                });
                            }
                        }
                        else if (statements[i].children[j] is DaedalusParser.ConstDefContext constDef)
                        {
                            var varType = constDef.typeReference().GetText();
                            var constValues = constDef.constValueDef();
                            for (int k = 0; k < constValues.Length; k++)
                            {
                                var innerVal = constValues[k].constValueAssignment().expressionBlock().expression();

                                localVars.Add(new Constant
                                {
                                    Type = varType,
                                    Column = constValues[k].Start.Column,
                                    Line = constValues[k].Start.Line,
                                    Name = constValues[k].nameNode().GetText(),
                                    Value = innerVal.GetText(),
                                    Definition = new Defintion
                                    {
                                        Start = new DefinitionIndex(constValues[k].Start.Line, constValues[k].Start.Column),
                                        End = new DefinitionIndex(constValues[k].Start.Line + constValues[k].GetText().Length, constValues[k].Stop.Column)
                                    }
                                });
                            }
                        }
                    }
                }
            }

            var parameters = new List<Variable>();
            var funcParameters = fd.parameterList()?.parameterDecl();
            if (funcParameters != null)
            {
                foreach (var pdef in funcParameters)
                {
                    parameters.Add(new Variable
                    {
                        Name = pdef.nameNode().GetText(),
                        Type = pdef.typeReference().GetText(),
                        Column = pdef.nameNode().Start.Column,
                        Line = pdef.nameNode().Start.Line,
                    });
                }
            }
            var summary = fd.symbolSummary();
            if (!_docCommentBuilderCleaned)
            {
                _docCommentBuilder.Clear();
                _docCommentBuilderCleaned = true;
            }
            if (summary?.Length > 0)
            {
                for (var i = 0; i < summary.Length; i++)
                {
                    var line = summary[i]?.GetText();
                    if (line == null) continue;
                    _docCommentBuilder.AppendLine(line.TrimStart('/', ' '));
                }
                _docCommentBuilderCleaned = false;
            }
            var fncNamenode = fd.nameNode();
            var fnc = new Function
            {
                Name = fncNamenode.GetText(),
                ReturnType = fd.typeReference().GetText(),
                Line = fncNamenode.Start.Line,
                Column = fncNamenode.Start.Column,
                Parameters = parameters,
                LocalVariables = localVars,
                Documentation = _docCommentBuilderCleaned ? "" : _docCommentBuilder.ToString(),
                Definition = new Defintion { Start = new DefinitionIndex(fd.Start.Line, fd.Start.Column), End = new DefinitionIndex(fd.Stop.Line, fd.Stop.Column) }
            };
            CurrentFunctionDef = fnc;
            GlobalFunctions.Add(fnc);
        }
        public override void ExitFunctionDef([NotNull] DaedalusParser.FunctionDefContext context)
        {
            CurrentFunctionDef = null;
        }
        private void AddGlobalsBlockDef(Antlr4.Runtime.ParserRuleContext ruleContext)
        {
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
