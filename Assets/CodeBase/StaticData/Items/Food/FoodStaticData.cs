using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.StaticData.Items.Food
{
    [CreateAssetMenu(menuName = "Static Data/Food Static Data", order = 0)]
    public class FoodStaticData : ScriptableObject
    {
        public List<FoodConfig> FoodConfigs;

        private void OnValidate()
        {
            FoodConfigs.ForEach(x => x.OnValidate());
        }
    }
}