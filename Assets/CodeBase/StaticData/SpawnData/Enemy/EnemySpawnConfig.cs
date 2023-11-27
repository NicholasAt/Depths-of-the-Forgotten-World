using CodeBase.StaticData.Enemy;
using System;
using UnityEngine;

namespace CodeBase.StaticData.SpawnData.Enemy
{
    [Serializable]
    public class EnemySpawnConfig
    {
        [SerializeField] private string _inspectorName;

        [field: SerializeField] public EnemyId Id { get; private set; }

        [field: SerializeField] public int CountTo { get; private set; } = 1;

        [field: SerializeField] public int CountFrom { get; private set; } = 1;

        public EnemySpawnConfig(EnemyId id, int countTo, int countFrom)
        {
            Id = id;
            CountTo = countTo;
            CountFrom = countFrom;
        }

        public void OnValidate()
        {
            _inspectorName = Id.ToString();
            if (CountTo < 1) CountTo = 1;
            if (CountFrom < CountTo) CountFrom = CountTo;
        }
    }
}