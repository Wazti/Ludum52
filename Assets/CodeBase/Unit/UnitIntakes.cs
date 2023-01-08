using System;
using System.Collections;
using CodeBase.Stats.Scriptables;
using UnityEngine;

namespace CodeBase.Unit
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class UnitIntakes : MonoBehaviour, IUnitIntakes
    {
        private const string _progressFMODEvent = "Intake";

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
            var t =
                1 - (Math.Min(Vector2.Distance(point, transform.position), 4f) / 4f);
            
            fmodEmitter.SetParameter(_progressFMODEvent, t);

            transform.Translate(new Vector3(0,
                point.y - transform.position.y, 0) * speed);
        }

        public void OnEndMove()
        {
            StartCoroutine(FadeOutSound());
        }

        private IEnumerator FadeOutSound()
        {
            var curLerpTime = 0f;
            var timer = 1f;
            var t = 0f;

            while (t < 1)
            {
                curLerpTime += Time.deltaTime;
                t = curLerpTime / timer;
                var value = Mathf.Lerp(1, 2, t);
                fmodEmitter.SetParameter(_progressFMODEvent, value);

                yield return null;
            }
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