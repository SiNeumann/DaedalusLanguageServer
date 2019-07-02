// Generated from b:\Source\DaedalusLanguageServer\Parser\Daedalus.g4 by ANTLR 4.7.1
import org.antlr.v4.runtime.atn.*;
import org.antlr.v4.runtime.dfa.DFA;
import org.antlr.v4.runtime.*;
import org.antlr.v4.runtime.misc.*;
import org.antlr.v4.runtime.tree.*;
import java.util.List;
import java.util.Iterator;
import java.util.ArrayList;

@SuppressWarnings({"all", "warnings", "unchecked", "unused", "cast"})
public class DaedalusParser extends Parser {
	static { RuntimeMetaData.checkVersion("4.7.1", RuntimeMetaData.VERSION); }

	protected static final DFA[] _decisionToDFA;
	protected static final PredictionContextCache _sharedContextCache =
		new PredictionContextCache();
	public static final int
		T__0=1, T__1=2, T__2=3, T__3=4, T__4=5, T__5=6, T__6=7, T__7=8, T__8=9, 
		T__9=10, T__10=11, T__11=12, T__12=13, T__13=14, T__14=15, T__15=16, T__16=17, 
		T__17=18, T__18=19, T__19=20, T__20=21, T__21=22, T__22=23, T__23=24, 
		T__24=25, T__25=26, T__26=27, T__27=28, T__28=29, T__29=30, T__30=31, 
		T__31=32, T__32=33, Const=34, Var=35, If=36, Int=37, Else=38, Func=39, 
		String=40, Class=41, Void=42, Return=43, Float=44, Prototype=45, Instance=46, 
		Null=47, NoFunc=48, Identifier=49, IntegerLiteral=50, FloatLiteral=51, 
		StringLiteral=52, Whitespace=53, Newline=54, BlockComment=55, LineComment=56;
	public static final int
		RULE_daedalusFile = 0, RULE_blockDef = 1, RULE_inlineDef = 2, RULE_functionDef = 3, 
		RULE_constDef = 4, RULE_classDef = 5, RULE_prototypeDef = 6, RULE_instanceDef = 7, 
		RULE_instanceDecl = 8, RULE_varDecl = 9, RULE_constArrayDef = 10, RULE_constArrayAssignment = 11, 
		RULE_constValueDef = 12, RULE_constValueAssignment = 13, RULE_varArrayDecl = 14, 
		RULE_varValueDecl = 15, RULE_parameterList = 16, RULE_parameterDecl = 17, 
		RULE_statementBlock = 18, RULE_statement = 19, RULE_funcCall = 20, RULE_assignment = 21, 
		RULE_ifCondition = 22, RULE_elseBlock = 23, RULE_elseIfBlock = 24, RULE_ifBlock = 25, 
		RULE_ifBlockStatement = 26, RULE_returnStatement = 27, RULE_funcArgExpression = 28, 
		RULE_expressionBlock = 29, RULE_expression = 30, RULE_arrayIndex = 31, 
		RULE_arraySize = 32, RULE_value = 33, RULE_referenceAtom = 34, RULE_reference = 35, 
		RULE_typeReference = 36, RULE_anyIdentifier = 37, RULE_nameNode = 38, 
		RULE_parentReference = 39, RULE_assignmentOperator = 40, RULE_addOperator = 41, 
		RULE_bitMoveOperator = 42, RULE_compOperator = 43, RULE_eqOperator = 44, 
		RULE_oneArgOperator = 45, RULE_multOperator = 46, RULE_binAndOperator = 47, 
		RULE_binOrOperator = 48, RULE_logAndOperator = 49, RULE_logOrOperator = 50;
	public static final String[] ruleNames = {
		"daedalusFile", "blockDef", "inlineDef", "functionDef", "constDef", "classDef", 
		"prototypeDef", "instanceDef", "instanceDecl", "varDecl", "constArrayDef", 
		"constArrayAssignment", "constValueDef", "constValueAssignment", "varArrayDecl", 
		"varValueDecl", "parameterList", "parameterDecl", "statementBlock", "statement", 
		"funcCall", "assignment", "ifCondition", "elseBlock", "elseIfBlock", "ifBlock", 
		"ifBlockStatement", "returnStatement", "funcArgExpression", "expressionBlock", 
		"expression", "arrayIndex", "arraySize", "value", "referenceAtom", "reference", 
		"typeReference", "anyIdentifier", "nameNode", "parentReference", "assignmentOperator", 
		"addOperator", "bitMoveOperator", "compOperator", "eqOperator", "oneArgOperator", 
		"multOperator", "binAndOperator", "binOrOperator", "logAndOperator", "logOrOperator"
	};

	private static final String[] _LITERAL_NAMES = {
		null, "';'", "','", "'{'", "'}'", "'('", "')'", "'['", "']'", "'='", "'.'", 
		"'+='", "'-='", "'*='", "'/='", "'+'", "'-'", "'<<'", "'>>'", "'<'", "'>'", 
		"'<='", "'>='", "'=='", "'!='", "'!'", "'~'", "'*'", "'/'", "'%'", "'&'", 
		"'|'", "'&&'", "'||'"
	};
	private static final String[] _SYMBOLIC_NAMES = {
		null, null, null, null, null, null, null, null, null, null, null, null, 
		null, null, null, null, null, null, null, null, null, null, null, null, 
		null, null, null, null, null, null, null, null, null, null, "Const", "Var", 
		"If", "Int", "Else", "Func", "String", "Class", "Void", "Return", "Float", 
		"Prototype", "Instance", "Null", "NoFunc", "Identifier", "IntegerLiteral", 
		"FloatLiteral", "StringLiteral", "Whitespace", "Newline", "BlockComment", 
		"LineComment"
	};
	public static final Vocabulary VOCABULARY = new VocabularyImpl(_LITERAL_NAMES, _SYMBOLIC_NAMES);

	/**
	 * @deprecated Use {@link #VOCABULARY} instead.
	 */
	@Deprecated
	public static final String[] tokenNames;
	static {
		tokenNames = new String[_SYMBOLIC_NAMES.length];
		for (int i = 0; i < tokenNames.length; i++) {
			tokenNames[i] = VOCABULARY.getLiteralName(i);
			if (tokenNames[i] == null) {
				tokenNames[i] = VOCABULARY.getSymbolicName(i);
			}

			if (tokenNames[i] == null) {
				tokenNames[i] = "<INVALID>";
			}
		}
	}

	@Override
	@Deprecated
	public String[] getTokenNames() {
		return tokenNames;
	}

	@Override

	public Vocabulary getVocabulary() {
		return VOCABULARY;
	}

	@Override
	public String getGrammarFileName() { return "Daedalus.g4"; }

	@Override
	public String[] getRuleNames() { return ruleNames; }

	@Override
	public String getSerializedATN() { return _serializedATN; }

	@Override
	public ATN getATN() { return _ATN; }

