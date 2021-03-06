﻿using System;

namespace Aritiafel.Artifacts.Calculator
{
    public class LongConst : NumberConst
    {
        public static LongConst MaxValue => new LongConst(long.MaxValue);
        public static LongConst MinValue => new LongConst(long.MinValue);

        private long _Value;
        public override object Value => _Value;
        public LongConst(long value)
            => _Value = value;
        public static implicit operator long(LongConst a) => a._Value;
        public static explicit operator DoubleConst(LongConst a) => new DoubleConst(Convert.ToDouble(a._Value));
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
        public static BooleanConst operator ==(LongConst a, LongConst b)
            => a.EqualTo(b);
        public static BooleanConst operator ==(LongConst a, DoubleConst b)
            => a.EqualTo(b);
        public static BooleanConst operator !=(LongConst a, LongConst b)
            => !a.EqualTo(b);
        public static BooleanConst operator !=(LongConst a, DoubleConst b)
            => !a.EqualTo(b);
        public static BooleanConst operator >(LongConst a, LongConst b)
            => a.GreaterThan(b);
        public static BooleanConst operator >(LongConst a, DoubleConst b)
            => a.GreaterThan(b);
        public static BooleanConst operator >=(LongConst a, LongConst b)
            => a.GreaterThan(b) || a.EqualTo(b);
        public static BooleanConst operator >=(LongConst a, DoubleConst b)
            => a.GreaterThan(b) || a.EqualTo(b);
        public static BooleanConst operator <(LongConst a, LongConst b)
            => a.LessThan(b);
        public static BooleanConst operator <(LongConst a, DoubleConst b)
            => a.LessThan(b);
        public static BooleanConst operator <=(LongConst a, LongConst b)
            => a.LessThan(b) || a.EqualTo(b);
        public static BooleanConst operator <=(LongConst a, DoubleConst b)
            => a.LessThan(b) || a.EqualTo(b);
        public override string ToString()
            => _Value.ToString();
        public override StringConst ToStringConst()
            => new StringConst(_Value.ToString());
        public object GetValue()
            => _Value;
        public override NumberConst GetResult(IVariableLinker vl)
            => this;
        protected override NumberConst ReverseAdd(NumberConst b)
            => b.Add(this);
        public override NumberConst Add(LongConst b)
            => new LongConst(_Value + b._Value);
        public override NumberConst Add(DoubleConst b)
            => LongAddDouble(_Value, (double)b.Value);
        public override NumberConst Minus(LongConst b)
            => new LongConst(_Value - b._Value);
        protected override NumberConst ReverseMinus(NumberConst b)
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
            => b.ExactlyDivide(this);
        public override NumberConst ExactlyDivide(LongConst b)
            => new LongConst(_Value / b._Value);
        public override NumberConst ExactlyDivide(DoubleConst b)
            => LongExactlyDivideDouble(_Value, (double)b.Value);
        public override NumberConst Remainder(LongConst b)
            => new LongConst(_Value % b._Value);
        public override NumberConst Remainder(DoubleConst b)
            => new DoubleConst(_Value % (double)b.Value);
        protected override NumberConst ReverseMultiply(NumberConst b)
            => b.Multiply(this);
        protected override NumberConst ReverseDivide(NumberConst b)
            => b.Divide(this);
        protected override NumberConst ReverseRemainder(NumberConst b)
            => b.Remainder(this);
        public override BooleanConst EqualTo(LongConst b)
            => new BooleanConst(_Value == b._Value);
        public override BooleanConst EqualTo(DoubleConst b)
            => new BooleanConst(_Value == (double)b.Value);
        public override BooleanConst GreaterThan(LongConst b)
            => new BooleanConst(_Value > b._Value);
        public override BooleanConst GreaterThan(DoubleConst b)
            => new BooleanConst(_Value > (double)b.Value);
        public override BooleanConst LessThan(LongConst b)
            => new BooleanConst(_Value < b._Value);
        public override BooleanConst LessThan(DoubleConst b)
            => new BooleanConst(_Value < (double)b.Value);
        protected override BooleanConst ReverseEqualTo(ObjectConst b)
            => b is NumberConst ? b.EqualTo(this) : throw new ArithmeticException();
        protected override BooleanConst ReverseGreaterThan(ObjectConst b)
            => b is NumberConst ? b.GreaterThan(this) : throw new ArithmeticException();
        protected override BooleanConst ReverseLessThan(ObjectConst b)
            => b is NumberConst ? b.LessThan(this) : throw new ArithmeticException();
        protected override BooleanConst ReverseEqualTo(NumberConst b)
            => b.EqualTo(this);
        protected override BooleanConst ReverseGreaterThan(NumberConst b)
            => b.GreaterThan(this);
        protected override BooleanConst ReverseLessThan(NumberConst b)
            => b.LessThan(this);
        public override BooleanConst EqualTo(StringConst b)
            => throw new ArithmeticException();
        public override BooleanConst GreaterThan(StringConst b)
            => throw new ArithmeticException();
        public override BooleanConst LessThan(StringConst b)
            => throw new ArithmeticException();
        public override NumberConst Add(CharConst b)
            => new LongConst(_Value + (char)b.Value);
        public override NumberConst Minus(CharConst b)
            => new LongConst(_Value - (char)b.Value);
        public override NumberConst Multiply(CharConst b)
            => new LongConst(_Value * (char)b.Value);
        public override NumberConst Divide(CharConst b)
            => LongDivideLong(_Value, (char)b.Value);
        public override NumberConst ExactlyDivide(CharConst b)
            => new LongConst(_Value / (char)b.Value);
        public override NumberConst Remainder(CharConst b)
            => new LongConst(_Value % (char)b.Value);
        public override BooleanConst GreaterThan(CharConst b)
            => new BooleanConst(_Value > (char)b.Value);
        public override BooleanConst LessThan(CharConst b)
            => new BooleanConst(_Value < (char)b.Value);
        public override BooleanConst EqualTo(CharConst b)
            => new BooleanConst(_Value == (char)b.Value);
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
                return true;
            if (obj is null)
                return false;
            if (!(obj is NumberConst n))
                return false;
            return n.EqualTo(this);
        }
        public override int GetHashCode()
            => _Value.GetHashCode();
        public override object Clone()
            => new LongConst(_Value);
        
    }
}
