using System;
using CodeBase.Infrastructure.Factory;
using CodeBase.Stats.Scriptables;
using CodeBase.Unit;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace CodeBase.Enemies
{
    public class EnemyMove : MonoBehaviour
    {
        [Inject] private IGameFactory _gameFactory;

        [SerializeField] private EnemyAnimator unitAnimator;

        [SerializeField] private Rigidbody2D rigidBody;


        private float _distanceLeft;
        public float _speed;

        public bool _isBack;

        [SerializeField] private bool isBackSprite = true;

        public float Velocity
        {
            get => rigidBody.velocity.x;
        }
        

        private Transform Hero;

        private bool isStay = false;

        private void Start()
        {
            unitAnimator.Walk();

            Hero = _gameFactory.Hero.transform;
        }

        private void FixedUpdate()
        {
            var posX = Hero.transform.position.x;

            var posEnemyX = transform.position.x;
            var leftPoint = (posX - 3f);
            var rightPoint = (posX + 3f);

            var diffLeft = Math.Abs(leftPoint - posEnemyX);
            var diffRight = Math.Abs(posEnemyX - rightPoint);

            if (diffLeft < 0.1f || diffRight < 0.1f)
            {
                unitAnimator.ResetToIdle();
                rigidBody.velocity = Vector2.zero;
                isStay = true;
                return;
            }

            if (diffLeft < diffRight)
            {
                _isBack = false;
            }
            else
            {
                _isBack = true;
            }

            rigidBody.velocity = new Vector2((_isBack ? -_speed : _speed), rigidBody.velocity.y);

            if (isStay)
            {
                unitAnimator.Walk();
            }

            isStay = true;

            unitAnimator.SetFlip(isBackSprite ? Velocity > 0 : Velocity < 0);
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