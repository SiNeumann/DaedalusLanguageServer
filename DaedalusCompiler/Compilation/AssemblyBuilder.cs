﻿using System;
using System.Collections.Generic;
using System.Linq;
using DaedalusCompiler.Dat;

namespace DaedalusCompiler.Compilation
{
    public class AssemblyBuilderSnapshot
    {
        public readonly BaseExecBlockContext ActiveExecBlock;
        public readonly bool IsInsideArgList;
        public readonly bool IsInsideAssignment;
        public readonly bool IsInsideIfCondition;
        public readonly bool IsInsideReturnStatement;
        public readonly DatSymbolType AssignmentType;
        public readonly FuncCallContext FuncCallCtx;

        public AssemblyBuilderSnapshot(AssemblyBuilder assemblyBuilder)
        {
            ActiveExecBlock = assemblyBuilder.ActiveExecBlock;
            IsInsideArgList = assemblyBuilder.IsInsideArgList;
            IsInsideAssignment = assemblyBuilder.IsInsideAssignment;
            IsInsideIfCondition = assemblyBuilder.IsInsideIfCondition;
            IsInsideReturnStatement = assemblyBuilder.IsInsideReturnStatement;
            AssignmentType = assemblyBuilder.AssignmentType;
            FuncCallCtx = assemblyBuilder.FuncCallCtx == null ? null : new FuncCallContext(assemblyBuilder.FuncCallCtx);
        }
    }

    public class AssemblyBuilder
    {
        public readonly List<BaseExecBlockContext> ExecBlocks;
        public BaseExecBlockContext ActiveExecBlock;
        public readonly ErrorContext ErrorContext;
        
        public readonly List<DatSymbol> Symbols;
        private readonly Dictionary<string, DatSymbol> _symbolsDict;
        
        private AssemblyBuilderContext _activeContext;
        private List<SymbolInstruction> _assignmentLeftSide;
        public FuncCallContext FuncCallCtx;

        
        private int _nextStringSymbolNumber;
        public bool IsInsideConstDef;
        public bool IsCurrentlyParsingExternals;

        public bool IsInsideArgList;
        public bool IsInsideAssignment;
        public bool IsInsideIfCondition;
        public bool IsInsideReturnStatement;
        public DatSymbolType AssignmentType;
        private int _nextSymbolIndex;
        private bool _verbose;

        public readonly List<CompilationMessage> Errors;
        
        public AssemblyBuilder(bool verbose = true)
        {
            ExecBlocks = new List<BaseExecBlockContext>();
            Symbols = new List<DatSymbol>();
            _symbolsDict = new Dictionary<string, DatSymbol>();
            _activeContext = null;
            ActiveExecBlock = null;
            _assignmentLeftSide = new List<SymbolInstruction>();
            FuncCallCtx = null;

            
            _nextStringSymbolNumber = 10000;
            IsInsideConstDef = false;
            IsCurrentlyParsingExternals = false;
            
            IsInsideArgList = false;
            IsInsideAssignment = false;
            IsInsideReturnStatement = false;
            AssignmentType = DatSymbolType.Undefined;
            _nextSymbolIndex = 0;
            _verbose = verbose;

            Errors = new List<CompilationMessage>();
            ErrorContext = new ErrorContext(this);
        }
        
        public AssemblyBuilderSnapshot MakeSnapshot()
        {
            return new AssemblyBuilderSnapshot(this);
        }

        public void LoadStateFromSnapshot(AssemblyBuilderSnapshot snapshot)
        {
            ActiveExecBlock = snapshot.ActiveExecBlock;
            
            IsInsideArgList = snapshot.IsInsideArgList;
            IsInsideAssignment = snapshot.IsInsideAssignment;
            IsInsideIfCondition = snapshot.IsInsideIfCondition;
            IsInsideReturnStatement = snapshot.IsInsideReturnStatement;
            AssignmentType = snapshot.AssignmentType;
            FuncCallCtx = snapshot.FuncCallCtx;
        }

        public List<DatSymbol> GetSymbols()
        {
            return Symbols;
        }
        
        public List<BaseExecBlockContext> GetExecBlocks()
        {
            return ExecBlocks;
        }

        public string NewStringSymbolName()
        {
            return $"{(char) 255}{_nextStringSymbolNumber++}";
        }

