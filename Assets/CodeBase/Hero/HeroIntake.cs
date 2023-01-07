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

        [SerializeField] private float offsetY;

        [SerializeField] private Vector2 sizeBox;
        private int _layerMask;

        private Collider2D[] _hits = new Collider2D[50];

        [SerializeField] private Transform viewTransform;
        [SerializeField] private HeroAnimator heroAnimator;
        [SerializeField] private SpriteRenderer lightRenderer;
        [SerializeField] private HeroTickUnits heroTickUnits;

        private Tween _fadeTween;

        private bool _isActive;

        private void Awake()
        {
            lightRenderer.color = new Color(255, 255, 255, 0);
            _layerMask = 1 << LayerMask.NameToLayer($"Intakes");
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
            HideLightAnimation();
        }

        private void ActivateIntake()
        {
            _isActive = true;
            heroAnimator.ShowFly();
            ShowLightAnimation();
        }

        private void Intake()
        {
            Physics2D.OverlapBoxNonAlloc((transform.position),
                new Vector2(sizeBox.x, sizeBox.y * 2),
                viewTransform.localEulerAngles.z, _hits,
                _layerMask);

            var results = _hits.Where(x => x != null).Select(item => item.transform.GetComponent<IUnitIntakes>())
                .ToList();


            heroTickUnits.AddNewUnits(results);

            _hits = new Collider2D[50];
        }

        private void ShowLightAnimation()
        {
            _fadeTween?.Kill();
            lightRenderer.color = new Color(255, 255, 255, 0);
            _fadeTween = lightRenderer.DOFade(.5f, 0.5f).SetDelay(0.2f);
        }

        private void HideLightAnimation()
        {
            _fadeTween?.Kill();

            _fadeTween = lightRenderer.DOFade(0, 0.1f);
        }
    }
}