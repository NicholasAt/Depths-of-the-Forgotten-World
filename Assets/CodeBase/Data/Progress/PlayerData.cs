using System;
using UnityEngine;

namespace CodeBase.Data.Progress
{
    [Serializable]
    public class PlayerData
    {
        [field: SerializeField] public float CurrentHealth { get; private set; }
        [field: SerializeField] public float Money { get; private set; }

        public Action OnHealthChange;
        public Action OnMoneyChange;

        public PlayerData(float health, int money)
        {
            CurrentHealth = health;
            Money = money;
        }

        public void ResetHealth(float value)
        {
            CurrentHealth = value;
            OnHealthChange?.Invoke();
        }

        public void DecrementHealth(float value)
        {
            CurrentHealth -= value;
            OnHealthChange?.Invoke();
        }

        public void ChangeMoneyValue(int value)
        {
            Money += value;
            OnMoneyChange?.Invoke();
        }

        public void Cleanup()
        {
            OnHealthChange = null;
            OnMoneyChange = null;
        }
    }
}