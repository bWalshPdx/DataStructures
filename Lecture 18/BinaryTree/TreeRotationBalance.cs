using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryTree
{
    class TreeRotationBalance
    {
        public bool TreeIsBalanced(Node node)
        {
            TreeIsBalanced(node, 0);

            return true;
        }

        public bool TreeIsBalanced(Node node, int l)
        {
            var newList = new List<Tuple<Node, int>>();

            //if (node.RightNode == null && node.LeftNode == null) //return new Tuple<Node, int>(node, l);

            //var left = TreeIsBalanced(node.LeftNode, l + 1);
            //var right = TreeIsBalanced(node.RightNode, l + 1);

            return true;
        }

        //TODO:Write this to return the chain of max nodes?:
        public Node GetMaxDepth(Node node)
        {
            return GetMaxDepth(node, 1).Item1;
        }

        public Tuple<Node, int> GetMaxDepth(Node node, int depth)
        {
            if (node.LeftNode == null && node.RightNode == null)
                return new Tuple<Node, int>(node, depth);

            Tuple<Node, int> right = new Tuple<Node, int>(new Node(), 0);
            if (node.RightNode != null)
                right = GetMaxDepth(node.RightNode, depth + 1);

            Tuple<Node, int> left = new Tuple<Node, int>(new Node(), 0);
            if (node.LeftNode != null)
                left = GetMaxDepth(node.LeftNode, depth + 1);

            if (left.Item2 > right.Item2)
            {
                var currentNode = new Node(node.Value).LeftNode = node.LeftNode;

                return new Tuple<Node, int>(currentNode, depth);
            }
            else
            {
                var currentNode = new Node(node.Value).RightNode = node.RightNode;
                return new Tuple<Node, int>(currentNode, depth);
            }
        }
         
        public int GetSecondMaxDepth(Node node, int maxDepth)
        {
            return GetSecondMaxDepth(node, maxDepth, 1);
        }

        public int GetSecondMaxDepth(Node node, int maxDepth, int currentDepth)
        {
            int currentMax = 0;
            
            if (node.RightNode == null && node.LeftNode == null)
                return currentDepth;

            int right = 0;
            if (node.RightNode != null)
                right = GetSecondMaxDepth(node.RightNode, maxDepth, currentDepth + 1);

            int left = 0;
            if (node.LeftNode != null)
                left = GetSecondMaxDepth(node.LeftNode, maxDepth, currentDepth + 1);

            if (right < left)
            {
                if (left != maxDepth)
                    return left;

                return right;
            }

            if (left <= right)
            {
                if (right != maxDepth)
                    return right;

                return left;
            }

            throw new Exception("this is not supposed to happen");
        }
    }
}
