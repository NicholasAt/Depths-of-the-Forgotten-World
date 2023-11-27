using CodeBase.BehaviourTree.Tree;
using CodeBase.Enemy;

namespace CodeBase.BehaviourTree.Tasks
{
    public class IdleTask : Node
    {
        private readonly EnemyAnimation _enemyAnimation;

        public IdleTask(EnemyAnimation enemyAnimation)
        {
            _enemyAnimation = enemyAnimation;
        }

        public override bool Evaluate()
        {
            _enemyAnimation.PlayIdle();
            return true;
        }
    }
}