using System;

namespace CodeBase.Services.GameObserver
{
    public class GameObserverService : IGameObserverService
    {
        public Action OnPlayerLose { get; set; }
        public Action<bool> OnPlayerTeleport { get; set; }

        public void Cleanup()
        {
            OnPlayerLose = null;
            OnPlayerTeleport = null;
        }

        public void SendPlayerTeleport(bool toHole) =>
            OnPlayerTeleport?.Invoke(toHole);

        public void SendPlayerLose() =>
            OnPlayerLose?.Invoke();
    }
}