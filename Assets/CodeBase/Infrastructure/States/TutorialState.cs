using CodeBase.Infrastructure.Logic;
using CodeBase.UI.Services.Factory;
using static CodeBase.Data.GameConstants;

namespace CodeBase.Infrastructure.States
{
    public class TutorialState : IState
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly IUIFactory _uiFactory;

        public TutorialState(IGameStateMachine stateMachine, SceneLoader sceneLoader, IUIFactory uiFactory)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _uiFactory = uiFactory;
        }

        public void Enter()
        {
            _sceneLoader.Load(TutorialSceneLey, OnLoaded);
        }

        public void Exit()
        { }

        private void OnLoaded()
        {
            _uiFactory.CreateUIRoot();
            _uiFactory.CreateTutorial();

            _stateMachine.Enter<LoopState>();
        }
    }
}