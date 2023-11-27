using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.StaticData.Levels
{
    [CreateAssetMenu(menuName = "Static Data/Levels Static Data", order = 0)]
    public class LevelsStaticData : ScriptableObject
    {
        [field: SerializeField] public float DelayLoadNextLevelAfterWin { get; private set; } = 2f;
        public List<LevelConfig> LevelConfigs;

        private void OnValidate()
        {
            if (DelayLoadNextLevelAfterWin < 0)
                DelayLoadNextLevelAfterWin = 0;

            foreach (LevelConfig config in LevelConfigs)
            {
                config.EnemyData.Configs.ForEach(x => x.OnValidate());

                config.ItemsSpawnData.MeleeWeaponData.ForEach(x => x.OnValidate());
                config.ItemsSpawnData.FoodData.ForEach(x => x.OnValidate());
                config.ItemsSpawnData.QuestItemData.ForEach(x => x.OnValidate());
            }
        }
    }
}