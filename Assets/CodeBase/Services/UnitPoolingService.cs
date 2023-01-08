using System.Collections.Generic;
using CodeBase.Infrastructure.Factory;
using CodeBase.Unit;
using UnityEngine;
using Zenject;

namespace CodeBase.Services
{
    public class UnitPoolingService : MonoBehaviour, IUnitPoolingService
    {
        private IUnitFactory _factory;

        [Inject]
        public void Construct(IUnitFactory unitFactory) => _factory = unitFactory;

        private readonly List<BaseUnit> _activeUnits = new List<BaseUnit>();

        private readonly List<BaseUnit> _freeUnits = new List<BaseUnit>();


        public BaseUnit SpawnUnit(UnitType type)
        {
            var freeUnit = GetFreeUnit(type);

            if (freeUnit != null)
            {
                RemoveFreeUnit(freeUnit);
                AddActiveUnit(freeUnit);

                freeUnit.Reset();
                return freeUnit;
            }

            var unit = _factory.Create(type);

            AddActiveUnit(unit);

            return unit;
        }

        public void DespawnUnit(BaseUnit unit)
        {
            RemoveActiveUnit(unit);
            AddFreeUnit(unit);
            unit.gameObject.SetActive(false);
        }

        private void AddFreeUnit(BaseUnit unit) => _freeUnits.Add(unit);

        private void AddActiveUnit(BaseUnit unit) => _activeUnits.Add(unit);

        private void RemoveActiveUnit(BaseUnit unit) => _activeUnits.Remove(unit);
        private void RemoveFreeUnit(BaseUnit unit) => _freeUnits.Remove(unit);


        private BaseUnit GetFreeUnit(UnitType type) => _freeUnits.Find(x => x.UnitType == type);
    }
}