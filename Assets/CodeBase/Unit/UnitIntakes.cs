using System;
using CodeBase.Stats.Scriptables;
using UnityEngine;

namespace CodeBase.Unit
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class UnitIntakes : MonoBehaviour, IUnitIntakes
    {
        private const string _progressFMODEvent = "Progress";

        [SerializeField] private UnitFly unitFly;
        [SerializeField] private UnitFall unitFall;
        [SerializeField] private UnitMove unitMove;

        [SerializeField] private UnitStats unitStats;
        [SerializeField] private StatType massStat;

        [SerializeField] private FMODUnity.StudioEventEmitter fmodEmitter;

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

        private void OnDisable()
        {
            fmodEmitter.Stop();
            fmodEmitter.SetParameter(_progressFMODEvent, 0);
        }


        public void Move(Vector3 point, float speed)
        {
            fmodEmitter.SetParameter(_progressFMODEvent,
                1 - (Math.Min(Vector2.Distance(point, transform.position), 4f) / 4f));

            transform.Translate(new Vector3(0,
                point.y - transform.position.y, 0) * speed);
        }

        public void IntakeUnit(Transform parent)
        {
            fmodEmitter.Play();
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
            fmodEmitter.Stop();
            transform.SetParent(_parent);
            _rigidbody.bodyType = RigidbodyType2D.Dynamic;
            unitFly.enabled = false;
            unitFall.enabled = true;
        }
    }
}