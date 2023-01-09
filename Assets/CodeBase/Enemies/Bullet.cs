using System;
using Aarthificial.Reanimation;
using CodeBase.Logic;
using TMPro;
using UnityEngine;

namespace CodeBase.Enemies
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private Reanimator _reanimator;

        public int damage = 1;

        private bool _stop;

        public bool forward = true;

        private void Awake()
        {
            _reanimator.AddListener("end", Disable);
        }

        private void Disable()
        {
            gameObject.SetActive(false);
        }

        public void Flip(bool flip)
        {
            forward = !flip;
            _reanimator.Flip = flip ;
        }
        private void Update()
        {
            if (_stop) return;

            Vector3 direction = new Vector3(forward ? 1f : -1f, 1f, 0f).normalized;

            transform.Translate(direction * Time.deltaTime * 3f);
        }

        public void Explodes()
        {
            _stop = true;
            _reanimator.Set(AnimatorDrivers.State, 1);
        }
    }
}