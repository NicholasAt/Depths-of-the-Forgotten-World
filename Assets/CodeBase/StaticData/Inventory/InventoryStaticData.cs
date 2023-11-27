using CodeBase.UI.Windows.Inventory.Slots.RefreshSlots.ItemsRefreshSlot;
using UnityEngine;

namespace CodeBase.StaticData.Inventory
{
    [CreateAssetMenu(menuName = "Static Data/Inventory Static Data", order = 0)]
    public class InventoryStaticData : ScriptableObject
    {
        [field: SerializeField] public int SlotsCount { get; private set; }
        [field: SerializeField] public Sprite EnableIcon { get; private set; }
        [field: SerializeField] public Sprite DisableIcon { get; private set; }
        [field: SerializeField] public Sprite EmptyIcon { get; private set; }
        [field: SerializeField] public GameObject InventorySlotPrefab { get; private set; }

        [field: SerializeField] public MeleeWeaponUIRefreshSlot MeleeWeaponUIRefreshSlotPrefab { get; private set; }
        [field: SerializeField] public FoodUIRefreshSlot FoodUIRefreshSlotPrefab { get; private set; }
        [field: SerializeField] public QuestItemsUIRefreshSlot QuestItemsUIRefreshSlotPrefab { get; private set; }

        private void OnValidate()
        {
            if (SlotsCount < 0)
                SlotsCount = 0;
        }
    }
}