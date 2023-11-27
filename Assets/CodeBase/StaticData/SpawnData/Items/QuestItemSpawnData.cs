using CodeBase.StaticData.Items;
using System;
using UnityEngine;

namespace CodeBase.StaticData.SpawnData.Items
{
    [Serializable]
    public class QuestItemSpawnData : BaseSpawnData<ItemId>
    {
        public QuestItemSpawnData(ItemId id, Vector2 position) : base(id, position)
        {
        }
    }
}