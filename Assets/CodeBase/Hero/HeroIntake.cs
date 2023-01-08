using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Logic;
using CodeBase.Services.InputService;
using CodeBase.Stats.Infrastructure;
using CodeBase.Stats.Scriptables;
using CodeBase.Unit;
using DG.Tweening;
using NaughtyAttributes;
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

        private Collider2D[] _hits = new Collider2D[50];

        [SerializeField] private Transform viewTransform;
        [SerializeField] private HeroAnimator heroAnimator;
        [SerializeField] private HeroTickUnits heroTickUnits;
        public event Action ActivateLight, DeactivateLight;

        private bool _isActive;

        private void Awake()
        {
            _layerMask = 1 << LayerMask.NameToLayer("Intakes");
            _groundMask = 1 << LayerMask.NameToLayer("Ground");
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