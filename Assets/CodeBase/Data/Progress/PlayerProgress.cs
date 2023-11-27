using CodeBase.Data.Inventory;
using System;

namespace CodeBase.Data.Progress
{
    [Serializable]
    public class PlayerProgress
    {
        public InventorySlotsContainer InventorySlotsContainer;
        public PlayerData PlayerData;

        public void Cleanup()
        {
            InventorySlotsContainer.Cleanup();
            PlayerData.Cleanup();
        }
    }
}