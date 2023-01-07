using CodeBase.Infrastructure.States;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour
    {
        private GameStateMachine _gameStateMachine;
        private Game _game;

        private ICoroutineRunner _coroutineRunner;


        [Inject]
        public void Construct(ICoroutineRunner coroutineRunner, GameStateMachine gameStateMachine)
        {
            _coroutineRunner = coroutineRunner;
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