	public DaedalusParser(TokenStream input) {
		super(input);
		_interp = new ParserATNSimulator(this,_ATN,_decisionToDFA,_sharedContextCache);
	}
	public static class DaedalusFileContext extends ParserRuleContext {
		public TerminalNode EOF() { return getToken(DaedalusParser.EOF, 0); }
		public List<BlockDefContext> blockDef() {
			return getRuleContexts(BlockDefContext.class);
		}
		public BlockDefContext blockDef(int i) {
			return getRuleContext(BlockDefContext.class,i);
		}
		public List<InlineDefContext> inlineDef() {
			return getRuleContexts(InlineDefContext.class);
		}
		public InlineDefContext inlineDef(int i) {
			return getRuleContext(InlineDefContext.class,i);
		}
		public DaedalusFileContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_daedalusFile; }
	}

	public final DaedalusFileContext daedalusFile() throws RecognitionException {
		DaedalusFileContext _localctx = new DaedalusFileContext(_ctx, getState());
		enterRule(_localctx, 0, RULE_daedalusFile);
		try {
			int _alt;
			enterOuterAlt(_localctx, 1);
			{
			setState(106);
			_errHandler.sync(this);
			_alt = getInterpreter().adaptivePredict(_input,1,_ctx);
			while ( _alt!=1 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1+1 ) {
					{
					setState(104);
					_errHandler.sync(this);
					switch ( getInterpreter().adaptivePredict(_input,0,_ctx) ) {
					case 1:
						{
						setState(102);
						blockDef();
						}
						break;
					case 2:
						{
						setState(103);
						inlineDef();
						}
						break;
					}
					} 
				}
				setState(108);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,1,_ctx);
			}
			setState(109);
			match(EOF);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class BlockDefContext extends ParserRuleContext {
		public FunctionDefContext functionDef() {
			return getRuleContext(FunctionDefContext.class,0);
		}
		public ClassDefContext classDef() {
			return getRuleContext(ClassDefContext.class,0);
		}
		public PrototypeDefContext prototypeDef() {
			return getRuleContext(PrototypeDefContext.class,0);
		}
		public InstanceDefContext instanceDef() {
			return getRuleContext(InstanceDefContext.class,0);
		}
		public BlockDefContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_blockDef; }
	}

	public final BlockDefContext blockDef() throws RecognitionException {
		BlockDefContext _localctx = new BlockDefContext(_ctx, getState());
		enterRule(_localctx, 2, RULE_blockDef);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(115);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case Func:
				{
				setState(111);
				functionDef();
				}
				break;
			case Class:
				{
				setState(112);
				classDef();
				}
				break;
			case Prototype:
				{
				setState(113);
				prototypeDef();
				}
				break;
			case Instance:
				{
				setState(114);
				instanceDef();
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
			setState(118);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==T__0) {
				{
				setState(117);
				match(T__0);
				}
			}

			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class InlineDefContext extends ParserRuleContext {
		public ConstDefContext constDef() {
			return getRuleContext(ConstDefContext.class,0);
		}
		public VarDeclContext varDecl() {
			return getRuleContext(VarDeclContext.class,0);
		}
		public InstanceDeclContext instanceDecl() {
			return getRuleContext(InstanceDeclContext.class,0);
		}
		public InlineDefContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_inlineDef; }
	}

	public final InlineDefContext inlineDef() throws RecognitionException {
		InlineDefContext _localctx = new InlineDefContext(_ctx, getState());
		enterRule(_localctx, 4, RULE_inlineDef);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(123);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case Const:
				{
				setState(120);
				constDef();
				}
				break;
			case Var:
				{
				setState(121);
				varDecl();
				}
				break;
			case Instance:
				{
				setState(122);
				instanceDecl();
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
			setState(125);
			match(T__0);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class FunctionDefContext extends ParserRuleContext {
		public TerminalNode Func() { return getToken(DaedalusParser.Func, 0); }
		public TypeReferenceContext typeReference() {
			return getRuleContext(TypeReferenceContext.class,0);
		}
		public NameNodeContext nameNode() {
			return getRuleContext(NameNodeContext.class,0);
		}
		public ParameterListContext parameterList() {
			return getRuleContext(ParameterListContext.class,0);
		}
		public StatementBlockContext statementBlock() {
			return getRuleContext(StatementBlockContext.class,0);
		}
		public FunctionDefContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_functionDef; }
	}

	public final FunctionDefContext functionDef() throws RecognitionException {
		FunctionDefContext _localctx = new FunctionDefContext(_ctx, getState());
		enterRule(_localctx, 6, RULE_functionDef);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(127);
			match(Func);
			setState(128);
			typeReference();
			setState(129);
			nameNode();
			setState(130);
			parameterList();
			setState(131);
			statementBlock();
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class ConstDefContext extends ParserRuleContext {
		public TerminalNode Const() { return getToken(DaedalusParser.Const, 0); }
		public TypeReferenceContext typeReference() {
			return getRuleContext(TypeReferenceContext.class,0);
		}
		public List<ConstValueDefContext> constValueDef() {
			return getRuleContexts(ConstValueDefContext.class);
		}
		public ConstValueDefContext constValueDef(int i) {
			return getRuleContext(ConstValueDefContext.class,i);
		}
		public List<ConstArrayDefContext> constArrayDef() {
			return getRuleContexts(ConstArrayDefContext.class);
		}
		public ConstArrayDefContext constArrayDef(int i) {
			return getRuleContext(ConstArrayDefContext.class,i);
		}
		public ConstDefContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_constDef; }
	}

	public final ConstDefContext constDef() throws RecognitionException {
		ConstDefContext _localctx = new ConstDefContext(_ctx, getState());
		enterRule(_localctx, 8, RULE_constDef);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(133);
			match(Const);
			setState(134);
			typeReference();
			setState(137);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,5,_ctx) ) {
			case 1:
				{
				setState(135);
				constValueDef();
				}
				break;
			case 2:
				{
				setState(136);
				constArrayDef();
				}
				break;
			}
			setState(146);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==T__1) {
				{
				{
				setState(139);
				match(T__1);
				setState(142);
				_errHandler.sync(this);
				switch ( getInterpreter().adaptivePredict(_input,6,_ctx) ) {
				case 1:
					{
					setState(140);
					constValueDef();
					}
					break;
				case 2:
					{
					setState(141);
					constArrayDef();
					}
					break;
				}
				}
				}
				setState(148);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class ClassDefContext extends ParserRuleContext {
		public TerminalNode Class() { return getToken(DaedalusParser.Class, 0); }
		public NameNodeContext nameNode() {
			return getRuleContext(NameNodeContext.class,0);
		}
		public List<VarDeclContext> varDecl() {
			return getRuleContexts(VarDeclContext.class);
		}
		public VarDeclContext varDecl(int i) {
			return getRuleContext(VarDeclContext.class,i);
		}
		public ClassDefContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_classDef; }
	}

	public final ClassDefContext classDef() throws RecognitionException {
		ClassDefContext _localctx = new ClassDefContext(_ctx, getState());
		enterRule(_localctx, 10, RULE_classDef);
		try {
			int _alt;
			enterOuterAlt(_localctx, 1);
			{
			setState(149);
			match(Class);
			setState(150);
			nameNode();
			setState(151);
			match(T__2);
			setState(157);
			_errHandler.sync(this);
			_alt = getInterpreter().adaptivePredict(_input,8,_ctx);
			while ( _alt!=1 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1+1 ) {
					{
					{
					setState(152);
					varDecl();
					setState(153);
					match(T__0);
					}
					} 
				}
				setState(159);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,8,_ctx);
			}
			setState(160);
			match(T__3);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class PrototypeDefContext extends ParserRuleContext {
		public TerminalNode Prototype() { return getToken(DaedalusParser.Prototype, 0); }
		public NameNodeContext nameNode() {
			return getRuleContext(NameNodeContext.class,0);
		}
		public ParentReferenceContext parentReference() {
			return getRuleContext(ParentReferenceContext.class,0);
		}
		public StatementBlockContext statementBlock() {
			return getRuleContext(StatementBlockContext.class,0);
		}
		public PrototypeDefContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_prototypeDef; }
	}

	public final PrototypeDefContext prototypeDef() throws RecognitionException {
		PrototypeDefContext _localctx = new PrototypeDefContext(_ctx, getState());
		enterRule(_localctx, 12, RULE_prototypeDef);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(162);
			match(Prototype);
			setState(163);
			nameNode();
			setState(164);
			match(T__4);
			setState(165);
			parentReference();
			setState(166);
			match(T__5);
			setState(167);
			statementBlock();
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class InstanceDefContext extends ParserRuleContext {
		public TerminalNode Instance() { return getToken(DaedalusParser.Instance, 0); }
		public NameNodeContext nameNode() {
			return getRuleContext(NameNodeContext.class,0);
		}
		public ParentReferenceContext parentReference() {
			return getRuleContext(ParentReferenceContext.class,0);
		}
		public StatementBlockContext statementBlock() {
			return getRuleContext(StatementBlockContext.class,0);
		}
		public InstanceDefContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_instanceDef; }
	}

	public final InstanceDefContext instanceDef() throws RecognitionException {
		InstanceDefContext _localctx = new InstanceDefContext(_ctx, getState());
		enterRule(_localctx, 14, RULE_instanceDef);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(169);
			match(Instance);
			setState(170);
			nameNode();
			setState(171);
			match(T__4);
			setState(172);
			parentReference();
			setState(173);
			match(T__5);
			setState(174);
			statementBlock();
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class InstanceDeclContext extends ParserRuleContext {
		public TerminalNode Instance() { return getToken(DaedalusParser.Instance, 0); }
		public List<NameNodeContext> nameNode() {
			return getRuleContexts(NameNodeContext.class);
		}
		public NameNodeContext nameNode(int i) {
			return getRuleContext(NameNodeContext.class,i);
		}
		public ParentReferenceContext parentReference() {
			return getRuleContext(ParentReferenceContext.class,0);
		}
		public InstanceDeclContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_instanceDecl; }
	}

	public final InstanceDeclContext instanceDecl() throws RecognitionException {
		InstanceDeclContext _localctx = new InstanceDeclContext(_ctx, getState());
		enterRule(_localctx, 16, RULE_instanceDecl);
		try {
			int _alt;
			enterOuterAlt(_localctx, 1);
			{
			setState(176);
			match(Instance);
			setState(177);
			nameNode();
			setState(182);
			_errHandler.sync(this);
			_alt = getInterpreter().adaptivePredict(_input,9,_ctx);
			while ( _alt!=1 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1+1 ) {
					{
					{
					setState(178);
					match(T__1);
					setState(179);
					nameNode();
					}
					} 
				}
				setState(184);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,9,_ctx);
			}
			setState(185);
			match(T__4);
			setState(186);
			parentReference();
			setState(187);
			match(T__5);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class VarDeclContext extends ParserRuleContext {
		public TerminalNode Var() { return getToken(DaedalusParser.Var, 0); }
		public TypeReferenceContext typeReference() {
			return getRuleContext(TypeReferenceContext.class,0);
		}
		public List<VarValueDeclContext> varValueDecl() {
			return getRuleContexts(VarValueDeclContext.class);
		}
		public VarValueDeclContext varValueDecl(int i) {
			return getRuleContext(VarValueDeclContext.class,i);
		}
		public List<VarArrayDeclContext> varArrayDecl() {
			return getRuleContexts(VarArrayDeclContext.class);
		}
		public VarArrayDeclContext varArrayDecl(int i) {
			return getRuleContext(VarArrayDeclContext.class,i);
		}
		public List<VarDeclContext> varDecl() {
			return getRuleContexts(VarDeclContext.class);
		}
		public VarDeclContext varDecl(int i) {
			return getRuleContext(VarDeclContext.class,i);
		}
		public VarDeclContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_varDecl; }
	}

	public final VarDeclContext varDecl() throws RecognitionException {
		VarDeclContext _localctx = new VarDeclContext(_ctx, getState());
		enterRule(_localctx, 18, RULE_varDecl);
		try {
			int _alt;
			enterOuterAlt(_localctx, 1);
			{
			setState(189);
			match(Var);
			setState(190);
			typeReference();
			setState(193);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,10,_ctx) ) {
			case 1:
				{
				setState(191);
				varValueDecl();
				}
				break;
			case 2:
				{
				setState(192);
				varArrayDecl();
				}
				break;
			}
			setState(203);
			_errHandler.sync(this);
			_alt = getInterpreter().adaptivePredict(_input,12,_ctx);
			while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1 ) {
					{
					{
					setState(195);
					match(T__1);
					setState(199);
					_errHandler.sync(this);
					switch ( getInterpreter().adaptivePredict(_input,11,_ctx) ) {
					case 1:
						{
						setState(196);
						varDecl();
						}
						break;
					case 2:
						{
						setState(197);
						varValueDecl();
						}
						break;
					case 3:
						{
						setState(198);
						varArrayDecl();
						}
						break;
					}
					}
					} 
				}
				setState(205);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,12,_ctx);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class ConstArrayDefContext extends ParserRuleContext {
		public NameNodeContext nameNode() {
			return getRuleContext(NameNodeContext.class,0);
		}
		public ArraySizeContext arraySize() {
			return getRuleContext(ArraySizeContext.class,0);
		}
		public ConstArrayAssignmentContext constArrayAssignment() {
			return getRuleContext(ConstArrayAssignmentContext.class,0);
		}
		public ConstArrayDefContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_constArrayDef; }
	}

	public final ConstArrayDefContext constArrayDef() throws RecognitionException {
		ConstArrayDefContext _localctx = new ConstArrayDefContext(_ctx, getState());
		enterRule(_localctx, 20, RULE_constArrayDef);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(206);
			nameNode();
			setState(207);
			match(T__6);
			setState(208);
			arraySize();
			setState(209);
			match(T__7);
			setState(210);
			constArrayAssignment();
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class ConstArrayAssignmentContext extends ParserRuleContext {
		public List<ExpressionBlockContext> expressionBlock() {
			return getRuleContexts(ExpressionBlockContext.class);
		}
		public ExpressionBlockContext expressionBlock(int i) {
			return getRuleContext(ExpressionBlockContext.class,i);
		}
		public ConstArrayAssignmentContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_constArrayAssignment; }
	}

	public final ConstArrayAssignmentContext constArrayAssignment() throws RecognitionException {
		ConstArrayAssignmentContext _localctx = new ConstArrayAssignmentContext(_ctx, getState());
		enterRule(_localctx, 22, RULE_constArrayAssignment);
		try {
			int _alt;
			enterOuterAlt(_localctx, 1);
			{
			setState(212);
			match(T__8);
			setState(213);
			match(T__2);
			{
			setState(214);
			expressionBlock();
			setState(219);
			_errHandler.sync(this);
			_alt = getInterpreter().adaptivePredict(_input,13,_ctx);
			while ( _alt!=1 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1+1 ) {
					{
					{
					setState(215);
					match(T__1);
					setState(216);
					expressionBlock();
					}
					} 
				}
				setState(221);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,13,_ctx);
			}
			}
			setState(222);
			match(T__3);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class ConstValueDefContext extends ParserRuleContext {
		public NameNodeContext nameNode() {
			return getRuleContext(NameNodeContext.class,0);
		}
		public ConstValueAssignmentContext constValueAssignment() {
			return getRuleContext(ConstValueAssignmentContext.class,0);
		}
		public ConstValueDefContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_constValueDef; }
	}

	public final ConstValueDefContext constValueDef() throws RecognitionException {
		ConstValueDefContext _localctx = new ConstValueDefContext(_ctx, getState());
		enterRule(_localctx, 24, RULE_constValueDef);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(224);
			nameNode();
			setState(225);
			constValueAssignment();
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class ConstValueAssignmentContext extends ParserRuleContext {
		public ExpressionBlockContext expressionBlock() {
			return getRuleContext(ExpressionBlockContext.class,0);
		}
		public ConstValueAssignmentContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_constValueAssignment; }
	}

	public final ConstValueAssignmentContext constValueAssignment() throws RecognitionException {
		ConstValueAssignmentContext _localctx = new ConstValueAssignmentContext(_ctx, getState());
		enterRule(_localctx, 26, RULE_constValueAssignment);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(227);
			match(T__8);
			setState(228);
			expressionBlock();
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class VarArrayDeclContext extends ParserRuleContext {
		public NameNodeContext nameNode() {
			return getRuleContext(NameNodeContext.class,0);
		}
		public ArraySizeContext arraySize() {
			return getRuleContext(ArraySizeContext.class,0);
		}
		public VarArrayDeclContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_varArrayDecl; }
	}

	public final VarArrayDeclContext varArrayDecl() throws RecognitionException {
		VarArrayDeclContext _localctx = new VarArrayDeclContext(_ctx, getState());
		enterRule(_localctx, 28, RULE_varArrayDecl);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(230);
			nameNode();
			setState(231);
			match(T__6);
			setState(232);
			arraySize();
			setState(233);
			match(T__7);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class VarValueDeclContext extends ParserRuleContext {
		public NameNodeContext nameNode() {
			return getRuleContext(NameNodeContext.class,0);
		}
		public VarValueDeclContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_varValueDecl; }
	}

	public final VarValueDeclContext varValueDecl() throws RecognitionException {
		VarValueDeclContext _localctx = new VarValueDeclContext(_ctx, getState());
		enterRule(_localctx, 30, RULE_varValueDecl);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(235);
			nameNode();
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class ParameterListContext extends ParserRuleContext {
		public List<ParameterDeclContext> parameterDecl() {
			return getRuleContexts(ParameterDeclContext.class);
		}
		public ParameterDeclContext parameterDecl(int i) {
			return getRuleContext(ParameterDeclContext.class,i);
		}
		public ParameterListContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_parameterList; }
	}

	public final ParameterListContext parameterList() throws RecognitionException {
		ParameterListContext _localctx = new ParameterListContext(_ctx, getState());
		enterRule(_localctx, 32, RULE_parameterList);
		int _la;
		try {
			int _alt;
			enterOuterAlt(_localctx, 1);
			{
			setState(237);
			match(T__4);
			setState(246);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==Var) {
				{
				setState(238);
				parameterDecl();
				setState(243);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,14,_ctx);
				while ( _alt!=1 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
					if ( _alt==1+1 ) {
						{
						{
						setState(239);
						match(T__1);
						setState(240);
						parameterDecl();
						}
						} 
					}
					setState(245);
					_errHandler.sync(this);
					_alt = getInterpreter().adaptivePredict(_input,14,_ctx);
				}
				}
			}

			setState(248);
			match(T__5);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class ParameterDeclContext extends ParserRuleContext {
		public TerminalNode Var() { return getToken(DaedalusParser.Var, 0); }
		public TypeReferenceContext typeReference() {
			return getRuleContext(TypeReferenceContext.class,0);
		}
		public NameNodeContext nameNode() {
			return getRuleContext(NameNodeContext.class,0);
		}
		public ArraySizeContext arraySize() {
			return getRuleContext(ArraySizeContext.class,0);
		}
		public ParameterDeclContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_parameterDecl; }
	}

	public final ParameterDeclContext parameterDecl() throws RecognitionException {
		ParameterDeclContext _localctx = new ParameterDeclContext(_ctx, getState());
		enterRule(_localctx, 34, RULE_parameterDecl);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(250);
			match(Var);
			setState(251);
			typeReference();
			setState(252);
			nameNode();
			setState(257);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==T__6) {
				{
				setState(253);
				match(T__6);
				setState(254);
				arraySize();
				setState(255);
				match(T__7);
				}
			}

			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class StatementBlockContext extends ParserRuleContext {
		public List<StatementContext> statement() {
			return getRuleContexts(StatementContext.class);
		}
		public StatementContext statement(int i) {
			return getRuleContext(StatementContext.class,i);
		}
		public List<IfBlockStatementContext> ifBlockStatement() {
			return getRuleContexts(IfBlockStatementContext.class);
		}
		public IfBlockStatementContext ifBlockStatement(int i) {
			return getRuleContext(IfBlockStatementContext.class,i);
		}
		public StatementBlockContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_statementBlock; }
	}

	public final StatementBlockContext statementBlock() throws RecognitionException {
		StatementBlockContext _localctx = new StatementBlockContext(_ctx, getState());
		enterRule(_localctx, 36, RULE_statementBlock);
		int _la;
		try {
			int _alt;
			enterOuterAlt(_localctx, 1);
			{
			setState(259);
			match(T__2);
			setState(271);
			_errHandler.sync(this);
			_alt = getInterpreter().adaptivePredict(_input,19,_ctx);
			while ( _alt!=1 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1+1 ) {
					{
					{
					setState(267);
					_errHandler.sync(this);
					switch (_input.LA(1)) {
					case T__4:
					case T__14:
					case T__15:
					case T__24:
					case T__25:
					case Const:
					case Var:
					case Int:
					case Func:
					case String:
					case Class:
					case Void:
					case Return:
					case Float:
					case Prototype:
					case Instance:
					case Null:
					case NoFunc:
					case Identifier:
					case IntegerLiteral:
					case FloatLiteral:
					case StringLiteral:
						{
						{
						setState(260);
						statement();
						setState(261);
						match(T__0);
						}
						}
						break;
					case If:
						{
						{
						setState(263);
						ifBlockStatement();
						setState(265);
						_errHandler.sync(this);
						_la = _input.LA(1);
						if (_la==T__0) {
							{
							setState(264);
							match(T__0);
							}
						}

						}
						}
						break;
					default:
						throw new NoViableAltException(this);
					}
					}
					} 
				}
				setState(273);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,19,_ctx);
			}
			setState(274);
			match(T__3);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class StatementContext extends ParserRuleContext {
		public AssignmentContext assignment() {
			return getRuleContext(AssignmentContext.class,0);
		}
		public ReturnStatementContext returnStatement() {
			return getRuleContext(ReturnStatementContext.class,0);
		}
		public ConstDefContext constDef() {
			return getRuleContext(ConstDefContext.class,0);
		}
		public VarDeclContext varDecl() {
			return getRuleContext(VarDeclContext.class,0);
		}
		public FuncCallContext funcCall() {
			return getRuleContext(FuncCallContext.class,0);
		}
		public ExpressionContext expression() {
			return getRuleContext(ExpressionContext.class,0);
		}
		public StatementContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_statement; }
	}

	public final StatementContext statement() throws RecognitionException {
		StatementContext _localctx = new StatementContext(_ctx, getState());
		enterRule(_localctx, 38, RULE_statement);
		try {
			setState(282);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,20,_ctx) ) {
			case 1:
				enterOuterAlt(_localctx, 1);
				{
				setState(276);
				assignment();
				}
				break;
			case 2:
				enterOuterAlt(_localctx, 2);
				{
				setState(277);
				returnStatement();
				}
				break;
			case 3:
				enterOuterAlt(_localctx, 3);
				{
				setState(278);
				constDef();
				}
				break;
			case 4:
				enterOuterAlt(_localctx, 4);
				{
				setState(279);
				varDecl();
				}
				break;
			case 5:
				enterOuterAlt(_localctx, 5);
				{
				setState(280);
				funcCall();
				}
				break;
			case 6:
				enterOuterAlt(_localctx, 6);
				{
				setState(281);
				expression(0);
				}
				break;
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class FuncCallContext extends ParserRuleContext {
		public NameNodeContext nameNode() {
			return getRuleContext(NameNodeContext.class,0);
		}
		public List<FuncArgExpressionContext> funcArgExpression() {
			return getRuleContexts(FuncArgExpressionContext.class);
		}
		public FuncArgExpressionContext funcArgExpression(int i) {
			return getRuleContext(FuncArgExpressionContext.class,i);
		}
		public FuncCallContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_funcCall; }
	}

	public final FuncCallContext funcCall() throws RecognitionException {
		FuncCallContext _localctx = new FuncCallContext(_ctx, getState());
		enterRule(_localctx, 40, RULE_funcCall);
		int _la;
		try {
			int _alt;
			enterOuterAlt(_localctx, 1);
			{
			setState(284);
			nameNode();
			setState(285);
			match(T__4);
			setState(294);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__4) | (1L << T__14) | (1L << T__15) | (1L << T__24) | (1L << T__25) | (1L << Int) | (1L << Func) | (1L << String) | (1L << Class) | (1L << Void) | (1L << Float) | (1L << Prototype) | (1L << Instance) | (1L << Null) | (1L << NoFunc) | (1L << Identifier) | (1L << IntegerLiteral) | (1L << FloatLiteral) | (1L << StringLiteral))) != 0)) {
				{
				setState(286);
				funcArgExpression();
				setState(291);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,21,_ctx);
				while ( _alt!=1 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
					if ( _alt==1+1 ) {
						{
						{
						setState(287);
						match(T__1);
						setState(288);
						funcArgExpression();
						}
						} 
					}
					setState(293);
					_errHandler.sync(this);
					_alt = getInterpreter().adaptivePredict(_input,21,_ctx);
				}
				}
			}

			setState(296);
			match(T__5);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class AssignmentContext extends ParserRuleContext {
		public ReferenceContext reference() {
			return getRuleContext(ReferenceContext.class,0);
		}
		public AssignmentOperatorContext assignmentOperator() {
			return getRuleContext(AssignmentOperatorContext.class,0);
		}
		public ExpressionBlockContext expressionBlock() {
			return getRuleContext(ExpressionBlockContext.class,0);
		}
		public AssignmentContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_assignment; }
	}

	public final AssignmentContext assignment() throws RecognitionException {
		AssignmentContext _localctx = new AssignmentContext(_ctx, getState());
		enterRule(_localctx, 42, RULE_assignment);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(298);
			reference();
			setState(299);
			assignmentOperator();
			setState(300);
			expressionBlock();
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class IfConditionContext extends ParserRuleContext {
		public ExpressionBlockContext expressionBlock() {
			return getRuleContext(ExpressionBlockContext.class,0);
		}
		public IfConditionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_ifCondition; }
	}

	public final IfConditionContext ifCondition() throws RecognitionException {
		IfConditionContext _localctx = new IfConditionContext(_ctx, getState());
		enterRule(_localctx, 44, RULE_ifCondition);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(302);
			expressionBlock();
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class ElseBlockContext extends ParserRuleContext {
		public TerminalNode Else() { return getToken(DaedalusParser.Else, 0); }
		public StatementBlockContext statementBlock() {
			return getRuleContext(StatementBlockContext.class,0);
		}
		public ElseBlockContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_elseBlock; }
	}

	public final ElseBlockContext elseBlock() throws RecognitionException {
		ElseBlockContext _localctx = new ElseBlockContext(_ctx, getState());
		enterRule(_localctx, 46, RULE_elseBlock);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(304);
			match(Else);
			setState(305);
			statementBlock();
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class ElseIfBlockContext extends ParserRuleContext {
		public TerminalNode Else() { return getToken(DaedalusParser.Else, 0); }
		public TerminalNode If() { return getToken(DaedalusParser.If, 0); }
		public IfConditionContext ifCondition() {
			return getRuleContext(IfConditionContext.class,0);
		}
		public StatementBlockContext statementBlock() {
			return getRuleContext(StatementBlockContext.class,0);
		}
		public ElseIfBlockContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_elseIfBlock; }
	}

	public final ElseIfBlockContext elseIfBlock() throws RecognitionException {
		ElseIfBlockContext _localctx = new ElseIfBlockContext(_ctx, getState());
		enterRule(_localctx, 48, RULE_elseIfBlock);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(307);
			match(Else);
			setState(308);
			match(If);
			setState(309);
			ifCondition();
			setState(310);
			statementBlock();
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class IfBlockContext extends ParserRuleContext {
		public TerminalNode If() { return getToken(DaedalusParser.If, 0); }
		public IfConditionContext ifCondition() {
			return getRuleContext(IfConditionContext.class,0);
		}
		public StatementBlockContext statementBlock() {
			return getRuleContext(StatementBlockContext.class,0);
		}
		public IfBlockContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_ifBlock; }
	}

	public final IfBlockContext ifBlock() throws RecognitionException {
		IfBlockContext _localctx = new IfBlockContext(_ctx, getState());
		enterRule(_localctx, 50, RULE_ifBlock);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(312);
			match(If);
			setState(313);
			ifCondition();
			setState(314);
			statementBlock();
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class IfBlockStatementContext extends ParserRuleContext {
		public IfBlockContext ifBlock() {
			return getRuleContext(IfBlockContext.class,0);
		}
		public List<ElseIfBlockContext> elseIfBlock() {
			return getRuleContexts(ElseIfBlockContext.class);
		}
		public ElseIfBlockContext elseIfBlock(int i) {
			return getRuleContext(ElseIfBlockContext.class,i);
		}
		public ElseBlockContext elseBlock() {
			return getRuleContext(ElseBlockContext.class,0);
		}
		public IfBlockStatementContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_ifBlockStatement; }
	}

	public final IfBlockStatementContext ifBlockStatement() throws RecognitionException {
		IfBlockStatementContext _localctx = new IfBlockStatementContext(_ctx, getState());
		enterRule(_localctx, 52, RULE_ifBlockStatement);
		int _la;
		try {
			int _alt;
			enterOuterAlt(_localctx, 1);
			{
			setState(316);
			ifBlock();
			setState(320);
			_errHandler.sync(this);
			_alt = getInterpreter().adaptivePredict(_input,23,_ctx);
			while ( _alt!=1 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1+1 ) {
					{
					{
					setState(317);
					elseIfBlock();
					}
					} 
				}
				setState(322);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,23,_ctx);
			}
			setState(324);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==Else) {
				{
				setState(323);
				elseBlock();
				}
			}

			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class ReturnStatementContext extends ParserRuleContext {
		public TerminalNode Return() { return getToken(DaedalusParser.Return, 0); }
		public ExpressionBlockContext expressionBlock() {
			return getRuleContext(ExpressionBlockContext.class,0);
		}
		public ReturnStatementContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_returnStatement; }
	}

	public final ReturnStatementContext returnStatement() throws RecognitionException {
		ReturnStatementContext _localctx = new ReturnStatementContext(_ctx, getState());
		enterRule(_localctx, 54, RULE_returnStatement);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(326);
			match(Return);
			setState(328);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__4) | (1L << T__14) | (1L << T__15) | (1L << T__24) | (1L << T__25) | (1L << Int) | (1L << Func) | (1L << String) | (1L << Class) | (1L << Void) | (1L << Float) | (1L << Prototype) | (1L << Instance) | (1L << Null) | (1L << NoFunc) | (1L << Identifier) | (1L << IntegerLiteral) | (1L << FloatLiteral) | (1L << StringLiteral))) != 0)) {
				{
				setState(327);
				expressionBlock();
				}
			}

			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class FuncArgExpressionContext extends ParserRuleContext {
		public ExpressionBlockContext expressionBlock() {
			return getRuleContext(ExpressionBlockContext.class,0);
		}
		public FuncArgExpressionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_funcArgExpression; }
	}

	public final FuncArgExpressionContext funcArgExpression() throws RecognitionException {
		FuncArgExpressionContext _localctx = new FuncArgExpressionContext(_ctx, getState());
		enterRule(_localctx, 56, RULE_funcArgExpression);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(330);
			expressionBlock();
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class ExpressionBlockContext extends ParserRuleContext {
		public ExpressionContext expression() {
			return getRuleContext(ExpressionContext.class,0);
		}
		public ExpressionBlockContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_expressionBlock; }
	}

	public final ExpressionBlockContext expressionBlock() throws RecognitionException {
		ExpressionBlockContext _localctx = new ExpressionBlockContext(_ctx, getState());
		enterRule(_localctx, 58, RULE_expressionBlock);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(332);
			expression(0);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class ExpressionContext extends ParserRuleContext {
		public ExpressionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_expression; }
	 
		public ExpressionContext() { }
		public void copyFrom(ExpressionContext ctx) {
			super.copyFrom(ctx);
		}
	}
	public static class BitMoveExpressionContext extends ExpressionContext {
		public List<ExpressionContext> expression() {
			return getRuleContexts(ExpressionContext.class);
		}
		public ExpressionContext expression(int i) {
			return getRuleContext(ExpressionContext.class,i);
		}
		public BitMoveOperatorContext bitMoveOperator() {
			return getRuleContext(BitMoveOperatorContext.class,0);
		}
		public BitMoveExpressionContext(ExpressionContext ctx) { copyFrom(ctx); }
	}
	public static class OneArgExpressionContext extends ExpressionContext {
		public OneArgOperatorContext oneArgOperator() {
			return getRuleContext(OneArgOperatorContext.class,0);
		}
		public ExpressionContext expression() {
			return getRuleContext(ExpressionContext.class,0);
		}
		public OneArgExpressionContext(ExpressionContext ctx) { copyFrom(ctx); }
	}
	public static class EqExpressionContext extends ExpressionContext {
		public List<ExpressionContext> expression() {
			return getRuleContexts(ExpressionContext.class);
		}
		public ExpressionContext expression(int i) {
			return getRuleContext(ExpressionContext.class,i);
		}
		public EqOperatorContext eqOperator() {
			return getRuleContext(EqOperatorContext.class,0);
		}
		public EqExpressionContext(ExpressionContext ctx) { copyFrom(ctx); }
	}
	public static class ValExpressionContext extends ExpressionContext {
		public ValueContext value() {
			return getRuleContext(ValueContext.class,0);
		}
		public ValExpressionContext(ExpressionContext ctx) { copyFrom(ctx); }
	}
	public static class AddExpressionContext extends ExpressionContext {
		public List<ExpressionContext> expression() {
			return getRuleContexts(ExpressionContext.class);
		}
		public ExpressionContext expression(int i) {
			return getRuleContext(ExpressionContext.class,i);
		}
		public AddOperatorContext addOperator() {
			return getRuleContext(AddOperatorContext.class,0);
		}
		public AddExpressionContext(ExpressionContext ctx) { copyFrom(ctx); }
	}
	public static class CompExpressionContext extends ExpressionContext {
		public List<ExpressionContext> expression() {
			return getRuleContexts(ExpressionContext.class);
		}
		public ExpressionContext expression(int i) {
			return getRuleContext(ExpressionContext.class,i);
		}
		public CompOperatorContext compOperator() {
			return getRuleContext(CompOperatorContext.class,0);
		}
		public CompExpressionContext(ExpressionContext ctx) { copyFrom(ctx); }
	}
	public static class LogOrExpressionContext extends ExpressionContext {
		public List<ExpressionContext> expression() {
			return getRuleContexts(ExpressionContext.class);
		}
		public ExpressionContext expression(int i) {
			return getRuleContext(ExpressionContext.class,i);
		}
		public LogOrOperatorContext logOrOperator() {
			return getRuleContext(LogOrOperatorContext.class,0);
		}
		public LogOrExpressionContext(ExpressionContext ctx) { copyFrom(ctx); }
	}
	public static class BinAndExpressionContext extends ExpressionContext {
		public List<ExpressionContext> expression() {
			return getRuleContexts(ExpressionContext.class);
		}
		public ExpressionContext expression(int i) {
			return getRuleContext(ExpressionContext.class,i);
		}
		public BinAndOperatorContext binAndOperator() {
			return getRuleContext(BinAndOperatorContext.class,0);
		}
		public BinAndExpressionContext(ExpressionContext ctx) { copyFrom(ctx); }
	}
	public static class BinOrExpressionContext extends ExpressionContext {
		public List<ExpressionContext> expression() {
			return getRuleContexts(ExpressionContext.class);
		}
		public ExpressionContext expression(int i) {
			return getRuleContext(ExpressionContext.class,i);
		}
		public BinOrOperatorContext binOrOperator() {
			return getRuleContext(BinOrOperatorContext.class,0);
		}
		public BinOrExpressionContext(ExpressionContext ctx) { copyFrom(ctx); }
	}
	public static class MultExpressionContext extends ExpressionContext {
		public List<ExpressionContext> expression() {
			return getRuleContexts(ExpressionContext.class);
		}
		public ExpressionContext expression(int i) {
			return getRuleContext(ExpressionContext.class,i);
		}
		public MultOperatorContext multOperator() {
			return getRuleContext(MultOperatorContext.class,0);
		}
		public MultExpressionContext(ExpressionContext ctx) { copyFrom(ctx); }
	}
	public static class BracketExpressionContext extends ExpressionContext {
		public ExpressionContext expression() {
			return getRuleContext(ExpressionContext.class,0);
		}
		public BracketExpressionContext(ExpressionContext ctx) { copyFrom(ctx); }
	}
	public static class LogAndExpressionContext extends ExpressionContext {
		public List<ExpressionContext> expression() {
			return getRuleContexts(ExpressionContext.class);
		}
		public ExpressionContext expression(int i) {
			return getRuleContext(ExpressionContext.class,i);
		}
		public LogAndOperatorContext logAndOperator() {
			return getRuleContext(LogAndOperatorContext.class,0);
		}
		public LogAndExpressionContext(ExpressionContext ctx) { copyFrom(ctx); }
	}

	public final ExpressionContext expression() throws RecognitionException {
		return expression(0);
	}

	private ExpressionContext expression(int _p) throws RecognitionException {
		ParserRuleContext _parentctx = _ctx;
		int _parentState = getState();
		ExpressionContext _localctx = new ExpressionContext(_ctx, _parentState);
		ExpressionContext _prevctx = _localctx;
		int _startState = 60;
		enterRecursionRule(_localctx, 60, RULE_expression, _p);
		try {
			int _alt;
			enterOuterAlt(_localctx, 1);
			{
			setState(343);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case T__4:
				{
				_localctx = new BracketExpressionContext(_localctx);
				_ctx = _localctx;
				_prevctx = _localctx;

				setState(335);
				match(T__4);
				setState(336);
				expression(0);
				setState(337);
				match(T__5);
				}
				break;
			case T__14:
			case T__15:
			case T__24:
			case T__25:
				{
				_localctx = new OneArgExpressionContext(_localctx);
				_ctx = _localctx;
				_prevctx = _localctx;
				setState(339);
				oneArgOperator();
				setState(340);
				expression(11);
				}
				break;
			case Int:
			case Func:
			case String:
			case Class:
			case Void:
			case Float:
			case Prototype:
			case Instance:
			case Null:
			case NoFunc:
			case Identifier:
			case IntegerLiteral:
			case FloatLiteral:
			case StringLiteral:
				{
				_localctx = new ValExpressionContext(_localctx);
				_ctx = _localctx;
				_prevctx = _localctx;
				setState(342);
				value();
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
			_ctx.stop = _input.LT(-1);
			setState(383);
			_errHandler.sync(this);
			_alt = getInterpreter().adaptivePredict(_input,28,_ctx);
			while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1 ) {
					if ( _parseListeners!=null ) triggerExitRuleEvent();
					_prevctx = _localctx;
					{
					setState(381);
					_errHandler.sync(this);
					switch ( getInterpreter().adaptivePredict(_input,27,_ctx) ) {
					case 1:
						{
						_localctx = new MultExpressionContext(new ExpressionContext(_parentctx, _parentState));
						pushNewRecursionContext(_localctx, _startState, RULE_expression);
						setState(345);
						if (!(precpred(_ctx, 10))) throw new FailedPredicateException(this, "precpred(_ctx, 10)");
						setState(346);
						multOperator();
						setState(347);
						expression(11);
						}
						break;
					case 2:
						{
						_localctx = new AddExpressionContext(new ExpressionContext(_parentctx, _parentState));
						pushNewRecursionContext(_localctx, _startState, RULE_expression);
						setState(349);
						if (!(precpred(_ctx, 9))) throw new FailedPredicateException(this, "precpred(_ctx, 9)");
						setState(350);
						addOperator();
						setState(351);
						expression(10);
						}
						break;
					case 3:
						{
						_localctx = new BitMoveExpressionContext(new ExpressionContext(_parentctx, _parentState));
						pushNewRecursionContext(_localctx, _startState, RULE_expression);
						setState(353);
						if (!(precpred(_ctx, 8))) throw new FailedPredicateException(this, "precpred(_ctx, 8)");
						setState(354);
						bitMoveOperator();
						setState(355);
						expression(9);
						}
						break;
					case 4:
						{
						_localctx = new CompExpressionContext(new ExpressionContext(_parentctx, _parentState));
						pushNewRecursionContext(_localctx, _startState, RULE_expression);
						setState(357);
						if (!(precpred(_ctx, 7))) throw new FailedPredicateException(this, "precpred(_ctx, 7)");
						setState(358);
						compOperator();
						setState(359);
						expression(8);
						}
						break;
					case 5:
						{
						_localctx = new EqExpressionContext(new ExpressionContext(_parentctx, _parentState));
						pushNewRecursionContext(_localctx, _startState, RULE_expression);
						setState(361);
						if (!(precpred(_ctx, 6))) throw new FailedPredicateException(this, "precpred(_ctx, 6)");
						setState(362);
						eqOperator();
						setState(363);
						expression(7);
						}
						break;
					case 6:
						{
						_localctx = new BinAndExpressionContext(new ExpressionContext(_parentctx, _parentState));
						pushNewRecursionContext(_localctx, _startState, RULE_expression);
						setState(365);
						if (!(precpred(_ctx, 5))) throw new FailedPredicateException(this, "precpred(_ctx, 5)");
						setState(366);
						binAndOperator();
						setState(367);
						expression(6);
						}
						break;
					case 7:
						{
						_localctx = new BinOrExpressionContext(new ExpressionContext(_parentctx, _parentState));
						pushNewRecursionContext(_localctx, _startState, RULE_expression);
						setState(369);
						if (!(precpred(_ctx, 4))) throw new FailedPredicateException(this, "precpred(_ctx, 4)");
						setState(370);
						binOrOperator();
						setState(371);
						expression(5);
						}
						break;
					case 8:
						{
						_localctx = new LogAndExpressionContext(new ExpressionContext(_parentctx, _parentState));
						pushNewRecursionContext(_localctx, _startState, RULE_expression);
						setState(373);
						if (!(precpred(_ctx, 3))) throw new FailedPredicateException(this, "precpred(_ctx, 3)");
						setState(374);
						logAndOperator();
						setState(375);
						expression(4);
						}
						break;
					case 9:
						{
						_localctx = new LogOrExpressionContext(new ExpressionContext(_parentctx, _parentState));
						pushNewRecursionContext(_localctx, _startState, RULE_expression);
						setState(377);
						if (!(precpred(_ctx, 2))) throw new FailedPredicateException(this, "precpred(_ctx, 2)");
						setState(378);
						logOrOperator();
						setState(379);
						expression(3);
						}
						break;
					}
					} 
				}
				setState(385);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,28,_ctx);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			unrollRecursionContexts(_parentctx);
		}
		return _localctx;
	}

	public static class ArrayIndexContext extends ParserRuleContext {
		public TerminalNode IntegerLiteral() { return getToken(DaedalusParser.IntegerLiteral, 0); }
		public ReferenceAtomContext referenceAtom() {
			return getRuleContext(ReferenceAtomContext.class,0);
		}
		public ArrayIndexContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_arrayIndex; }
	}

	public final ArrayIndexContext arrayIndex() throws RecognitionException {
		ArrayIndexContext _localctx = new ArrayIndexContext(_ctx, getState());
		enterRule(_localctx, 62, RULE_arrayIndex);
		try {
			setState(388);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case IntegerLiteral:
				enterOuterAlt(_localctx, 1);
				{
				setState(386);
				match(IntegerLiteral);
				}
				break;
			case Int:
			case Func:
			case String:
			case Class:
			case Void:
			case Float:
			case Prototype:
			case Instance:
			case Null:
			case Identifier:
				enterOuterAlt(_localctx, 2);
				{
				setState(387);
				referenceAtom();
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class ArraySizeContext extends ParserRuleContext {
		public TerminalNode IntegerLiteral() { return getToken(DaedalusParser.IntegerLiteral, 0); }
		public ReferenceAtomContext referenceAtom() {
			return getRuleContext(ReferenceAtomContext.class,0);
		}
		public ArraySizeContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_arraySize; }
	}

	public final ArraySizeContext arraySize() throws RecognitionException {
		ArraySizeContext _localctx = new ArraySizeContext(_ctx, getState());
		enterRule(_localctx, 64, RULE_arraySize);
		try {
			setState(392);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case IntegerLiteral:
				enterOuterAlt(_localctx, 1);
				{
				setState(390);
				match(IntegerLiteral);
				}
				break;
			case Int:
			case Func:
			case String:
			case Class:
			case Void:
			case Float:
			case Prototype:
			case Instance:
			case Null:
			case Identifier:
				enterOuterAlt(_localctx, 2);
				{
				setState(391);
				referenceAtom();
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class ValueContext extends ParserRuleContext {
		public ValueContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_value; }
	 
		public ValueContext() { }
		public void copyFrom(ValueContext ctx) {
			super.copyFrom(ctx);
		}
	}
	public static class IntegerLiteralValueContext extends ValueContext {
		public TerminalNode IntegerLiteral() { return getToken(DaedalusParser.IntegerLiteral, 0); }
		public IntegerLiteralValueContext(ValueContext ctx) { copyFrom(ctx); }
	}
	public static class FloatLiteralValueContext extends ValueContext {
		public TerminalNode FloatLiteral() { return getToken(DaedalusParser.FloatLiteral, 0); }
		public FloatLiteralValueContext(ValueContext ctx) { copyFrom(ctx); }
	}
	public static class StringLiteralValueContext extends ValueContext {
		public TerminalNode StringLiteral() { return getToken(DaedalusParser.StringLiteral, 0); }
		public StringLiteralValueContext(ValueContext ctx) { copyFrom(ctx); }
	}
	public static class AnyIdentifierValueContext extends ValueContext {
		public NameNodeContext nameNode() {
			return getRuleContext(NameNodeContext.class,0);
		}
		public AnyIdentifierValueContext(ValueContext ctx) { copyFrom(ctx); }
	}
	public static class NoFuncLiteralValueContext extends ValueContext {
		public TerminalNode NoFunc() { return getToken(DaedalusParser.NoFunc, 0); }
		public NoFuncLiteralValueContext(ValueContext ctx) { copyFrom(ctx); }
	}
	public static class NullLiteralValueContext extends ValueContext {
		public TerminalNode Null() { return getToken(DaedalusParser.Null, 0); }
		public NullLiteralValueContext(ValueContext ctx) { copyFrom(ctx); }
	}
	public static class FuncCallValueContext extends ValueContext {
		public FuncCallContext funcCall() {
			return getRuleContext(FuncCallContext.class,0);
		}
		public FuncCallValueContext(ValueContext ctx) { copyFrom(ctx); }
	}
	public static class ReferenceValueContext extends ValueContext {
		public ReferenceContext reference() {
			return getRuleContext(ReferenceContext.class,0);
		}
		public ReferenceValueContext(ValueContext ctx) { copyFrom(ctx); }
	}

	public final ValueContext value() throws RecognitionException {
		ValueContext _localctx = new ValueContext(_ctx, getState());
		enterRule(_localctx, 66, RULE_value);
		try {
			setState(402);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,31,_ctx) ) {
			case 1:
				_localctx = new IntegerLiteralValueContext(_localctx);
				enterOuterAlt(_localctx, 1);
				{
				setState(394);
				match(IntegerLiteral);
				}
				break;
			case 2:
				_localctx = new FloatLiteralValueContext(_localctx);
				enterOuterAlt(_localctx, 2);
				{
				setState(395);
				match(FloatLiteral);
				}
				break;
			case 3:
				_localctx = new StringLiteralValueContext(_localctx);
				enterOuterAlt(_localctx, 3);
				{
				setState(396);
				match(StringLiteral);
				}
				break;
			case 4:
				_localctx = new NullLiteralValueContext(_localctx);
				enterOuterAlt(_localctx, 4);
				{
				setState(397);
				match(Null);
				}
				break;
			case 5:
				_localctx = new NoFuncLiteralValueContext(_localctx);
				enterOuterAlt(_localctx, 5);
				{
				setState(398);
				match(NoFunc);
				}
				break;
			case 6:
				_localctx = new FuncCallValueContext(_localctx);
				enterOuterAlt(_localctx, 6);
				{
				setState(399);
				funcCall();
				}
				break;
			case 7:
				_localctx = new ReferenceValueContext(_localctx);
				enterOuterAlt(_localctx, 7);
				{
				setState(400);
				reference();
				}
				break;
			case 8:
				_localctx = new AnyIdentifierValueContext(_localctx);
				enterOuterAlt(_localctx, 8);
				{
				setState(401);
				nameNode();
				}
				break;
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class ReferenceAtomContext extends ParserRuleContext {
		public NameNodeContext nameNode() {
			return getRuleContext(NameNodeContext.class,0);
		}
		public ArrayIndexContext arrayIndex() {
			return getRuleContext(ArrayIndexContext.class,0);
		}
		public ReferenceAtomContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_referenceAtom; }
	}

	public final ReferenceAtomContext referenceAtom() throws RecognitionException {
		ReferenceAtomContext _localctx = new ReferenceAtomContext(_ctx, getState());
		enterRule(_localctx, 68, RULE_referenceAtom);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(404);
			nameNode();
			setState(409);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,32,_ctx) ) {
			case 1:
				{
				setState(405);
				match(T__6);
				setState(406);
				arrayIndex();
				setState(407);
				match(T__7);
				}
				break;
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class ReferenceContext extends ParserRuleContext {
		public List<ReferenceAtomContext> referenceAtom() {
			return getRuleContexts(ReferenceAtomContext.class);
		}
		public ReferenceAtomContext referenceAtom(int i) {
			return getRuleContext(ReferenceAtomContext.class,i);
		}
		public ReferenceContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_reference; }
	}

	public final ReferenceContext reference() throws RecognitionException {
		ReferenceContext _localctx = new ReferenceContext(_ctx, getState());
		enterRule(_localctx, 70, RULE_reference);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(411);
			referenceAtom();
			setState(414);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,33,_ctx) ) {
			case 1:
				{
				setState(412);
				match(T__9);
				setState(413);
				referenceAtom();
				}
				break;
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class TypeReferenceContext extends ParserRuleContext {
		public TerminalNode Identifier() { return getToken(DaedalusParser.Identifier, 0); }
		public TerminalNode Void() { return getToken(DaedalusParser.Void, 0); }
		public TerminalNode Int() { return getToken(DaedalusParser.Int, 0); }
		public TerminalNode Float() { return getToken(DaedalusParser.Float, 0); }
		public TerminalNode String() { return getToken(DaedalusParser.String, 0); }
		public TerminalNode Func() { return getToken(DaedalusParser.Func, 0); }
		public TerminalNode Instance() { return getToken(DaedalusParser.Instance, 0); }
		public TypeReferenceContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_typeReference; }
	}

	public final TypeReferenceContext typeReference() throws RecognitionException {
		TypeReferenceContext _localctx = new TypeReferenceContext(_ctx, getState());
		enterRule(_localctx, 72, RULE_typeReference);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(416);
			_la = _input.LA(1);
			if ( !((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << Int) | (1L << Func) | (1L << String) | (1L << Void) | (1L << Float) | (1L << Instance) | (1L << Identifier))) != 0)) ) {
			_errHandler.recoverInline(this);
			}
			else {
				if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
				_errHandler.reportMatch(this);
				consume();
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class AnyIdentifierContext extends ParserRuleContext {
		public TerminalNode Void() { return getToken(DaedalusParser.Void, 0); }
		public TerminalNode Int() { return getToken(DaedalusParser.Int, 0); }
		public TerminalNode Float() { return getToken(DaedalusParser.Float, 0); }
		public TerminalNode String() { return getToken(DaedalusParser.String, 0); }
		public TerminalNode Func() { return getToken(DaedalusParser.Func, 0); }
		public TerminalNode Instance() { return getToken(DaedalusParser.Instance, 0); }
		public TerminalNode Class() { return getToken(DaedalusParser.Class, 0); }
		public TerminalNode Prototype() { return getToken(DaedalusParser.Prototype, 0); }
		public TerminalNode Null() { return getToken(DaedalusParser.Null, 0); }
		public TerminalNode Identifier() { return getToken(DaedalusParser.Identifier, 0); }
		public AnyIdentifierContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_anyIdentifier; }
	}

	public final AnyIdentifierContext anyIdentifier() throws RecognitionException {
		AnyIdentifierContext _localctx = new AnyIdentifierContext(_ctx, getState());
		enterRule(_localctx, 74, RULE_anyIdentifier);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(418);
			_la = _input.LA(1);
			if ( !((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << Int) | (1L << Func) | (1L << String) | (1L << Class) | (1L << Void) | (1L << Float) | (1L << Prototype) | (1L << Instance) | (1L << Null) | (1L << Identifier))) != 0)) ) {
			_errHandler.recoverInline(this);
			}
			else {
				if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
				_errHandler.reportMatch(this);
				consume();
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class NameNodeContext extends ParserRuleContext {
		public AnyIdentifierContext anyIdentifier() {
			return getRuleContext(AnyIdentifierContext.class,0);
		}
		public NameNodeContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_nameNode; }
	}

	public final NameNodeContext nameNode() throws RecognitionException {
		NameNodeContext _localctx = new NameNodeContext(_ctx, getState());
		enterRule(_localctx, 76, RULE_nameNode);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(420);
			anyIdentifier();
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class ParentReferenceContext extends ParserRuleContext {
		public TerminalNode Identifier() { return getToken(DaedalusParser.Identifier, 0); }
		public ParentReferenceContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_parentReference; }
	}

	public final ParentReferenceContext parentReference() throws RecognitionException {
		ParentReferenceContext _localctx = new ParentReferenceContext(_ctx, getState());
		enterRule(_localctx, 78, RULE_parentReference);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(422);
			match(Identifier);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class AssignmentOperatorContext extends ParserRuleContext {
		public AssignmentOperatorContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_assignmentOperator; }
	}

	public final AssignmentOperatorContext assignmentOperator() throws RecognitionException {
		AssignmentOperatorContext _localctx = new AssignmentOperatorContext(_ctx, getState());
		enterRule(_localctx, 80, RULE_assignmentOperator);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(424);
			_la = _input.LA(1);
			if ( !((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__8) | (1L << T__10) | (1L << T__11) | (1L << T__12) | (1L << T__13))) != 0)) ) {
			_errHandler.recoverInline(this);
			}
			else {
				if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
				_errHandler.reportMatch(this);
				consume();
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class AddOperatorContext extends ParserRuleContext {
		public AddOperatorContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_addOperator; }
	}

	public final AddOperatorContext addOperator() throws RecognitionException {
		AddOperatorContext _localctx = new AddOperatorContext(_ctx, getState());
		enterRule(_localctx, 82, RULE_addOperator);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(426);
			_la = _input.LA(1);
			if ( !(_la==T__14 || _la==T__15) ) {
			_errHandler.recoverInline(this);
			}
			else {
				if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
				_errHandler.reportMatch(this);
				consume();
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class BitMoveOperatorContext extends ParserRuleContext {
		public BitMoveOperatorContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_bitMoveOperator; }
	}

	public final BitMoveOperatorContext bitMoveOperator() throws RecognitionException {
		BitMoveOperatorContext _localctx = new BitMoveOperatorContext(_ctx, getState());
		enterRule(_localctx, 84, RULE_bitMoveOperator);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(428);
			_la = _input.LA(1);
			if ( !(_la==T__16 || _la==T__17) ) {
			_errHandler.recoverInline(this);
			}
			else {
				if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
				_errHandler.reportMatch(this);
				consume();
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class CompOperatorContext extends ParserRuleContext {
		public CompOperatorContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_compOperator; }
	}

	public final CompOperatorContext compOperator() throws RecognitionException {
		CompOperatorContext _localctx = new CompOperatorContext(_ctx, getState());
		enterRule(_localctx, 86, RULE_compOperator);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(430);
			_la = _input.LA(1);
			if ( !((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__18) | (1L << T__19) | (1L << T__20) | (1L << T__21))) != 0)) ) {
			_errHandler.recoverInline(this);
			}
			else {
				if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
				_errHandler.reportMatch(this);
				consume();
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class EqOperatorContext extends ParserRuleContext {
		public EqOperatorContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_eqOperator; }
	}

	public final EqOperatorContext eqOperator() throws RecognitionException {
		EqOperatorContext _localctx = new EqOperatorContext(_ctx, getState());
		enterRule(_localctx, 88, RULE_eqOperator);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(432);
			_la = _input.LA(1);
			if ( !(_la==T__22 || _la==T__23) ) {
			_errHandler.recoverInline(this);
			}
			else {
				if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
				_errHandler.reportMatch(this);
				consume();
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class OneArgOperatorContext extends ParserRuleContext {
		public OneArgOperatorContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_oneArgOperator; }
	}

	public final OneArgOperatorContext oneArgOperator() throws RecognitionException {
		OneArgOperatorContext _localctx = new OneArgOperatorContext(_ctx, getState());
		enterRule(_localctx, 90, RULE_oneArgOperator);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(434);
			_la = _input.LA(1);
			if ( !((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__14) | (1L << T__15) | (1L << T__24) | (1L << T__25))) != 0)) ) {
			_errHandler.recoverInline(this);
			}
			else {
				if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
				_errHandler.reportMatch(this);
				consume();
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class MultOperatorContext extends ParserRuleContext {
		public MultOperatorContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_multOperator; }
	}

	public final MultOperatorContext multOperator() throws RecognitionException {
		MultOperatorContext _localctx = new MultOperatorContext(_ctx, getState());
		enterRule(_localctx, 92, RULE_multOperator);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(436);
			_la = _input.LA(1);
			if ( !((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__26) | (1L << T__27) | (1L << T__28))) != 0)) ) {
			_errHandler.recoverInline(this);
			}
			else {
				if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
				_errHandler.reportMatch(this);
				consume();
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class BinAndOperatorContext extends ParserRuleContext {
		public BinAndOperatorContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_binAndOperator; }
	}

	public final BinAndOperatorContext binAndOperator() throws RecognitionException {
		BinAndOperatorContext _localctx = new BinAndOperatorContext(_ctx, getState());
		enterRule(_localctx, 94, RULE_binAndOperator);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(438);
			match(T__29);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class BinOrOperatorContext extends ParserRuleContext {
		public BinOrOperatorContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_binOrOperator; }
	}

	public final BinOrOperatorContext binOrOperator() throws RecognitionException {
		BinOrOperatorContext _localctx = new BinOrOperatorContext(_ctx, getState());
		enterRule(_localctx, 96, RULE_binOrOperator);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(440);
			match(T__30);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class LogAndOperatorContext extends ParserRuleContext {
		public LogAndOperatorContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_logAndOperator; }
	}

	public final LogAndOperatorContext logAndOperator() throws RecognitionException {
		LogAndOperatorContext _localctx = new LogAndOperatorContext(_ctx, getState());
		enterRule(_localctx, 98, RULE_logAndOperator);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(442);
			match(T__31);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class LogOrOperatorContext extends ParserRuleContext {
		public LogOrOperatorContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_logOrOperator; }
	}

	public final LogOrOperatorContext logOrOperator() throws RecognitionException {
		LogOrOperatorContext _localctx = new LogOrOperatorContext(_ctx, getState());
		enterRule(_localctx, 100, RULE_logOrOperator);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(444);
			match(T__32);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public boolean sempred(RuleContext _localctx, int ruleIndex, int predIndex) {
		switch (ruleIndex) {
		case 30:
			return expression_sempred((ExpressionContext)_localctx, predIndex);
		}
		return true;
	}
	private boolean expression_sempred(ExpressionContext _localctx, int predIndex) {
		switch (predIndex) {
		case 0:
			return precpred(_ctx, 10);
		case 1:
			return precpred(_ctx, 9);
		case 2:
			return precpred(_ctx, 8);
		case 3:
			return precpred(_ctx, 7);
		case 4:
			return precpred(_ctx, 6);
		case 5:
			return precpred(_ctx, 5);
		case 6:
			return precpred(_ctx, 4);
		case 7:
			return precpred(_ctx, 3);
		case 8:
			return precpred(_ctx, 2);
		}
		return true;
	}

	public static final String _serializedATN =
		"\3\u608b\ua72a\u8133\ub9ed\u417c\u3be7\u7786\u5964\3:\u01c1\4\2\t\2\4"+
		"\3\t\3\4\4\t\4\4\5\t\5\4\6\t\6\4\7\t\7\4\b\t\b\4\t\t\t\4\n\t\n\4\13\t"+
		"\13\4\f\t\f\4\r\t\r\4\16\t\16\4\17\t\17\4\20\t\20\4\21\t\21\4\22\t\22"+
		"\4\23\t\23\4\24\t\24\4\25\t\25\4\26\t\26\4\27\t\27\4\30\t\30\4\31\t\31"+
		"\4\32\t\32\4\33\t\33\4\34\t\34\4\35\t\35\4\36\t\36\4\37\t\37\4 \t \4!"+
		"\t!\4\"\t\"\4#\t#\4$\t$\4%\t%\4&\t&\4\'\t\'\4(\t(\4)\t)\4*\t*\4+\t+\4"+
		",\t,\4-\t-\4.\t.\4/\t/\4\60\t\60\4\61\t\61\4\62\t\62\4\63\t\63\4\64\t"+
		"\64\3\2\3\2\7\2k\n\2\f\2\16\2n\13\2\3\2\3\2\3\3\3\3\3\3\3\3\5\3v\n\3\3"+
		"\3\5\3y\n\3\3\4\3\4\3\4\5\4~\n\4\3\4\3\4\3\5\3\5\3\5\3\5\3\5\3\5\3\6\3"+
		"\6\3\6\3\6\5\6\u008c\n\6\3\6\3\6\3\6\5\6\u0091\n\6\7\6\u0093\n\6\f\6\16"+
		"\6\u0096\13\6\3\7\3\7\3\7\3\7\3\7\3\7\7\7\u009e\n\7\f\7\16\7\u00a1\13"+
		"\7\3\7\3\7\3\b\3\b\3\b\3\b\3\b\3\b\3\b\3\t\3\t\3\t\3\t\3\t\3\t\3\t\3\n"+
		"\3\n\3\n\3\n\7\n\u00b7\n\n\f\n\16\n\u00ba\13\n\3\n\3\n\3\n\3\n\3\13\3"+
		"\13\3\13\3\13\5\13\u00c4\n\13\3\13\3\13\3\13\3\13\5\13\u00ca\n\13\7\13"+
		"\u00cc\n\13\f\13\16\13\u00cf\13\13\3\f\3\f\3\f\3\f\3\f\3\f\3\r\3\r\3\r"+
		"\3\r\3\r\7\r\u00dc\n\r\f\r\16\r\u00df\13\r\3\r\3\r\3\16\3\16\3\16\3\17"+
		"\3\17\3\17\3\20\3\20\3\20\3\20\3\20\3\21\3\21\3\22\3\22\3\22\3\22\7\22"+
		"\u00f4\n\22\f\22\16\22\u00f7\13\22\5\22\u00f9\n\22\3\22\3\22\3\23\3\23"+
		"\3\23\3\23\3\23\3\23\3\23\5\23\u0104\n\23\3\24\3\24\3\24\3\24\3\24\3\24"+
		"\5\24\u010c\n\24\5\24\u010e\n\24\7\24\u0110\n\24\f\24\16\24\u0113\13\24"+
		"\3\24\3\24\3\25\3\25\3\25\3\25\3\25\3\25\5\25\u011d\n\25\3\26\3\26\3\26"+
		"\3\26\3\26\7\26\u0124\n\26\f\26\16\26\u0127\13\26\5\26\u0129\n\26\3\26"+
		"\3\26\3\27\3\27\3\27\3\27\3\30\3\30\3\31\3\31\3\31\3\32\3\32\3\32\3\32"+
		"\3\32\3\33\3\33\3\33\3\33\3\34\3\34\7\34\u0141\n\34\f\34\16\34\u0144\13"+
		"\34\3\34\5\34\u0147\n\34\3\35\3\35\5\35\u014b\n\35\3\36\3\36\3\37\3\37"+
		"\3 \3 \3 \3 \3 \3 \3 \3 \3 \5 \u015a\n \3 \3 \3 \3 \3 \3 \3 \3 \3 \3 "+
		"\3 \3 \3 \3 \3 \3 \3 \3 \3 \3 \3 \3 \3 \3 \3 \3 \3 \3 \3 \3 \3 \3 \3 "+
		"\3 \3 \3 \7 \u0180\n \f \16 \u0183\13 \3!\3!\5!\u0187\n!\3\"\3\"\5\"\u018b"+
		"\n\"\3#\3#\3#\3#\3#\3#\3#\3#\5#\u0195\n#\3$\3$\3$\3$\3$\5$\u019c\n$\3"+
		"%\3%\3%\5%\u01a1\n%\3&\3&\3\'\3\'\3(\3(\3)\3)\3*\3*\3+\3+\3,\3,\3-\3-"+
		"\3.\3.\3/\3/\3\60\3\60\3\61\3\61\3\62\3\62\3\63\3\63\3\64\3\64\3\64\n"+
		"l\u009f\u00b8\u00dd\u00f5\u0111\u0125\u0142\3>\65\2\4\6\b\n\f\16\20\22"+
		"\24\26\30\32\34\36 \"$&(*,.\60\62\64\668:<>@BDFHJLNPRTVXZ\\^`bdf\2\13"+
		"\b\2\'\')*,,..\60\60\63\63\6\2\'\'),.\61\63\63\4\2\13\13\r\20\3\2\21\22"+
		"\3\2\23\24\3\2\25\30\3\2\31\32\4\2\21\22\33\34\3\2\35\37\2\u01c5\2l\3"+
		"\2\2\2\4u\3\2\2\2\6}\3\2\2\2\b\u0081\3\2\2\2\n\u0087\3\2\2\2\f\u0097\3"+
		"\2\2\2\16\u00a4\3\2\2\2\20\u00ab\3\2\2\2\22\u00b2\3\2\2\2\24\u00bf\3\2"+
		"\2\2\26\u00d0\3\2\2\2\30\u00d6\3\2\2\2\32\u00e2\3\2\2\2\34\u00e5\3\2\2"+
		"\2\36\u00e8\3\2\2\2 \u00ed\3\2\2\2\"\u00ef\3\2\2\2$\u00fc\3\2\2\2&\u0105"+
		"\3\2\2\2(\u011c\3\2\2\2*\u011e\3\2\2\2,\u012c\3\2\2\2.\u0130\3\2\2\2\60"+
		"\u0132\3\2\2\2\62\u0135\3\2\2\2\64\u013a\3\2\2\2\66\u013e\3\2\2\28\u0148"+
		"\3\2\2\2:\u014c\3\2\2\2<\u014e\3\2\2\2>\u0159\3\2\2\2@\u0186\3\2\2\2B"+
		"\u018a\3\2\2\2D\u0194\3\2\2\2F\u0196\3\2\2\2H\u019d\3\2\2\2J\u01a2\3\2"+
		"\2\2L\u01a4\3\2\2\2N\u01a6\3\2\2\2P\u01a8\3\2\2\2R\u01aa\3\2\2\2T\u01ac"+
		"\3\2\2\2V\u01ae\3\2\2\2X\u01b0\3\2\2\2Z\u01b2\3\2\2\2\\\u01b4\3\2\2\2"+
		"^\u01b6\3\2\2\2`\u01b8\3\2\2\2b\u01ba\3\2\2\2d\u01bc\3\2\2\2f\u01be\3"+
		"\2\2\2hk\5\4\3\2ik\5\6\4\2jh\3\2\2\2ji\3\2\2\2kn\3\2\2\2lm\3\2\2\2lj\3"+
		"\2\2\2mo\3\2\2\2nl\3\2\2\2op\7\2\2\3p\3\3\2\2\2qv\5\b\5\2rv\5\f\7\2sv"+
		"\5\16\b\2tv\5\20\t\2uq\3\2\2\2ur\3\2\2\2us\3\2\2\2ut\3\2\2\2vx\3\2\2\2"+
		"wy\7\3\2\2xw\3\2\2\2xy\3\2\2\2y\5\3\2\2\2z~\5\n\6\2{~\5\24\13\2|~\5\22"+
		"\n\2}z\3\2\2\2}{\3\2\2\2}|\3\2\2\2~\177\3\2\2\2\177\u0080\7\3\2\2\u0080"+
		"\7\3\2\2\2\u0081\u0082\7)\2\2\u0082\u0083\5J&\2\u0083\u0084\5N(\2\u0084"+
		"\u0085\5\"\22\2\u0085\u0086\5&\24\2\u0086\t\3\2\2\2\u0087\u0088\7$\2\2"+
		"\u0088\u008b\5J&\2\u0089\u008c\5\32\16\2\u008a\u008c\5\26\f\2\u008b\u0089"+
		"\3\2\2\2\u008b\u008a\3\2\2\2\u008c\u0094\3\2\2\2\u008d\u0090\7\4\2\2\u008e"+
		"\u0091\5\32\16\2\u008f\u0091\5\26\f\2\u0090\u008e\3\2\2\2\u0090\u008f"+
		"\3\2\2\2\u0091\u0093\3\2\2\2\u0092\u008d\3\2\2\2\u0093\u0096\3\2\2\2\u0094"+
		"\u0092\3\2\2\2\u0094\u0095\3\2\2\2\u0095\13\3\2\2\2\u0096\u0094\3\2\2"+
		"\2\u0097\u0098\7+\2\2\u0098\u0099\5N(\2\u0099\u009f\7\5\2\2\u009a\u009b"+
		"\5\24\13\2\u009b\u009c\7\3\2\2\u009c\u009e\3\2\2\2\u009d\u009a\3\2\2\2"+
		"\u009e\u00a1\3\2\2\2\u009f\u00a0\3\2\2\2\u009f\u009d\3\2\2\2\u00a0\u00a2"+
		"\3\2\2\2\u00a1\u009f\3\2\2\2\u00a2\u00a3\7\6\2\2\u00a3\r\3\2\2\2\u00a4"+
		"\u00a5\7/\2\2\u00a5\u00a6\5N(\2\u00a6\u00a7\7\7\2\2\u00a7\u00a8\5P)\2"+
		"\u00a8\u00a9\7\b\2\2\u00a9\u00aa\5&\24\2\u00aa\17\3\2\2\2\u00ab\u00ac"+
		"\7\60\2\2\u00ac\u00ad\5N(\2\u00ad\u00ae\7\7\2\2\u00ae\u00af\5P)\2\u00af"+
		"\u00b0\7\b\2\2\u00b0\u00b1\5&\24\2\u00b1\21\3\2\2\2\u00b2\u00b3\7\60\2"+
		"\2\u00b3\u00b8\5N(\2\u00b4\u00b5\7\4\2\2\u00b5\u00b7\5N(\2\u00b6\u00b4"+
		"\3\2\2\2\u00b7\u00ba\3\2\2\2\u00b8\u00b9\3\2\2\2\u00b8\u00b6\3\2\2\2\u00b9"+
		"\u00bb\3\2\2\2\u00ba\u00b8\3\2\2\2\u00bb\u00bc\7\7\2\2\u00bc\u00bd\5P"+
		")\2\u00bd\u00be\7\b\2\2\u00be\23\3\2\2\2\u00bf\u00c0\7%\2\2\u00c0\u00c3"+
		"\5J&\2\u00c1\u00c4\5 \21\2\u00c2\u00c4\5\36\20\2\u00c3\u00c1\3\2\2\2\u00c3"+
		"\u00c2\3\2\2\2\u00c4\u00cd\3\2\2\2\u00c5\u00c9\7\4\2\2\u00c6\u00ca\5\24"+
		"\13\2\u00c7\u00ca\5 \21\2\u00c8\u00ca\5\36\20\2\u00c9\u00c6\3\2\2\2\u00c9"+
		"\u00c7\3\2\2\2\u00c9\u00c8\3\2\2\2\u00ca\u00cc\3\2\2\2\u00cb\u00c5\3\2"+
		"\2\2\u00cc\u00cf\3\2\2\2\u00cd\u00cb\3\2\2\2\u00cd\u00ce\3\2\2\2\u00ce"+
		"\25\3\2\2\2\u00cf\u00cd\3\2\2\2\u00d0\u00d1\5N(\2\u00d1\u00d2\7\t\2\2"+
		"\u00d2\u00d3\5B\"\2\u00d3\u00d4\7\n\2\2\u00d4\u00d5\5\30\r\2\u00d5\27"+
		"\3\2\2\2\u00d6\u00d7\7\13\2\2\u00d7\u00d8\7\5\2\2\u00d8\u00dd\5<\37\2"+
		"\u00d9\u00da\7\4\2\2\u00da\u00dc\5<\37\2\u00db\u00d9\3\2\2\2\u00dc\u00df"+
		"\3\2\2\2\u00dd\u00de\3\2\2\2\u00dd\u00db\3\2\2\2\u00de\u00e0\3\2\2\2\u00df"+
		"\u00dd\3\2\2\2\u00e0\u00e1\7\6\2\2\u00e1\31\3\2\2\2\u00e2\u00e3\5N(\2"+
		"\u00e3\u00e4\5\34\17\2\u00e4\33\3\2\2\2\u00e5\u00e6\7\13\2\2\u00e6\u00e7"+
		"\5<\37\2\u00e7\35\3\2\2\2\u00e8\u00e9\5N(\2\u00e9\u00ea\7\t\2\2\u00ea"+
		"\u00eb\5B\"\2\u00eb\u00ec\7\n\2\2\u00ec\37\3\2\2\2\u00ed\u00ee\5N(\2\u00ee"+
		"!\3\2\2\2\u00ef\u00f8\7\7\2\2\u00f0\u00f5\5$\23\2\u00f1\u00f2\7\4\2\2"+
		"\u00f2\u00f4\5$\23\2\u00f3\u00f1\3\2\2\2\u00f4\u00f7\3\2\2\2\u00f5\u00f6"+
		"\3\2\2\2\u00f5\u00f3\3\2\2\2\u00f6\u00f9\3\2\2\2\u00f7\u00f5\3\2\2\2\u00f8"+
		"\u00f0\3\2\2\2\u00f8\u00f9\3\2\2\2\u00f9\u00fa\3\2\2\2\u00fa\u00fb\7\b"+
		"\2\2\u00fb#\3\2\2\2\u00fc\u00fd\7%\2\2\u00fd\u00fe\5J&\2\u00fe\u0103\5"+
		"N(\2\u00ff\u0100\7\t\2\2\u0100\u0101\5B\"\2\u0101\u0102\7\n\2\2\u0102"+
		"\u0104\3\2\2\2\u0103\u00ff\3\2\2\2\u0103\u0104\3\2\2\2\u0104%\3\2\2\2"+
		"\u0105\u0111\7\5\2\2\u0106\u0107\5(\25\2\u0107\u0108\7\3\2\2\u0108\u010e"+
		"\3\2\2\2\u0109\u010b\5\66\34\2\u010a\u010c\7\3\2\2\u010b\u010a\3\2\2\2"+
		"\u010b\u010c\3\2\2\2\u010c\u010e\3\2\2\2\u010d\u0106\3\2\2\2\u010d\u0109"+
		"\3\2\2\2\u010e\u0110\3\2\2\2\u010f\u010d\3\2\2\2\u0110\u0113\3\2\2\2\u0111"+
		"\u0112\3\2\2\2\u0111\u010f\3\2\2\2\u0112\u0114\3\2\2\2\u0113\u0111\3\2"+
		"\2\2\u0114\u0115\7\6\2\2\u0115\'\3\2\2\2\u0116\u011d\5,\27\2\u0117\u011d"+
		"\58\35\2\u0118\u011d\5\n\6\2\u0119\u011d\5\24\13\2\u011a\u011d\5*\26\2"+
		"\u011b\u011d\5> \2\u011c\u0116\3\2\2\2\u011c\u0117\3\2\2\2\u011c\u0118"+
		"\3\2\2\2\u011c\u0119\3\2\2\2\u011c\u011a\3\2\2\2\u011c\u011b\3\2\2\2\u011d"+
		")\3\2\2\2\u011e\u011f\5N(\2\u011f\u0128\7\7\2\2\u0120\u0125\5:\36\2\u0121"+
		"\u0122\7\4\2\2\u0122\u0124\5:\36\2\u0123\u0121\3\2\2\2\u0124\u0127\3\2"+
		"\2\2\u0125\u0126\3\2\2\2\u0125\u0123\3\2\2\2\u0126\u0129\3\2\2\2\u0127"+
		"\u0125\3\2\2\2\u0128\u0120\3\2\2\2\u0128\u0129\3\2\2\2\u0129\u012a\3\2"+
		"\2\2\u012a\u012b\7\b\2\2\u012b+\3\2\2\2\u012c\u012d\5H%\2\u012d\u012e"+
		"\5R*\2\u012e\u012f\5<\37\2\u012f-\3\2\2\2\u0130\u0131\5<\37\2\u0131/\3"+
		"\2\2\2\u0132\u0133\7(\2\2\u0133\u0134\5&\24\2\u0134\61\3\2\2\2\u0135\u0136"+
		"\7(\2\2\u0136\u0137\7&\2\2\u0137\u0138\5.\30\2\u0138\u0139\5&\24\2\u0139"+
		"\63\3\2\2\2\u013a\u013b\7&\2\2\u013b\u013c\5.\30\2\u013c\u013d\5&\24\2"+
		"\u013d\65\3\2\2\2\u013e\u0142\5\64\33\2\u013f\u0141\5\62\32\2\u0140\u013f"+
		"\3\2\2\2\u0141\u0144\3\2\2\2\u0142\u0143\3\2\2\2\u0142\u0140\3\2\2\2\u0143"+
		"\u0146\3\2\2\2\u0144\u0142\3\2\2\2\u0145\u0147\5\60\31\2\u0146\u0145\3"+
		"\2\2\2\u0146\u0147\3\2\2\2\u0147\67\3\2\2\2\u0148\u014a\7-\2\2\u0149\u014b"+
		"\5<\37\2\u014a\u0149\3\2\2\2\u014a\u014b\3\2\2\2\u014b9\3\2\2\2\u014c"+
		"\u014d\5<\37\2\u014d;\3\2\2\2\u014e\u014f\5> \2\u014f=\3\2\2\2\u0150\u0151"+
		"\b \1\2\u0151\u0152\7\7\2\2\u0152\u0153\5> \2\u0153\u0154\7\b\2\2\u0154"+
		"\u015a\3\2\2\2\u0155\u0156\5\\/\2\u0156\u0157\5> \r\u0157\u015a\3\2\2"+
		"\2\u0158\u015a\5D#\2\u0159\u0150\3\2\2\2\u0159\u0155\3\2\2\2\u0159\u0158"+
		"\3\2\2\2\u015a\u0181\3\2\2\2\u015b\u015c\f\f\2\2\u015c\u015d\5^\60\2\u015d"+
		"\u015e\5> \r\u015e\u0180\3\2\2\2\u015f\u0160\f\13\2\2\u0160\u0161\5T+"+
		"\2\u0161\u0162\5> \f\u0162\u0180\3\2\2\2\u0163\u0164\f\n\2\2\u0164\u0165"+
		"\5V,\2\u0165\u0166\5> \13\u0166\u0180\3\2\2\2\u0167\u0168\f\t\2\2\u0168"+
		"\u0169\5X-\2\u0169\u016a\5> \n\u016a\u0180\3\2\2\2\u016b\u016c\f\b\2\2"+
		"\u016c\u016d\5Z.\2\u016d\u016e\5> \t\u016e\u0180\3\2\2\2\u016f\u0170\f"+
		"\7\2\2\u0170\u0171\5`\61\2\u0171\u0172\5> \b\u0172\u0180\3\2\2\2\u0173"+
		"\u0174\f\6\2\2\u0174\u0175\5b\62\2\u0175\u0176\5> \7\u0176\u0180\3\2\2"+
		"\2\u0177\u0178\f\5\2\2\u0178\u0179\5d\63\2\u0179\u017a\5> \6\u017a\u0180"+
		"\3\2\2\2\u017b\u017c\f\4\2\2\u017c\u017d\5f\64\2\u017d\u017e\5> \5\u017e"+
		"\u0180\3\2\2\2\u017f\u015b\3\2\2\2\u017f\u015f\3\2\2\2\u017f\u0163\3\2"+
		"\2\2\u017f\u0167\3\2\2\2\u017f\u016b\3\2\2\2\u017f\u016f\3\2\2\2\u017f"+
		"\u0173\3\2\2\2\u017f\u0177\3\2\2\2\u017f\u017b\3\2\2\2\u0180\u0183\3\2"+
		"\2\2\u0181\u017f\3\2\2\2\u0181\u0182\3\2\2\2\u0182?\3\2\2\2\u0183\u0181"+
		"\3\2\2\2\u0184\u0187\7\64\2\2\u0185\u0187\5F$\2\u0186\u0184\3\2\2\2\u0186"+
		"\u0185\3\2\2\2\u0187A\3\2\2\2\u0188\u018b\7\64\2\2\u0189\u018b\5F$\2\u018a"+
		"\u0188\3\2\2\2\u018a\u0189\3\2\2\2\u018bC\3\2\2\2\u018c\u0195\7\64\2\2"+
		"\u018d\u0195\7\65\2\2\u018e\u0195\7\66\2\2\u018f\u0195\7\61\2\2\u0190"+
		"\u0195\7\62\2\2\u0191\u0195\5*\26\2\u0192\u0195\5H%\2\u0193\u0195\5N("+
		"\2\u0194\u018c\3\2\2\2\u0194\u018d\3\2\2\2\u0194\u018e\3\2\2\2\u0194\u018f"+
		"\3\2\2\2\u0194\u0190\3\2\2\2\u0194\u0191\3\2\2\2\u0194\u0192\3\2\2\2\u0194"+
		"\u0193\3\2\2\2\u0195E\3\2\2\2\u0196\u019b\5N(\2\u0197\u0198\7\t\2\2\u0198"+
		"\u0199\5@!\2\u0199\u019a\7\n\2\2\u019a\u019c\3\2\2\2\u019b\u0197\3\2\2"+
		"\2\u019b\u019c\3\2\2\2\u019cG\3\2\2\2\u019d\u01a0\5F$\2\u019e\u019f\7"+
		"\f\2\2\u019f\u01a1\5F$\2\u01a0\u019e\3\2\2\2\u01a0\u01a1\3\2\2\2\u01a1"+
		"I\3\2\2\2\u01a2\u01a3\t\2\2\2\u01a3K\3\2\2\2\u01a4\u01a5\t\3\2\2\u01a5"+
		"M\3\2\2\2\u01a6\u01a7\5L\'\2\u01a7O\3\2\2\2\u01a8\u01a9\7\63\2\2\u01a9"+
		"Q\3\2\2\2\u01aa\u01ab\t\4\2\2\u01abS\3\2\2\2\u01ac\u01ad\t\5\2\2\u01ad"+
		"U\3\2\2\2\u01ae\u01af\t\6\2\2\u01afW\3\2\2\2\u01b0\u01b1\t\7\2\2\u01b1"+
		"Y\3\2\2\2\u01b2\u01b3\t\b\2\2\u01b3[\3\2\2\2\u01b4\u01b5\t\t\2\2\u01b5"+
		"]\3\2\2\2\u01b6\u01b7\t\n\2\2\u01b7_\3\2\2\2\u01b8\u01b9\7 \2\2\u01b9"+
		"a\3\2\2\2\u01ba\u01bb\7!\2\2\u01bbc\3\2\2\2\u01bc\u01bd\7\"\2\2\u01bd"+
		"e\3\2\2\2\u01be\u01bf\7#\2\2\u01bfg\3\2\2\2$jlux}\u008b\u0090\u0094\u009f"+
		"\u00b8\u00c3\u00c9\u00cd\u00dd\u00f5\u00f8\u0103\u010b\u010d\u0111\u011c"+
		"\u0125\u0128\u0142\u0146\u014a\u0159\u017f\u0181\u0186\u018a\u0194\u019b"+
		"\u01a0";
	public static final ATN _ATN =
		new ATNDeserializer().deserialize(_serializedATN.toCharArray());
	static {
		_decisionToDFA = new DFA[_ATN.getNumberOfDecisions()];
		for (int i = 0; i < _ATN.getNumberOfDecisions(); i++) {
			_decisionToDFA[i] = new DFA(_ATN.getDecisionState(i), i);
		}
	}
}