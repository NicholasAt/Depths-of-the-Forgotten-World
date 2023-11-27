using CodeBase.BehaviourTree.Tree;
using CodeBase.Enemy;
using UnityEngine;

namespace CodeBase.BehaviourTree.Tasks
{
    public class MoveStartPositionTask : Node
    {
        private readonly Transform _transform;
        private readonly EnemyMove _enemyMove;
        private readonly EnemyAnimation _enemyAnimation;
        private readonly Vector2 _startPosition;
        private readonly EnemyFindTarget _findTarget;
        private readonly float _moveSpeed;

        public MoveStartPositionTask(Transform transform, EnemyFindTarget findTarget, EnemyAnimation enemyAnimation, Vector2 startPosition, EnemyMove enemyMove, float moveSpeed)
        {
            _transform = transform;
            _findTarget = findTarget;
            _enemyAnimation = enemyAnimation;
            _startPosition = startPosition;
            _enemyMove = enemyMove;
            _moveSpeed = moveSpeed;
        }

        public override bool Evaluate()
        {
            if (_findTarget.PlayerTransform)
            {
                return false;
            }
            if (Vector2.Distance(_transform.position, _startPosition) < 0.1f)//offset
            {
                return true;
            }
            _enemyAnimation.PlayMove();
            _enemyMove.Move(_startPosition, _moveSpeed);
            return false;
        }
    }
}