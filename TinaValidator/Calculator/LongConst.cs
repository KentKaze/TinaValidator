﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Aritiafel.Artifacts.Calculator
{
    public class LongConst : NumberConst
    {
        private long _Value;
        public override object Value => _Value;
        public LongConst(long value)
            => _Value = value;
        public static NumberConst operator +(LongConst a, LongConst b)
            => a.Add(b);
        public static NumberConst operator +(LongConst a, DoubleConst b)
            => a.Add(b);
        public static NumberConst operator -(LongConst a, LongConst b)
            => a.Minus(b);
        public static NumberConst operator -(LongConst a, DoubleConst b)
            => a.Minus(b);
        public static NumberConst operator *(LongConst a, LongConst b)
            => a.Multiply(b);
        public static NumberConst operator *(LongConst a, DoubleConst b)
            => a.Multiply(b);
        public static NumberConst operator /(LongConst a, LongConst b)
            => a.Divide(b);
        public static NumberConst operator /(LongConst a, DoubleConst b)
            => a.Divide(b);
        public static NumberConst operator %(LongConst a, LongConst b)
            => a.Remainder(b);
        public static NumberConst operator %(LongConst a, DoubleConst b)
            => a.Remainder(b);
        public override string ToString()
            => _Value.ToString();
        public object GetValue()
            => _Value;
        public override NumberConst GetResult(IVariableLinker vl)
            => this;
        public override NumberConst ReverseAdd(NumberConst b)
            => b.Add(this);
        public override NumberConst Add(LongConst b)
            => new LongConst(_Value + b._Value);
        public override NumberConst Add(DoubleConst b)
            => LongAddDouble(_Value, (double)b.Value);
        public override NumberConst Minus(LongConst b)
            => new LongConst(_Value - b._Value);
        public override NumberConst ReverseMinus(NumberConst b)
            => b.Minus(this);
        public override NumberConst Minus(DoubleConst b)
            => LongMinusDouble(_Value, (double)b.Value);
        public override NumberConst Multiply(LongConst b)
            => new LongConst(_Value * b._Value);
        public override NumberConst Multiply(DoubleConst b)
            => LongMultiplyDouble(_Value, (double)b.Value);
        public override NumberConst Divide(LongConst b)
            => LongDivideLong(_Value, b._Value);
        public override NumberConst Divide(DoubleConst b)
            => LongDivideDouble(_Value, (double)b.Value);
        public override NumberConst ReverseExactlyDivide(NumberConst b)
            => b.Divide(this);
        public override NumberConst ExactlyDivide(LongConst b)
            => new LongConst(_Value / b._Value);
        public override NumberConst ExactlyDivide(DoubleConst b)
            => LongExactlyDivideDouble(_Value, (double)b.Value);
        public override NumberConst Remainder(LongConst b)
            => new LongConst(_Value % b._Value);
        public override NumberConst Remainder(DoubleConst b)
            => new LongConst(_Value % (long)Math.Round((double)b.Value));
        public override NumberConst ReverseMultiply(NumberConst b)
            => b.Multiply(this);
        public override NumberConst ReverseDivide(NumberConst b)
            => b.Divide(this);
        public override NumberConst ReverseRemainder(NumberConst b)
            => b.Remainder(this);
    }
}
