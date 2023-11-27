using CodeBase.Items;
using System;
using UnityEngine;

namespace CodeBase.StaticData.Items.Food
{
    [Serializable]
    public class FoodConfig : BaseItemConfig
    {
        [field: SerializeField] public float HealValue { get; private set; }

        public override void OnValidate()
        {
            base.OnValidate();

            if (ItemsCheckCorrectId.IsFood(ItemId) == false)
                ResetItemId();
        }
    }
}