using CodeBase.StaticData.SpawnData.Enemy;
using CodeBase.StaticData.SpawnData.Items;
using CodeBase.StaticData.SpawnData.NPC.ShopNpc;
using CodeBase.StaticData.SpawnData.PlayerTeleport;
using CodeBase.StaticData.SpawnData.Reward;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.StaticData.Levels
{
    [Serializable]
    public class LevelConfig
    {
        [field: SerializeField] public string LevelKey { get; private set; }
        [field: SerializeField] public string NextLevelKey { get; private set; }
        [field: SerializeField] public string DisplayedLevelNameWorld { get; private set; } = "World";
        [field: SerializeField] public string DisplayedLevelNameHole { get; private set; } = "Hole";
        [field: SerializeField] public bool IsFinalScene { get; private set; }
        [field: SerializeField] public Vector2 PlayerInitialPosition { get; private set; }

        public ItemsSpawnDataContainer ItemsSpawnData;
        public List<ShopNpcSpawnData> ShopNpcData;
        public List<PlayerTeleportSpawnData> TeleportSpawns;
        public RewardSpawnData RewardData;
        public EnemySpawnData EnemyData;

        public void SetData(Vector2 playerInitialPosition, RewardSpawnData rewardData, ItemsSpawnDataContainer itemsSpawnData, List<PlayerTeleportSpawnData> teleportSpawns, EnemySpawnData enemyData, List<ShopNpcSpawnData> shopNpcData)
        {
            ItemsSpawnData = itemsSpawnData;
            PlayerInitialPosition = playerInitialPosition;
            EnemyData = enemyData;
            ShopNpcData = shopNpcData;
            RewardData = rewardData;
            TeleportSpawns = teleportSpawns;
        }
    }
}