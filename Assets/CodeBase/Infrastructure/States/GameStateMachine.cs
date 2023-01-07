using System;
using System.Collections.Generic;
using CodeBase.Infrastructure.Factory;
using Zenject;

namespace CodeBase.Infrastructure.States
{
    public class GameStateMachine : IGameStateMachine
    {
        private readonly Dictionary<Type, IExitableState> _states;
        private IExitableState _activeState;

        [Inject]
        public GameStateMachine(BootstrapState bootstrapState, LoadLevelState loadLevelState,
            GameLoopState gameLoopState, LoadProgressState loadProgressState)
        {
            bootstrapState.BindGameStateMachine(this);
            loadLevelState.BindGameStateMachine(this);
            loadProgressState.BindGameStateMachine(this);

            _states = new Dictionary<Type, IExitableState>()
            {
                [typeof(BootstrapState)] = bootstrapState,
                [typeof(LoadLevelState)] = loadLevelState,
                [typeof(LoadProgressState)] = loadProgressState,
                [typeof(GameLoopState)] = gameLoopState,
            };
        }

        public void Enter<TState>() where TState : class, IState
        {
            var state = ChangeState<TState>();
            state.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
        {
            var state = ChangeState<TState>();

            state.Enter(payload);
        }

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exit();

            TState state = GetState<TState>();
            _activeState = state;

            return state;
        }


        private TState GetState<TState>() where TState : class, IExitableState => _states[typeof(TState)] as TState;
    }
}