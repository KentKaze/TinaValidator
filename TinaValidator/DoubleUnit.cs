﻿using System;

namespace Aritiafel.Artifacts.TinaValidator
{
    public class DoubleUnit : Unit, IUnit
    {
        public CompareMethod CompareMethod { get; set; }
        public double Value1 { get; set; } //min exact
        public double Value2 { get; set; } //max

        public DoubleUnit(double exactValue)
        {
            CompareMethod = CompareMethod.Exact;
            Value1 = exactValue;
        }

        public DoubleUnit(double minValue, double maxValue)
        {
            CompareMethod = CompareMethod.MinMax;
            Value1 = minValue;
            Value2 = maxValue;
        }

        public bool Compare(object b)
        {
            if (!double.TryParse(b.ToString(), out double d))
                return false;
            if (CompareMethod == CompareMethod.Exact)
                return Value1 == d;
            else
                return d > Value1 && d < Value2;
        }

        public object Random()
        {
            if (CompareMethod == CompareMethod.Exact)
                return Value1;
            else
            {
                Random rnd = new Random(Convert.ToInt32(DateTime.Now.Ticks));
                return rnd.NextDouble() * (Value2 - Value1) + Value1;
            }
        }
    }
}
