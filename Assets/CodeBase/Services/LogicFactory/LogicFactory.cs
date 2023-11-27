using CodeBase.Enemy;
using CodeBase.Infrastructure.Logic;
using CodeBase.Infrastructure.States;
using CodeBase.Logic.Infrastructure;
using CodeBase.Services.Factory;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.StaticData;

namespace CodeBase.Services.LogicFactory
{
    public class LogicFactory : ILogicFactory
    {
        private readonly AllServices _services;
        private readonly ICoroutineRunner _coroutineRunner;

        public InventorySlotsHandler InventorySlotsHandler { get; private set; }
        public KillCounterAndLoadNextLevel KillCountAndLoadNextLevel { get; private set; }

        public LogicFactory(AllServices services, ICoroutineRunner coroutineRunner)
        {
            _services = services;
            _coroutineRunner = coroutineRunner;
        }

        public void InitInventorySlotsHandler() =>
            InventorySlotsHandler = new InventorySlotsHandler(
                GetService<IPersistentProgressService>().PlayerProgress.InventorySlotsContainer,
                GetService<IGameFactory>());

        public void InitKillCountAndLoadNextLevel(EnemyHealth[] enemyHealths)
        {
            IGameStateMachine gameStateMachine = GetService<IGameStateMachine>();
            IStaticDataService dataService = GetService<IStaticDataService>();
            IGameFactory factory = GetService<IGameFactory>();

            KillCountAndLoadNextLevel = new KillCounterAndLoadNextLevel(enemyHealths, _coroutineRunner, factory, gameStateMachine, dataService);
        }

        public void Cleanup()
        {
            InventorySlotsHandler = null;
            KillCountAndLoadNextLevel = null;
        }

        private TService GetService<TService>() where TService : IService =>
            _services.Single<TService>();
    }
}