        public List<AssemblyElement> GetKeywordInstructions(string symbolName)
        {
            if (symbolName == "nofunc")
            {
                return new List<AssemblyElement> { new PushInt(-1) };
            }

            if (symbolName == "null")
            {
                DatSymbol symbol = ResolveSymbol($"{(char)255}instance_help");
                return new List<AssemblyElement> { new PushInstance(symbol) };
            }
            
            return new List<AssemblyElement>();
        }
        

        public DatSymbolReference GetDatSymbolReference(DaedalusParser.ReferenceContext referenceContext)
        {
            ErrorContext.Context = referenceContext;

            DaedalusParser.ReferenceAtomContext[] referenceAtoms = referenceContext.referenceAtom();
            DaedalusParser.ReferenceAtomContext objectPart = referenceAtoms[0];

            DatSymbolReference reference = new DatSymbolReference();

            DatSymbol symbol;

            try
            {
                symbol = GetReferenceAtomSymbol(referenceContext);
            }
            catch (UndeclaredIdentifierException)
            {
                Errors.Add(new UndeclaredIdentifierError(ErrorContext));
                return reference;
            }

            if (IsDottedReference(referenceContext))
            {
                DaedalusParser.ReferenceAtomContext attributePart = referenceAtoms[1];
                string attributeName = attributePart.nameNode().GetText();
                try
                {
                    reference.Attribute = ResolveAttribute(symbol, attributeName);
                }
                catch (AttributeNotFoundException)
                {
                    Errors.Add(new AttributeNotFoundError(ErrorContext));
                    return reference;
                }

                reference.Index = GetArrayIndex(attributePart);
            }
            else
            {
                reference.Index = GetArrayIndex(objectPart);
            }

            reference.Object = symbol;

            return reference;
        }

        public DatSymbol GetReferenceAtomSymbol(DaedalusParser.ReferenceContext referenceContext)
        {
            var referenceAtoms = referenceContext.referenceAtom();
            var referenceAtom = referenceAtoms[0];
            string symbolNameLower = referenceAtom.nameNode().GetText().ToLower();
            bool isSymbolNameSelfKeyword = symbolNameLower == "slf" || symbolNameLower == "self";


            if (ActiveExecBlock != null && isSymbolNameSelfKeyword)
            {

                bool isDottedReference = IsDottedReference(referenceContext);
                DatSymbolType activeSymbolType = ActiveExecBlock.GetSymbol().Type;

                if (
                    activeSymbolType == DatSymbolType.Instance
                    || (activeSymbolType == DatSymbolType.Prototype && isDottedReference)
                )
                {
                    return ActiveExecBlock.GetSymbol();
                }
            }

            return ResolveSymbol(referenceAtom.nameNode().GetText());
        }
        
        
        public int GetArrayIndex(DaedalusParser.ReferenceAtomContext context)
        {
            var indexContext = context.arrayIndex();
            
            
            int arrIndex = -1;
            if (indexContext != null)
            {
                if (!int.TryParse(indexContext.GetText(), out arrIndex))
                {
                    var constSymbol = ResolveSymbol(indexContext.GetText());
                    if (!constSymbol.Flags.HasFlag(DatSymbolFlag.Const) || constSymbol.Type != DatSymbolType.Int)
                    {
                        throw new Exception($"Expected integer constant: {indexContext.GetText()}");
                    }

                    arrIndex = (int) constSymbol.Content[0];
                }
            }

            return arrIndex;
        }
        
        public AssemblyInstruction PushSymbol(DatSymbol symbol, DatSymbolType? asType=null)
        {
            if (asType == DatSymbolType.Func || (asType == DatSymbolType.Int && symbol.Type != DatSymbolType.Int))
            {
                return new PushInt(symbol.Index);
            }

            if (symbol.Type == DatSymbolType.Instance || asType == DatSymbolType.Instance)  /* DatSymbolType.Class isn't possible */
            {
                return new PushInstance(symbol);
            }
            return new PushVar(symbol);
        }
        
