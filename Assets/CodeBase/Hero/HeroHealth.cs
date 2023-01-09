using System;
using CodeBase.Enemies;
using CodeBase.Infrastructure;
using CodeBase.Logic;
using CodeBase.Services;
using CodeBase.Stats.Scriptables;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace CodeBase.Hero
{
    public class HeroHealth : MonoBehaviour
    {
        [Inject] private ILevelSessionService _levelService;
        [Inject] private ISceneLoader _sceneLoader;
        [Inject] private ICurtainService _curtainService;

        [SerializeField] private StatType MaxHealth;
        [SerializeField] private HeroStatsSystem _heroStats;
        [SerializeField] private HeroViewDamage _viewDamage;

        private int CurrentHealth
        {
            get { return _currentHealth; }
            set
            {
                //почему maxhealth float??
                OnHealthChange?.Invoke(_currentHealth, (int) (_heroStats.StatsSystem.GetStat(MaxHealth).Value));
                _currentHealth = value;
            }
        }

        public Action<int, int> OnHealthChange;
        [SerializeField] private TriggerObserver _triggerObserver;
        private int _currentHealth;
        private HealthBar _healthBar;


        private void Awake()
        {
            _triggerObserver.TriggerEnter += OnTrigger;

            //shitcode
        }

        private void Start()
        {
            CurrentHealth = (int) (_heroStats.StatsSystem.GetStat(MaxHealth).Value);

            _healthBar = FindObjectOfType<HealthBar>();
            OnHealthChange += _healthBar.SetHealth;
            CurrentHealth = CurrentHealth;
        }

        private void OnTrigger(Collider2D obj)
        {
            if (!obj.TryGetComponent<Bullet>(out var bullet)) return;

            _viewDamage.OnDamage();
            bullet.Explodes();
            CurrentHealth -= bullet.damage;
            CurrentHealth = Math.Max(0, CurrentHealth);
        }
    }
}