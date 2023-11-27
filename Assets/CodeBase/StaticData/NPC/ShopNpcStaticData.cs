using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.StaticData.NPC
{
    [CreateAssetMenu(menuName = "Static Data/Shop Npc Static Data", order = 0)]
    public class ShopNpcStaticData : ScriptableObject
    {
        public List<ShopNpcConfig> ShopNpcConfigs;

        private void OnValidate()
        {
            ShopNpcConfigs.ForEach(x => x.OnValidate());
        }
    }
}