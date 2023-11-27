using CodeBase.Items;
using CodeBase.StaticData.Items;
using System;
using UnityEngine;

namespace CodeBase.Data.Items
{
    [Serializable]
    public class FoodInventoryData : BaseItemInventoryData
    {
        public FoodInventoryData(ItemId id) : base(id)
        {
            if (ItemsCheckCorrectId.IsFood(id) == false)
                Debug.LogError("wrong id");
        }
    }
}