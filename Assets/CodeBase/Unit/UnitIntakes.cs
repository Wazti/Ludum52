using System;
using System.Collections;
using CodeBase.Stats.Scriptables;
using UnityEngine;

namespace CodeBase.Unit
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class UnitIntakes : MonoBehaviour, IUnitIntakes
    {
        [SerializeField] private UnitFly unitFly;
        [SerializeField] private UnitFall unitFall;
        [SerializeField] private UnitMove unitMove;

        [SerializeField] private UnitStats unitStats;
        [SerializeField] private StatType massStat;

        private Transform _parent;
        private Rigidbody2D _rigidbody;


        private Vector3 _startPoint;

        public GameObject GameObject => gameObject;

        private float mass;

        public float Mass
        {
            get => unitStats.StatsSystem.GetStat(massStat).Value;
        }

        private void Awake()
        {
            _parent = transform.parent;
            _rigidbody = GetComponent<Rigidbody2D>();
        }


        public void Move(Vector3 point, float speed)
        {
            transform.Translate(new Vector3(0,
                point.y - transform.position.y, 0) * speed);
        }

        public void IntakeUnit(Transform parent)
        {
            _parent = transform.parent;

            transform.SetParent(parent);

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