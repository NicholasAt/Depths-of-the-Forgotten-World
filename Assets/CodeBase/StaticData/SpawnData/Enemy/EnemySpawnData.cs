using System;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.StaticData.SpawnData.Enemy
{
    [Serializable]
    public class EnemySpawnData
    {
        public List<EnemySpawnConfig> Configs;

        [field: SerializeField] public float Radius { get; private set; }

        [field: SerializeField] public Vector2 Position { get; private set; }

        public EnemySpawnData(Vector2 position, float radius, List<EnemySpawnConfig> configs)
        {
            Position = position;
            Radius = radius;
            Configs = configs;
        }

        public void OnValidate()
        {
            Configs.ForEach(x => x.OnValidate());
        }
    }
}