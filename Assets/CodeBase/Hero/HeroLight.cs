using System;
using CodeBase.Stats.Scriptables;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeBase.Hero
{
    public class HeroLight : MonoBehaviour
    {
        [MinMaxSlider(.75f, 3.5f, true)]
        [SerializeField]
        private Vector2 rangeLimitLight;

        [SerializeField] private StatType rangeLight;

        [SerializeField] private HeroStatsSystem heroStatsSystem;

        [SerializeField] private SpriteRenderer lightRender;
        [SerializeField] private HeroIntake heroIntake;

        private static readonly int Fade = Shader.PropertyToID("_Fade");

        private Tween _fadeTween;

        private void Awake()
        {
            heroIntake.ActivateLight += ShowLightAnimation;
            heroIntake.DeactivateLight += HideLightAnimation;
        }

        private void Start()
        {
            lightRender.color = new Color(255, 255, 255, 0);
        }

        private void FixedUpdate()
        {
            SetLengthLight();
        }

        private void OnDestroy()
        {
            heroIntake.ActivateLight -= ShowLightAnimation;
            heroIntake.DeactivateLight -= HideLightAnimation;
        }

        private void SetLengthLight()
        {
            lightRender.material.SetFloat(Fade, GetCurrentLength());
        }

        private float GetCurrentLength()
        {
            var length = heroIntake.CurrentLength;

            if (length < rangeLimitLight.x) return 0.2f;

            if (length > rangeLimitLight.y) return 1;

            return 0.2f + ((length - rangeLimitLight.x) /
                             (rangeLimitLight.y - rangeLimitLight.x)) * 0.8f;
        }

        private void ShowLightAnimation()
        {
            _fadeTween?.Kill();

            lightRender.color = new Color(255, 255, 255, 0);
            _fadeTween = lightRender.DOFade(.5f, 0.3f).SetDelay(0.1f);
        }

        private void HideLightAnimation()
        {
            _fadeTween?.Kill();

            _fadeTween = lightRender.DOFade(0, 0.1f);
        }

        [ShowInInspector]
        [ProgressBar(0f, 100f, 0.1f, .3f, .4f)]
        public float Percentage
        {
            get
            {
                if (heroStatsSystem.StatsSystem == null) return 0;

                return (heroStatsSystem.StatsSystem.GetStat(rangeLight).Value - rangeLimitLight.x) /
                    (rangeLimitLight.y - rangeLimitLight.x) * 100;
            }
        }
    }
}