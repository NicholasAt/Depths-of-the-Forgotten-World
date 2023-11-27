using CodeBase.StaticData.Audio;
using CodeBase.StaticData.Enemy;
using CodeBase.StaticData.Inventory;
using CodeBase.StaticData.Items;
using CodeBase.StaticData.Levels;
using CodeBase.StaticData.NPC;
using CodeBase.StaticData.Player;
using CodeBase.StaticData.PlayerTeleport;
using CodeBase.StaticData.Reward;
using CodeBase.StaticData.Windows;
using CodeBase.StaticData.Windows.Tutorial;

namespace CodeBase.Services.StaticData
{
    public interface IStaticDataService : IService
    {
        void Load();

        WindowStaticData WindowData { get; }
        InventoryStaticData InventoryStaticData { get; }
        PlayerStaticData PlayerData { get; }
        LevelsStaticData LevelData { get; }
        AudioStaticData AudioData { get; }
        TutorialStaticData TutorialData { get; }

        LevelConfig ForLevel(in string levelKey);

        EnemyConfig ForEnemy(EnemyId id);

        BaseItemConfig ForItem(ItemId id);

        ShopNpcConfig ForShopNpc(ShopNpcId id);

        AudioConfig ForAudio(AudioId id);

        RewardConfig ForReward(RewardId id);

        TeleportConfig ForTeleport(TeleportId id);
    }
}