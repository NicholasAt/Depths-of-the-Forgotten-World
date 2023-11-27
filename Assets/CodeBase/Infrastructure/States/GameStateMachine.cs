using CodeBase.Infrastructure.Logic;
using CodeBase.Services;
using CodeBase.Services.Cleanup;
using CodeBase.Services.Factory;
using CodeBase.Services.GameSound;
using CodeBase.Services.LogicFactory;
using CodeBase.Services.StaticData;
using CodeBase.UI.Services.Factory;
using System;
using System.Collections.Generic;

namespace CodeBase.Infrastructure.States
{
    public class GameStateMachine : IGameStateMachine
    {
        private readonly Dictionary<Type, IExitable> _states;
        private IExitable _activeState;

        public GameStateMachine(SceneLoader sceneLoader, LoadCurtain loadCurtain, AllServices services, ICoroutineRunner coroutineRunner)
        {
            _states = new Dictionary<Type, IExitable>
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, services, coroutineRunner),
                [typeof(TutorialState)] = new TutorialState(this, sceneLoader, services.Single<IUIFactory>()),

                [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader, loadCurtain,
                    services.Single<IGameFactory>(),
                    services.Single<IUIFactory>(),
                    services.Single<ICleanupService>(),
                    services.Single<ILogicFactory>(),
                    services.Single<IStaticDataService>(),
                    services.Single<IGameSoundService>()),

                [typeof(LoopState)] = new LoopState(),
            };
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadState<TPayload>
        {
            var state = ChangeState<TState>();
            state.Enter(payload);
        }

        public void Enter<TState>() where TState : class, IState
        {
            var state = ChangeState<TState>();
            state.Enter();
        }

        private TState ChangeState<TState>() where TState : class, IExitable
        {
            _activeState?.Exit();
            TState state = _states[typeof(TState)] as TState;
            _activeState = state;
            return state;
        }
    }
}