using System;
using UnityEngine;

namespace CodeBase.Buildings
{
    public class BuildingTint : MonoBehaviour, IBuildingIntakes
    {
        [SerializeField] private BuildingContainer buildingContainer;
        
        [SerializeField] private SpriteRenderer _renderer;

        private static readonly int Outline = Shader.PropertyToID("_Outline");

        private bool _isActive = false;

        public bool IsActive
        {
            get => _isActive;
        }

        private void Awake()
        {
            buildingContainer.UnitChange += OnChangeUnit;
        }

        private void OnChangeUnit()
        {
            if (!buildingContainer.IsContainUnits) gameObject.SetActive(false);
        }

        public void Intake()
        {
            _isActive = true;
            _renderer.material.SetInt(Outline, 1);
        }

        public void OutTake()
        {
            _isActive = false;
            _renderer.material.SetInt(Outline, 0);
        }

        private void OnDisable()
        {
            OutTake();
        }
    }
}