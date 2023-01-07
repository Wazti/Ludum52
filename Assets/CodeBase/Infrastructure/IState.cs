using CodeBase.Infrastructure.States;

namespace CodeBase.Infrastructure
{
    public interface IState : IExitableState
    {
        void Enter();

        void BindGameStateMachine(IGameStateMachine gameStateMachine);
    }

    public interface IExitableState
    {
        void Exit();
    }

    public interface IPayloadedState<TPayload> : IExitableState
    {
        void Enter(TPayload tPayload);
    }
}