using CodeBase.Items;
using CodeBase.StaticData.Items;
using System;
using UnityEngine;

namespace CodeBase.Data.Items
{
    [Serializable]
    public class QuestItemsInventoryData : BaseItemInventoryData
    {
        public QuestItemsInventoryData(ItemId id) : base(id)
        {
            if (ItemsCheckCorrectId.IsQuestItem(id) == false)
                Debug.LogError("wrong id");
        }
    }
}