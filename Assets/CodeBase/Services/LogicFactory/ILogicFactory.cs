using CodeBase.Enemy;
using CodeBase.Logic.Infrastructure;

namespace CodeBase.Services.LogicFactory
{
    public interface ILogicFactory : IService
    {
        InventorySlotsHandler InventorySlotsHandler { get; }
        KillCounterAndLoadNextLevel KillCountAndLoadNextLevel { get; }

        void Cleanup();

        void InitInventorySlotsHandler();

        void InitKillCountAndLoadNextLevel(EnemyHealth[] enemyHealths);
    }
}