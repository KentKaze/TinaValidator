﻿using System;

namespace Aritiafel.Artifacts.Calculator
{
    public class ArithmeticExpression : Expression, INumber
    {
        public INumber A { get; set; }
        public INumber B { get; set; }
        public Operator OP { get; set; }
        public ArithmeticExpression(INumber a = null, INumber b = null, Operator op = Operator.Plus)
        {
            A = a;
            B = b;
            OP = op;
        }
        public NumberConst GetResult(IVariableLinker vl)
        {
            switch (OP)
            {
                case Operator.Plus:
                    return A.GetResult(vl) + B.GetResult(vl);
                case Operator.Minus:
                    return A.GetResult(vl) - B.GetResult(vl);
                case Operator.Multiply:
                    return A.GetResult(vl) * B.GetResult(vl);
                case Operator.Divide:
                    return A.GetResult(vl) / B.GetResult(vl);
                case Operator.ExactlyDivide:
                    return B.GetResult(vl).ReverseExactlyDivide(A.GetResult(vl));
                case Operator.Remainder:
                    return A.GetResult(vl) % B.GetResult(vl);
                case Operator.PlusOne:
                    return A.GetResult(vl) + new LongConst(1);
                case Operator.MinusOne:
                    return A.GetResult(vl) - new LongConst(1);
                default:
                    throw new ArithmeticException();
            }
        }
        public override ObjectConst GetObject(IVariableLinker vl)
            => GetResult(vl);

        public override Type GetObjectType()
            => typeof(INumber);

        public override object Clone()
            => new ArithmeticExpression((INumber)A.Clone(), (INumber)B.Clone(), OP);
    }
}
