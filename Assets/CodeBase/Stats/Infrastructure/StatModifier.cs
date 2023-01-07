using CodeBase.Stats.Enums;
using UnityEngine;

namespace CodeBase.Stats.Infrastructure
{
    public class StatModifier
    {
        [SerializeField] private readonly StatModifierTypes modifierType;
        [SerializeField] private readonly float value;

        public StatModifier(StatModifierTypes modifierType, float value)
        {
            this.modifierType = modifierType;
            this.value = value;
        }
        public float Value => value;
        public StatModifierTypes ModifierType => modifierType;

    }
}