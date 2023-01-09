using System;
using Aarthificial.Reanimation;
using CodeBase.Logic;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace CodeBase.Enemies
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private Reanimator _reanimator;

        [SerializeField] private FMODUnity.EventReference fmodEvent;

        public int damage = 1;

        public int speed;

        private bool _stop;
        private Tween _tween;
        public bool forward = true;

        private void Awake()
        {
            _reanimator.AddListener("end", Disable);
        }

        private void Disable()
        {
            gameObject.SetActive(false);
        }


        public void Flip(bool isBack)
        {
            forward = !isBack;
            _reanimator.Flip = isBack;
            _tween = DOVirtual.DelayedCall(5f, () => { Destroy(gameObject); });
        }

        private void Update()
        {
            if (_stop) return;

            Vector3 direction = new Vector3(forward ? 1f : -1f, 1f, 0f).normalized;

            transform.Translate(direction * Time.deltaTime * speed);
        }

        public void Explodes()
        {
            _tween?.Kill();
            _stop = true;
            FMODUnity.RuntimeManager.PlayOneShot(fmodEvent, transform.position);
            _reanimator.Set(AnimatorDrivers.State, 1);
        }
    }
}