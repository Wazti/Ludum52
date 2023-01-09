using CodeBase.Services;
using CodeBase.Unit;
using UnityEngine;
using Zenject;

namespace CodeBase.Enemies
{
    public class EnemyIntake : MonoBehaviour, IUnitIntakes
    {
        [Inject] private IUnitPoolingService poolingService;

        [SerializeField] private EnemyMove enemyMove;
        [SerializeField] private EnemyShoot enemyShoot;
        [SerializeField] private EnemyAnimator animator;
        [SerializeField] private UnitType typeSpawn;

        [SerializeField] private Rigidbody2D rigidBody;
        [SerializeField] private Collider2D collider2d;
        public UnitType UnitType { get; }
        public GameObject GameObject { get; }
        public float Mass { get; }

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
            rigidBody.bodyType = RigidbodyType2D.Kinematic;
            collider2d.isTrigger = true;
            collider2d.enabled = false;

            var prefab = poolingService.SpawnUnit(typeSpawn);

            prefab.transform.position =
                new Vector3(transform.position.x,
                    transform.position.y, 0);

            prefab.transform.SetParent(transform);

            prefab.gameObject.SetActive(true);
            animator.SetState(EnemyAnimatorState.Dead);
            enemyMove.enabled = false;
            enemyShoot.enabled = false;
        
        }


        public void OutUnit()
        {
            //
        }
    }
}