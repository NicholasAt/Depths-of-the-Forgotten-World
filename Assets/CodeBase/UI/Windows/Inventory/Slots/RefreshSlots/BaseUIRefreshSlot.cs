using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.Inventory.Slots.RefreshSlots
{
    public abstract class BaseUIRefreshSlot : MonoBehaviour
    {
        [field: SerializeField] public Image IconImage { get; private set; }

        public void BaseConstruct(Sprite icon)
        {
            IconImage.sprite = icon;
        }

        public void Remove()
        {
            OnRemove();
            Destroy(gameObject);
        }

        protected virtual void OnRemove()
        { }
    }
}