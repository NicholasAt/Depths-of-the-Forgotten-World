using CodeBase.Data.Inventory;
using CodeBase.Data.Progress;
using CodeBase.Infrastructure.Logic;
using CodeBase.Services;
using CodeBase.Services.Cleanup;
using CodeBase.Services.Factory;
using CodeBase.Services.GameObserver;
using CodeBase.Services.GameSound;
using CodeBase.Services.Input;
using CodeBase.Services.LogicFactory;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.StaticData;
using CodeBase.UI.Services.Factory;
using static CodeBase.Data.GameConstants;

namespace CodeBase.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services;
        private readonly ICoroutineRunner _coroutineRunner;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, AllServices services, ICoroutineRunner coroutineRunner)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _services = services;
            _coroutineRunner = coroutineRunner;

            RegisterServices();
        }

        public void Enter()
        {
            _sceneLoader.Load(InitSceneKey, OnLoaded);
        }

        public void Exit()
        { }

        private void RegisterServices()
        {
            _services.RegisterSingle<IGameStateMachine>(_stateMachine);
            RegisterStaticData();
            _services.RegisterSingle<IGameFactory>(new GameFactory(_services));
            _services.RegisterSingle<IUIFactory>(new UIFactory(_services));
            _services.RegisterSingle<ILogicFactory>(new LogicFactory(_services, _coroutineRunner));
            _services.RegisterSingle<IInputService>(new InputService());
            _services.RegisterSingle<IGameObserverService>(new GameObserverService());
            RegisterProgressService(_services.Single<IStaticDataService>());
            RegisterAudio();

            _services.RegisterSingle<ICleanupService>(new CleanupService(_services.Single<IGameFactory>(),
                _services.Single<ILogicFactory>(),
                _services.Single<IPersistentProgressService>(),
                _services.Single<IInputService>()));
        }

        private void OnLoaded()
        {
            _stateMachine.Enter<TutorialState>();
        }

        private void RegisterAudio()
        {
            GameSoundService implementation = new GameSoundService(_services.Single<IStaticDataService>());
            implementation.InitializeAudioSource();
            _services.RegisterSingle<IGameSoundService>(implementation);
        }

        private void RegisterProgressService(IStaticDataService dataService)
        {
            PersistentProgressService service = new PersistentProgressService();
            service.PlayerProgress = new PlayerProgress();
            service.PlayerProgress.PlayerData = new PlayerData(dataService.PlayerData.Health, dataService.PlayerData.Money);
            service.PlayerProgress.InventorySlotsContainer = new InventorySlotsContainer();
            service.PlayerProgress.InventorySlotsContainer.InitSlots(dataService.InventoryStaticData.SlotsCount);
            _services.RegisterSingle<IPersistentProgressService>(service);
        }

        private void RegisterStaticData()
        {
            var service = new StaticDataService();
            service.Load();
            _services.RegisterSingle<IStaticDataService>(service);
        }
    }
}