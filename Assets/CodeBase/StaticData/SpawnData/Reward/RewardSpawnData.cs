using CodeBase.StaticData.Reward;
using System;
using UnityEngine;

namespace CodeBase.StaticData.SpawnData.Reward
{
    [Serializable]
    public class RewardSpawnData : BaseSpawnData<RewardId>
    {
        public RewardSpawnData(RewardId id, Vector2 position) : base(id, position)
        { }
    }
}