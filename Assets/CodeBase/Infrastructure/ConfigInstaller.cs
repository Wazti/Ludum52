using CodeBase.Infrastructure.Factory.Scriptables;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure
{
    /// <summary>
    /// Injects all settings and containers
    /// </summary>
    [CreateAssetMenu(fileName = "ConfigInstaller", menuName = "Config/ConfigInstaller", order = 1)]
    public class ConfigInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private UnitConfigContainer unitConfigContainer;
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<UnitConfigContainer>()
                .FromInstance(unitConfigContainer)
                .AsSingle();
            
        }
    }
}