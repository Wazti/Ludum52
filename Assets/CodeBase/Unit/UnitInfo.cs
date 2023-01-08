using System;

namespace CodeBase.Unit
{
    [Serializable]
    public class UnitInfo
    {
        public float Mass;

        public UnitInfo(float mass)
        {
            Mass = mass;
        }
    }
}