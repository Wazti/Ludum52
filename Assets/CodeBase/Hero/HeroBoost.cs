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
        [Range(0, 4f)]
        [SerializeField] private float Cost;
        
        [SerializeField] private Rigidbody2D rigidbody;

        [SerializeField] private HeroEnergy heroEnergy;

        [SerializeField] private HeroStatsSystem heroStats;

        [SerializeField] private StatType boostStat;

        [Inject] private IInputService _inputService;

        private void Update()
        {
            if (!_inputService.IsBoostButton() || !IsEnoughEnergy()) return;

            rigidbody.velocity *= heroStats.StatsSystem.GetStat(boostStat).Value;

            heroEnergy.DecreaseEnergy(Time.deltaTime * Cost);
        }

        private bool IsEnoughEnergy()
        {
            return heroEnergy.Energy.Value >= 0;
        }
    }
}