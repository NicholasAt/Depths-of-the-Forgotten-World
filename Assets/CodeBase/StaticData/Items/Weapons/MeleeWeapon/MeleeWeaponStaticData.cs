using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.StaticData.Items.Weapons.MeleeWeapon
{
    [CreateAssetMenu(menuName = "Static Data/Melee Weapon Static Data", order = 0)]
    public class MeleeWeaponStaticData : ScriptableObject
    {
        public List<MeleeWeaponConfig> ItemConfigs;

        private void OnValidate()
        {
            ItemConfigs.ForEach(x => x.OnValidate());
        }
    }
}