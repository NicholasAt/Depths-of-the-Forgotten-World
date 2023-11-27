using CodeBase.Data.Items;
using CodeBase.StaticData.Items;
using JetBrains.Annotations;
using System;
using UnityEngine;

namespace CodeBase.Data.Inventory
{
    [Serializable]
    public class InventorySlot
    {
        [HideInInspector] [SerializeField] private MeleeWeaponInventoryData _meleeWeapon;
        [HideInInspector] [SerializeField] private FoodInventoryData _food;
        [HideInInspector] [SerializeField] private QuestItemsInventoryData _questItem;
        public bool IsSelect { get; private set; }
        public Action OnSetItem;
        public Action OnCleanItem;
        public Action<bool> OnSelectChange;
        public Action<InventorySlot> OnSelectChangeRefSlot;

        public void Cleanup()
        {
            OnSetItem = null;
            OnCleanItem = null;
            OnSelectChange = null;
            OnSelectChangeRefSlot = null;
            IsSelect = false;
        }

        public void SetItem(BaseItemInventoryData data)
        {
            switch (data)
            {
                case MeleeWeaponInventoryData meleeWeapon:
                    _meleeWeapon = meleeWeapon;
                    break;

                case FoodInventoryData food:
                    _food = food;
                    break;

                case QuestItemsInventoryData questItem:
                    _questItem = questItem;
                    break;

                default:
                    Debug.LogError($"I can't store the {data} item");
                    return;
            }
            OnSetItem?.Invoke();
        }

        public void RemoveItem()
        {
            _meleeWeapon = null;
            _food = null;
            _questItem = null;
            OnCleanItem?.Invoke();
        }

        public void SetSelect(bool isSelect)
        {
            IsSelect = isSelect;
            OnSelectChange?.Invoke(isSelect);
            OnSelectChangeRefSlot?.Invoke(this);
        }

        [CanBeNull]
        public BaseItemInventoryData GetItem()
        {
            if (_meleeWeapon != null && _meleeWeapon.Id != ItemId.None)
                return _meleeWeapon;

            if (_food != null && _food.Id != ItemId.None)
                return _food;

            if (_questItem != null && _questItem.Id != ItemId.None)
                return _questItem;

            return null;
        }
    }
}