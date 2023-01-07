using System;
using System.Collections.Generic;
using CodeBase.Stats.Interfaces;
using CodeBase.Stats.Scriptables;

namespace CodeBase.Stats.Infrastructure
{
    public class StatsSystem
    {
        private readonly Dictionary<IStatType, Stat> stats = new Dictionary<IStatType, Stat>();

        public event Action<IStatType> StatsModified;

        public StatsSystem()
        {
        }

        public StatsSystem(BaseStats baseStats)
        {
            foreach (var stat in baseStats.Stats)
            {
                stats.Add(stat.StatType, new Stat(stat.Value));
            }
        }

        public void AddModifier(IStatType type, StatModifier modifier)
        {
            if (!stats.TryGetValue(type, out Stat stat))
            {
                stat = new Stat(type);

                stats.Add(type, stat);
            }

            stat.AddModifier(modifier);
            StatsModified?.Invoke(type);
        }

        public Stat GetStat(IStatType type)
        {
            stats.TryGetValue(type, out Stat stat);

            return stat;
        }

        public void RemoveModifier(IStatType type, StatModifier modifier)
        {
            if (!stats.TryGetValue(type, out Stat stat))
            {
                return;
            }

            stat.RemoveModifier(modifier);
            StatsModified?.Invoke(type);
        }
    }
}