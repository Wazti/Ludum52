using CodeBase.Infrastructure.States;
using Zenject;

namespace CodeBase.Infrastructure
{
    public class Game
    {
        public IGameStateMachine StateMachine;

        public Game(IGameStateMachine gameStateMachine)
        {
            StateMachine = gameStateMachine;
        }
    }
}