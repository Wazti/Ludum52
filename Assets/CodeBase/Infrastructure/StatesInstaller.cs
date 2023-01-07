using CodeBase.Infrastructure.States;
using Zenject;

namespace CodeBase.Infrastructure
{
    public class StatesInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<BootstrapState>().AsSingle().NonLazy();
            Container.Bind<GameLoopState>().AsSingle().NonLazy();
            Container.Bind<LoadLevelState>().AsSingle().NonLazy();
            Container.Bind<LoadProgressState>().AsSingle().NonLazy();

            Container.Bind<GameStateMachine>().AsSingle().NonLazy();
        }
    }
}