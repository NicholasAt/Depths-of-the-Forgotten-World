using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.StaticData.Items.Quest
{
    [CreateAssetMenu(menuName = "Static Data/Quest Items Static Data", order = 0)]
    public class QuestItemStaticData : ScriptableObject
    {
        public List<QuestItemConfig> QuestItemConfigs;

        private void OnValidate()
        {
            QuestItemConfigs.ForEach(x => x.OnValidate());
        }
    }
}