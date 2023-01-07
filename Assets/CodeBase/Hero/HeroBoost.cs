using System;
using CodeBase.Services.InputService;
using CodeBase.Stats.Infrastructure;
using CodeBase.Stats.Interfaces;
using CodeBase.Stats.Scriptables;
using UnityEngine;
using Zenject;

namespace CodeBase.Hero
{
    [RequireComponent(typeof(HeroEnergy), typeof(Rigidbody2D))]
    public class HeroBoost : MonoBehaviour
    {
        [Range(0, 4f)] [SerializeField] private float Cost;

        [SerializeField] private Rigidbody2D rigidbody;

        [SerializeField] private HeroEnergy heroEnergy;

        [SerializeField] private HeroStatsSystem heroStats;

        [SerializeField] private StatType boostStat;

        private bool _isActive = true;

        [Inject] private IInputService _inputService;

        private void Update()
        {
            if (!_inputService.IsBoostButton() || !IsEnoughEnergy())
            {
                _isActive = false;
                return;
            }

            _isActive = true;

            heroEnergy.DecreaseEnergy(Time.deltaTime * Cost);
        }

        public float BoostValue() => !_isActive ? 1f : heroStats.StatsSystem.GetStat(boostStat).Value;

        private bool IsEnoughEnergy()
        {
            return heroEnergy.Energy.Value >= 0;
        }
    }
}