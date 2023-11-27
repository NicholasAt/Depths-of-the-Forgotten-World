using CodeBase.Camera;
using CodeBase.Data.Items;
using CodeBase.Enemy;
using CodeBase.Infrastructure.Logic;
using CodeBase.Services.Cleanup;
using CodeBase.Services.Factory;
using CodeBase.Services.GameSound;
using CodeBase.Services.LogicFactory;
using CodeBase.Services.StaticData;
using CodeBase.StaticData.Levels;
using CodeBase.StaticData.SpawnData.Enemy;
using CodeBase.StaticData.SpawnData.Items;
using CodeBase.StaticData.SpawnData.NPC.ShopNpc;
using CodeBase.StaticData.SpawnData.PlayerTeleport;
using CodeBase.UI.Services.Factory;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure.States
{
    public class LoadLevelState : IPayloadState<string>
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadCurtain _loadingCurtain;
        private readonly IGameFactory _gameFactory;
        private readonly IUIFactory _uiFactory;
        private readonly ICleanupService _cleanupService;
        private readonly ILogicFactory _logicFactory;
        private readonly IStaticDataService _staticDataService;
        private readonly IGameSoundService _gameSoundService;

        public LoadLevelState(IGameStateMachine stateMachine, SceneLoader sceneLoader, LoadCurtain loadingCurtain,
            IGameFactory gameFactory,
            IUIFactory uiFactory,
            ICleanupService cleanupService,
            ILogicFactory logicFactory,
            IStaticDataService staticDataService,
            IGameSoundService gameSoundService)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
            _gameFactory = gameFactory;
            _uiFactory = uiFactory;
            _cleanupService = cleanupService;
            _logicFactory = logicFactory;
            _staticDataService = staticDataService;
            _gameSoundService = gameSoundService;
        }

        public void Enter(string sceneName)
        {
            _loadingCurtain.Show();
            _cleanupService.Cleanup();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit()
        {
            _loadingCurtain.Hide();
        }

        private void OnLoaded()
        {
            LevelConfig levelConfig = _staticDataService.ForLevel(SceneManager.GetActiveScene().name);

            _uiFactory.CreateUIRoot();
            _logicFactory.InitInventorySlotsHandler();
            _uiFactory.CreateHUD(levelConfig.DisplayedLevelNameWorld, levelConfig.DisplayedLevelNameHole);

            _gameFactory.CreatePlayer(levelConfig.PlayerInitialPosition);
            InitCamera();
            InitItems(levelConfig);

            EnemyHealth[] enemyHealths = InitEnemy(levelConfig);
            _logicFactory.InitKillCountAndLoadNextLevel(enemyHealths);
            InitShop(levelConfig);
            InitTeleport(levelConfig);

            _gameSoundService.PlayBackgroundMusic();

            _stateMachine.Enter<LoopState>();
        }

        private void InitTeleport(LevelConfig levelConfig)
        {
            foreach (PlayerTeleportSpawnData data in levelConfig.TeleportSpawns)
                _gameFactory.CreatePlayerTeleport(data.Id, data.Position, data.TeleportPosition, data.IsToHole);
        }

        private void InitShop(LevelConfig levelConfig)
        {
            foreach (ShopNpcSpawnData data in levelConfig.ShopNpcData)
                _gameFactory.CreateShopNpc(data.Id, data.Position, data.Items);
        }

        private EnemyHealth[] InitEnemy(LevelConfig levelConfig)
        {
            List<EnemyHealth> enemyHealths = new List<EnemyHealth>();

            EnemySpawnData enemyData = levelConfig.EnemyData;
            foreach (EnemySpawnConfig config in enemyData.Configs)
            {
                int enemyCount = Random.Range(config.CountTo, config.CountTo);
                for (int i = 0; i < enemyCount; i++)
                {
                    Vector2 position = enemyData.Position + Random.insideUnitCircle * enemyData.Radius;
                    GameObject instance = _gameFactory.CreateEnemy(config.Id, position);
                    enemyHealths.Add(instance.GetComponent<EnemyHealth>());
                }
            }

            return enemyHealths.ToArray();
        }

        private void InitItems(LevelConfig levelConfig)
        {
            ItemsSpawnDataContainer itemsData = levelConfig.ItemsSpawnData;
            CreateMeleeWeapon();
            CreateFood();
            CreateQuestItems();

            void CreateMeleeWeapon()
            {
                foreach (MeleeWeaponSpawnData data in itemsData.MeleeWeaponData)
                    _gameFactory.CreateItemPiece(new MeleeWeaponInventoryData(data.Id), data.Position);
            }
            void CreateFood()
            {
                foreach (FoodSpawnData data in itemsData.FoodData)
                    _gameFactory.CreateItemPiece(new FoodInventoryData(data.Id), data.Position);
            }
            void CreateQuestItems()
            {
                foreach (var data in itemsData.QuestItemData)
                    _gameFactory.CreateItemPiece(new QuestItemsInventoryData(data.Id), data.Position);
            }
        }

        private void InitCamera()
        {
            UnityEngine.Camera.main.GetComponent<CameraFollowTarget>().SetTarget(_gameFactory.Player.transform);
        }
    }
}