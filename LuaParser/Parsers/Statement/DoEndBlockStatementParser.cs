using System.Collections.Generic;
using LuaParser.Extensions;
using LuaParser.Syntax;

namespace LuaParser.Parsers.Statement
{
    internal class DoEndBlockStatementParser : StatementParser
    {
        public override Syntax.Statement Parse(ITokenEnumerator reader)
        {
            reader.VerifyExpectedToken(Keyword.Do);
            reader.Advance();
            var statements = new List<Syntax.Statement>();
            while (reader.Current != Keyword.End)
                statements.Add(SyntaxParser.ReadStatement(reader));
            reader.VerifyExpectedToken(Keyword.End);
            return new DoEndBlock(new StatementBlock(statements));
        }
    }
}