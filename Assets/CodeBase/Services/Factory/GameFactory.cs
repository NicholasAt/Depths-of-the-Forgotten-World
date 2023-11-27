using CodeBase.BehaviourTree.Brains;
using CodeBase.Data.Items;
using CodeBase.Enemy;
using CodeBase.Items;
using CodeBase.Items.Weapon.MeleeWeapon;
using CodeBase.Npc.ShopNpc;
using CodeBase.Player;
using CodeBase.PlayerTeleport;
using CodeBase.Services.GameObserver;
using CodeBase.Services.GameSound;
using CodeBase.Services.Input;
using CodeBase.Services.LogicFactory;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.StaticData;
using CodeBase.StaticData.Enemy;
using CodeBase.StaticData.Items;
using CodeBase.StaticData.Items.Weapons.MeleeWeapon;
using CodeBase.StaticData.NPC;
using CodeBase.StaticData.Player;
using CodeBase.StaticData.PlayerTeleport;
using CodeBase.StaticData.Reward;
using CodeBase.UI.Services.Factory;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CodeBase.Services.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly AllServices _allServices;
        public List<IFreezePlayer> PlayerFreezes { get; private set; }
        public GameObject Player { get; private set; }

        public GameFactory(AllServices allServices)
        {
            _allServices = allServices;
        }

        public void Cleanup()
        {
            PlayerFreezes?.Clear();
        }

        public void CreatePlayer(Vector2 at)
        {
            IInputService inputService = GetService<IInputService>();
            ILogicFactory logicFactory = GetService<ILogicFactory>();
            IStaticDataService dataService = GetService<IStaticDataService>();
            IPersistentProgressService persistentProgress = GetService<IPersistentProgressService>();
            IGameObserverService observerService = GetService<IGameObserverService>();

            PlayerStaticData config = dataService.PlayerData;

            GameObject instance = Object.Instantiate(config.PlayerPrefab, at, Quaternion.identity);
            instance.GetComponent<PlayerMove>().Construct(inputService, config);
            instance.GetComponent<PlayerAudio>().Construct(dataService, inputService);
            instance.GetComponent<PlayerHealth>().Construct(persistentProgress, observerService, logicFactory, config, at);
            instance.GetComponent<PlayerAnimation>().Construct(inputService, dataService);
            instance.GetComponent<PlayerInteraction>().Construct(inputService, dataService);
            instance.GetComponent<PlayerChangeItemInHand>().Construct(persistentProgress, logicFactory, this);
            instance.GetComponent<PlayerChangeSelectSlotStatus>().Construct(inputService, logicFactory, dataService);
            PlayerFreezes = instance.GetComponentsInChildren<IFreezePlayer>().ToList();

            Player = instance;
        }

        public void CreateReward(RewardId id, Vector2 at)
        {
            GameObject prefab = GetService<IStaticDataService>().ForReward(id).Prefab;
            Object.Instantiate(prefab, at, Quaternion.identity);
        }

        public GameObject CreateEnemy(EnemyId id, Vector2 at)
        {
            IGameObserverService observerService = GetService<IGameObserverService>();
            IPersistentProgressService progressService = GetService<IPersistentProgressService>();
            IGameSoundService soundService = GetService<IGameSoundService>();

            EnemyConfig config = GetService<IStaticDataService>().ForEnemy(id);
            GameObject instance = Object.Instantiate(config.Prefab, at, Quaternion.identity);
            instance.GetComponent<EnemyBrain>().Construct(config);
            instance.GetComponent<EnemyAudio>().Construct(config, soundService);
            instance.GetComponent<EnemyHealth>().Construct(config, progressService);
            instance.GetComponentInChildren<EnemyFindTarget>().Construct(observerService);
            return instance;
        }

        public void CreateItemPiece(BaseItemInventoryData itemData, Vector2 at)
        {
            IGameSoundService soundService = GetService<IGameSoundService>();
            BaseItemConfig config = GetService<IStaticDataService>().ForItem(itemData.Id);

            ItemCollect instance = Object.Instantiate(config.PrefabPiece, at, Quaternion.identity);
            instance.Construct(GetService<ILogicFactory>(), config.CollectedClip, soundService, itemData);
        }

        public void CreateShopNpc(ShopNpcId id, Vector2 at, List<ItemId> ids)
        {
            IUIFactory uiFactory = GetService<IUIFactory>();

            ShopNpcConfig config = GetService<IStaticDataService>().ForShopNpc(id);
            GameObject instantiate = Object.Instantiate(config.Prefab, at, Quaternion.identity);
            instantiate.GetComponent<OpenShop>().Construct(ids, uiFactory);
        }

        public void CreatePlayerTeleport(TeleportId id, Vector2 at, Vector2 teleportPosition, bool toHole)
        {
            IStaticDataService dataService = GetService<IStaticDataService>();
            IGameObserverService observerService = GetService<IGameObserverService>();

            TeleportPlayerAnotherLocation prefab = dataService.ForTeleport(id).Prefab;
            TeleportPlayerAnotherLocation instantiate = Object.Instantiate(prefab, at, Quaternion.identity);
            instantiate.Construct(toHole, teleportPosition, this, observerService);
        }

        public IItemInHand CreateItemInHand(BaseItemInventoryData itemData, Transform root)
        {
            IStaticDataService dataService = GetService<IStaticDataService>();

            switch (itemData)
            {
                case MeleeWeaponInventoryData meleeWeapon:
                    MeleeWeaponConfig config = dataService.ForItem(meleeWeapon.Id) as MeleeWeaponConfig;
                    MeleeWeaponAttackHandler meleeInstance = Object.Instantiate(config.PrefabInHand, root);
                    meleeInstance.Construct(config, Player.GetComponent<PlayerAnimation>(), Player.GetComponent<PlayerAudio>(), GetService<IInputService>());
                    return meleeInstance;

                default:
                    return null;
            }
        }

        private TService GetService<TService>() where TService : IService =>
            _allServices.Single<TService>();
    }
}