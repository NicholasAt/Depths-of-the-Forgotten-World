using CodeBase.StaticData.Items;
using System;
using UnityEngine;

namespace CodeBase.StaticData.SpawnData.Items
{
    [Serializable]
    public class MeleeWeaponSpawnData : BaseSpawnData<ItemId>
    {
        public MeleeWeaponSpawnData(ItemId id, Vector2 position) : base(id, position)
        {
        }
    }
}