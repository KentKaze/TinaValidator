﻿using Aritiafel.Locations;
using System.Collections.Generic;
using Aritiafel.Artifacts.Calculator;

namespace Aritiafel.Artifacts.TinaValidator
{

    public abstract class Part : TNode, IPart
    {
        public abstract int Validate(List<ObjectConst> thing, int startIndex = 0, IVariableLinker vl = null);
        public abstract List<ObjectConst> Random(IVariableLinker vl = null);
        protected Part(TNode nextNode = null, Area parent = null, string id = null)
            : base(nextNode, parent, id ?? IdentifyShop.GetNewID("P"))
        { }
    }
}