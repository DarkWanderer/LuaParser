﻿using System.Collections.Generic;
using LuaParser.Extensions;
using LuaParser.Syntax;
using Enumerable = System.Linq.Enumerable;

namespace LuaParser.Parsers.Statement
{
    internal class StatementBlockParser : StatementParser
    {
        private readonly HashSet<string> _terminatingTokens;

        public StatementBlockParser(params string[] terminatingTokens)
            : this(Enumerable.AsEnumerable(terminatingTokens))
        {
        }

        public StatementBlockParser(IEnumerable<string> terminatingTokens)
        {
            _terminatingTokens = new HashSet<string>(terminatingTokens);
        }

        public override Syntax.Statement Parse(ITokenEnumerator reader)
        {
            return ParseBlock(reader);
        }

        public StatementBlock ParseBlock(ITokenEnumerator reader)
        {
            var statements = new List<Syntax.Statement>();
            while (!_terminatingTokens.Contains(reader.Current))
                statements.Add(SyntaxParser.ReadStatement(reader));
            reader.VerifyExpectedToken(_terminatingTokens);
            return new StatementBlock(statements);
        }
    }
}