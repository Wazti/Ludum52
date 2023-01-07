using System;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Stats.Scriptables
{
    [CreateAssetMenu(fileName = "New_BaseStats", menuName = "Stats/Base Stat")]
    public class BaseStats : ScriptableObject
    {
        [SerializeField] private List<BaseStat> stats = new List<BaseStat>();

        public List<BaseStat> Stats => stats;

        [Serializable]
        public class BaseStat
        {
            [SerializeField] private StatType statType = null;
            [SerializeField] private float value = 0f;

            public StatType StatType => statType;
            public float Value => value;
        }

        public int GetValueOfStat(StatType type)
        {
            var stat = Stats.Find(item => item.StatType == type);
            return stat == null ? 0 : (int) stat.Value;
        }

        public bool TryGetStat(StatType type)
        {
            var stat = Stats.Find(item => item.StatType == type);
            return stat != null;
        }
        public bool IsStatInBase(StatType type)
        {
            return Stats.Find(item => item.StatType == type) != null;
        }
    }
}