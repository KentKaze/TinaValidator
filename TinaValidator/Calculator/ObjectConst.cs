﻿using System;

namespace Aritiafel.Artifacts.Calculator
{
    public abstract class ObjectConst : IObject
    {
        public abstract ObjectConst GetObject(IVariableLinker vl);
        public abstract Type GetObjectType();

        public abstract StringConst ToStringConst();
        protected abstract BooleanConst ReverseEqualTo(ObjectConst b);
        public abstract BooleanConst EqualTo(LongConst b);
        public abstract BooleanConst EqualTo(CharConst b);
        public abstract BooleanConst EqualTo(DoubleConst b);
        public abstract BooleanConst EqualTo(StringConst b);            
        protected abstract BooleanConst ReverseGreaterThan(ObjectConst b);
        public abstract BooleanConst GreaterThan(LongConst b);            
        public abstract BooleanConst GreaterThan(CharConst b);
        public abstract BooleanConst GreaterThan(DoubleConst b);
        public abstract BooleanConst GreaterThan(StringConst b);
        protected abstract BooleanConst ReverseLessThan(ObjectConst b);
        public abstract BooleanConst LessThan(LongConst b);
        public abstract BooleanConst LessThan(CharConst b);            
        public abstract BooleanConst LessThan(DoubleConst b);            
        public abstract BooleanConst LessThan(StringConst b);
        public abstract object Clone();

        public static BooleanConst operator ==(ObjectConst a, ObjectConst b)
            => b.ReverseEqualTo(a);
        public static BooleanConst operator !=(ObjectConst a, ObjectConst b)
            => !b.ReverseEqualTo(a);
        public static BooleanConst operator >(ObjectConst a, ObjectConst b)
            => b.ReverseGreaterThan(a);
        public static BooleanConst operator >=(ObjectConst a, ObjectConst b)
            => b.ReverseGreaterThan(a) || b.ReverseEqualTo(a);
        public static BooleanConst operator <(ObjectConst a, ObjectConst b)
            => b.ReverseLessThan(a);
        public static BooleanConst operator <=(ObjectConst a, ObjectConst b)
            => b.ReverseLessThan(a) || (b.ReverseEqualTo(a));
    }
}
