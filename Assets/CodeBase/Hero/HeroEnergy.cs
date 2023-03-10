using System;
using CodeBase.Infrastructure;
using CodeBase.Services;
using CodeBase.Stats.Scriptables;
using DG.Tweening;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;
using Zenject;

namespace CodeBase.Hero
{
    [RequireComponent(typeof(HeroStatsSystem), typeof(Rigidbody2D))]
    public class HeroEnergy : MonoBehaviour
    {
        [Inject] private ILevelSessionService _levelService;
        [Inject] private ISceneLoader _sceneLoader;
        [Inject] private ICurtainService _curtainService;

        [SerializeField] private HeroMovement heroMovement;

        [SerializeField] private HeroStatsSystem heroStats;

        [SerializeField] private HeroHealth heroHealth;
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

            if (_isOutEnergy) return;

            _isOutEnergy = true;


            heroMovement.enabled = false;
            _rigidbody.gravityScale = 1;
        }

        public void DecreaseEnergy(float energy)
        {
            Energy.Value -= energy;
        }

        [ShowInInspector]
        [ProgressBar(0, "MaxEnergy", 0.2f, 0.24f, 0.2f)]
        public float EnergyDebug
        {
            get => Energy.Value;
        }
    }
}