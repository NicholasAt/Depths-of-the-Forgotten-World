using CodeBase.Services.Factory;
using CodeBase.Services.Input;
using CodeBase.Services.LogicFactory;
using CodeBase.Services.PersistentProgress;

namespace CodeBase.Services.Cleanup
{
    public class CleanupService : ICleanupService
    {
        private readonly IGameFactory _gameFactory;
        private readonly ILogicFactory _logicFactory;
        private readonly IPersistentProgressService _persistentProgressService;
        private readonly IInputService _inputService;

        public CleanupService(IGameFactory gameFactory, ILogicFactory logicFactory, IPersistentProgressService persistentProgressService, IInputService inputService)
        {
            _gameFactory = gameFactory;
            _logicFactory = logicFactory;
            _persistentProgressService = persistentProgressService;
            _inputService = inputService;
        }

        public void Cleanup()
        {
            _gameFactory.Cleanup();
            _logicFactory.Cleanup();
            _persistentProgressService.PlayerProgress.Cleanup();
            _inputService.Cleanup();
        }
    }
}