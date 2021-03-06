﻿using System;
using System.Collections.Generic;

namespace Aritiafel.Artifacts.Calculator
{
    public class DeclareVariableStatement : Statement
    {
        public string Name { get; set; }
        public Type Type { get; set; }
        public byte Dimension { get; set; }
        public int[] Counts { get; set; }
        public IObject InitialValue { get; set; }

        public DeclareVariableStatement(string name, Type type, byte dimension = 0, int[] counts = null, IObject initialValue = null)
        {
            Name = name;
            Type = type;
            Dimension = dimension;
            Counts = counts;
            InitialValue = initialValue;
        }
    }
}
