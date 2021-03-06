﻿using System.Collections.Generic;
using Aritiafel.Artifacts.Calculator;

namespace Aritiafel.Artifacts.TinaValidator
{
    public class CharsToBooleanPart : Part
    {
        public CompareMethod CompareMethod { get; set; }
        public IBoolean Value { get; set; }        
        public CharsToBooleanPart()
            : this(CompareMethod.Any)
        { }
        public CharsToBooleanPart(CompareMethod compareMethod = CompareMethod.Any)
            => CompareMethod = compareMethod;
        public CharsToBooleanPart(BooleanUnit bu)
        {
            CompareMethod = bu.CompareMethod;
            Value = bu.Value;
        }
        public CharsToBooleanPart(bool value, CompareMethod compareMethod = CompareMethod.Exact)
            : this(new BooleanConst(value) as IBoolean, compareMethod)
        { }
        public CharsToBooleanPart(IBoolean value, CompareMethod compareMethod = CompareMethod.Exact)
        {
            CompareMethod = compareMethod;
            Value = value;
        }

        public override int Validate(List<ObjectConst> thing, int startIndex = 0, IVariableLinker vl = null)
        {
            bool? result = null;

            if (startIndex + 3 < thing.Count && thing[startIndex] is CharConst c1 &&
               thing[startIndex + 1] is CharConst c2 && thing[startIndex + 2] is CharConst c3 &&
               thing[startIndex + 3] is CharConst c4)
            {
                if (char.ToUpper(c1) == 'T' && char.ToUpper(c2) == 'R' &&
                    char.ToUpper(c3) == 'U' && char.ToUpper(c4) == 'E')
                    result = true;
                else if (startIndex + 4 < thing.Count && thing[startIndex + 4] is CharConst c5 &&
                char.ToUpper(c1) == 'F' && char.ToUpper(c2) == 'A' && char.ToUpper(c3) == 'L' &&
                char.ToUpper(c4) == 'S' && char.ToUpper(c5) == 'E')
                    result = false;
            }

            if (result == null)
                return -1;

            BooleanUnit bu = new BooleanUnit(this);
            if (!bu.Compare(new BooleanConst((bool)result), vl))
                return -1;
            return (bool)result ? startIndex + 4 : startIndex + 5;
        }

        public override List<ObjectConst> Random(IVariableLinker vl = null)
        {
            BooleanUnit bu = new BooleanUnit(this);
            return bu.Random(vl).ToString().ToLower().ToObjectList();
        }
    }
}
