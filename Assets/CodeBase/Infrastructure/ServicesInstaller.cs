using CodeBase.Infrastructure.Factory;
using CodeBase.Services;
using CodeBase.Services.InputService;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.SaveLoad;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure
{
    public class ServicesInstaller : MonoInstaller
    {
        [SerializeField] private CoroutineRunner coroutineRunner;
        [SerializeField] private UnitPoolingService unitPoolingService;
        [SerializeField] private CurtainService curtainService;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<InputService>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PersistentProgressService>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<GameFactory>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<UnitFactory>().AsSingle().NonLazy();
            Container.Bind<ICoroutineRunner>().To<CoroutineRunner>().FromComponentInNewPrefab(coroutineRunner)
                .AsSingle()
                .NonLazy();
            Container.Bind<IUnitPoolingService>().To<UnitPoolingService>().FromComponentInNewPrefab(unitPoolingService)
                .AsSingle()
                .NonLazy();
            Container.Bind<ICurtainService>().To<CurtainService>().FromComponentInNewPrefab(curtainService)
                .AsSingle()
                .NonLazy();
            Container.BindInterfacesAndSelfTo<SaveLoadService>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<StaticDataService>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<SceneLoader>().AsSingle().NonLazy();
        }
    }
}