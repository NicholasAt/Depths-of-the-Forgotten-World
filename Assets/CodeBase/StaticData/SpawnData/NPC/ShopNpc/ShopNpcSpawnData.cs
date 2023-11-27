using CodeBase.StaticData.Items;
using CodeBase.StaticData.NPC;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.StaticData.SpawnData.NPC.ShopNpc
{
    [Serializable]
    public class ShopNpcSpawnData : BaseSpawnData<ShopNpcId>
    {
        [field: SerializeField] public List<ItemId> Items { get; private set; }

        public ShopNpcSpawnData(ShopNpcId id, Vector2 position, List<ItemId> items) : base(id, position)
        {
            Items = items;
        }
    }
}