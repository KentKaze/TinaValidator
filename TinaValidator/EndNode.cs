﻿using System;
using System.Collections.Generic;

namespace Aritiafel.Artifacts.TinaValidator
{
    public class EndNode : TNode
    {
        public static EndNode Instance { get; } = new EndNode();
        private EndNode()
        { }
    }
}
