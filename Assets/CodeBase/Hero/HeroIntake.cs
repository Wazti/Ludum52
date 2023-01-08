using System;
using System.Linq;
using CodeBase.Buildings;
using CodeBase.Services.InputService;
using CodeBase.Stats.Scriptables;
using CodeBase.Unit;
using UnityEngine;
using Zenject;

namespace CodeBase.Hero
{
    public class HeroIntake : MonoBehaviour
    {
        [Inject] private IInputService _inputService;

        public float CurrentLength;

        [SerializeField] private Vector2 sizeBox;

        [SerializeField] private StatType rangeStat;
        [SerializeField] private HeroStatsSystem heroStats;

        private int _layerMask;
        private int _groundMask;
        private int _buildingMask;

        private Collider2D[] _hits = new Collider2D[50];

        [SerializeField] private Transform viewTransform;
        [SerializeField] private HeroAnimator heroAnimator;
        [SerializeField] private HeroTickUnits heroTickUnits;
        [SerializeField] private HeroTickBuilding heroTickBuildings;
        public event Action ActivateLight, DeactivateLight;

        private bool _isActive;

        private void Awake()
        {
            _layerMask = 1 << LayerMask.NameToLayer("Intakes");
            _groundMask = 1 << LayerMask.NameToLayer("Ground");
            _buildingMask = 1 << LayerMask.NameToLayer("Building");
        }

        private void Update()
        {
            if (_inputService.IsIntakeButtonUp())
            {
                DeActivateIntake();
                return;
            }

            if (_inputService.IsIntakeButtonDown())
            {
                ActivateIntake();
            }
        }

        private void FixedUpdate()
        {
            if (!_isActive) return;

            Intake();
        }

        private void DeActivateIntake()
        {
            _isActive = false;
            heroAnimator.HideFly();

            heroTickBuildings.ClearBuildings();
            heroTickUnits.ClearUnits();
            DeactivateLight?.Invoke();
        }

        private void ActivateIntake()
        {
            _isActive = true;
            heroAnimator.ShowFly();
            ActivateLight?.Invoke();
        }

        private void Intake()
        {
            CurrentLength = GetActualRange();

            IntakeBuilding();
            IntakeUnits();
        }

        private void IntakeBuilding()
        {
            Physics2D.OverlapBoxNonAlloc((transform.position),
                new Vector2(sizeBox.x, GetActualRange() * 2),
                viewTransform.localEulerAngles.z, _hits,
                _buildingMask);

            var results = _hits.Where(x => x != null).Select(item => item.transform.GetComponent<IBuildingIntakes>())
                .ToList();

            heroTickBuildings.RemoveOldBuilding(results);
            heroTickBuildings.AddNewBuildings(results);

            _hits = new Collider2D[50];
        }

        private void IntakeUnits()
        {
            Physics2D.OverlapBoxNonAlloc((transform.position),
                new Vector2(sizeBox.x, GetActualRange() * 2),
                viewTransform.localEulerAngles.z, _hits,
                _layerMask);

            var results = _hits.Where(x => x != null).Select(item => item.transform.GetComponent<IUnitIntakes>())
                .ToList();

            heroTickUnits.AddNewUnits(results);

            _hits = new Collider2D[50];
        }

        private float GetActualRange()
        {
            float angle = viewTransform.localEulerAngles.z - 90;

            var direction = GetDirectionVector2D(angle);

            var hit = Physics2D.Raycast(transform.position, direction, GetRange(),
                _groundMask);

            if (hit.collider != null) return Vector2.Distance(transform.position, hit.point);

            return GetRange();
        }

        private float GetRange()
        {
            return heroStats.StatsSystem.GetStat(rangeStat).Value;
        }

        public Vector2 GetDirectionVector2D(float angle)
        {
            return new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)).normalized;
        }
    }
}