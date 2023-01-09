using System;
using System.Collections.Generic;
using CodeBase.Data;
using CodeBase.Services.PersistentProgress;
using CodeBase.Unit;
using UnityEngine;
using Zenject;

namespace CodeBase.Services
{
    public class LevelSessionService : ILevelSessionService
    {
        [Inject] private IPersistentProgressService _progressService;

        private readonly List<UnitInfo> _currentUnits = new List<UnitInfo>();

        public List<UnitInfo> CurrentUnits => _currentUnits;


        public event Action<int> CountChange;

        public StatisticsUnits StatisticsUnits { get; set; }

        public int xpOld { get; set; }

        public int levelOld { get; set; }
        public float progressOld { get; set; }


        public void CopyData()
        {
            _currentUnits.Clear();
            xpOld = _progressService.Progress.LevelHero.xp;
            levelOld = _progressService.Progress.LevelHero.CurrentLevel;
            progressOld = _progressService.Progress.LevelHero.Progress();
        }

        public void WinLevel()
        {
            var dictionary = _progressService.Progress.StatisticsUnits.countUnits;
            var weight = 0;

            _currentUnits.ForEach(item =>
            {
                dictionary[item.Type] += 1;
                weight += (int) item.Mass;
            });
            _progressService.Progress.LevelHero.xp += weight;

            _progressService.Progress.LevelHero.ReCalcLevels();

            _progressService.Progress.Day += 1;
            
            _progressService.Progress.Points += _progressService.Progress.LevelHero.CurrentLevel - levelOld;
        }

        public void AddUnit(UnitInfo unit)
        {
            _currentUnits.Add(unit);
            CountChange?.Invoke(_currentUnits.Count);
        }

        public void Clear()
        {
            _currentUnits.Clear();
            xpOld = 0;
            levelOld = 0;
        }
    }
}