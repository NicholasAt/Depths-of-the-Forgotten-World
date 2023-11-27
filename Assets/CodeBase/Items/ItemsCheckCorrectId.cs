using CodeBase.StaticData.Items;

namespace CodeBase.Items
{
    public class ItemsCheckCorrectId
    {
        private const int MeleeWeaponTo = 1;
        private const int MeleeWeaponFrom = 999;

        private const int FoodTo = 1000;
        private const int FoodFrom = 1999;

        private const int QuestItemTo = 2000;
        private const int QuestItemFrom = 2999;

        public static bool IsMeleeWeapon(ItemId id) =>
            (int)id >= MeleeWeaponTo && (int)id <= MeleeWeaponFrom;

        public static bool IsFood(ItemId id) =>
            (int)id >= FoodTo && (int)id <= FoodFrom;

        public static bool IsQuestItem(ItemId id) =>
            (int)id >= QuestItemTo && (int)id <= QuestItemFrom;
    }
}