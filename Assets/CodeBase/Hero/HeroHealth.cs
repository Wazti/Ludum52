using System;
using CodeBase.Enemies;
using CodeBase.Logic;
using CodeBase.Stats.Scriptables;
using Unity.VisualScripting;
using UnityEngine;

namespace CodeBase.Hero
{
    public class HeroHealth : MonoBehaviour
    {
        [SerializeField] private StatType MaxHealth;
        [SerializeField] private HeroStatsSystem _heroStats;
        [SerializeField] private HeroViewDamage _viewDamage;

        private int CurrentHealth
        {
            get
            {
                return _currentHealth;
            }
            set
            {
                //почему maxhealth float??
                OnHealthChange.Invoke(_currentHealth, (int) MaxHealth.DefaultValue);
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
            _healthBar = FindObjectOfType<HealthBar>();
            OnHealthChange += _healthBar.SetHealth;
        }

        private void Start()
        {
            CurrentHealth = (int) (_heroStats.StatsSystem.GetStat(MaxHealth).Value);
        }

        private void OnTrigger(Collider2D obj)
        {
            if (!obj.TryGetComponent<Bullet>(out var bullet)) return;
            
            _viewDamage.OnDamage();
            bullet.Explodes();
            CurrentHealth -= bullet.damage;
        }
    }
}