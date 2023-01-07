using System.ComponentModel;
using CodeBase.Infrastructure.Factory;
using CodeBase.Services;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.SaveLoad;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private const string Initial = "Initial";

        private IGameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;

        public BootstrapState(SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        public void BindGameStateMachine(IGameStateMachine gameStateMachine) => _stateMachine = gameStateMachine;

        public void Enter()
        {
            _sceneLoader.Load(Initial, onLoaded: EnterLoadLevel);
        }

        public void Exit()
        {
        }

        private void EnterLoadLevel() => _stateMachine.Enter<LoadProgressState>();
    }
}