using CodeBase.Infrastructure.Logic;
using CodeBase.Infrastructure.States;
using CodeBase.Services;

namespace CodeBase.Infrastructure
{
    public class Game
    {
        public GameStateMachine StateMachine { get; }

        public Game(ICoroutineRunner runner) =>
            StateMachine = new GameStateMachine(new SceneLoader(runner), new LoadCurtain(runner), AllServices.Container, runner);
    }
}