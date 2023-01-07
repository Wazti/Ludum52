using CodeBase.Stats.Interfaces;
using UnityEngine;

namespace CodeBase.Stats.Scriptables
{
    [CreateAssetMenu(fileName = "New Stat Type", menuName = "Stats/Stat Type")]
    public class StatType : ScriptableObject, IStatType
    {
        [SerializeField] private new string name = "New Stat Type Name";
        [SerializeField] private float defaultValue = 0f;
        public string Name => name;
        public float DefaultValue => defaultValue;
    }
}