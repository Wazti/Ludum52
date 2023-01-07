using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Logic;
using CodeBase.Services.InputService;
using CodeBase.Unit;
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

        private readonly List<IUnitIntakes> _currentUnits = new List<IUnitIntakes>();
        public event Action ChangeProcessUnit;


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
            if (!obj.gameObject.TryGetComponent<IUnitIntakes>(out var enemyIntakes)) return;

            if (!_currentUnits.Contains(enemyIntakes)) return;

            _currentUnits.Remove(enemyIntakes);
            heroCapacity.AddUnit(enemyIntakes);
            ChangeProcessUnit?.Invoke();
        }

        private void Update()
        {
            Accumulation += (_inputService.IsIntakeButton() ? 1 : -1) * (Time.deltaTime * Strength);

            lightRenderer.color = new Color(255, 255, 255, Accumulation);

            if (!_inputService.IsIntakeButton())
            {
                ClearUnits();
                return;
            }

            Intake();
        }

        private void FixedUpdate()
        {
            TickEnemies();
        }

        public List<IUnitIntakes> GetCurrentProcessEnemies() => _currentUnits;

        private void Intake()
        {
            Physics2D.OverlapBoxNonAlloc((transform.position + Vector3.up * offsetY), sizeBox, 0, _hits,
                _layerMask);

            var results = _hits.Where(x => x != null).Select(item => item.transform.GetComponent<IUnitIntakes>())
                .ToList();

            RemoveOldEnemies(results);

            AddNewUnits(results);

            _hits = new Collider2D[50];
        }

        private void TickEnemies()
        {
            foreach (IUnitIntakes enemyIntakes in _currentUnits)
            {
                enemyIntakes.Move(pointToIntake.position,
                    (1 / enemyIntakes.Mass) * StrengthEngine * Time.deltaTime);
            }
        }

        private void RemoveOldEnemies(List<IUnitIntakes> current)
        {
            foreach (IUnitIntakes enemyIntakes in _currentUnits.ToList()
                         .Where(enemyIntakes => !current.Contains(enemyIntakes)))
            {
                enemyIntakes.OutUnit();
                _currentUnits.Remove(enemyIntakes);
            }

            ChangeProcessUnit?.Invoke();
        }

        private void AddNewUnits(List<IUnitIntakes> last)
        {
            foreach (IUnitIntakes unit in last.Where(enemy => !_currentUnits.Contains(enemy)))
            {
                unit.IntakeUnit(transform);

                _currentUnits.Add(unit);
            }

            ChangeProcessUnit?.Invoke();
        }

        private void ClearUnits()
        {
            if (_currentUnits.Count == 0) return;

            _currentUnits.ForEach((unit) => unit.OutUnit());

            _currentUnits.Clear();

            ChangeProcessUnit?.Invoke();
        }
    }
}