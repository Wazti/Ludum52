using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Logic;
using CodeBase.Stats.Scriptables;
using CodeBase.Unit;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeBase.Hero
{
    public class HeroTickUnits : MonoBehaviour
    {
        private readonly List<IUnitIntakes> _currentUnits = new List<IUnitIntakes>();
        public event Action ChangeProcessUnit;

        [SerializeField] private StatType StrengthEngine;
        private float strengthEngine;


      
        [SerializeField] private Transform pointToIntake;
        
        [SerializeField] private HeroStatsSystem heroStats;
        [SerializeField] private HeroCapacity heroCapacity;
        [SerializeField] private TriggerObserver triggerObserver;

        private void OnEnemyPoint(Collider2D obj)
        {
            if (!obj.gameObject.TryGetComponent<IUnitIntakes>(out var enemyIntakes)) return;

            if (!_currentUnits.Contains(enemyIntakes)) return;

            _currentUnits.Remove(enemyIntakes);
            heroCapacity.AddUnit(enemyIntakes);
            ChangeProcessUnit?.Invoke();
        }

        private void Awake()
        {
            triggerObserver.TriggerEnter += OnEnemyPoint;
        }

        private void Start()
        {
            strengthEngine = heroStats.StatsSystem.GetStat(StrengthEngine).Value;
        }

        private void FixedUpdate()
        {
            TickEnemies();
        }


        private void TickEnemies()
        {
            foreach (IUnitIntakes enemyIntakes in _currentUnits)
            {
                enemyIntakes.Move(pointToIntake.position,
                    (1 / enemyIntakes.Mass) * strengthEngine * Time.deltaTime);
            }
        }

        public List<IUnitIntakes> GetCurrentProcessEnemies() => _currentUnits;

        public void AddNewUnits(List<IUnitIntakes> last)
        {
            foreach (IUnitIntakes unit in last.Where(enemy => !_currentUnits.Contains(enemy)))
            {
                unit.IntakeUnit(transform);

                _currentUnits.Add(unit);
            }

            ChangeProcessUnit?.Invoke();
        }

        public void RemoveOldEnemies(List<IUnitIntakes> current)
        {
            foreach (IUnitIntakes enemyIntakes in _currentUnits.ToList()
                         .Where(enemyIntakes => !current.Contains(enemyIntakes)))
            {
                enemyIntakes.OutUnit();
                _currentUnits.Remove(enemyIntakes);
            }

            ChangeProcessUnit?.Invoke();
        }

        public void ClearUnits()
        {
            if (_currentUnits.Count == 0) return;

            _currentUnits.ForEach((unit) => unit.OutUnit());

            _currentUnits.Clear();

            ChangeProcessUnit?.Invoke();
        }
        
        [ShowInInspector] private float CountUnitsIn => _currentUnits.Count;
        [ShowInInspector] private float TotalWeight => _currentUnits.Aggregate(0f, (acc, x) => acc + x.Mass);
    }
}