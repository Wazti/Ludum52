using System;
using System.Collections;
using System.Collections.Generic;
using CodeBase.Logic;
using CodeBase.Services.PersistentProgress;
using UnityEngine;
using Zenject;

namespace CodeBase.Sections
{
    public class SectionDynamic : MonoBehaviour
    {
        [Inject] private IPersistentProgressService _progressService;

        private const float lengthSection = 24f;
        [SerializeField] public float LengthSection => lengthSection;

        [SerializeField] private List<Transform> _variants;

        private Transform _camera;

        private int _lengthSections;

        public event Action<SectionDynamic> OnFarSection;

        private void Awake()
        {
            var level = _progressService.Progress.LevelHero.CurrentLevel;

            _variants.ForEach(t => { t.gameObject.SetActive(false); });

            _variants[GetType(level)].gameObject.SetActive(true);
        }

        private int GetType(int level)
        {
            if (level < 2) return 0;

            if (level < 5) return 1;

            return 2;
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