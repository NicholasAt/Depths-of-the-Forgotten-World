using CodeBase.Data.Progress;
using CodeBase.Logic;
using CodeBase.Logic.Infrastructure;
using CodeBase.Services.GameObserver;
using CodeBase.Services.LogicFactory;
using CodeBase.Services.PersistentProgress;
using CodeBase.StaticData.Player;
using System;
using System.Collections;
using UnityEngine;

namespace CodeBase.Player
{
    public class PlayerHealth : MonoBehaviour, IApplyDamage
    {
        [SerializeField] private PlayerAudio _playerAudio;

        private PlayerData _data;
        private IGameObserverService _gameObserver;
        private bool _immortalState;
        private PlayerStaticData _config;
        private Vector2 _startPosition;
        private InventorySlotsHandler _slotsHandler;
        public Action Happened;

        public void Construct(IPersistentProgressService progressService, IGameObserverService gameObserver, ILogicFactory logicFactory, PlayerStaticData playerStaticData, Vector2 startPosition)
        {
            _data = progressService.PlayerProgress.PlayerData;
            _gameObserver = gameObserver;
            _slotsHandler = logicFactory.InventorySlotsHandler;
            _config = playerStaticData;
            _startPosition = startPosition;
        }

        public void ApplyDamage(float damage)
        {
            if (_immortalState)
                return;

            _data.DecrementHealth(damage);
            if (_data.CurrentHealth <= 0.0f)
            {
                _immortalState = true;
                _gameObserver.SendPlayerLose();
                _playerAudio.PlayDie();
                StartCoroutine(DisableImmortalState());
                StartCoroutine(DelayRespawn());
                _slotsHandler.DropAllItems(_config.DropItemsRadius);
                Happened?.Invoke();
            }
        }

        private IEnumerator DelayRespawn()
        {
            yield return new WaitForSeconds(_config.DelayMoveAfterDead);
            transform.position = _startPosition;
            _data.ResetHealth(_config.Health);
        }

        private IEnumerator DisableImmortalState()
        {
            yield return new WaitForSeconds(_config.DurationOfImmortality);
            _immortalState = false;
        }
    }
}