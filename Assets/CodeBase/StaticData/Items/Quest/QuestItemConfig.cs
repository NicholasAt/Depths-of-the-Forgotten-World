using CodeBase.Items;
using System;

namespace CodeBase.StaticData.Items.Quest
{
    [Serializable]
    public class QuestItemConfig : BaseItemConfig
    {
        public override void OnValidate()
        {
            base.OnValidate();
            if (ItemsCheckCorrectId.IsQuestItem(ItemId) == false)
                ResetItemId();
        }
    }
}