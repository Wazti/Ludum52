using System;
using System.Collections.Generic;
using CodeBase.Unit.Scriptables;
using CodeBase.Upgrades;

namespace CodeBase.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public List<UpgradeConfig> upgradeList;

        private int _points;

        public int Points
        {
            get => _points;
            set
            {
                _points = value;

                ChangePoints?.Invoke();
            }
        }

        public StatisticsUnits StatisticsUnits;

        public int Day;

        public LevelHero LevelHero;

        public event Action ChangePoints;

        public PlayerProgress(string initialLevel)
        {
            upgradeList = new List<UpgradeConfig>();

            Points = 0;
            Day = 1;

            StatisticsUnits = new StatisticsUnits();
            LevelHero = new LevelHero();
        }
    }
}