using UnityEngine;

namespace CodeBase.Enemy
{
    public interface IEnemyIntakes
    {
        GameObject GameObject { get; }
        float Mass { get; set; }

        void Move(Vector3 point, float speed);
        void IntakeEnemy(Transform parent);
        void OutEnemy();
    }
}