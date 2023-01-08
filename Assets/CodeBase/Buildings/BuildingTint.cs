using UnityEngine;

namespace CodeBase.Buildings
{
    public class BuildingTint : MonoBehaviour, IBuildingIntakes
    {
        [SerializeField] private SpriteRenderer _renderer;

        private static readonly int Outline = Shader.PropertyToID("_Outline");

        private bool _isActive = false;

        public bool IsActive
        {
            get => _isActive;
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
    }
}