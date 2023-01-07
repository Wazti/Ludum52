using System;
using CodeBase.Stats.Scriptables;
using UniRx;
using UnityEngine;

namespace CodeBase.Hero
{
    [RequireComponent(typeof(HeroStatsSystem), typeof(Rigidbody2D))]
    public class HeroEnergy : MonoBehaviour
    {
        [SerializeField] private HeroMovement heroMovement;

        [SerializeField] private HeroStatsSystem heroStats;

        [SerializeField] private StatType maxEnergy;

        private Rigidbody2D _rigidbody;

        public float MaxEnergy;

        public ReactiveProperty<float> Energy;

        private bool _isOutEnergy;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            MaxEnergy = heroStats.StatsSystem.GetStat(maxEnergy).Value;
            Energy.Value = MaxEnergy;
        }

        private void FixedUpdate()
        {
            if (_isOutEnergy) return;

            DecreaseEnergy();
        }

        private void DecreaseEnergy()
        {
            Energy.Value -= Time.deltaTime;

            if (!(Energy.Value <= 0f)) return;

            _isOutEnergy = true;
            heroMovement.enabled = false;
            _rigidbody.gravityScale = 1;
        }

        public void DecreaseEnergy(float energy)
        {
            Energy.Value -= energy;
        }
    }
}