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

        private int currentHealth;

        [SerializeField] private TriggerObserver _triggerObserver;


        private void Awake()
        {
            _triggerObserver.TriggerEnter += OnTrigger;
        }

        private void Start()
        {
            currentHealth = (int) (_heroStats.StatsSystem.GetStat(MaxHealth).Value);
        }

        private void OnTrigger(Collider2D obj)
        {
            if (!obj.TryGetComponent<Bullet>(out var bullet)) return;
            
            _viewDamage.OnDamage();
            bullet.Explodes();
            currentHealth -= bullet.damage;
        }
    }
}