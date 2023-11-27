using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.BehaviourTree.Tree
{
    public abstract class TreeAI : MonoBehaviour
    {
        private Node _root;

        private void Start()
        {
            _root = SetupTree();
        }

        private void Update()
        {
            _root.Evaluate();
        }

        public abstract Node SetupTree();
    }

    public class Node
    {
        protected List<Node> Children = new List<Node>();
        public Node Parent;

        public Node()
        {
            Parent = null;
        }

        public Node(List<Node> children)
        {
            foreach (var child in children)
                SetParents(child);
        }

        private void SetParents(Node node)
        {
            node.Parent = this;
            Children.Add(node);
        }

        public virtual bool Evaluate() => false;
    }
}