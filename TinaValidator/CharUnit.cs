﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Aritiafel.Artifacts.TinaValidator
{
    public class CharUnit : Unit, IUnit
    {
        public CompareMethod CompareMethod { get; set; }
        public char Value1 { get; set; } //min exact
        public char Value2 { get; set; } //max
        public CharUnit(string id, char exactValue)
            : base(id)
        {
            CompareMethod = CompareMethod.Exact;
            Value1 = exactValue;
        }

        public CharUnit(string id, char minValue, char maxValue)
            : base(id)
        {
            CompareMethod = CompareMethod.MinMax;
            Value1 = minValue;
            Value2 = maxValue;
        }

        public bool Compare(object b)
        {
            if (!(b is char))
                return false;            
            if (CompareMethod == CompareMethod.Exact)
                return Value1 == (char)b;
            else
                return (char)b > Value1 && (char)b < Value2;
        }

        public object Random()
        {
            if (CompareMethod == CompareMethod.Exact)
                return Value1;
            else
            {
                Random rnd = new Random(Convert.ToInt32(DateTime.Now.Ticks));
                return (char)rnd.Next(char.MinValue, char.MaxValue);
            }
        }
    }
}
