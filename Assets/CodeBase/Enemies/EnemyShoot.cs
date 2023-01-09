﻿using System;
using System.Collections;
using System.Linq;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Enemies
{
    public class EnemyShoot : MonoBehaviour
    {
        [SerializeField] private EnemyMove enemyMove;

        [SerializeField] private Vector2 rangeToShoot;

        [SerializeField] private float angle;

        [SerializeField] private EnemyAnimator enemyAnimator;

        [SerializeField] private LayerMask _layerMask;

        public float CoolDown;

        [SerializeField] private Bullet bullet;
        [SerializeField] private Transform pointLeftToShoot;
        [SerializeField] private Transform pointRightToShoot;

        [SerializeField] private float _coolDown;

        private Collider2D[] _hits = new Collider2D[1];

        private bool _isBack;

        private void Awake()
        {
            _layerMask = 1 << LayerMask.NameToLayer("Hero");
            enemyAnimator.animator.AddListener(AnimatorDrivers.Attack, Explode);
        }

        private void Explode()
        {
            var _bullet = Instantiate(bullet,
                _isBack ? pointLeftToShoot.position : pointRightToShoot.position,
                Quaternion.identity);
            bullet.Flip(_isBack);
            StartCoroutine(EnableMovement());
        }

        private IEnumerator EnableMovement()
        {
            yield return new WaitForSeconds(1f);
            enemyMove.enabled = true;
        }

        private void FixedUpdate()
        {
            _coolDown -= Time.deltaTime;

            if (_coolDown <= 0)
            {
                CheckInRange();
            }
        }

        private void CheckInRange()
        {
            Physics2D.OverlapBoxNonAlloc(transform.position, rangeToShoot,
                enemyAnimator.animator.Renderer.flipX ? angle : -angle, _hits, _layerMask);

            var hero = _hits.FirstOrDefault();

            if (hero != null)
            {
                enemyMove.enabled = false;
                Shoot();
                _hits = new Collider2D[1];
            }
        }

        private void Shoot()
        {
            _isBack = enemyMove._isBack;
            _coolDown = CoolDown;
            enemyAnimator.Shoot();
        }
    }
}