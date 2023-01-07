using CodeBase.Unit;

namespace CodeBase.Infrastructure.Factory
{
    public interface IUnitFactory
    {
        BaseUnit Create(UnitType param);
    }
}