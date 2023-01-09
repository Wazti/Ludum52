using CodeBase.Infrastructure.Factory;
using CodeBase.Services.PersistentProgress;
using Zenject;

namespace CodeBase.Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private const string Initialpoint = "InitialPoint";
        private IGameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly IGameFactory _gameFactory;
        private IPersistentProgressService _progressService;

        [Inject]
        public LoadLevelState(SceneLoader sceneLoader,
            IGameFactory gameFactory, IPersistentProgressService progressService)
        {
            _progressService = progressService;
            _sceneLoader = sceneLoader;
            _gameFactory = gameFactory;
        }

        public void BindGameStateMachine(IGameStateMachine gameStateMachine) => _gameStateMachine = gameStateMachine;

        public void Enter(string nameScene)
        {
            _gameFactory.Cleanup();
            
            _sceneLoader.Load(nameScene, OnLoaded);
        }

        public void Exit()
        {
        }

        private void OnLoaded()
        {
            InformProgressReaders();
            _gameStateMachine.Enter<GameLoopState>();
        }

        private void InformProgressReaders()
        {
            foreach (var progressReader in _gameFactory.ProgressReaders)
            {
                progressReader.LoadProgress(_progressService.Progress);
            }
        }
    }
}