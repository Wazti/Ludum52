using CodeBase.Infrastructure.States;
using Zenject;

namespace CodeBase.Infrastructure
{
    public class Game
    {
        public GameStateMachine StateMachine;

        public Game(GameStateMachine gameStateMachine)
        {
            StateMachine = gameStateMachine;
        }
    }
}