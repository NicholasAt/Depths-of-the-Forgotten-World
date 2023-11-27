using CodeBase.Items;
using CodeBase.Logic.Markers;
using CodeBase.Logic.Markers.Enemy;
using CodeBase.Logic.Markers.Items;
using CodeBase.Logic.Markers.NPC.Shop;
using CodeBase.Logic.Markers.PlayerTeleport;
using CodeBase.Logic.Markers.Reward;
using CodeBase.StaticData.Items;
using CodeBase.StaticData.Levels;
using CodeBase.StaticData.SpawnData.Enemy;
using CodeBase.StaticData.SpawnData.Items;
using CodeBase.StaticData.SpawnData.NPC.ShopNpc;
using CodeBase.StaticData.SpawnData.PlayerTeleport;
using CodeBase.StaticData.SpawnData.Reward;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Editor
{
    public class CollectLevelDataEditor
    {
        private const string LevelStaticDataPath = "Levels/LevelsStaticData";

        [MenuItem("Tools/Collect Level Data")]
        private static void CollectLevelData()
        {
            LevelsStaticData data = Resources.Load<LevelsStaticData>(LevelStaticDataPath);
            string sceneKey = SceneManager.GetActiveScene().name;

            foreach (LevelConfig config in data.LevelConfigs)
                if (config.LevelKey == sceneKey)
                {
                    Vector2 playerInitPosition = Object.FindObjectOfType<PlayerInitialMarker>().transform.position;
                    config.SetData(playerInitPosition, RewardData(), ItemsData(), TeleportPlayerData(), EnemySpawnData(), ShopNpcData());

                    EditorUtility.SetDirty(data);
                    return;
                }

            Debug.LogError("I can't find this scene in the static data");
        }

        private static List<PlayerTeleportSpawnData> TeleportPlayerData()
        {
            IEnumerable<PlayerTeleportSpawnData> data = Object.FindObjectsOfType<PlayerTeleportSpawnMarker>()
                  .Select(x => new PlayerTeleportSpawnData(x.ID, x.transform.position, x.TeleportPoint.position, x.IsTeleportToHole));

            return data.ToList();
        }

        private static RewardSpawnData RewardData()
        {
            RewardSpawnMarker marker = Object.FindObjectOfType<RewardSpawnMarker>();
            if (marker == null)
                return null;

            RewardSpawnData reward = new RewardSpawnData(marker.ID, marker.transform.position);
            return reward;
        }

        private static ItemsSpawnDataContainer ItemsData()
        {
            ItemsSpawnDataContainer data = new ItemsSpawnDataContainer();
            foreach (ItemsSpawnMarker marker in Object.FindObjectsOfType<ItemsSpawnMarker>())
            {
                if (ItemsCheckCorrectId.IsMeleeWeapon(marker.ID))
                {
                    data.MeleeWeaponData.Add(new MeleeWeaponSpawnData(marker.ID, marker.transform.position));
                }
                else if (ItemsCheckCorrectId.IsFood(marker.ID))
                {
                    data.FoodData.Add(new FoodSpawnData(marker.ID, marker.transform.position));
                }
                else if (ItemsCheckCorrectId.IsQuestItem(marker.ID))
                {
                    data.QuestItemData.Add(new QuestItemSpawnData(marker.ID, marker.transform.position));
                }
                else
                {
                    Debug.LogError("I can't recognize this item");
                }
            }

            return data;
        }

        private static EnemySpawnData EnemySpawnData()
        {
            EnemySpawnMarker enemySpawnMarker = Object.FindObjectOfType<EnemySpawnMarker>();
            EnemySpawnData enemyData = new EnemySpawnData(enemySpawnMarker.transform.position, enemySpawnMarker.SpawnRadius,
                enemySpawnMarker.Enemys.ToList());
            return enemyData;
        }

        private static List<ShopNpcSpawnData> ShopNpcData()
        {
            List<ShopNpcSpawnData> shopNpcData = new List<ShopNpcSpawnData>();

            foreach (ShopNpcSpawnMarker marker in Object.FindObjectsOfType<ShopNpcSpawnMarker>())
            {
                List<ItemId> ids = new List<ItemId>();
                marker.Ids.ForEach(x => ids.Add(x));
                shopNpcData.Add(new ShopNpcSpawnData(marker.ID, marker.transform.position, ids));
            }

            return shopNpcData;
        }
    }
}