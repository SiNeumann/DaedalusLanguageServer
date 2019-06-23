@ECHO OFF
PUSHD ..\Parser
REM Change this to antlr command line:
SET ANTLR=java -jar "..\scripts\antlr-4.7.2-complete.jar"
%ANTLR% -o Output -Dlanguage=CSharp Daedalus.g4
popd
PAUSE