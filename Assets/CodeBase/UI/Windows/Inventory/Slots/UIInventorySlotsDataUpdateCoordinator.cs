using CodeBase.Data.Inventory;
using CodeBase.Data.Items;
using CodeBase.UI.Services.Factory;
using CodeBase.UI.Windows.Inventory.Slots.RefreshSlots;
using UnityEngine;

namespace CodeBase.UI.Windows.Inventory.Slots
{
    public class UIInventorySlotsDataUpdateCoordinator : MonoBehaviour
    {
        private IUIFactory _uiFactory;

        private InventorySlot _slot;
        private BaseUIRefreshSlot _refreshUiSlot;

        public void Construct(IUIFactory uiFactory, InventorySlot slot)
        {
            _uiFactory = uiFactory;
            _slot = slot;

            _slot.OnSetItem += CloseAndInitSlotRefresher;
            _slot.OnCleanItem += CloseAndInitSlotRefresher;
            CloseAndInitSlotRefresher();
        }

        private void OnDestroy()
        {
            _slot.OnSetItem -= CloseAndInitSlotRefresher;
            _slot.OnCleanItem -= CloseAndInitSlotRefresher;
        }

        private void CloseAndInitSlotRefresher()
        {
            CloseSlotRefresher();

            BaseItemInventoryData item = _slot.GetItem();
            if (item != null)
                _refreshUiSlot = _uiFactory.CreateInventoryRefreshSlot(item, transform);
        }

        private void CloseSlotRefresher()
        {
            if (_refreshUiSlot != null)
            {
                _refreshUiSlot.Remove();
                _refreshUiSlot = null;
            }
        }
    }
}