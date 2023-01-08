using System;
using System.Collections;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Sections
{
    public class SectionDynamic : MonoBehaviour
    {
        private const float lengthSection = 24f;
        [SerializeField] public float LengthSection => lengthSection;

        private Transform _camera;

        private int _lengthSections;

        public event Action<SectionDynamic> OnFarSection;

        private void Awake()
        {
            _camera = Camera.main.transform;
        }

        private bool _isActive = true;

        /*private void FixedUpdate()
        {
            if (!_isActive) return;

            var distance = transform.position.x - _camera.position.x;

            if (Math.Abs(distance) > lengthSection * _lengthSections / 2)
            {
                _isActive = false;
                OnFarSection?.Invoke(this);
            }
        }*/

        public void SetupLengthSections(int length) => _lengthSections = length;

        


        public void SetActive(bool active) => _isActive = active;
    }
}