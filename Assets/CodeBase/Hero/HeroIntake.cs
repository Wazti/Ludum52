using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Enemy;
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

        [SerializeField] private SpriteRenderer lightRenderer;

        private void Awake()
        {
            lightRenderer.color = new Color(255, 255, 255, 0);
            _layerMask = 1 << LayerMask.NameToLayer($"Intakes");
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
        }

        private void AddNewEnemies(List<IEnemyIntakes> last)
        {
            foreach (IEnemyIntakes enemy in last.Where(enemy => !_currentEnemy.Contains(enemy)))
            {
                enemy.IntakeEnemy(transform);

                _currentEnemy.Add(enemy);
            }
        }

        private void ClearEnemies()
        {
            _currentEnemy.ForEach((enemy) => enemy.OutEnemy());

            _currentEnemy.Clear();
        }
    }
}