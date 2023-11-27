using CodeBase.StaticData.Items;
using System;
using UnityEngine;

namespace CodeBase.StaticData.SpawnData.Items
{
    [Serializable]
    public class FoodSpawnData : BaseSpawnData<ItemId>
    {
        public FoodSpawnData(ItemId id, Vector2 position) : base(id, position)
        { }
    }
}