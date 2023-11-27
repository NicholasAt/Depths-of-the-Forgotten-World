using CodeBase.Items;
using CodeBase.StaticData.Items;
using System;
using UnityEngine;

namespace CodeBase.Data.Items
{
    [Serializable]
    public class MeleeWeaponInventoryData : BaseItemInventoryData
    {
        public MeleeWeaponInventoryData(ItemId id) : base(id)
        {
            if (ItemsCheckCorrectId.IsMeleeWeapon(id) == false)
                Debug.LogError("wrong id");
        }
    }
}