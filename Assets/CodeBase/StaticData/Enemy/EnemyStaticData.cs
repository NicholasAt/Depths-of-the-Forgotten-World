using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.StaticData.Enemy
{
    [CreateAssetMenu(menuName = "Static Data/Enemy Static Data", order = 0)]
    public class EnemyStaticData : ScriptableObject
    {
        public List<EnemyConfig> EnemyConfigs;

        private void OnValidate()
        {
            EnemyConfigs.ForEach(x => x.OnValidate());
        }
    }
}