using CodeBase.Stats.Scriptables;
using UnityEngine;

namespace CodeBase.Unit.Scriptables
{
    [CreateAssetMenu(fileName = "Enemy", menuName = "Enemy/Config")]
    public class UnitConfig : ScriptableObject
    {
        public BaseUnit prefab;
        public string name;
        public BaseStats stats;
    }
}