using CodeBase.UI.Windows.Shop;
using CodeBase.UI.Windows.Tutorial;
using UnityEngine;

namespace CodeBase.StaticData.Windows
{
    [CreateAssetMenu(menuName = "Static Data/Window static data")]
    public class WindowStaticData : ScriptableObject
    {
        [field: SerializeField] public GameObject UIRoot { get; private set; }
        [field: SerializeField] public GameObject HUDPrefab { get; private set; }
        [field: SerializeField] public ShopWindow ShopWindowPrefab { get; private set; }
        [field: SerializeField] public TutorialWindow TutorialWindowPrefab { get; private set; }
        [field: SerializeField] public KeepShopItemSlot ShopSlotPrefab { get; private set; }
    }
}