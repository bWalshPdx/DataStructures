using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace BinaryTree
{
    public class AVLTree<T> where T : IComparable
    {
        public Node<T> RootNode { get; set;}

        public AVLTree(Node<T> rootNode)
        {
            RootNode = rootNode;
        }

        public AVLTree()
        {
            
        }

        public void InsertValue(T value)
        {
            RootNode = InsertValue(RootNode, value);
        }

        public Node<T> InsertValue(Node<T> currentNode, T value)
        {
            //Insert the RootNode:
            int comparison = value.CompareTo(currentNode.Value);

            if (comparison == -1)
            {
                if (currentNode.LeftNode == null)
                {
                    currentNode.LeftNode = new Node<T>(value);
                }
                else
                {
                    currentNode.RightNode = InsertValue(currentNode.LeftNode, value);
                }
            }
            else if (comparison == 1)
            {
                if (currentNode.RightNode == null)
                {
                    currentNode.RightNode = new Node<T>(value);
                }
                else
                {
                    currentNode.RightNode = InsertValue(currentNode.RightNode, value);
                }
            }
            
            //Check Balance of the tree and rotate if needed:
            var balanceValue = GetBalanceValue(currentNode);

            if (balanceValue <= -2)
               currentNode = Rotate(currentNode, true);

            if (2 <= balanceValue)
                currentNode = Rotate(currentNode, false);

            return currentNode;
        }

        public int GetBalanceValue(Node<T> node)
        {
            var leftDepth = 0;
            var rightDepth = 0;

            if (node.LeftNode != null)
                leftDepth = GetMaxDepth(node.LeftNode) + 1;

            if(node.RightNode != null)
                rightDepth = GetMaxDepth(node.RightNode) + 1;

            return leftDepth - rightDepth;
        }

        public int GetMaxDepth(Node<T> node)
        {
            if (node != null)
                return GetMaxDepth(node, 0);

            return 0;
        }

        public int GetMaxDepth(Node<T> node, int depth)
        {
            if (node.LeftNode == null && node.RightNode == null)
                return 0;

            return Math.Max(GetMaxDepth(node.LeftNode), GetMaxDepth(node.RightNode)) + 1;
        }
       
        public Node<T> Rotate(Node<T> node, bool rotateRight)
        {
            bool goRight = rotateRight;

            //Move from the children to the root:
            //Get Children of Children
            var orphanChild = Utilities.GetTargetNode(new Queue<bool>(new[] { goRight, !goRight}), node);
            //Get Direct Children
            var newLeftChild = node;
            var tailOfRoot = Utilities.GetTargetNode(new Queue<bool>(new[] { goRight, goRight }), node);
            //Get New Root
            var newRoot = Utilities.GetTargetNode(new Queue<bool>(new[] { goRight}), node);
            
            //Perform Rotation:
            //Attach Children of Children to Direct Children
            if (goRight)
            {
                newLeftChild.RightNode = orphanChild;
            }
            else
            {
                newLeftChild.LeftNode = orphanChild;
            }

            //Attach Children to Root:
            if (goRight)
            {
                newRoot.LeftNode = newLeftChild;
                newRoot.RightNode = tailOfRoot;
            }
            else
            {
                newRoot.RightNode = newLeftChild;
                newRoot.LeftNode = tailOfRoot;
            }

            return newRoot;
        }
    }
}
