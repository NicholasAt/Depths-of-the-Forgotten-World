using CodeBase.Data.Inventory;
using CodeBase.Data.Items;
using CodeBase.Infrastructure.States;
using CodeBase.Services;
using CodeBase.Services.Factory;
using CodeBase.Services.GameObserver;
using CodeBase.Services.GameSound;
using CodeBase.Services.LogicFactory;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.StaticData;
using CodeBase.StaticData.Items;
using CodeBase.UI.Windows.Inventory;
using CodeBase.UI.Windows.Inventory.Slots;
using CodeBase.UI.Windows.Inventory.Slots.RefreshSlots;
using CodeBase.UI.Windows.Inventory.Slots.RefreshSlots.ItemsRefreshSlot;
using CodeBase.UI.Windows.LevelName;
using CodeBase.UI.Windows.Player;
using CodeBase.UI.Windows.Shop;
using CodeBase.UI.Windows.Tutorial;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CodeBase.UI.Services.Factory
{
    public class UIFactory : IUIFactory
    {
        private Transform _uiRoot;
        private readonly AllServices _allServices;

        public UIFactory(AllServices allServices)
        {
            _allServices = allServices;
        }

        public void CreateUIRoot()
        {
            IStaticDataService dataService = GetService<IStaticDataService>();
            _uiRoot = Object.Instantiate(dataService.WindowData.UIRoot).transform;
        }

        public void CreateShopWindow(List<ItemId> ids)
        {
            IStaticDataService dataService = GetService<IStaticDataService>();
            ILogicFactory logicFactory = GetService<ILogicFactory>();
            IPersistentProgressService persistentProgressService = GetService<IPersistentProgressService>();
            IGameFactory gameFactory = GetService<IGameFactory>();
            IGameSoundService soundService = GetService<IGameSoundService>();

            ShopWindow shopPrefab = dataService.WindowData.ShopWindowPrefab;
            ShopWindow shopInstance = Object.Instantiate(shopPrefab, _uiRoot);

            shopInstance.Construct(ids, this, soundService, dataService.InventoryStaticData.EmptyIcon, persistentProgressService, logicFactory, gameFactory);
        }

        public void CreateTutorial()
        {
            IStaticDataService dataService = GetService<IStaticDataService>();
            IGameStateMachine stateMachine = GetService<IGameStateMachine>();

            TutorialWindow prefab = dataService.WindowData.TutorialWindowPrefab;
            TutorialWindow instantiate = Object.Instantiate(prefab, _uiRoot);
            instantiate.Construct(dataService, stateMachine);
        }

        public void CreateHUD(in string worldName, in string holeName)
        {
            IStaticDataService dataService = GetService<IStaticDataService>();
            IPersistentProgressService progressService = GetService<IPersistentProgressService>();
            IGameObserverService observerService = GetService<IGameObserverService>();

            GameObject hudPrefab = dataService.WindowData.HUDPrefab;
            GameObject hudInstance = Object.Instantiate(hudPrefab, _uiRoot);
            hudInstance.GetComponentInChildren<PlayerHealthWindow>().Construct(progressService, dataService);
            hudInstance.GetComponentInChildren<DisplayLevelNameWindow>().Construct(in worldName, in holeName, observerService);
            hudInstance.GetComponentInChildren<PlayerMoneyWindow>().Construct(progressService);
            InitInventoryWindow();
            void InitInventoryWindow()
            {
                InventoryWindow inventoryWindow = hudInstance.GetComponentInChildren<InventoryWindow>();
                GameObject inventorySlotPrefab = dataService.InventoryStaticData.InventorySlotPrefab;

                foreach (InventorySlot slot in progressService.PlayerProgress.InventorySlotsContainer.Slots)
                {
                    GameObject inventorySlotInstance = Object.Instantiate(inventorySlotPrefab, inventoryWindow.SlotsRoot);
                    inventorySlotInstance.GetComponent<UISlotRefreshSelect>().Construct(slot, dataService.InventoryStaticData.EnableIcon, dataService.InventoryStaticData.DisableIcon);
                    inventorySlotInstance.GetComponent<UIInventorySlotsDataUpdateCoordinator>().Construct(this, slot);
                }
            }
        }

        public BaseUIRefreshSlot CreateInventoryRefreshSlot(BaseItemInventoryData itemData, Transform parent)
        {
            IStaticDataService dataService = GetService<IStaticDataService>();
            BaseItemConfig config = dataService.ForItem(itemData.Id);

            switch (itemData)
            {
                case MeleeWeaponInventoryData _:
                    MeleeWeaponUIRefreshSlot meleeRefreshUIPrefab = dataService.InventoryStaticData.MeleeWeaponUIRefreshSlotPrefab;
                    BaseUIRefreshSlot meleeInstance = CreateBaseInventoryRefreshSlot(config, meleeRefreshUIPrefab, parent);
                    return meleeInstance;

                case FoodInventoryData _:
                    FoodUIRefreshSlot foodRefreshUISlotPrefab = dataService.InventoryStaticData.FoodUIRefreshSlotPrefab;
                    BaseUIRefreshSlot foodInstance = CreateBaseInventoryRefreshSlot(config, foodRefreshUISlotPrefab, parent);
                    return foodInstance;

                case QuestItemsInventoryData _:
                    QuestItemsUIRefreshSlot questRefreshUISlotPrefab = dataService.InventoryStaticData.QuestItemsUIRefreshSlotPrefab;
                    BaseUIRefreshSlot questInstance = CreateBaseInventoryRefreshSlot(config, questRefreshUISlotPrefab, parent);
                    return questInstance;
            }

            Debug.LogError($"I couldn't create a slot with the {itemData} item");
            return null;
        }

        public List<KeepShopItemSlot> CreateShopSlot(List<ItemId> ids, Transform root)
        {
            IStaticDataService dataService = GetService<IStaticDataService>();
            KeepShopItemSlot slotPrefab = dataService.WindowData.ShopSlotPrefab;
            List<KeepShopItemSlot> slots = new List<KeepShopItemSlot>(ids.Count);

            foreach (var id in ids)
            {
                BaseItemConfig config = dataService.ForItem(id);
                KeepShopItemSlot keepShopItemSlot = Object.Instantiate(slotPrefab, root);
                keepShopItemSlot.Construct(config);
                slots.Add(keepShopItemSlot);
            }

            return slots;
        }

        private static BaseUIRefreshSlot CreateBaseInventoryRefreshSlot(BaseItemConfig config, BaseUIRefreshSlot prefab, Transform parent)
        {
            BaseUIRefreshSlot instance = Object.Instantiate(prefab, parent);
            instance.BaseConstruct(config.Icon);
            return instance;
        }

        private TService GetService<TService>() where TService : IService =>
            _allServices.Single<TService>();
    }
}