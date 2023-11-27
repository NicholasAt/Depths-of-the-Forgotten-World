using CodeBase.Data.Items;
using CodeBase.Data.Progress;
using CodeBase.Logic.Infrastructure;
using CodeBase.Services.Factory;
using CodeBase.Services.GameSound;
using CodeBase.Services.LogicFactory;
using CodeBase.Services.PersistentProgress;
using CodeBase.StaticData.Items;
using CodeBase.StaticData.Items.Food;
using CodeBase.StaticData.Items.Quest;
using CodeBase.StaticData.Items.Weapons.MeleeWeapon;
using CodeBase.UI.Services.Factory;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.Shop
{
    public class ShopWindow : MonoBehaviour
    {
        [SerializeField] private Transform _shopSlotsRoot;
        [SerializeField] private Transform _playerSlotsRoot;

        [SerializeField] private Button _closeButton;

        [SerializeField] private Image _iconImage;
        [SerializeField] private TMP_Text _sellBuyText;
        [SerializeField] private Button _sellBuyButton;

        private readonly List<KeepShopItemSlot> _playerItemSlots = new List<KeepShopItemSlot>();
        private PlayerData _playerData;
        private InventorySlotsHandler _slotsHandler;
        private Sprite _emptyIcon;
        private IGameFactory _gameFactory;
        private IUIFactory _uiFactory;

        private KeepShopItemSlot _currentSlot;
        private IGameSoundService _soundService;

        public void Construct(List<ItemId> shopIds, IUIFactory uiFactory, IGameSoundService soundService, Sprite emptyIcon, IPersistentProgressService persistentProgressService, ILogicFactory logicFactory, IGameFactory gameFactory)
        {
            _playerData = persistentProgressService.PlayerProgress.PlayerData;
            _slotsHandler = logicFactory.InventorySlotsHandler;
            _emptyIcon = emptyIcon;
            _gameFactory = gameFactory;
            _uiFactory = uiFactory;
            _soundService = soundService;

            _closeButton.onClick.AddListener(Close);
            FreezePlayer(true);
            InitShopSlots(shopIds);
        }

        private void OnDestroy()
        {
            _slotsHandler.OnItemAdded -= RefreshPlayerShopSlots;
            _slotsHandler.OnItemRemove -= RefreshPlayerShopSlots;
        }

        private void InitShopSlots(List<ItemId> shopIds)
        {
            _uiFactory.CreateShopSlot(shopIds, _shopSlotsRoot).ForEach(x => x.OnClick += RefreshBuy);

            _slotsHandler.OnItemAdded += RefreshPlayerShopSlots;
            _slotsHandler.OnItemRemove += RefreshPlayerShopSlots;
            RefreshPlayerShopSlots();
        }

        private void RefreshPlayerShopSlots()
        {
            _playerItemSlots.ForEach(x => x.Remove());
            _playerItemSlots.Clear();
            List<ItemId> playerItems = _slotsHandler.GetAllItems().Select(x => x.Id).ToList();
            foreach (KeepShopItemSlot shopItemSlot in _uiFactory.CreateShopSlot(playerItems, _playerSlotsRoot))
            {
                _playerItemSlots.Add(shopItemSlot);
                shopItemSlot.OnClick += RefreshSell;
            }
        }

        private void Close()
        {
            FreezePlayer(false);
            Destroy(gameObject);
        }

        private void RefreshBuy(KeepShopItemSlot slot)
        {
            _currentSlot = slot;
            string context = $"Buy: {_currentSlot.ItemConfig.PurchasePrice}";
            RefreshIconAndPrice(_currentSlot.ItemConfig.Icon, context);
            RefreshBuyInteractable();

            _sellBuyButton.onClick.RemoveAllListeners();
            _sellBuyButton.onClick.AddListener(() =>
            {
                Buy();
                RefreshBuyInteractable();
            });
        }

        private void RefreshSell(KeepShopItemSlot slot)
        {
            _currentSlot = slot;
            string context = $"Sell: {_currentSlot.ItemConfig.SellingPrice}";
            RefreshIconAndPrice(_currentSlot.ItemConfig.Icon, context);
            _sellBuyButton.interactable = true;
            _sellBuyButton.onClick.RemoveAllListeners();
            _sellBuyButton.onClick.AddListener(Sell);
        }

        private void RefreshBuyInteractable() =>
            _sellBuyButton.interactable = _playerData.Money >= _currentSlot.ItemConfig.PurchasePrice;

        private void RefreshIconAndPrice(Sprite icon, in string priceText)
        {
            _iconImage.sprite = icon;
            _sellBuyText.text = priceText;
        }

        private void Buy()
        {
            _playerData.ChangeMoneyValue(-_currentSlot.ItemConfig.PurchasePrice);
            _soundService.PlayOneShot(_currentSlot.ItemConfig.BuyClip);

            BaseItemInventoryData item = null;

            switch (_currentSlot.ItemConfig)
            {
                case MeleeWeaponConfig _:
                    item = new MeleeWeaponInventoryData(_currentSlot.ItemConfig.ItemId);
                    break;

                case FoodConfig _:
                    item = new FoodInventoryData(_currentSlot.ItemConfig.ItemId);
                    break;

                case QuestItemConfig _:
                    item = new QuestItemsInventoryData(_currentSlot.ItemConfig.ItemId);
                    break;

                default:
                    Debug.LogError($"I can't buy this item {_currentSlot.ItemConfig}");
                    break;
            }
            _slotsHandler.AddOrDropItem(item);
        }

        private void Sell()
        {
            _playerData.ChangeMoneyValue(+_currentSlot.ItemConfig.SellingPrice);
            _soundService.PlayOneShot(_currentSlot.ItemConfig.BuyClip);

            _slotsHandler.RemoveItem(_currentSlot.ItemConfig.ItemId);
            RefreshPlayerShopSlots();
            _currentSlot = null;

            CleanSellBuyData();
        }

        private void CleanSellBuyData()
        {
            _sellBuyButton.interactable = false;
            _sellBuyText.text = string.Empty;
            _sellBuyButton.onClick.RemoveAllListeners();
            _iconImage.sprite = _emptyIcon;
        }

        private void FreezePlayer(bool isFreeze)
        {
            if (isFreeze)
                _gameFactory.PlayerFreezes.ForEach(x => x.Freeze());
            else
                _gameFactory.PlayerFreezes.ForEach(x => x.Unfreeze());
        }
    }
}