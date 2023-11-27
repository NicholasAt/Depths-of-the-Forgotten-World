using CodeBase.Items;
using UnityEngine;

namespace CodeBase.StaticData.Items
{
    public abstract class BaseItemConfig
    {
        [SerializeField] private string _inspectorName;
        [field: SerializeField] public ItemId ItemId { get; private set; }
        [field: SerializeField] public Sprite Icon { get; private set; }
        [field: SerializeField] public int PurchasePrice { get; private set; } = 1;
        [field: SerializeField] public int SellingPrice { get; private set; } = 1;
        [field: SerializeField] public AudioClip CollectedClip { get; private set; }
        [field: SerializeField] public AudioClip BuyClip { get; private set; }
        [field: SerializeField] public ItemCollect PrefabPiece { get; private set; }

        public virtual void OnValidate()
        {
            _inspectorName = ItemId.ToString();
        }

        protected void ResetItemId()
        {
            ItemId = ItemId.None;
            _inspectorName = "none";
        }
    }
}