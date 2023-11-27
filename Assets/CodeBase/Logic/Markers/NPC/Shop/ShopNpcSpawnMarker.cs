using CodeBase.StaticData.Items;
using CodeBase.StaticData.NPC;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Logic.Markers.NPC.Shop
{
    public class ShopNpcSpawnMarker : BaseSpanMarker<ShopNpcId>
    {
        [field: SerializeField] public List<ItemId> Ids { get; private set; }
        protected override string ObjectName { get; set; } = "Shop Npc";
    }
}