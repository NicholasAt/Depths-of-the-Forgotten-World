using CodeBase.Logic.Infrastructure;
using CodeBase.Services.Input;
using CodeBase.Services.LogicFactory;
using CodeBase.Services.StaticData;
using CodeBase.StaticData.Player;
using System.Collections;
using UnityEngine;

namespace CodeBase.Player
{
    public class PlayerChangeSelectSlotStatus : MonoBehaviour, IFreezePlayer
    {
        [SerializeField] private PlayerHealth _health;
        [SerializeField] private PlayerMove _playerMove;

        private IInputService _inputService;
        private InventorySlotsHandler _slotsHandler;
        private PlayerStaticData _config;

        public void Construct(IInputService inputService, ILogicFactory logicFactory, IStaticDataService dataService)
        {
            _inputService = inputService;
            _slotsHandler = logicFactory.InventorySlotsHandler;
            _config = dataService.PlayerData;

            _health.Happened += Dead;
            _inputService.OnChangeSelectSlot += ChangeStatus;
        }

        private void OnDestroy()
        {
            _inputService.OnChangeSelectSlot -= ChangeStatus;
        }

        public void Freeze()
        {
            _slotsHandler.DeselectSlots();
            _inputService.OnChangeSelectSlot -= ChangeStatus;
        }

        public void Unfreeze()
        {
            _inputService.OnChangeSelectSlot -= ChangeStatus;
            _inputService.OnChangeSelectSlot += ChangeStatus;
        }

        private void ChangeStatus(int index)
        {
            _slotsHandler.SetSelectSlot(index);
        }

        private void Dead()
        {
            Freeze();
            StartCoroutine(DelayRespawn());
        }

        private IEnumerator DelayRespawn()
        {
            yield return new WaitForSeconds(_config.DelayMoveAfterDead);
            Unfreeze();
        }
    }
}