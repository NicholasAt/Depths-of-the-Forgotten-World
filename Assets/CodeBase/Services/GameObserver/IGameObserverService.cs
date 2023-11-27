using System;

namespace CodeBase.Services.GameObserver
{
    public interface IGameObserverService : IService
    {
        Action OnPlayerLose { get; set; }
        Action<bool> OnPlayerTeleport { get; set; }

        void Cleanup();

        void SendPlayerLose();

        void SendPlayerTeleport(bool toHole);
    }
}