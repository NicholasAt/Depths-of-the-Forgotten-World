using System;

namespace CodeBase.Data.Inventory
{
    [Serializable]
    public class InventorySlotsContainer
    {
        public InventorySlot[] Slots;

        public void InitSlots(int slotCount)
        {
            Slots = new InventorySlot[slotCount];
            for (int i = 0; i < slotCount; i++)
            {
                Slots[i] = new InventorySlot();
            }
        }

        public void Cleanup()
        {
            foreach (InventorySlot slot in Slots)
                slot.Cleanup();
        }
    }
}