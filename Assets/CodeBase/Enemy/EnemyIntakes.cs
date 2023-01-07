using System;
using System.Collections;
using UnityEngine;

namespace CodeBase.Enemy
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class EnemyIntakes : MonoBehaviour, IEnemyIntakes
    {
        private Transform _parent;

        private Rigidbody2D _rigidbody;

        private Vector3 _startPoint;

        public Transform Transform => transform;


        [SerializeField] [Range(0, 1000)] private float mass;
        private Coroutine _moveCenterCoroutine;

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

        public void IntakeEnemy(Transform parent)
        {
            StopMovingCenter();

            transform.parent = parent;
            _startPoint = transform.position;
            _rigidbody.bodyType = RigidbodyType2D.Kinematic;
            _moveCenterCoroutine = StartCoroutine(MoveToCenter());
        }

        private IEnumerator MoveToCenter()
        {
            float speed = 0f;
            float localX = transform.localPosition.x;
            while (Math.Abs(transform.localPosition.x) > 0.01f)
            {
                transform.localPosition = new Vector3(Mathf.Lerp(localX, 0f, speed), transform.localPosition.y,
                    transform.localPosition.z);
                speed += Time.deltaTime;
                yield return new WaitForFixedUpdate();
            }
        }

        public void OutEnemy()
        {
            StopMovingCenter();

            transform.SetParent(_parent);
            _rigidbody.bodyType = RigidbodyType2D.Dynamic;
        }

        private void StopMovingCenter()
        {
            if (_moveCenterCoroutine != null)
                StopCoroutine(_moveCenterCoroutine);
        }
    }
}