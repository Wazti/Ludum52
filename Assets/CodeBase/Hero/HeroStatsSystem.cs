using System;
using CodeBase.Stats.Infrastructure;
using CodeBase.Stats.Scriptables;
using UnityEngine;

namespace CodeBase.Hero
{
    public class HeroStatsSystem : MonoBehaviour
    {
        [SerializeField] private BaseStats heroStats;

        private StatsSystem _statsSystem;
        public StatsSystem StatsSystem => _statsSystem;

        private void Awake()
        {
            _statsSystem = new StatsSystem(heroStats);
        }
    }
}