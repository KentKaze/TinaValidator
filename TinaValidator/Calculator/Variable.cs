﻿using Aritiafel.Locations;
using System;
using System.Collections.Generic;

namespace Aritiafel.Artifacts.Calculator
{
    public enum VariableSource
    {
        EnvoirmentVariable = 0,
        CustomVariable
    }

    public abstract class Variable : IObject
    {
        public VariableSource Source { get; set; }
        public string Name { get; set; }
        public List<object> Keys { get; set; }
        public object Tag { get; set; }
        public abstract ObjectConst GetObject(IVariableLinker vl);
        public abstract Type GetObjectType();
        protected Variable(string name = null, List<object> keys = null, VariableSource source = VariableSource.CustomVariable)
        {
            Name = name ?? IdentifyShop.GetNewID();
            Keys = keys;
            Source = source;
        }

        
    }
}
