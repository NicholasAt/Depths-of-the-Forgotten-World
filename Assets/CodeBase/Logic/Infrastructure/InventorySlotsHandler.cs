using CodeBase.Data.Inventory;
using CodeBase.Data.Items;
using CodeBase.Services.Factory;
using CodeBase.StaticData.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CodeBase.Logic.Infrastructure
{
    public class InventorySlotsHandler
    {
        private readonly InventorySlotsContainer _inventorySlotsContainer;
        private readonly IGameFactory _gameFactory;
        private InventorySlot _selectedSlot;
        public Action<InventorySlot> OnItemAddedToSelectedSlot;
        public Action OnItemAdded;
        public Action OnItemRemove;

        public InventorySlotsHandler(InventorySlotsContainer inventorySlotsContainer, IGameFactory gameFactory)
        {
            _inventorySlotsContainer = inventorySlotsContainer;
            _gameFactory = gameFactory;
        }

        public void DeselectSlots()
        {
            _selectedSlot = null;
            foreach (InventorySlot slot in _inventorySlotsContainer.Slots)
                slot.SetSelect(false);
        }

        public void SetSelectSlot(int index)
        {
            if (index > _inventorySlotsContainer.Slots.Length)
            {
                Debug.LogError("it seems there are fewer slots than you expected");
                return;
            }

            InventorySlot newSlot = _inventorySlotsContainer.Slots[index];
            if (_selectedSlot == newSlot)
            {
                _selectedSlot.SetSelect(!_selectedSlot.IsSelect);
            }
            else
            {
                _selectedSlot?.SetSelect(false);

                _selectedSlot = newSlot;
                _selectedSlot.SetSelect(true);
            }
        }

        public List<BaseItemInventoryData> GetAllItems()
        {
            return _inventorySlotsContainer.Slots
                .Where(x => x.GetItem() != null)
                .Select(x => x.GetItem())
                .ToList();
        }

        public void RemoveItem(ItemId id)
        {
            foreach (InventorySlot slot in _inventorySlotsContainer.Slots)
            {
                BaseItemInventoryData item = slot.GetItem();
                if (item != null && item.Id == id)
                {
                    slot.RemoveItem();
                    break;
                }
            }
        }

        public void AddOrDropItem(BaseItemInventoryData item)
        {
            foreach (InventorySlot slot in _inventorySlotsContainer.Slots)
            {
                if (slot.GetItem() == null)
                {
                    slot.SetItem(item);
                    if (slot.IsSelect)
                    {
                        _selectedSlot = slot;
                        OnItemAddedToSelectedSlot?.Invoke(_selectedSlot);
                    }
                    OnItemAdded?.Invoke();
                    return;
                }
            }

            DropItem(item);
        }

        public void DropAllItems(float radius)
        {
            _selectedSlot?.SetSelect(false);
            _selectedSlot = null;

            foreach (InventorySlot slot in _inventorySlotsContainer.Slots.Where(x => x.GetItem() != null))
            {
                Vector2 playerPosition = new Vector2(_gameFactory.Player.transform.position.x, _gameFactory.Player.transform.position.y);
                Vector2 position = playerPosition + Random.insideUnitCircle * radius;
                _gameFactory.CreateItemPiece(slot.GetItem(), position);
                slot.RemoveItem();
            }
        }

        private void DropItem(BaseItemInventoryData baseItemInventoryData)
        {
            Vector2 playerPosition = new Vector2(_gameFactory.Player.transform.position.x, _gameFactory.Player.transform.position.y);
            _gameFactory.CreateItemPiece(baseItemInventoryData, playerPosition);
        }

        public bool IsInventoryFull() =>
            _inventorySlotsContainer.Slots.All(slot => slot.GetItem() != null);
    }
}