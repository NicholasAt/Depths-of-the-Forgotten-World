using CodeBase.Data.Inventory;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.Inventory.Slots.RefreshSlots
{
    public class UISlotRefreshSelect : MonoBehaviour
    {
        [SerializeField] private Image _selectImage;

        private InventorySlot _inventorySlot;
        private Sprite _selectIcon;
        private Sprite _deselectIcon;

        public void Construct(InventorySlot inventorySlot, Sprite selectIcon, Sprite deselectIcon)
        {
            _inventorySlot = inventorySlot;
            _selectIcon = selectIcon;
            _deselectIcon = deselectIcon;

            _inventorySlot.OnSelectChange += RefreshSelectStatus;
            RefreshSelectStatus(_inventorySlot.IsSelect);
        }

        private void OnDestroy() =>
            _inventorySlot.OnSelectChange -= RefreshSelectStatus;

        private void RefreshSelectStatus(bool isSelect) =>
            _selectImage.sprite = isSelect ? _selectIcon : _deselectIcon;
    }
}