        public AssemblyElement GetProperPushInstruction(DatSymbol symbol, int arrIndex)
        {
            BaseExecBlockContext activeBlock = ActiveExecBlock;
            
            if (arrIndex > 0)
            {
                return new PushArrayVar(symbol, arrIndex);
            }
            
            if (IsInsideArgList)
            {
                return PushSymbol(symbol, FuncCallCtx.GetParameterType());
            }
            
            if (IsInsideReturnStatement && activeBlock != null)
            {
                return PushSymbol(symbol, activeBlock.GetSymbol().ReturnType);
            }
            
            if (IsInsideAssignment)
            {
                return PushSymbol(symbol, AssignmentType);
            }
            
            if (IsInsideIfCondition)
            {
                return PushSymbol(symbol, DatSymbolType.Int);
            }

            return PushSymbol(symbol);
        }

        public bool IsDottedReference(DaedalusParser.ReferenceContext referenceContext)
        {
            DaedalusParser.ReferenceAtomContext[] nodes = referenceContext.referenceAtom();

            if (nodes.Length > 2)
            {
                throw new Exception("Too many nodes in reference.");
            }
            return nodes.Length == 2;
        }


        public List<AssemblyElement> GetDatSymbolReferenceInstructions(DaedalusParser.ReferenceContext referenceContext)
        {
            DatSymbolReference reference = GetDatSymbolReference(referenceContext);
            return GetDatSymbolReferenceInstructions(reference);
        }

        public List<AssemblyElement> GetDatSymbolReferenceInstructions(DatSymbolReference reference)
        {
            List<AssemblyElement> instructions = new List<AssemblyElement>();

            if (reference.HasAttribute())
            {
                bool isInsideExecBlock = ActiveExecBlock != null;
                bool isSymbolSelf = reference.Object == ActiveExecBlock?.GetSymbol(); // self.attribute, slf.attribute cases
                bool isSymbolPassedToInstanceParameter = IsInsideArgList && FuncCallCtx.GetParameterType() == DatSymbolType.Instance;
                bool isSymbolPassedToFuncParameter = IsInsideArgList && FuncCallCtx.GetParameterType() == DatSymbolType.Func;
                bool isInsideFuncAssignment = IsInsideAssignment && AssignmentType == DatSymbolType.Func;

                if (isInsideExecBlock
                    && !isSymbolSelf
                    && !isInsideFuncAssignment
                    && !(isSymbolPassedToInstanceParameter || isSymbolPassedToFuncParameter)
                )
                {
                    instructions.Add(new SetInstance(reference.Object));
                }

                instructions.Add(GetProperPushInstruction(reference.Attribute, reference.Index));
            }
            else
            {
                instructions.Add(GetProperPushInstruction(reference.Object, reference.Index));
            }
            return instructions;

        }

        public bool IsContextInsideExecBlock()
        {
            return ActiveExecBlock != null;
        }

        public void AddInstruction(AssemblyInstruction instruction)
        {
            AddInstructions(new List<AssemblyElement>() {instruction});
        }

        public void AddInstructions(List<AssemblyElement> instructions)
        {
            _activeContext.AddInstructions(instructions);
        }

        public void SharedBlockStart(List<DatSymbol> symbols)
        {
            ActiveExecBlock = new SharedExecBlockContext(symbols);
            _activeContext = ActiveExecBlock;
            ExecBlocks.Add(ActiveExecBlock);
        }
        
        public void ExecBlockStart(DatSymbol symbol, ExecBlockType blockType)
        {
            switch (blockType)
            {
                case ExecBlockType.Function:
                    ActiveExecBlock = new FunctionBlockContext(symbol);
                    break;
                case ExecBlockType.Instance:
                    ActiveExecBlock = new InstanceBlockContext(symbol);
                    break;
                case ExecBlockType.Prototype:
                    ActiveExecBlock = new PrototypeBlockContext(symbol);
                    break;
                default:
                    throw new NotImplementedException();
            }

            _activeContext = ActiveExecBlock;
            ExecBlocks.Add(ActiveExecBlock);

        }

        public void ExecBlockEnd()
        {
            ActiveExecBlock = null;
            ActiveContextEnd();
        }

        public void AssignmentStart(SymbolInstruction[] instructions)
        {
            _assignmentLeftSide.AddRange(instructions);
        }

