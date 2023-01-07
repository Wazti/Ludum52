using System;
using System.Collections;
using UnityEngine;

namespace CodeBase.Unit
{
    public class UnitFly : MonoBehaviour
    {
        [SerializeField] private UnitAnimator unitAnimator;
        [SerializeField] private Collider2D _collider;

        private Coroutine _moveCenterCoroutine;

        private void Awake()
        {
            enabled = false;
        }

        private void OnEnable()
        {
            _moveCenterCoroutine = StartCoroutine(MoveToCenter());
            unitAnimator.Fly();
            _collider.isTrigger = true;
        }

        private void OnDisable()
        {
            StopMovingCenter();
            _collider.isTrigger = false;
        }

        private void StopMovingCenter()
        {
            if (_moveCenterCoroutine != null)
                StopCoroutine(_moveCenterCoroutine);
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
    }
}