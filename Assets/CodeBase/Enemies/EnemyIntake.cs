using CodeBase.Services;
using CodeBase.Unit;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace CodeBase.Enemies
{
    public class EnemyIntake : MonoBehaviour, IUnitIntakes
    {
        [Inject] private IUnitPoolingService poolingService;

       [SerializeField] private Sprite death;

        [SerializeField] private EnemyMove enemyMove;
        [SerializeField] private EnemyShoot enemyShoot;
        [SerializeField] private EnemyAnimator animator;
        [SerializeField] private UnitType typeSpawn;

        [SerializeField] private Rigidbody2D rigidBody;
        [SerializeField] private Collider2D collider2d;
        public UnitType UnitType { get; }
        public GameObject GameObject { get; }
        public float Mass { get; }

        private bool _isDone;

        public void Move(Vector3 point, float speed)
        {
            //
        }

        public void OnEndMove()
        {
            //
        }

        public void IntakeUnit(Transform parent)
        {
            if (_isDone) return;

            _isDone = true;
            rigidBody.bodyType = RigidbodyType2D.Static;
            collider2d.enabled = false;
            gameObject.layer = LayerMask.NameToLayer("Water");

            var prefab = poolingService.SpawnUnit(typeSpawn);

            prefab.transform.position =
                new Vector3(transform.position.x,
                    transform.position.y, 0);

            prefab.transform.SetParent(transform);

            prefab.gameObject.SetActive(true);

            animator.SetState(EnemyAnimatorState.Dead);
            enemyMove.enabled = false;
            enemyShoot.enabled = false;
            enabled = false;
            animator.enabled = false;
            animator.animator.enabled = false;
            animator.animator.Renderer.sprite = death;
        }


        public void OutUnit()
        {
            //
        }
    }
}