        public void AssignmentEnd(string assignmentOperator)
        {
            //TODO check if there are any possibilities of assignmentLeftSide longer than 2 instructions?
            var operationType = _assignmentLeftSide.Last().Symbol.Type; 
            var assignmentInstruction =
                AssemblyBuilderHelpers.GetInstructionForOperator(assignmentOperator, true, operationType);

            if (!IsInsideConstDef)
            {
                AddInstructions(new List<AssemblyElement>(_assignmentLeftSide));   
            }
            _assignmentLeftSide = new List<SymbolInstruction>();
            AddInstruction(assignmentInstruction);
        }

        public void ExitOperator(string operatorText, bool twoArg=true)
        {
            var instruction = AssemblyBuilderHelpers.GetInstructionForOperator(operatorText, twoArg);
            _activeContext.SetEndInstruction(instruction);
        }

       

        public void FuncCallStart(string funcName)
        {
            DatSymbol symbol = GetSymbolByName(funcName);
            
            List<DatSymbolType> parametersTypes = new List<DatSymbolType>();
            for (int i = 1; i <= symbol.ParametersCount; ++i)
            {
                DatSymbol parameter = Symbols[symbol.Index + i];
                parametersTypes.Add(parameter.Type);
            }
            
            AssemblyInstruction instruction;
            if (symbol.Flags.HasFlag(DatSymbolFlag.External))
            {
                instruction = new CallExternal(symbol);
            }
            else
            {
                instruction = new Call(symbol);
            }

            FuncCallCtx = new FuncCallContext(_activeContext, FuncCallCtx, parametersTypes, symbol);
            _activeContext = FuncCallCtx;
            _activeContext.SetEndInstruction(instruction);
            
            IsInsideArgList = true;
        }

        public void FuncCallEnd()
        {
            List<AssemblyElement> instructions = _activeContext.GetInstructions();
            FuncCallCtx = FuncCallCtx.OuterCall;
            _activeContext = _activeContext.Parent;
            AddInstructions(instructions);
            
            if (FuncCallCtx == null)
            {
                IsInsideArgList = false;
            }
        }
        
        
        
        public void FuncCallArgStart()
        {
            FuncCallCtx.ArgIndex++;
            _activeContext = new BlockContext(_activeContext);
        }

        public void FuncCallArgEnd()
        {
            ActiveContextEnd();
        }

        
        
        
        public void ExpressionStart()
        {
            _activeContext = new ExpressionContext(_activeContext);
        }

        public void ExpressionEnd()
        {
            ActiveContextEnd();
        }

        
        public void IfBlockStatementStart()
        {
            _activeContext = new IfBlockStatementContext(_activeContext);
        }

        public void IfBlockStatementEnd()
        {
            ActiveContextEnd();
        }


        public void IfBlockStart()
        {
            _activeContext = new IfBlockContext(_activeContext);
        }

        public void IfBlockEnd()
        {
            ActiveContextEnd();
        }
        
        
        public void ElseIfBlockStart()
        {
            _activeContext = new ElseIfBlockContext(_activeContext);
        }

        public void ElseIfBlockEnd()
        {
            ActiveContextEnd();
        }

        
        public void ElseBlockStart()
        {
            _activeContext = new ElseBlockContext(_activeContext);
        }

        public void ElseBlockEnd()
        {
            ActiveContextEnd();
        }


        public void ActiveContextEnd()
        {
            _activeContext.Parent?.FetchInstructions(_activeContext);
            _activeContext = _activeContext.Parent;
        }
        
        public void AddSymbol(DatSymbol symbol)
        {
            if (IsCurrentlyParsingExternals)
            {
                if (symbol.Type == DatSymbolType.Func && symbol.Flags.HasFlag(DatSymbolFlag.Const))
                {
                    symbol.Flags |= DatSymbolFlag.External;
                }

                if (symbol.Name == "instance_help")
                {
                    symbol.Name = $"{(char) 255}INSTANCE_HELP";
                }
            }

            if (!symbol.Name.StartsWith($"{(char) 255}"))
            {
                symbol.Name = symbol.Name.ToUpper();
            }
            
            _symbolsDict[symbol.Name.ToUpper()] = symbol;
            
            Symbols.Add(symbol);
            symbol.Index = _nextSymbolIndex;
            _nextSymbolIndex++;
        }

