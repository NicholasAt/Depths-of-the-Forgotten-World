using CodeBase.Items;
using CodeBase.Items.Weapon.MeleeWeapon;
using System;
using UnityEngine;

namespace CodeBase.StaticData.Items.Weapons.MeleeWeapon
{
    [Serializable]
    public class MeleeWeaponConfig : BaseItemConfig
    {
        [field: SerializeField] public float Damage { get; private set; }
        [field: SerializeField] public float AttackRadius { get; private set; } = 0.5f;
        [field: SerializeField] public float DelayBeforeAttack { get; private set; } = 1f;
        [field: SerializeField] public float DelayBeforeApplyDamage { get; private set; } = 0.4f;
        [field: SerializeField] public string AttackAnimationName { get; private set; } = "AttackSword";
        [field: SerializeField] public MeleeWeaponAttackHandler PrefabInHand { get; private set; }

        public override void OnValidate()
        {
            base.OnValidate();

            if (Damage < 0)
                Damage = 0;

            if (ItemsCheckCorrectId.IsMeleeWeapon(ItemId) == false)
                ResetItemId();
        }
    }
}