﻿using System.Collections.Generic;

namespace LuaParser.Syntax
{
    internal class EmptyStatement : Statement
    {
        public override IEnumerable<Unit> Children => new Unit[0];
    }
}