        public DatSymbol ResolveAttribute(DatSymbol symbol, string attributeName)
        {
            string attributePath = $"{symbol.Name}.{attributeName}";

            DatSymbol attributeSymbol = null;
            
            while (symbol != null)
            {
                attributeSymbol = _symbolsDict.GetValueOrDefault(attributePath.ToUpper(), null);
                
                if (attributeSymbol == null)
                {
                    if (symbol.ParentIndex == DatSymbol.NULL_INDEX)
                    {
                        break;
                    }

                    symbol = Symbols[symbol.ParentIndex];
                    attributePath = $"{symbol.Name}.{attributeName}";
                    
                    if (symbol.Type == DatSymbolType.Prototype && symbol.ParentIndex != DatSymbol.NULL_INDEX)
                    {
                        symbol = Symbols[symbol.ParentIndex];
                        attributePath = $"{symbol.Name}.{attributeName}";
                    }
                    
                }
                else
                {
                    break;
                }
            }
            
            if (attributeSymbol == null)
            {
                throw new AttributeNotFoundException();
            }

            return attributeSymbol;
            
        }
        
        public DatSymbol ResolveSymbol(string symbolName)
        {
            DatSymbol symbol;

            if (ActiveExecBlock != null && !symbolName.Contains("."))
            {
                DatSymbol currentExecBlockSymbol = ActiveExecBlock.GetSymbol();

                while (currentExecBlockSymbol != null)
                {
                    var targetSymbolName = $"{currentExecBlockSymbol.Name}.{symbolName}";
                    symbol = _symbolsDict.GetValueOrDefault(targetSymbolName.ToUpper(), null);
                    
                    
                    if (symbol == null)
                    {
                        if (currentExecBlockSymbol.ParentIndex == DatSymbol.NULL_INDEX)
                        {
                            break;
                        }

                        currentExecBlockSymbol = Symbols[currentExecBlockSymbol.ParentIndex];
                    }
                    else
                    {
                        return symbol;
                    }
                }
            }

            symbol = _symbolsDict.GetValueOrDefault(symbolName.ToUpper(), null);
            
            if (symbol == null)
            {
                throw new UndeclaredIdentifierException();
            }

            return symbol;
        }

        public DatSymbol GetSymbolByName(string symbolName)
        {
            try
            {
                return _symbolsDict[symbolName.ToUpper()];
            }
            catch (KeyNotFoundException)
            {
                Errors.Add(new UndeclaredIdentifierError(ErrorContext));
                return DatSymbolReference.UndeclaredSymbol;
            }
        }

        public bool IsInsideOneArgOperatorsEvaluationMode()
        {
            bool isInsideFloatAssignment = IsInsideAssignment && AssignmentType == DatSymbolType.Float;
            bool isInsideFloatArgument = IsInsideArgList && FuncCallCtx.GetParameterType() == DatSymbolType.Float;
            return IsInsideConstDef || isInsideFloatAssignment || isInsideFloatArgument;
        }

        public string GetAssembler()
        {
            return new AssemblyBuilderTraverser().GetAssembler(ExecBlocks);
        }

        public void SaveToDat(string path)
        {
            DatBuilder datBuilder = new DatBuilder(this);
            DatFile datFile = datBuilder.GetDatFile();
            datFile.Save(path);
        }
        
        public void Finish()
        {
            int counter = 0;
            int maxCounter = ExecBlocks.Count;
            foreach (BaseExecBlockContext execBlock in ExecBlocks)
            {
                if(_verbose) Console.Write($"\r{++counter}/{maxCounter} lazy references resolved");
                for (int i = 0; i < execBlock.Body.Count; ++i)
                {
                    AssemblyElement element = execBlock.Body[i];
                    if (element is LazyReferenceAtomInstructions nodeInstructions)
                    {
                        LoadStateFromSnapshot(nodeInstructions.AssemblyBuilderSnapshot);
                        List<AssemblyElement> instructions = GetDatSymbolReferenceInstructions(nodeInstructions.ReferenceContext);
                        execBlock.Body.RemoveAt(i);
                        execBlock.Body.InsertRange(i, instructions);
                    }
                    
                    if (element is PushVar pushVar && pushVar.Symbol.IsStringLiteralSymbol())
                    {
                        pushVar.Symbol.Name = NewStringSymbolName();
                        AddSymbol(pushVar.Symbol);
                    }
                }
            }
            if(_verbose) Console.WriteLine("");
        }
    }
}
