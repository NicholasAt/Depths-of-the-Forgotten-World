using CodeBase.Data.Items;
using CodeBase.Services;
using CodeBase.StaticData.Items;
using CodeBase.UI.Windows.Inventory.Slots.RefreshSlots;
using CodeBase.UI.Windows.Shop;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.UI.Services.Factory
{
    public interface IUIFactory : IService
    {
        void CreateUIRoot();

        BaseUIRefreshSlot CreateInventoryRefreshSlot(BaseItemInventoryData itemData, Transform parent);

        void CreateHUD(in string worldName, in string holeName);

        void CreateShopWindow(List<ItemId> ids);

        List<KeepShopItemSlot> CreateShopSlot(List<ItemId> ids, Transform root);

        void CreateTutorial();
    }
}