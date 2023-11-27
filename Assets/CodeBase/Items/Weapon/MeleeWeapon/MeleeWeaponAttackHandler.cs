using CodeBase.Logic;
using CodeBase.Player;
using CodeBase.Services.Input;
using CodeBase.StaticData.Items.Weapons.MeleeWeapon;
using System.Collections;
using UnityEngine;

namespace CodeBase.Items.Weapon.MeleeWeapon
{
    public class MeleeWeaponAttackHandler : MonoBehaviour, IItemInHand
    {
        [SerializeField] private Transform _attackPoint;

        private PlayerAnimation _animation;
        private IInputService _inputService;
        private MeleeWeaponConfig _config;
        private float _currentDelay;
        private PlayerAudio _playerAudio;

        public void Construct(MeleeWeaponConfig config, PlayerAnimation playerAnimation, PlayerAudio playerAudio, IInputService inputService)
        {
            _inputService = inputService;
            _config = config;
            _animation = playerAnimation;
            _playerAudio = playerAudio;
            _currentDelay = _config.DelayBeforeAttack;

            _inputService.OnUseItem1 += Attack;
        }

        private void Update()
        {
            _currentDelay += Time.deltaTime;
        }

        private void OnDestroy()
        {
            _inputService.OnUseItem1 -= Attack;
        }

        public void Close()
        {
            Destroy(gameObject);
        }

        private void Attack()
        {
            if (_currentDelay < _config.DelayBeforeAttack)
                return;
            _currentDelay = 0.0f;

            _animation.PlayAnimation(_config.AttackAnimationName);
            _playerAudio.PlayAttack();
            StartCoroutine(ApplyHit());
        }

        private IEnumerator ApplyHit()
        {
            yield return new WaitForSeconds(_config.DelayBeforeApplyDamage);
            Collider2D[] colliders = Physics2D.OverlapCircleAll(_attackPoint.position, _config.AttackRadius);

            foreach (Collider2D collision in colliders)
            {
                if (collision.isTrigger)
                    continue;

                Rigidbody2D attachedRigidbody = collision.attachedRigidbody;
                if (attachedRigidbody != null
                    && attachedRigidbody.TryGetComponent(out IApplyDamage apply)
                    && apply.GetType() != typeof(PlayerHealth))
                {
                    apply.ApplyDamage(_config.Damage);
                }
            }
        }

        private void OnDrawGizmos()
        {
            if (_config != null)
            {
                Gizmos.color = Color.magenta;
                Gizmos.DrawSphere(_attackPoint.position, _config.AttackRadius);
            }
        }
    }
}