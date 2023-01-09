using System;
using CodeBase.Services.PersistentProgress;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace CodeBase.UI
{
    public class UpgradeButtonUI : MonoBehaviour
    {
        [Inject] private IPersistentProgressService _progressService;

        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private CanvasGroup canvasGroup;

        public event Action PressedButton;

        private float startPoint;

        private bool _isActive;

        private Tween _showAnimation;

        private void Awake()
        {
            startPoint = rectTransform.localPosition.y;
        }


        public void OnClick()
        {
            PressedButton?.Invoke();
        }

        public void Show()
        {
            if (_isActive) return;

            _showAnimation?.Kill();
            rectTransform.localPosition = new Vector3(rectTransform.localPosition.x, startPoint - 20,
                rectTransform.localPosition.z);
            canvasGroup.alpha = 0;

            var fade = _progressService.Progress.Points > 0 ? 1f : 0.5f;

            _showAnimation = DOTween.Sequence(rectTransform.DOLocalMoveY(startPoint, .5f))
                .Join(canvasGroup.DOFade(fade, .5f));
            _isActive = true;
        }

        public void Hide()
        {
            _isActive = false;
            _showAnimation?.Kill();
            _showAnimation = DOTween.Sequence(rectTransform.DOLocalMoveY(startPoint - 20, .5f))
                .Join(canvasGroup.DOFade(0, .5f));
        }
    }
}