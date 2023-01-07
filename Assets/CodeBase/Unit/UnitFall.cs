using System;
using UnityEngine;

namespace CodeBase.Unit
{
    [RequireComponent(typeof(UnitAnimator))]
    public class UnitFall : MonoBehaviour
    {
        [SerializeField] private UnitMove unitMove;
        [SerializeField] private UnitAnimator unitAnimator;

        [SerializeField] private Vector2 sizeBox;

        [SerializeField] private LayerMask layerMask;
        [SerializeField] private float offSetY;

        private void Awake()
        {
            enabled = false;
        }

        private void OnEnable()
        {
            unitAnimator.Fly();
        }

        private void FixedUpdate()
        {
            CheckGround();
        }

        private void CheckGround()
        {
            var results = new Collider2D[1];
            Physics2D.OverlapBoxNonAlloc(transform.position + new Vector3(0, offSetY, 0), sizeBox, 0, results,
                layerMask);

            if (results[0] == null) return;

            unitMove.enabled = true;
            enabled = false;
        }
    }
}