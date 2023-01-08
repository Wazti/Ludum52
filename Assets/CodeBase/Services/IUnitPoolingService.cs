using CodeBase.Unit;

namespace CodeBase.Services
{
    public interface IUnitPoolingService
    {
        BaseUnit SpawnUnit(UnitType type);
        void DespawnUnit(BaseUnit unit);
    }
}