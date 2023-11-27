using CodeBase.StaticData.Items;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.Shop
{
    public class KeepShopItemSlot : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Image _iconImage;

        public BaseItemConfig ItemConfig { get; private set; }
        public Action<KeepShopItemSlot> OnClick;

        public void Construct(BaseItemConfig itemConfig)
        {
            _iconImage.sprite = itemConfig.Icon;
            ItemConfig = itemConfig;
        }

        public void Remove()
        {
            OnClick = null;
            Destroy(gameObject);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            OnClick?.Invoke(this);
        }
    }
}