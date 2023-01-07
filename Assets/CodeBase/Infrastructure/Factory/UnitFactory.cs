using CodeBase.Infrastructure.Factory.Interfaces;
using CodeBase.Unit;
using Zenject;

namespace CodeBase.Infrastructure.Factory
{
    public class UnitFactory : IFactory<UnitType, BaseUnit>
    {
        private readonly IUnitConfigContainer _configContainer;
        private readonly DiContainer _container;


        public UnitFactory(IUnitConfigContainer prefabContainer, DiContainer diContainer)
        {
            _configContainer = prefabContainer;
            _container = diContainer;
        }

        public BaseUnit Create(UnitType param)
        {
            var gameObject = _container.InstantiatePrefabForComponent<BaseUnit>(_configContainer.Get(param).prefab);
            return gameObject;
        }
    }
}