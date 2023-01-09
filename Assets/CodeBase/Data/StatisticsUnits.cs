using System;
using UnityEngine;

namespace CodeBase.Data
{
    [Serializable]
    public class StatisticsUnits
    {
        public int CountThin;

        public int CountMedium;

        public int CountHeavy;

        public int CountCows;
        
        
        public StatisticsUnits()
        {
            CountThin = 0;
            CountMedium = 0;
            CountHeavy = 0;
            CountCows = 0;
        }
        
    }
}