using System;
using CodeBase.Services.PersistentProgress;
using CodeBase.Stats.Enums;
using CodeBase.Stats.Infrastructure;
using CodeBase.Stats.Scriptables;
using UnityEngine;
using Zenject;

namespace CodeBase.Hero
{
    public class HeroStatsSystem : MonoBehaviour
    {
        [Inject] private IPersistentProgressService persistentProgressService;

        [SerializeField] private BaseStats heroStats;

        private StatsSystem _statsSystem;
        public StatsSystem StatsSystem => _statsSystem;

        private void Awake()
        {
            _statsSystem = new StatsSystem(heroStats);

            persistentProgressService.Progress.upgradeList.ForEach((item) =>
            {
                _statsSystem.AddModifier(item.statUpgrade,
                    new StatModifier(StatModifierTypes.Flat, item.statValue));
            });
        }
    }
}