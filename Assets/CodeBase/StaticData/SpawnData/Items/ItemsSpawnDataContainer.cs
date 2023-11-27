using System;
using System.Collections.Generic;

namespace CodeBase.StaticData.SpawnData.Items
{
    [Serializable]
    public class ItemsSpawnDataContainer
    {
        public List<MeleeWeaponSpawnData> MeleeWeaponData = new List<MeleeWeaponSpawnData>();
        public List<FoodSpawnData> FoodData = new List<FoodSpawnData>();
        public List<QuestItemSpawnData> QuestItemData = new List<QuestItemSpawnData>();
    }
}