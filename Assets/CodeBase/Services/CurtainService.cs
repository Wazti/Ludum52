using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Services
{
    public class CurtainService : MonoBehaviour, ICurtainService
    {
        [SerializeField] private Image image;

        private Tween _tween;

        public void DOFade(int value, float duration, Action callback, bool ignoreTimeScale = false)
        {
            _tween?.Kill();
            _tween = image.DOFade(value, duration).SetEase(Ease.Linear).OnComplete(() => { callback?.Invoke(); }
            ).SetUpdate(ignoreTimeScale).SetDelay(.5f);
        }

        public void SetFade(int value)
        {
            image.color = new Color(0, 0, 0, value);
        }
    }
}