using System;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.Buildings
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class BuildingFilling : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _renderer;

        [SerializeField] private BuildingContainer buildingContainer;


        private Tween _fadeTween;
        private static readonly int Fade = Shader.PropertyToID("_Fade");

        private void Awake()
        {
            buildingContainer.UnitChange += SetFade;
            _renderer.material.SetFloat(Fade, 1);
        }

        private void OnDestroy()
        {
            buildingContainer.UnitChange -= SetFade;
        }

        private void SetFade()
        {
            _fadeTween?.Kill();

            _fadeTween = DOVirtual.Float(_renderer.material.GetFloat(Fade), buildingContainer.Percentage, 0.5f,
                (v) => { _renderer.material.SetFloat(Fade, v); });
        }
    }
}