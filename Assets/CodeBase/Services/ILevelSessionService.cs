using System;
using System.Collections.Generic;
using CodeBase.Data;
using CodeBase.Unit;

namespace CodeBase.Services
{
    public interface ILevelSessionService
    {
        public event Action<int> CountChange;
        List<UnitInfo> CurrentUnits { get; }
        StatisticsUnits StatisticsUnits { get; set; }
        int xpOld { get; set; }
        int levelOld { get; set; }
        float progressOld { get; set; }
        void CopyData();
        void WinLevel();
        void LoseLevel();
        void Clear();
        void AddUnit(UnitInfo unit);
    }
}