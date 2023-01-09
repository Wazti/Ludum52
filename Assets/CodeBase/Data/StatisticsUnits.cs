using System;
using System.Collections.Generic;
using CodeBase.Unit;
using UnityEngine;

namespace CodeBase.Data
{
    [Serializable]
    public class StatisticsUnits
    {
        public Dictionary<UnitType, int> countUnits;
        


        public StatisticsUnits()
        {
            countUnits = new Dictionary<UnitType, int>
            {
                {UnitType.Thin, 0},
                {UnitType.Heavy, 0},
                {UnitType.Medium, 0},
                {UnitType.Cow, 0}
            };
        }
    }
}