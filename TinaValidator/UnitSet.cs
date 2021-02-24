﻿using System.Collections.Generic;

namespace Aritiafel.Artifacts.TinaValidator
{
    public class UnitSet : Part
    {
        public List<Unit> Units { get; set; } = new List<Unit>();
        public UnitSet(Unit unit, Area parent = null)
            : this(null, parent, unit)
        { }

        public UnitSet(List<Unit> units = null, Area parent = null)
            : this(null, parent, units)
        { }

        public UnitSet(string id, Area parent, Unit unit)
            : base(null, parent, id)
        {
            Units.Add(unit);
        }

        public UnitSet(string id, Area parent = null, List < Unit> units = null)
            : base(null, parent)
        {
            if (units != null)
                Units = units;
        }

        public override List<object> Random()
        {
            List<object> result = new List<object>();
            for (int i = 0; i < Units.Count; i++)
                result.Add(Units[i].Random());
            return result;
        }

        public override int Validate(List<object> thing, int startIndex)
        {
            for (int i = 0; i < Units.Count; i++)
                if (!Units[i].Compare(thing[startIndex + i]))
                    return -1;
            return startIndex + Units.Count;
        }        
    }
}
