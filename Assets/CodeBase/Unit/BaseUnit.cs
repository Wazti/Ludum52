using CodeBase.Unit.Scriptables;
using UnityEngine;

namespace CodeBase.Unit
{
    public class BaseUnit : MonoBehaviour
    {
        [SerializeField] private UnitConfig _config;

        [SerializeField] private UnitMove unitMove;
        [SerializeField] private UnitFall unitFall;
        [SerializeField] private UnitFly unitFly;

        [SerializeField] private Collider2D _collider;
        [SerializeField] private Rigidbody2D _rigidbody2D;
        public UnitType UnitType => _config.unitType;

        public void Reset()
        {
            _rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
            _collider.isTrigger = false;
            unitMove.enabled = true;
            unitFall.enabled = false;
            unitFly.enabled = false;
            gameObject.SetActive(true);
        }

        public Vector2 BorderPositions;

        public void SetBorderValues(Vector2 border)
        {
            BorderPositions = border;
        }
        
    }
}