using System.Collections.Generic;
using System.Linq;

namespace CodeBase.BehaviourTree.Tree
{
    public class Selector : Node
    {
        public Selector() : base()
        {
        }

        public Selector(List<Node> children) : base(children)
        {
        }

        public override bool Evaluate()
        {
            return Children.Any(child => child.Evaluate());
        }
    }
}