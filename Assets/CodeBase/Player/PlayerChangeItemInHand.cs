using CodeBase.Data.Inventory;
using CodeBase.Data.Items;
using CodeBase.Items;
using CodeBase.Logic.Infrastructure;
using CodeBase.Services.Factory;
using CodeBase.Services.LogicFactory;
using CodeBase.Services.PersistentProgress;
using UnityEngine;

namespace CodeBase.Player
{
    public class PlayerChangeItemInHand : MonoBehaviour
    {
        [SerializeField] private Transform _itemInHandRoot;

        private IItemInHand _currentItem;
        private InventorySlotsContainer _inventorySlots;
        private IGameFactory _gameFactory;
        private InventorySlotsHandler _slotsHandler;

        public void Construct(IPersistentProgressService progressService, ILogicFactory logicFactory, IGameFactory gameFactory)
        {
            _inventorySlots = progressService.PlayerProgress.InventorySlotsContainer;
            _gameFactory = gameFactory;
            _slotsHandler = logicFactory.InventorySlotsHandler;
            ObserveSlotsSelect(true);
        }

        private void OnDestroy()
        {
            ObserveSlotsSelect(false);
        }

        private void ChangeItemInHand(InventorySlot slot)
        {
            _currentItem?.Close();
            _currentItem = null;

            if (slot.IsSelect == false)
                return;

            BaseItemInventoryData item = slot.GetItem();
            if (item != null)
                _currentItem = _gameFactory.CreateItemInHand(item, _itemInHandRoot);
        }

        private void ObserveSlotsSelect(bool isSubscribe)
        {
            if (isSubscribe)
            {
                _slotsHandler.OnItemAddedToSelectedSlot += ChangeItemInHand;

                foreach (InventorySlot slot in _inventorySlots.Slots)
                    slot.OnSelectChangeRefSlot += ChangeItemInHand;
            }
            else
            {
                _slotsHandler.OnItemAddedToSelectedSlot -= ChangeItemInHand;

                foreach (InventorySlot slot in _inventorySlots.Slots)
                    slot.OnSelectChangeRefSlot -= ChangeItemInHand;
            }
        }
    }
}