using UnityEngine;

namespace CodeBase.Unit
{
    public interface IUnitIntakes
    {
        GameObject GameObject { get; }
        float Mass { get; set; }

        void Move(Vector3 point, float speed);
        void IntakeUnit(Transform parent);
        void OutUnit();
    }
}