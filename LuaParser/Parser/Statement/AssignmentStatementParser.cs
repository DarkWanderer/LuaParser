using System.Collections.Generic;
using DW.Lua.Extensions;
using DW.Lua.Parsers.Expression;
using DW.Lua.Syntax;
using DW.Lua.Syntax.Statement;

namespace DW.Lua.Parsers.Statement
{
    internal class AssignmentStatementParser : StatementParser
    {
        public override LuaStatement Parse(ITokenEnumerator reader, IParserContext context)
        {
            bool local = false;
            if (reader.Current == "local")
            {
                local = true;
                reader.MoveNext();
            }

            var variables = ReadDeclarations(reader);
            foreach (var variable in variables)
                context.CurrentScope.AddVariable(variable);
            reader.VerifyExpectedTokenAndMoveNext(LuaToken.EqualsSign);
            var assignedExpressionParser = new ExpressionListParser();
            var expressions = assignedExpressionParser.Parse(reader, context);

            return new Assignment(variables, expressions, local);
        }

        private IList<Variable> ReadDeclarations(ITokenEnumerator reader)
        {
            var result = new List<Variable>();
            while (reader.Next != null)
            {
                var variable = new Variable(reader.Current);
                result.Add(variable);
                reader.MoveNext();
                reader.VerifyExpectedToken(LuaToken.Comma,LuaToken.EqualsSign);
                if (reader.Current == LuaToken.EqualsSign)
                    break;
                reader.MoveNext();
            }
            return result;
        }
    }
}