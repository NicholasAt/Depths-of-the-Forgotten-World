using UnityEngine;

namespace CodeBase.Enemy
{
    public class EnemyAnimation : MonoBehaviour
    {
        private readonly int MoveBlendHash = Animator.StringToHash("Move");
        private readonly int AttackHash = Animator.StringToHash("Attack");

        private const int IdleId = 0;
        private const int MoveId = 1;

        [SerializeField] private Animator _animator;

        public void PlayAttack()
        {
            _animator.Play(AttackHash, 0, 0);
        }

        public void PlayMove()
        {
            _animator.SetFloat(MoveBlendHash, MoveId);
        }

        public void PlayIdle()
        {
            _animator.SetFloat(MoveBlendHash, IdleId);
        }
    }
}