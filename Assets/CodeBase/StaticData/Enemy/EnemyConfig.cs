using System;
using UnityEngine;

namespace CodeBase.StaticData.Enemy
{
    [Serializable]
    public class EnemyConfig
    {
        [SerializeField] private string _inspectorName;
        [field: SerializeField] public float Health { get; private set; } = 10;
        [field: SerializeField] public float Damage { get; private set; } = 1;
        [field: SerializeField] public float Speed { get; private set; } = 5f;
        [field: SerializeField] public float AttackDistance { get; private set; } = 2f;
        [field: SerializeField] public float AttackDelay { get; private set; } = 1f;
        [field: SerializeField] public int MoneyForKill { get; private set; } = 10;
        [field: SerializeField] public AudioClip AttackClip { get; private set; }
        [field: SerializeField] public EnemyId Id { get; private set; }
        [field: SerializeField] public GameObject Prefab { get; private set; }

        public void OnValidate()
        {
            _inspectorName = Id.ToString();
            if (Health < 0)
                Health = 0;
        }
    }
}