using System;
using CodeBase.Data;
using CodeBase.Services;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.SaveLoad;
using Zenject;

namespace CodeBase.Infrastructure.States
{
    public class LoadProgressState : IState
    {
        private IGameStateMachine _gameStateMachine;
        private readonly IPersistentProgressService _progressService;
        private readonly ISaveLoadService _saveLoadService;
        private readonly ILevelSessionService _sessionService;

        [Inject]
        public LoadProgressState(IPersistentProgressService progressService,
            ISaveLoadService saveLoadService, ILevelSessionService sessionService)
        {
            _progressService = progressService;
            _saveLoadService = saveLoadService;
            _sessionService = sessionService;
        }

        public void Enter()
        {
            //  LoadProgressOrInitNew();
            _sessionService.CopyData();
            _gameStateMachine.Enter<LoadLevelState, string>("Main");
        }

        public void BindGameStateMachine(IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        public void Exit()
        {
        }

        private void LoadProgressOrInitNew() =>
            _progressService.Progress = _saveLoadService.LoadProgress() ?? NewProgress();

        private PlayerProgress NewProgress()
        {
            var progress = new PlayerProgress(initialLevel: "Main");
            return progress;
        }
    }
}