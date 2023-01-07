using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Enemy;
using CodeBase.Logic;
using CodeBase.Services.InputService;
using Unity.Mathematics;
using UnityEngine;
using Zenject;

namespace CodeBase.Hero
{
    public class HeroIntake : MonoBehaviour
    {
        [Inject] private IInputService _inputService;

        public float Strength;
        public float StrengthEngine;

        [SerializeField] private float offsetY;
        [SerializeField] private Vector2 sizeBox;

        [SerializeField] private Transform pointToIntake;

        private float _accumulation;

        public float Accumulation
        {
            get => _accumulation;
            set => _accumulation = Math.Min(Math.Max(0, value), 0.75f);
        }

        private int _layerMask;
        private Collider2D[] _hits = new Collider2D[50];

        private List<IEnemyIntakes> _currentEnemy = new List<IEnemyIntakes>();
        public event Action ChangeProcessEnemy;


        [SerializeField] private HeroCapacity heroCapacity;
        [SerializeField] private TriggerObserver triggerObserver;
        [SerializeField] private SpriteRenderer lightRenderer;

        private void Awake()
        {
            lightRenderer.color = new Color(255, 255, 255, 0);
            triggerObserver.TriggerEnter += OnEnemyPoint;
            _layerMask = 1 << LayerMask.NameToLayer($"Intakes");
        }

        private void OnEnemyPoint(Collider2D obj)
        {
            if (!obj.gameObject.TryGetComponent<IEnemyIntakes>(out var enemyIntakes)) return;

            if (!_currentEnemy.Contains(enemyIntakes)) return;

            _currentEnemy.Remove(enemyIntakes);
            heroCapacity.AddEnemy(enemyIntakes);
            ChangeProcessEnemy?.Invoke();
        }

        private void Update()
        {
            Accumulation += (_inputService.IsIntakeButton() ? 1 : -1) * (Time.deltaTime * Strength);

            lightRenderer.color = new Color(255, 255, 255, Accumulation);

            if (!_inputService.IsIntakeButton())
            {
                ClearEnemies();
                return;
            }

            Intake();
        }

        private void FixedUpdate()
        {
            TickEnemies();
        }

        public List<IEnemyIntakes> GetCurrentProcessEnemies() => _currentEnemy;

        private void Intake()
        {
            Physics2D.OverlapBoxNonAlloc((transform.position + Vector3.up * offsetY), sizeBox, 0, _hits,
                _layerMask);

            var results = _hits.Where(x => x != null).Select(item => item.transform.GetComponent<IEnemyIntakes>())
                .ToList();

            RemoveOldEnemies(results);

            AddNewEnemies(results);

            _hits = new Collider2D[50];
        }

        private void TickEnemies()
        {
            foreach (IEnemyIntakes enemyIntakes in _currentEnemy)
            {
                enemyIntakes.Move(pointToIntake.position,
                    (1 / enemyIntakes.Mass) * StrengthEngine * Time.deltaTime);
            }
        }

        private void RemoveOldEnemies(List<IEnemyIntakes> current)
        {
            foreach (IEnemyIntakes enemyIntakes in _currentEnemy.ToList()
                         .Where(enemyIntakes => !current.Contains(enemyIntakes)))
            {
                enemyIntakes.OutEnemy();
                _currentEnemy.Remove(enemyIntakes);
            }

            ChangeProcessEnemy?.Invoke();
        }

        private void AddNewEnemies(List<IEnemyIntakes> last)
        {
            foreach (IEnemyIntakes enemy in last.Where(enemy => !_currentEnemy.Contains(enemy)))
            {
                enemy.IntakeEnemy(transform);

                _currentEnemy.Add(enemy);
            }

            ChangeProcessEnemy?.Invoke();
        }

        private void ClearEnemies()
        {
            if (_currentEnemy.Count == 0) return;

            _currentEnemy.ForEach((enemy) => enemy.OutEnemy());

            _currentEnemy.Clear();

            ChangeProcessEnemy?.Invoke();
        }
    }
}