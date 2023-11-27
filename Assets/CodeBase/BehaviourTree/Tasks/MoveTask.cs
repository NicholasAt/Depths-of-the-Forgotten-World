using CodeBase.BehaviourTree.Tree;
using CodeBase.Enemy;

namespace CodeBase.BehaviourTree.Tasks
{
    public class MoveTask : Node
    {
        private readonly EnemyMove _move;
        private readonly float _followSpeed;
        private readonly EnemyFindTarget _findTarget;
        private readonly EnemyAnimation _animation;

        public MoveTask(EnemyMove move, float followSpeed, EnemyFindTarget findTarget, EnemyAnimation animation)
        {
            _move = move;
            _followSpeed = followSpeed;
            _findTarget = findTarget;
            _animation = animation;
        }

        public override bool Evaluate()
        {
            if (_findTarget.PlayerTransform == null)
            {
                _animation.PlayIdle();
                return false;
            }
            _move.Move(_findTarget.PlayerTransform.position, _followSpeed);
            _animation.PlayMove();
            return true;
        }
    }
}