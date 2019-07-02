// Generated from b:\Source\DaedalusLanguageServer\Parser\Daedalus.g4 by ANTLR 4.7.1
import org.antlr.v4.runtime.Lexer;
import org.antlr.v4.runtime.CharStream;
import org.antlr.v4.runtime.Token;
import org.antlr.v4.runtime.TokenStream;
import org.antlr.v4.runtime.*;
import org.antlr.v4.runtime.atn.*;
import org.antlr.v4.runtime.dfa.DFA;
import org.antlr.v4.runtime.misc.*;

@SuppressWarnings({"all", "warnings", "unchecked", "unused", "cast"})
public class DaedalusLexer extends Lexer {
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
	public static String[] channelNames = {
		"DEFAULT_TOKEN_CHANNEL", "HIDDEN"
	};

	public static String[] modeNames = {
		"DEFAULT_MODE"
	};

	public static final String[] ruleNames = {
		"T__0", "T__1", "T__2", "T__3", "T__4", "T__5", "T__6", "T__7", "T__8", 
		"T__9", "T__10", "T__11", "T__12", "T__13", "T__14", "T__15", "T__16", 
		"T__17", "T__18", "T__19", "T__20", "T__21", "T__22", "T__23", "T__24", 
		"T__25", "T__26", "T__27", "T__28", "T__29", "T__30", "T__31", "T__32", 
		"Const", "Var", "If", "Int", "Else", "Func", "String", "Class", "Void", 
		"Return", "Float", "Prototype", "Instance", "Null", "NoFunc", "Identifier", 
		"IntegerLiteral", "FloatLiteral", "StringLiteral", "Whitespace", "Newline", 
		"BlockComment", "LineComment", "IdStart", "IdContinue", "IdSpecial", "GermanCharacter", 
		"Digit", "PointFloat", "ExponentFloat", "Exponent"
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


	public DaedalusLexer(CharStream input) {
		super(input);
		_interp = new LexerATNSimulator(this,_ATN,_decisionToDFA,_sharedContextCache);
	}

	@Override
	public String getGrammarFileName() { return "Daedalus.g4"; }

	@Override
	public String[] getRuleNames() { return ruleNames; }

	@Override
	public String getSerializedATN() { return _serializedATN; }

	@Override
	public String[] getChannelNames() { return channelNames; }

	@Override
	public String[] getModeNames() { return modeNames; }

	@Override
	public ATN getATN() { return _ATN; }

	public static final String _serializedATN =
		"\3\u608b\ua72a\u8133\ub9ed\u417c\u3be7\u7786\u5964\2:\u020b\b\1\4\2\t"+
		"\2\4\3\t\3\4\4\t\4\4\5\t\5\4\6\t\6\4\7\t\7\4\b\t\b\4\t\t\t\4\n\t\n\4\13"+
		"\t\13\4\f\t\f\4\r\t\r\4\16\t\16\4\17\t\17\4\20\t\20\4\21\t\21\4\22\t\22"+
		"\4\23\t\23\4\24\t\24\4\25\t\25\4\26\t\26\4\27\t\27\4\30\t\30\4\31\t\31"+
		"\4\32\t\32\4\33\t\33\4\34\t\34\4\35\t\35\4\36\t\36\4\37\t\37\4 \t \4!"+
		"\t!\4\"\t\"\4#\t#\4$\t$\4%\t%\4&\t&\4\'\t\'\4(\t(\4)\t)\4*\t*\4+\t+\4"+
		",\t,\4-\t-\4.\t.\4/\t/\4\60\t\60\4\61\t\61\4\62\t\62\4\63\t\63\4\64\t"+
		"\64\4\65\t\65\4\66\t\66\4\67\t\67\48\t8\49\t9\4:\t:\4;\t;\4<\t<\4=\t="+
		"\4>\t>\4?\t?\4@\t@\4A\tA\3\2\3\2\3\3\3\3\3\4\3\4\3\5\3\5\3\6\3\6\3\7\3"+
		"\7\3\b\3\b\3\t\3\t\3\n\3\n\3\13\3\13\3\f\3\f\3\f\3\r\3\r\3\r\3\16\3\16"+
		"\3\16\3\17\3\17\3\17\3\20\3\20\3\21\3\21\3\22\3\22\3\22\3\23\3\23\3\23"+
		"\3\24\3\24\3\25\3\25\3\26\3\26\3\26\3\27\3\27\3\27\3\30\3\30\3\30\3\31"+
		"\3\31\3\31\3\32\3\32\3\33\3\33\3\34\3\34\3\35\3\35\3\36\3\36\3\37\3\37"+
		"\3 \3 \3!\3!\3!\3\"\3\"\3\"\3#\3#\3#\3#\3#\3#\3#\3#\3#\3#\5#\u00dc\n#"+
		"\3$\3$\3$\3$\3$\3$\5$\u00e4\n$\3%\3%\3%\3%\5%\u00ea\n%\3&\3&\3&\3&\3&"+
		"\3&\5&\u00f2\n&\3\'\3\'\3\'\3\'\3\'\3\'\3\'\3\'\5\'\u00fc\n\'\3(\3(\3"+
		"(\3(\3(\3(\3(\3(\5(\u0106\n(\3)\3)\3)\3)\3)\3)\3)\3)\3)\3)\3)\3)\5)\u0114"+
		"\n)\3*\3*\3*\3*\3*\3*\3*\3*\3*\3*\5*\u0120\n*\3+\3+\3+\3+\3+\3+\3+\3+"+
		"\5+\u012a\n+\3,\3,\3,\3,\3,\3,\3,\3,\3,\3,\3,\3,\5,\u0138\n,\3-\3-\3-"+
		"\3-\3-\3-\3-\3-\3-\3-\5-\u0144\n-\3.\3.\3.\3.\3.\3.\3.\3.\3.\3.\3.\3."+
		"\3.\3.\3.\3.\3.\3.\5.\u0158\n.\3/\3/\3/\3/\3/\3/\3/\3/\3/\3/\3/\3/\3/"+
		"\3/\3/\3/\5/\u016a\n/\3\60\3\60\3\60\3\60\3\60\3\60\3\60\3\60\3\60\3\60"+
		"\3\60\3\60\5\60\u0178\n\60\3\61\3\61\3\61\3\61\3\61\3\61\3\61\3\61\3\61"+
		"\3\61\3\61\3\61\3\61\3\61\3\61\3\61\3\61\3\61\5\61\u018c\n\61\3\62\3\62"+
		"\7\62\u0190\n\62\f\62\16\62\u0193\13\62\3\63\6\63\u0196\n\63\r\63\16\63"+
		"\u0197\3\64\3\64\5\64\u019c\n\64\3\65\3\65\3\65\3\65\3\65\5\65\u01a3\n"+
		"\65\7\65\u01a5\n\65\f\65\16\65\u01a8\13\65\3\65\3\65\3\66\6\66\u01ad\n"+
		"\66\r\66\16\66\u01ae\3\66\3\66\3\67\3\67\5\67\u01b5\n\67\3\67\5\67\u01b8"+
		"\n\67\3\67\3\67\38\38\38\38\78\u01c0\n8\f8\168\u01c3\138\38\38\38\38\3"+
		"8\39\39\39\39\79\u01ce\n9\f9\169\u01d1\139\39\39\3:\3:\5:\u01d7\n:\3;"+
		"\3;\3;\5;\u01dc\n;\3<\3<\3=\3=\3>\3>\3?\7?\u01e5\n?\f?\16?\u01e8\13?\3"+
		"?\3?\6?\u01ec\n?\r?\16?\u01ed\3?\6?\u01f1\n?\r?\16?\u01f2\3?\3?\5?\u01f7"+
		"\n?\3@\6@\u01fa\n@\r@\16@\u01fb\3@\5@\u01ff\n@\3@\3@\3A\3A\5A\u0205\n"+
		"A\3A\6A\u0208\nA\rA\16A\u0209\3\u01c1\2B\3\3\5\4\7\5\t\6\13\7\r\b\17\t"+
		"\21\n\23\13\25\f\27\r\31\16\33\17\35\20\37\21!\22#\23%\24\'\25)\26+\27"+
		"-\30/\31\61\32\63\33\65\34\67\359\36;\37= ?!A\"C#E$G%I&K\'M(O)Q*S+U,W"+
		"-Y.[/]\60_\61a\62c\63e\64g\65i\66k\67m8o9q:s\2u\2w\2y\2{\2}\2\177\2\u0081"+
		"\2\3\2\13\6\2\f\f\17\17$$^^\4\2\13\13\"\"\4\2\f\f\17\17\5\2C\\aac|\4\2"+
		"BB``\6\2\u00e1\u00e1\u00e6\u00e6\u00f8\u00f8\u00fe\u00fe\3\2\62;\4\2G"+
		"Ggg\4\2--//\2\u0229\2\3\3\2\2\2\2\5\3\2\2\2\2\7\3\2\2\2\2\t\3\2\2\2\2"+
		"\13\3\2\2\2\2\r\3\2\2\2\2\17\3\2\2\2\2\21\3\2\2\2\2\23\3\2\2\2\2\25\3"+
		"\2\2\2\2\27\3\2\2\2\2\31\3\2\2\2\2\33\3\2\2\2\2\35\3\2\2\2\2\37\3\2\2"+
		"\2\2!\3\2\2\2\2#\3\2\2\2\2%\3\2\2\2\2\'\3\2\2\2\2)\3\2\2\2\2+\3\2\2\2"+
		"\2-\3\2\2\2\2/\3\2\2\2\2\61\3\2\2\2\2\63\3\2\2\2\2\65\3\2\2\2\2\67\3\2"+
		"\2\2\29\3\2\2\2\2;\3\2\2\2\2=\3\2\2\2\2?\3\2\2\2\2A\3\2\2\2\2C\3\2\2\2"+
		"\2E\3\2\2\2\2G\3\2\2\2\2I\3\2\2\2\2K\3\2\2\2\2M\3\2\2\2\2O\3\2\2\2\2Q"+
		"\3\2\2\2\2S\3\2\2\2\2U\3\2\2\2\2W\3\2\2\2\2Y\3\2\2\2\2[\3\2\2\2\2]\3\2"+
		"\2\2\2_\3\2\2\2\2a\3\2\2\2\2c\3\2\2\2\2e\3\2\2\2\2g\3\2\2\2\2i\3\2\2\2"+
		"\2k\3\2\2\2\2m\3\2\2\2\2o\3\2\2\2\2q\3\2\2\2\3\u0083\3\2\2\2\5\u0085\3"+
		"\2\2\2\7\u0087\3\2\2\2\t\u0089\3\2\2\2\13\u008b\3\2\2\2\r\u008d\3\2\2"+
		"\2\17\u008f\3\2\2\2\21\u0091\3\2\2\2\23\u0093\3\2\2\2\25\u0095\3\2\2\2"+
		"\27\u0097\3\2\2\2\31\u009a\3\2\2\2\33\u009d\3\2\2\2\35\u00a0\3\2\2\2\37"+
		"\u00a3\3\2\2\2!\u00a5\3\2\2\2#\u00a7\3\2\2\2%\u00aa\3\2\2\2\'\u00ad\3"+
		"\2\2\2)\u00af\3\2\2\2+\u00b1\3\2\2\2-\u00b4\3\2\2\2/\u00b7\3\2\2\2\61"+
		"\u00ba\3\2\2\2\63\u00bd\3\2\2\2\65\u00bf\3\2\2\2\67\u00c1\3\2\2\29\u00c3"+
		"\3\2\2\2;\u00c5\3\2\2\2=\u00c7\3\2\2\2?\u00c9\3\2\2\2A\u00cb\3\2\2\2C"+
		"\u00ce\3\2\2\2E\u00db\3\2\2\2G\u00e3\3\2\2\2I\u00e9\3\2\2\2K\u00f1\3\2"+
		"\2\2M\u00fb\3\2\2\2O\u0105\3\2\2\2Q\u0113\3\2\2\2S\u011f\3\2\2\2U\u0129"+
		"\3\2\2\2W\u0137\3\2\2\2Y\u0143\3\2\2\2[\u0157\3\2\2\2]\u0169\3\2\2\2_"+
		"\u0177\3\2\2\2a\u018b\3\2\2\2c\u018d\3\2\2\2e\u0195\3\2\2\2g\u019b\3\2"+
		"\2\2i\u019d\3\2\2\2k\u01ac\3\2\2\2m\u01b7\3\2\2\2o\u01bb\3\2\2\2q\u01c9"+
		"\3\2\2\2s\u01d6\3\2\2\2u\u01db\3\2\2\2w\u01dd\3\2\2\2y\u01df\3\2\2\2{"+
		"\u01e1\3\2\2\2}\u01f6\3\2\2\2\177\u01fe\3\2\2\2\u0081\u0202\3\2\2\2\u0083"+
		"\u0084\7=\2\2\u0084\4\3\2\2\2\u0085\u0086\7.\2\2\u0086\6\3\2\2\2\u0087"+
		"\u0088\7}\2\2\u0088\b\3\2\2\2\u0089\u008a\7\177\2\2\u008a\n\3\2\2\2\u008b"+
		"\u008c\7*\2\2\u008c\f\3\2\2\2\u008d\u008e\7+\2\2\u008e\16\3\2\2\2\u008f"+
		"\u0090\7]\2\2\u0090\20\3\2\2\2\u0091\u0092\7_\2\2\u0092\22\3\2\2\2\u0093"+
		"\u0094\7?\2\2\u0094\24\3\2\2\2\u0095\u0096\7\60\2\2\u0096\26\3\2\2\2\u0097"+
		"\u0098\7-\2\2\u0098\u0099\7?\2\2\u0099\30\3\2\2\2\u009a\u009b\7/\2\2\u009b"+
		"\u009c\7?\2\2\u009c\32\3\2\2\2\u009d\u009e\7,\2\2\u009e\u009f\7?\2\2\u009f"+
		"\34\3\2\2\2\u00a0\u00a1\7\61\2\2\u00a1\u00a2\7?\2\2\u00a2\36\3\2\2\2\u00a3"+
		"\u00a4\7-\2\2\u00a4 \3\2\2\2\u00a5\u00a6\7/\2\2\u00a6\"\3\2\2\2\u00a7"+
		"\u00a8\7>\2\2\u00a8\u00a9\7>\2\2\u00a9$\3\2\2\2\u00aa\u00ab\7@\2\2\u00ab"+
		"\u00ac\7@\2\2\u00ac&\3\2\2\2\u00ad\u00ae\7>\2\2\u00ae(\3\2\2\2\u00af\u00b0"+
		"\7@\2\2\u00b0*\3\2\2\2\u00b1\u00b2\7>\2\2\u00b2\u00b3\7?\2\2\u00b3,\3"+
		"\2\2\2\u00b4\u00b5\7@\2\2\u00b5\u00b6\7?\2\2\u00b6.\3\2\2\2\u00b7\u00b8"+
		"\7?\2\2\u00b8\u00b9\7?\2\2\u00b9\60\3\2\2\2\u00ba\u00bb\7#\2\2\u00bb\u00bc"+
		"\7?\2\2\u00bc\62\3\2\2\2\u00bd\u00be\7#\2\2\u00be\64\3\2\2\2\u00bf\u00c0"+
		"\7\u0080\2\2\u00c0\66\3\2\2\2\u00c1\u00c2\7,\2\2\u00c28\3\2\2\2\u00c3"+
		"\u00c4\7\61\2\2\u00c4:\3\2\2\2\u00c5\u00c6\7\'\2\2\u00c6<\3\2\2\2\u00c7"+
		"\u00c8\7(\2\2\u00c8>\3\2\2\2\u00c9\u00ca\7~\2\2\u00ca@\3\2\2\2\u00cb\u00cc"+
		"\7(\2\2\u00cc\u00cd\7(\2\2\u00cdB\3\2\2\2\u00ce\u00cf\7~\2\2\u00cf\u00d0"+
		"\7~\2\2\u00d0D\3\2\2\2\u00d1\u00d2\7e\2\2\u00d2\u00d3\7q\2\2\u00d3\u00d4"+
		"\7p\2\2\u00d4\u00d5\7u\2\2\u00d5\u00dc\7v\2\2\u00d6\u00d7\7E\2\2\u00d7"+
		"\u00d8\7Q\2\2\u00d8\u00d9\7P\2\2\u00d9\u00da\7U\2\2\u00da\u00dc\7V\2\2"+
		"\u00db\u00d1\3\2\2\2\u00db\u00d6\3\2\2\2\u00dcF\3\2\2\2\u00dd\u00de\7"+
		"x\2\2\u00de\u00df\7c\2\2\u00df\u00e4\7t\2\2\u00e0\u00e1\7X\2\2\u00e1\u00e2"+
		"\7C\2\2\u00e2\u00e4\7T\2\2\u00e3\u00dd\3\2\2\2\u00e3\u00e0\3\2\2\2\u00e4"+
		"H\3\2\2\2\u00e5\u00e6\7k\2\2\u00e6\u00ea\7h\2\2\u00e7\u00e8\7K\2\2\u00e8"+
		"\u00ea\7H\2\2\u00e9\u00e5\3\2\2\2\u00e9\u00e7\3\2\2\2\u00eaJ\3\2\2\2\u00eb"+
		"\u00ec\7k\2\2\u00ec\u00ed\7p\2\2\u00ed\u00f2\7v\2\2\u00ee\u00ef\7K\2\2"+
		"\u00ef\u00f0\7P\2\2\u00f0\u00f2\7V\2\2\u00f1\u00eb\3\2\2\2\u00f1\u00ee"+
		"\3\2\2\2\u00f2L\3\2\2\2\u00f3\u00f4\7g\2\2\u00f4\u00f5\7n\2\2\u00f5\u00f6"+
		"\7u\2\2\u00f6\u00fc\7g\2\2\u00f7\u00f8\7G\2\2\u00f8\u00f9\7N\2\2\u00f9"+
		"\u00fa\7U\2\2\u00fa\u00fc\7G\2\2\u00fb\u00f3\3\2\2\2\u00fb\u00f7\3\2\2"+
		"\2\u00fcN\3\2\2\2\u00fd\u00fe\7h\2\2\u00fe\u00ff\7w\2\2\u00ff\u0100\7"+
		"p\2\2\u0100\u0106\7e\2\2\u0101\u0102\7H\2\2\u0102\u0103\7W\2\2\u0103\u0104"+
		"\7P\2\2\u0104\u0106\7E\2\2\u0105\u00fd\3\2\2\2\u0105\u0101\3\2\2\2\u0106"+
		"P\3\2\2\2\u0107\u0108\7u\2\2\u0108\u0109\7v\2\2\u0109\u010a\7t\2\2\u010a"+
		"\u010b\7k\2\2\u010b\u010c\7p\2\2\u010c\u0114\7i\2\2\u010d\u010e\7U\2\2"+
		"\u010e\u010f\7V\2\2\u010f\u0110\7T\2\2\u0110\u0111\7K\2\2\u0111\u0112"+
		"\7P\2\2\u0112\u0114\7I\2\2\u0113\u0107\3\2\2\2\u0113\u010d\3\2\2\2\u0114"+
		"R\3\2\2\2\u0115\u0116\7e\2\2\u0116\u0117\7n\2\2\u0117\u0118\7c\2\2\u0118"+
		"\u0119\7u\2\2\u0119\u0120\7u\2\2\u011a\u011b\7E\2\2\u011b\u011c\7N\2\2"+
		"\u011c\u011d\7C\2\2\u011d\u011e\7U\2\2\u011e\u0120\7U\2\2\u011f\u0115"+
		"\3\2\2\2\u011f\u011a\3\2\2\2\u0120T\3\2\2\2\u0121\u0122\7x\2\2\u0122\u0123"+
		"\7q\2\2\u0123\u0124\7k\2\2\u0124\u012a\7f\2\2\u0125\u0126\7X\2\2\u0126"+
		"\u0127\7Q\2\2\u0127\u0128\7K\2\2\u0128\u012a\7F\2\2\u0129\u0121\3\2\2"+
		"\2\u0129\u0125\3\2\2\2\u012aV\3\2\2\2\u012b\u012c\7t\2\2\u012c\u012d\7"+
		"g\2\2\u012d\u012e\7v\2\2\u012e\u012f\7w\2\2\u012f\u0130\7t\2\2\u0130\u0138"+
		"\7p\2\2\u0131\u0132\7T\2\2\u0132\u0133\7G\2\2\u0133\u0134\7V\2\2\u0134"+
		"\u0135\7W\2\2\u0135\u0136\7T\2\2\u0136\u0138\7P\2\2\u0137\u012b\3\2\2"+
		"\2\u0137\u0131\3\2\2\2\u0138X\3\2\2\2\u0139\u013a\7h\2\2\u013a\u013b\7"+
		"n\2\2\u013b\u013c\7q\2\2\u013c\u013d\7c\2\2\u013d\u0144\7v\2\2\u013e\u013f"+
		"\7H\2\2\u013f\u0140\7N\2\2\u0140\u0141\7Q\2\2\u0141\u0142\7C\2\2\u0142"+
		"\u0144\7V\2\2\u0143\u0139\3\2\2\2\u0143\u013e\3\2\2\2\u0144Z\3\2\2\2\u0145"+
		"\u0146\7r\2\2\u0146\u0147\7t\2\2\u0147\u0148\7q\2\2\u0148\u0149\7v\2\2"+
		"\u0149\u014a\7q\2\2\u014a\u014b\7v\2\2\u014b\u014c\7{\2\2\u014c\u014d"+
		"\7r\2\2\u014d\u0158\7g\2\2\u014e\u014f\7R\2\2\u014f\u0150\7T\2\2\u0150"+
		"\u0151\7Q\2\2\u0151\u0152\7V\2\2\u0152\u0153\7Q\2\2\u0153\u0154\7V\2\2"+
		"\u0154\u0155\7[\2\2\u0155\u0156\7R\2\2\u0156\u0158\7G\2\2\u0157\u0145"+
		"\3\2\2\2\u0157\u014e\3\2\2\2\u0158\\\3\2\2\2\u0159\u015a\7k\2\2\u015a"+
		"\u015b\7p\2\2\u015b\u015c\7u\2\2\u015c\u015d\7v\2\2\u015d\u015e\7c\2\2"+
		"\u015e\u015f\7p\2\2\u015f\u0160\7e\2\2\u0160\u016a\7g\2\2\u0161\u0162"+
		"\7K\2\2\u0162\u0163\7P\2\2\u0163\u0164\7U\2\2\u0164\u0165\7V\2\2\u0165"+
		"\u0166\7C\2\2\u0166\u0167\7P\2\2\u0167\u0168\7E\2\2\u0168\u016a\7G\2\2"+
		"\u0169\u0159\3\2\2\2\u0169\u0161\3\2\2\2\u016a^\3\2\2\2\u016b\u016c\7"+
		"p\2\2\u016c\u016d\7w\2\2\u016d\u016e\7n\2\2\u016e\u0178\7n\2\2\u016f\u0170"+
		"\7P\2\2\u0170\u0171\7w\2\2\u0171\u0172\7n\2\2\u0172\u0178\7n\2\2\u0173"+
		"\u0174\7P\2\2\u0174\u0175\7W\2\2\u0175\u0176\7N\2\2\u0176\u0178\7N\2\2"+
		"\u0177\u016b\3\2\2\2\u0177\u016f\3\2\2\2\u0177\u0173\3\2\2\2\u0178`\3"+
		"\2\2\2\u0179\u017a\7p\2\2\u017a\u017b\7q\2\2\u017b\u017c\7h\2\2\u017c"+
		"\u017d\7w\2\2\u017d\u017e\7p\2\2\u017e\u018c\7e\2\2\u017f\u0180\7P\2\2"+
		"\u0180\u0181\7q\2\2\u0181\u0182\7H\2\2\u0182\u0183\7w\2\2\u0183\u0184"+
		"\7p\2\2\u0184\u018c\7e\2\2\u0185\u0186\7P\2\2\u0186\u0187\7Q\2\2\u0187"+
		"\u0188\7H\2\2\u0188\u0189\7W\2\2\u0189\u018a\7P\2\2\u018a\u018c\7E\2\2"+
		"\u018b\u0179\3\2\2\2\u018b\u017f\3\2\2\2\u018b\u0185\3\2\2\2\u018cb\3"+
		"\2\2\2\u018d\u0191\5s:\2\u018e\u0190\5u;\2\u018f\u018e\3\2\2\2\u0190\u0193"+
		"\3\2\2\2\u0191\u018f\3\2\2\2\u0191\u0192\3\2\2\2\u0192d\3\2\2\2\u0193"+
		"\u0191\3\2\2\2\u0194\u0196\5{>\2\u0195\u0194\3\2\2\2\u0196\u0197\3\2\2"+
		"\2\u0197\u0195\3\2\2\2\u0197\u0198\3\2\2\2\u0198f\3\2\2\2\u0199\u019c"+
		"\5}?\2\u019a\u019c\5\177@\2\u019b\u0199\3\2\2\2\u019b\u019a\3\2\2\2\u019c"+
		"h\3\2\2\2\u019d\u01a6\7$\2\2\u019e\u01a5\n\2\2\2\u019f\u01a2\7^\2\2\u01a0"+
		"\u01a3\13\2\2\2\u01a1\u01a3\7\2\2\3\u01a2\u01a0\3\2\2\2\u01a2\u01a1\3"+
		"\2\2\2\u01a3\u01a5\3\2\2\2\u01a4\u019e\3\2\2\2\u01a4\u019f\3\2\2\2\u01a5"+
		"\u01a8\3\2\2\2\u01a6\u01a4\3\2\2\2\u01a6\u01a7\3\2\2\2\u01a7\u01a9\3\2"+
		"\2\2\u01a8\u01a6\3\2\2\2\u01a9\u01aa\7$\2\2\u01aaj\3\2\2\2\u01ab\u01ad"+
		"\t\3\2\2\u01ac\u01ab\3\2\2\2\u01ad\u01ae\3\2\2\2\u01ae\u01ac\3\2\2\2\u01ae"+
		"\u01af\3\2\2\2\u01af\u01b0\3\2\2\2\u01b0\u01b1\b\66\2\2\u01b1l\3\2\2\2"+
		"\u01b2\u01b4\7\17\2\2\u01b3\u01b5\7\f\2\2\u01b4\u01b3\3\2\2\2\u01b4\u01b5"+
		"\3\2\2\2\u01b5\u01b8\3\2\2\2\u01b6\u01b8\7\f\2\2\u01b7\u01b2\3\2\2\2\u01b7"+
		"\u01b6\3\2\2\2\u01b8\u01b9\3\2\2\2\u01b9\u01ba\b\67\2\2\u01ban\3\2\2\2"+
		"\u01bb\u01bc\7\61\2\2\u01bc\u01bd\7,\2\2\u01bd\u01c1\3\2\2\2\u01be\u01c0"+
		"\13\2\2\2\u01bf\u01be\3\2\2\2\u01c0\u01c3\3\2\2\2\u01c1\u01c2\3\2\2\2"+
		"\u01c1\u01bf\3\2\2\2\u01c2\u01c4\3\2\2\2\u01c3\u01c1\3\2\2\2\u01c4\u01c5"+
		"\7,\2\2\u01c5\u01c6\7\61\2\2\u01c6\u01c7\3\2\2\2\u01c7\u01c8\b8\2\2\u01c8"+
		"p\3\2\2\2\u01c9\u01ca\7\61\2\2\u01ca\u01cb\7\61\2\2\u01cb\u01cf\3\2\2"+
		"\2\u01cc\u01ce\n\4\2\2\u01cd\u01cc\3\2\2\2\u01ce\u01d1\3\2\2\2\u01cf\u01cd"+
		"\3\2\2\2\u01cf\u01d0\3\2\2\2\u01d0\u01d2\3\2\2\2\u01d1\u01cf\3\2\2\2\u01d2"+
		"\u01d3\b9\2\2\u01d3r\3\2\2\2\u01d4\u01d7\5y=\2\u01d5\u01d7\t\5\2\2\u01d6"+
		"\u01d4\3\2\2\2\u01d6\u01d5\3\2\2\2\u01d7t\3\2\2\2\u01d8\u01dc\5s:\2\u01d9"+
		"\u01dc\5w<\2\u01da\u01dc\5{>\2\u01db\u01d8\3\2\2\2\u01db\u01d9\3\2\2\2"+
		"\u01db\u01da\3\2\2\2\u01dcv\3\2\2\2\u01dd\u01de\t\6\2\2\u01dex\3\2\2\2"+
		"\u01df\u01e0\t\7\2\2\u01e0z\3\2\2\2\u01e1\u01e2\t\b\2\2\u01e2|\3\2\2\2"+
		"\u01e3\u01e5\5{>\2\u01e4\u01e3\3\2\2\2\u01e5\u01e8\3\2\2\2\u01e6\u01e4"+
		"\3\2\2\2\u01e6\u01e7\3\2\2\2\u01e7\u01e9\3\2\2\2\u01e8\u01e6\3\2\2\2\u01e9"+
		"\u01eb\7\60\2\2\u01ea\u01ec\5{>\2\u01eb\u01ea\3\2\2\2\u01ec\u01ed\3\2"+
		"\2\2\u01ed\u01eb\3\2\2\2\u01ed\u01ee\3\2\2\2\u01ee\u01f7\3\2\2\2\u01ef"+
		"\u01f1\5{>\2\u01f0\u01ef\3\2\2\2\u01f1\u01f2\3\2\2\2\u01f2\u01f0\3\2\2"+
		"\2\u01f2\u01f3\3\2\2\2\u01f3\u01f4\3\2\2\2\u01f4\u01f5\7\60\2\2\u01f5"+
		"\u01f7\3\2\2\2\u01f6\u01e6\3\2\2\2\u01f6\u01f0\3\2\2\2\u01f7~\3\2\2\2"+
		"\u01f8\u01fa\5{>\2\u01f9\u01f8\3\2\2\2\u01fa\u01fb\3\2\2\2\u01fb\u01f9"+
		"\3\2\2\2\u01fb\u01fc\3\2\2\2\u01fc\u01ff\3\2\2\2\u01fd\u01ff\5}?\2\u01fe"+
		"\u01f9\3\2\2\2\u01fe\u01fd\3\2\2\2\u01ff\u0200\3\2\2\2\u0200\u0201\5\u0081"+
		"A\2\u0201\u0080\3\2\2\2\u0202\u0204\t\t\2\2\u0203\u0205\t\n\2\2\u0204"+
		"\u0203\3\2\2\2\u0204\u0205\3\2\2\2\u0205\u0207\3\2\2\2\u0206\u0208\5{"+
		">\2\u0207\u0206\3\2\2\2\u0208\u0209\3\2\2\2\u0209\u0207\3\2\2\2\u0209"+
		"\u020a\3\2\2\2\u020a\u0082\3\2\2\2\'\2\u00db\u00e3\u00e9\u00f1\u00fb\u0105"+
		"\u0113\u011f\u0129\u0137\u0143\u0157\u0169\u0177\u018b\u0191\u0197\u019b"+
		"\u01a2\u01a4\u01a6\u01ae\u01b4\u01b7\u01c1\u01cf\u01d6\u01db\u01e6\u01ed"+
		"\u01f2\u01f6\u01fb\u01fe\u0204\u0209\3\b\2\2";
	public static final ATN _ATN =
		new ATNDeserializer().deserialize(_serializedATN.toCharArray());
	static {
		_decisionToDFA = new DFA[_ATN.getNumberOfDecisions()];
		for (int i = 0; i < _ATN.getNumberOfDecisions(); i++) {
			_decisionToDFA[i] = new DFA(_ATN.getDecisionState(i), i);
		}
	}
}