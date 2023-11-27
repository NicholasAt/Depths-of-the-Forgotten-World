using CodeBase.Player;
using CodeBase.Services.Factory;
using CodeBase.Services.GameObserver;
using UnityEngine;

namespace CodeBase.PlayerTeleport
{
    public class TeleportPlayerAnotherLocation : MonoBehaviour, IPlayerInteraction
    {
        public bool IsToHole { get; private set; }

        private Vector2 _position;
        private IGameFactory _gameFactory;
        private IGameObserverService _observerService;

        public void Construct(bool toHole, Vector2 position, IGameFactory gameFactory, IGameObserverService observerService)
        {
            IsToHole = toHole;
            _position = position;
            _gameFactory = gameFactory;
            _observerService = observerService;
        }

        public void Interaction()
        {
            _gameFactory.Player.transform.position = _position;
            _observerService.SendPlayerTeleport(IsToHole);
        }
    }
}