using UnityEngine;

namespace CodeBase.Unit
{
    public interface IUnitIntakes
    {
        GameObject GameObject { get; }
        float Mass { get; }

        void Move(Vector3 point, float speed);
        void OnEndMove();
        void IntakeUnit(Transform parent);
        void OutUnit();
    }
}