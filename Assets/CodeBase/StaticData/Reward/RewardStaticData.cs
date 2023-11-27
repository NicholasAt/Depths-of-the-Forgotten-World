using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.StaticData.Reward
{
    [CreateAssetMenu(menuName = "Static Data/Reward Static Data", order = 0)]
    public class RewardStaticData : ScriptableObject
    {
        public List<RewardConfig> Configs;

        private void OnValidate()
        {
            Configs.ForEach(x => x.OnValidate());
        }
    }
}