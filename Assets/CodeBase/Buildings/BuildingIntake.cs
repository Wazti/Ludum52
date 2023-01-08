using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CodeBase.Buildings
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class BuildingIntake : MonoBehaviour, IBuildingIntakes
    {
        private BoxCollider2D _collider;

        public event Action<BuildingIntake> IntakeEnter, IntakeExit;

        private bool _isActive;

        private void Awake()
        {
            _collider = GetComponent<BoxCollider2D>();
        }

        public void Intake()
        {
            _isActive = true;
            IntakeEnter?.Invoke(this);
        }

        public void OutTake()
        {
            _isActive = false;
            IntakeExit?.Invoke(this);
        }

        public Vector3 GetPosition()
        {
            Vector2 colliderPos = (Vector2) transform.position + _collider.offset;
            float randomPosX = Random.Range(colliderPos.x - _collider.size.x / 2, colliderPos.x + _collider.size.x / 2);
            float randomPosY = Random.Range(colliderPos.y - _collider.size.y / 2, colliderPos.y + _collider.size.y / 2);

            return new Vector3(randomPosX, randomPosY, 0);
        }
    }
}