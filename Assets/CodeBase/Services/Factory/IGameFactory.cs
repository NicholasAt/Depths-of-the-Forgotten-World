using CodeBase.Data.Items;
using CodeBase.Items;
using CodeBase.Player;
using CodeBase.StaticData.Enemy;
using CodeBase.StaticData.Items;
using CodeBase.StaticData.NPC;
using CodeBase.StaticData.PlayerTeleport;
using CodeBase.StaticData.Reward;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Services.Factory
{
    public interface IGameFactory : IService
    {
        IItemInHand CreateItemInHand(BaseItemInventoryData itemData, Transform root);

        void CreateItemPiece(BaseItemInventoryData itemData, Vector2 at);

        GameObject Player { get; }
        List<IFreezePlayer> PlayerFreezes { get; }

        void CreatePlayer(Vector2 at);

        GameObject CreateEnemy(EnemyId id, Vector2 at);

        void Cleanup();

        void CreateShopNpc(ShopNpcId id, Vector2 at, List<ItemId> ids);

        void CreateReward(RewardId id, Vector2 at);

        void CreatePlayerTeleport(TeleportId id, Vector2 at, Vector2 teleportPosition, bool toHole);
    }
}