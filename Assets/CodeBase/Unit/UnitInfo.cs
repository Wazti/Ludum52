using System;

namespace CodeBase.Unit
{
    [Serializable]
    public class UnitInfo
    {
        public float Mass;

        public UnitType Type;        
        public UnitInfo(float mass, UnitType type)
        {
            Mass = mass;
            Type = type;
        }
    }
}