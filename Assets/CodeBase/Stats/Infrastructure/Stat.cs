using System;
using System.Collections.Generic;
using CodeBase.Stats.Enums;
using CodeBase.Stats.Interfaces;

namespace CodeBase.Stats.Infrastructure
{
    public class Stat : IStat
    {
        private readonly List<StatModifier> modifiers = new List<StatModifier>();

        private float baseValue = 0f;
        private bool isDirty = true;

        private float value;
        
        public float Value
        {
            get
            {
                if (!isDirty) return value;
                value = CalculateValue();
                isDirty = false;
                return value;
            }
        }
        public Stat(float initialValue) => baseValue = initialValue;

        public Stat(IStatType statType) => baseValue = statType.DefaultValue;

        public void UpdateBaseValue(float newBase)
        {
            isDirty = true;
            baseValue = newBase;
        }

        public void AddModifier(StatModifier modifier)
        {
            isDirty = true;

            var index = modifiers.BinarySearch(modifier, new ByPriority());

            if (index < 0) { index = ~index; }
            modifiers.Insert(index, modifier);
        }
        
        public void RemoveModifier(StatModifier modifier)
        {
            isDirty = true;

            modifiers.Remove(modifier);
        }

        private float CalculateValue()
        {
            var finalValue = baseValue;
            var sumPercentAdditive = 0f;
            for (var i = 0; i< modifiers.Count; i++)
            {
                var modifier = modifiers[i];
                switch (modifier.ModifierType)
                {
                    case StatModifierTypes.Flat:
                        finalValue += modifier.Value;
                        break;
                    case StatModifierTypes.PercentAdditive:
                        sumPercentAdditive += modifier.Value;
                        if (i + 1 >= modifiers.Count || modifiers[i + 1].ModifierType != StatModifierTypes.PercentAdditive)
                        {
                            finalValue *= 1 + sumPercentAdditive;
                        }
                        break;
                    case StatModifierTypes.PercentMultiplicative:
                        finalValue *= 1 + modifier.Value;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

            }
            return finalValue;
        }
        private class ByPriority : IComparer<StatModifier>
        {
            public int Compare(StatModifier x, StatModifier y)
            {
                if (x == null || y == null) return 0;
                if (x.ModifierType > y.ModifierType) { return 1; }
                if (x.ModifierType < y.ModifierType) { return -1; }
                return 0;
            }
        }
    }
}