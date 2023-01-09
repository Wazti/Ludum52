using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace CodeBase.Hero
{
    public class HeroViewDamage : MonoBehaviour
    {
        [SerializeField] private float _fadeOutTime = 0.5f;
        [SerializeField] private AnimationCurve _fadeCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
        [SerializeField] private Material _material;
        
        private Coroutine _lastFadeCoroutine;
        private float _currentFadeValue;
        private static readonly int Fade1 = Shader.PropertyToID("_Fade");

        private float CurrentFadeValue
        {
            get
            {
                return _currentFadeValue;
            }
            set
            {
                _currentFadeValue = value;
                _material.SetFloat(Fade1, value);
            }
        }
        
        public void OnDamage()
        {
            if (_lastFadeCoroutine != null)
            {
                StopCoroutine(_lastFadeCoroutine);
            }

            _lastFadeCoroutine = StartCoroutine(Fade());
        }

        private IEnumerator Fade()
        {
            var curTime = 0f;
            var t = 0f;
            
            while (curTime < _fadeOutTime)
            {
                curTime += Time.deltaTime;
                t = Mathf.Lerp(1,0 ,curTime / _fadeOutTime);
                _fadeCurve.Evaluate(t);

                CurrentFadeValue = t;

                yield return null;
            }
        }
    }
}