﻿using System.Collections.Generic;

namespace Aritiafel.Artifacts.Calculator
{
    public class LongVar : NumberVar
    {
        public override NumberConst GetResult(IVariableLinker vl)
            => vl.GetValue(this) as NumberConst;
        public override object Clone()
            => new LongVar { Name = Name, Parent = Parent, Keys = Keys, Source = Source, Tag = Tag };
        public LongVar(string name = null, object parent = null, List<object> keys = null, VariableSource source = VariableSource.UserVariable)
            : base(name, parent, keys, source)
        { }
    }
}
