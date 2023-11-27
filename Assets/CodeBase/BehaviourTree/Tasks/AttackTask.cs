using CodeBase.BehaviourTree.Tree;
using CodeBase.Enemy;
using UnityEngine;

namespace CodeBase.BehaviourTree.Tasks
{
    public class AttackTask : Node
    {
        private readonly EnemyAnimation _animation;
        private readonly EnemyAudio _audio;
        private readonly float _attackDistance;
        private readonly Transform _transform;
        private readonly EnemyFindTarget _findTarget;
        private readonly float _attackDelay;
        private readonly float _damage;

        private float _currentDelay;

        public AttackTask(Transform transform, EnemyFindTarget findTarget, EnemyAnimation enemyAnimation, EnemyAudio audio, float attackDistance, float attackDelay, float damage)
        {
            _transform = transform;
            _findTarget = findTarget;
            _animation = enemyAnimation;
            _audio = audio;
            _attackDistance = attackDistance;
            _attackDelay = attackDelay;
            _damage = damage;
        }

        public override bool Evaluate()
        {
            if (_findTarget.PlayerTransform == null)
            {
                return false;
            }
            if (Vector3.Distance(_transform.position, _findTarget.PlayerTransform.position) <= _attackDistance)
            {
                _currentDelay += Time.deltaTime;
                if (_currentDelay >= _attackDelay)
                {
                    _animation.PlayAttack();
                    _currentDelay = 0;
                    _audio.PlayAttack();
                    _findTarget.PlayerHealth.ApplyDamage(_damage);
                }
                return true;
            }

            return false;
        }
    }
}