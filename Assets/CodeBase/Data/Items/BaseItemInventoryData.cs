using CodeBase.StaticData.Items;
using UnityEngine;

namespace CodeBase.Data.Items
{
    public abstract class BaseItemInventoryData
    {
        [field: SerializeField] public ItemId Id { get; private set; }

        protected BaseItemInventoryData(ItemId id)
        {
            Id = id;
        }
    }
}