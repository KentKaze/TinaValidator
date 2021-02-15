﻿using System.Collections.Generic;

namespace Aritiafel.Artifacts.TinaValidator
{
    public class UnitSet : Part
    {
        public string ID { get; set; }
        public List<Unit> Units { get; set; } = new List<Unit>();
        public UnitSet(Unit unit)
            : this (null, unit)
        { }

        public UnitSet(List<Unit> units = null)
            : this(null, units)
        { }

        public UnitSet(string id, Unit unit)
        {
            ID = id;
            Units.Add(unit);
        }

        public UnitSet(string id, List<Unit> units = null)
        {
            ID = id;
            if (units != null)
                Units = units;
        }

        public override bool Compare(List<object> thing)
        {
            if (Units.Count != thing.Count)
                return false;
            for (int i = 0; i < Units.Count; i++)
                if (!Units[i].Compare(thing[i]))
                    return false;
            return true;
        }

        public override List<object> Random()
        {
            List<object> result = new List<object>();
            for (int i = 0; i < Units.Count; i++)
                result.Add(Units[i].Random());
            return result;
        }
    }
}
