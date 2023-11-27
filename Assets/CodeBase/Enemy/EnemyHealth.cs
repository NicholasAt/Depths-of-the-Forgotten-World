using CodeBase.Data.Progress;
using CodeBase.Logic;
using CodeBase.Services.PersistentProgress;
using CodeBase.StaticData.Enemy;
using System;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class EnemyHealth : MonoBehaviour, IApplyDamage
    {
        public Action OnHappened;
        private float _currentHealth;
        private PlayerData _playerData;
        private EnemyConfig _config;

        public void Construct(EnemyConfig config, IPersistentProgressService persistentProgressService)
        {
            _config = config;
            _currentHealth = config.Health;
            _playerData = persistentProgressService.PlayerProgress.PlayerData;
        }

        public void ApplyDamage(float damage)
        {
            _currentHealth -= damage;
            if (_currentHealth <= 0)
            {
                OnHappened?.Invoke();
                _playerData.ChangeMoneyValue(_config.MoneyForKill);
                Destroy(gameObject);
            }
        }
    }
}