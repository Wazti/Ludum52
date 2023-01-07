using System;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Unit
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class UnitMove : MonoBehaviour, IWalkableUnit
    {
        [SerializeField] private UnitAnimator unitAnimator;

        [SerializeField] private Rigidbody2D rigidBody;

        public float Velocity
        {
            get => rigidBody.velocity.x;
        }

        private void FixedUpdate()
        {
            rigidBody.velocity = new Vector2(-2f, rigidBody.velocity.y);

            unitAnimator.SetFlip(Velocity > 0);
        }

        private void Start()
        {
            unitAnimator.Walk();
        }

        private void OnEnable()
        {
            unitAnimator.Walk();
        }

        private void OnDisable()
        {
            rigidBody.velocity = Vector2.zero;
        }
    }
}