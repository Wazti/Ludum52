using CodeBase.Infrastructure.States;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour
    {
        private IGameStateMachine _gameStateMachine;
        private Game _game;

        [Inject]
        public void Construct(IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        private void Awake()
        {
            _game = new Game(_gameStateMachine);

            _game.StateMachine.Enter<BootstrapState>();

            DontDestroyOnLoad(this);
        }
    }
}