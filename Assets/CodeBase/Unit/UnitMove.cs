using System;
using CodeBase.Logic;
using CodeBase.Stats.Scriptables;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CodeBase.Unit
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class UnitMove : MonoBehaviour, IWalkableUnit
    {
        [SerializeField] private BaseUnit baseUnit;

        [SerializeField] private UnitAnimator unitAnimator;
        [SerializeField] private UnitStats unitStats;

        [SerializeField] private Rigidbody2D rigidBody;

        [SerializeField] private float distanceToDecision;

        [SerializeField] private StatType statSpeed;

        private float _distanceLeft;
        private float _speed;

        private bool _isBack;

        [SerializeField] private bool isBackSprite = true;

        public float Velocity
        {
            get => rigidBody.velocity.x;
        }

        private void Start()
        {
            unitAnimator.Walk();
            _speed = unitStats.StatsSystem.GetStat(statSpeed).Value;
        }

        private void FixedUpdate()
        {
            rigidBody.velocity = new Vector2((_isBack ? -_speed : _speed), rigidBody.velocity.y);

            _distanceLeft += Time.deltaTime * (_isBack ? _speed : -_speed);


            unitAnimator.SetFlip(isBackSprite ? Velocity > 0 : Velocity < 0);

            if (Math.Abs(_distanceLeft) < 0.05f)
            {
                GenerateDistance();
            }
        }

        private void GenerateDistance()
        {
            var point = Random.Range(Math.Max(baseUnit.BorderPositions.x, transform.position.x - 5f),
                Math.Max(baseUnit.BorderPositions.y, transform.position.x + 5f));

            _distanceLeft = point - transform.position.x;

            _isBack = _distanceLeft < 0;
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