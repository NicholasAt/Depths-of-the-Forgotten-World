using CodeBase.BehaviourTree.Tasks;
using CodeBase.BehaviourTree.Tree;
using CodeBase.Enemy;
using CodeBase.StaticData.Enemy;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.BehaviourTree.Brains
{
    public class EnemyBrain : TreeAI
    {
        [SerializeField] private EnemyAudio _enemyAudio;
        [SerializeField] private EnemyAnimation _animator;
        [SerializeField] private EnemyMove _move;
        [SerializeField] private EnemyFindTarget _findTarget;

        private EnemyConfig _config;

        public void Construct(EnemyConfig config)
        {
            _config = config;
        }

        public override Node SetupTree()
        {
            Node root = new Selector(new List<Node>
            {
                new Sequence(new List<Node>
                {
                    new AttackTask(transform, _findTarget, _animator, _enemyAudio, _config.AttackDistance, _config.AttackDelay, _config.Damage),
                }),
                new Sequence(new List<Node>
                {
                    new MoveTask(_move, _config.Speed, _findTarget, _animator),
                }),
                new Sequence(new List<Node>
                {
                    new MoveStartPositionTask(transform, _findTarget, _animator, transform.position, _move, _config.Speed),
                    new IdleTask(_animator),
                })
            });
            return root;
        }
    }
}