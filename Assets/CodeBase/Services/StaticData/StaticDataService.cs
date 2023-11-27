using CodeBase.StaticData.Audio;
using CodeBase.StaticData.Enemy;
using CodeBase.StaticData.Inventory;
using CodeBase.StaticData.Items;
using CodeBase.StaticData.Items.Food;
using CodeBase.StaticData.Items.Quest;
using CodeBase.StaticData.Items.Weapons.MeleeWeapon;
using CodeBase.StaticData.Levels;
using CodeBase.StaticData.NPC;
using CodeBase.StaticData.Player;
using CodeBase.StaticData.PlayerTeleport;
using CodeBase.StaticData.Reward;
using CodeBase.StaticData.Windows;
using CodeBase.StaticData.Windows.Tutorial;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CodeBase.Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        #region Paths

        private const string WindowStaticDataPath = "Windows/WindowStaticData";
        private const string InventoryStaticDataPath = "Inventory/InventoryStaticData";
        private const string MeleeWeaponStaticDataPath = "Items/Weapons/MeleeWeapon/MeleeWeaponStaticData";
        private const string FoodStaticDataPath = "Items/Food/FoodStaticData";
        private const string QuestItemsStaticDataPath = "Items/Quests/QuestItemStaticData";
        private const string LevelsStaticDataPath = "Levels/LevelsStaticData";
        private const string PlayerStaticDataPath = "Player/PlayerStaticData";
        private const string EnemyStaticDataPath = "Enemy/EnemyStaticData";
        private const string ShopNpcStaticDataPath = "Npc/ShopNpc/ShopNpcStaticData";
        private const string AudioStaticDataPath = "Audio/AudioStaticData";
        private const string RewardStaticDataPath = "Reward/RewardStaticData";
        private const string TutorialStaticDataPath = "Windows/Turotial/TutorialStaticData";
        private const string TeleportStaticDataPath = "PlayerTeleport/PlayerTeleportStaticData";

        #endregion Paths

        public PlayerStaticData PlayerData { get; private set; }
        public WindowStaticData WindowData { get; private set; }
        public LevelsStaticData LevelData { get; private set; }
        public InventoryStaticData InventoryStaticData { get; private set; }
        public AudioStaticData AudioData { get; private set; }
        public TutorialStaticData TutorialData { get; private set; }
        private readonly Dictionary<ItemId, BaseItemConfig> _itemConfigs = new Dictionary<ItemId, BaseItemConfig>();
        private Dictionary<EnemyId, EnemyConfig> _enemyConfigs;
        private Dictionary<string, LevelConfig> _levelConfigs;
        private Dictionary<ShopNpcId, ShopNpcConfig> _shopNpcConfigs;
        private Dictionary<AudioId, AudioConfig> _audioConfigs;
        private Dictionary<RewardId, RewardConfig> _rewardConfigs;
        private Dictionary<TeleportId, TeleportConfig> _teleportConfigs;

        public void Load()
        {
            WindowData = Resources.Load<WindowStaticData>(WindowStaticDataPath);
            InventoryStaticData = Resources.Load<InventoryStaticData>(InventoryStaticDataPath);
            PlayerData = Resources.Load<PlayerStaticData>(PlayerStaticDataPath);
            _shopNpcConfigs = Resources.Load<ShopNpcStaticData>(ShopNpcStaticDataPath).ShopNpcConfigs.ToDictionary(x => x.Id, x => x);
            _enemyConfigs = Resources.Load<EnemyStaticData>(EnemyStaticDataPath).EnemyConfigs.ToDictionary(x => x.Id, x => x);
            _rewardConfigs = Resources.Load<RewardStaticData>(RewardStaticDataPath).Configs.ToDictionary(x => x.Id, x => x);
            TutorialData = Resources.Load<TutorialStaticData>(TutorialStaticDataPath);
            _teleportConfigs = Resources.Load<PlayerTeleportStaticData>(TeleportStaticDataPath).TeleportConfigs.ToDictionary(x => x.Id, x => x);
            LoadLevelData();
            LoadInventoryItems();
            LoadAudio();
        }

        public LevelConfig ForLevel(in string levelKey) =>
            _levelConfigs.TryGetValue(levelKey, out LevelConfig cfg) ? cfg : null;

        public EnemyConfig ForEnemy(EnemyId id) =>
            _enemyConfigs.TryGetValue(id, out EnemyConfig cfg) ? cfg : null;

        public ShopNpcConfig ForShopNpc(ShopNpcId id) =>
            _shopNpcConfigs.TryGetValue(id, out ShopNpcConfig cfg) ? cfg : null;

        public RewardConfig ForReward(RewardId id) =>
            _rewardConfigs.TryGetValue(id, out RewardConfig cfg) ? cfg : null;

        public AudioConfig ForAudio(AudioId id) =>
            _audioConfigs.TryGetValue(id, out AudioConfig cfg) ? cfg : null;

        public TeleportConfig ForTeleport(TeleportId id) =>
            _teleportConfigs.TryGetValue(id, out TeleportConfig cfg) ? cfg : null;

        public BaseItemConfig ForItem(ItemId id)
        {
            if (_itemConfigs.TryGetValue(id, out BaseItemConfig cfg))
                return cfg;

            Debug.LogError("ID does not exist or is not linked to the config");
            return null;
        }

        private void LoadLevelData()
        {
            LevelData = Resources.Load<LevelsStaticData>(LevelsStaticDataPath);
            _levelConfigs = LevelData.LevelConfigs.ToDictionary(x => x.LevelKey, x => x);
        }

        private void LoadAudio()
        {
            AudioData = Resources.Load<AudioStaticData>(AudioStaticDataPath);
            _audioConfigs = AudioData.Configs.ToDictionary(x => x.Id, x => x);
        }

        private void LoadInventoryItems()
        {
            Resources.Load<QuestItemStaticData>(QuestItemsStaticDataPath).QuestItemConfigs.ForEach(x => _itemConfigs.Add(x.ItemId, x));
            Resources.Load<MeleeWeaponStaticData>(MeleeWeaponStaticDataPath).ItemConfigs.ForEach(x => _itemConfigs.Add(x.ItemId, x));
            Resources.Load<FoodStaticData>(FoodStaticDataPath).FoodConfigs.ForEach(x => _itemConfigs.Add(x.ItemId, x));
        }
    }
}