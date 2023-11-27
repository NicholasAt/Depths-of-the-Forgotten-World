using System.Collections.Generic;
using System.Linq;

namespace CodeBase.BehaviourTree.Tree
{
    public class Sequence : Node
    {
        public Sequence() : base()
        {
        }

        public Sequence(List<Node> children) : base(children)
        {
        }

        public override bool Evaluate()
        {
            return Children.All(child => child.Evaluate());
        }
    }
}