using CodeBase.Enemy;
using CodeBase.Infrastructure.Logic;
using CodeBase.Infrastructure.States;
using CodeBase.Services.Factory;
using CodeBase.Services.StaticData;
using CodeBase.StaticData.Levels;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Logic.Infrastructure
{
    public class KillCounterAndLoadNextLevel
    {
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly IGameFactory _gameFactory;
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IStaticDataService _dataService;
        private readonly int _needToWinValue;
        private readonly LevelConfig _levelConfig;
        private int _currentValue;

        public KillCounterAndLoadNextLevel(EnemyHealth[] enemyHealth, ICoroutineRunner coroutineRunner, IGameFactory gameFactory, IGameStateMachine gameStateMachine, IStaticDataService dataService)
        {
            _coroutineRunner = coroutineRunner;
            _gameFactory = gameFactory;
            _gameStateMachine = gameStateMachine;
            _dataService = dataService;
            _needToWinValue = enemyHealth.Length;

            _levelConfig = _dataService.ForLevel(LevelKey());

            foreach (EnemyHealth health in enemyHealth)
                health.OnHappened += IncrementCount;
        }

        private void IncrementCount()
        {
            _currentValue++;
            if (_currentValue >= _needToWinValue)
            {
                if (_levelConfig.IsFinalScene)
                    _gameFactory.CreateReward(_levelConfig.RewardData.Id, _levelConfig.RewardData.Position);
                else
                    _coroutineRunner.StartCoroutine(LoadNextLevel());
            }
        }

        private IEnumerator LoadNextLevel()
        {
            float delay = _dataService.LevelData.DelayLoadNextLevelAfterWin;
            yield return new WaitForSeconds(delay);

            _gameStateMachine.Enter<LoadLevelState, string>(_levelConfig.NextLevelKey);
        }

        private static string LevelKey() =>
            SceneManager.GetActiveScene().name;
    }
}