using System;
using System.Collections;
using UnityEngine;

namespace CodeBase.Unit
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class UnitIntakes : MonoBehaviour, IUnitIntakes
    {
        [SerializeField] private UnitFly unitFly;
        [SerializeField] private UnitFall unitFall;
        [SerializeField] private UnitMove unitMove;

        private Transform _parent;
        private Rigidbody2D _rigidbody;


        private Vector3 _startPoint;

        public GameObject GameObject => gameObject;

        [SerializeField] [Range(0, 1000)] private float mass;

        public float Mass
        {
            get => mass;
            set => mass = value;
        }

        private void Awake()
        {
            _parent = transform.parent;
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        public void Move(Vector3 point, float speed)
        {
            transform.Translate(new Vector3(0,
                point.y - _startPoint.y, 0) * speed);
        }

        public void IntakeUnit(Transform parent)
        {
            transform.parent = parent;
            _startPoint = transform.position;
            _rigidbody.bodyType = RigidbodyType2D.Kinematic;

            unitFall.enabled = false;
            unitFly.enabled = true;
            unitMove.enabled = false;
        }


        public void OutUnit()
        {
            transform.SetParent(_parent);
            _rigidbody.bodyType = RigidbodyType2D.Dynamic;
            unitFly.enabled = false;
            unitFall.enabled = true;
        }
    }
}