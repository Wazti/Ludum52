using CodeBase.Stats.Infrastructure;

namespace CodeBase.Stats.Interfaces
{
    public interface IStat
    {
        float Value { get; }
        void UpdateBaseValue(float newBase);

        void AddModifier(StatModifier modifier);

        void RemoveModifier(StatModifier modifier);
    }
}