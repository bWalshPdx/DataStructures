using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace BinaryTree
{
    

    public class AVLSelfBalance
    {
        public Node RootNode = new Node();

        public AVLSelfBalance(Node rootNode)
        {
            this.RootNode = rootNode;
        }

        public AVLSelfBalance()
        {
            
        }

        public void InsertValue(int value)
        {
            RootNode = InsertValue(RootNode, value);
        }

        public Node InsertValue(Node currentNode, int value)
        {

            //Insert the RootNode:
            int comparison = value.CompareTo(currentNode.Value);

            if (comparison == -1)
            {
                if (currentNode.LeftNode == null)
                {
                    currentNode.LeftNode = new Node(value);
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
                    currentNode.RightNode = new Node(value);
                }
                else
                {
                    currentNode.RightNode = InsertValue(currentNode.RightNode, value);
                }
            }
            
            //Check Balance of the tree and rotate if needed:
            var balanceValue = GetBalanceValue(currentNode);

            if (balanceValue <= -2)
               currentNode = RR(currentNode);

            if (2 <= balanceValue)
                currentNode = LL(currentNode);

            return currentNode;
        }

        //TODO: Make sure this looks right:
        public int GetBalanceValue(Node node)
        {
            var leftDepth = 0;
            var rightDepth = 0;

            if (node.LeftNode != null)
                leftDepth = GetMaxDepth(node.LeftNode) + 1;

            if(node.RightNode != null)
                rightDepth = GetMaxDepth(node.RightNode) + 1;

            return leftDepth - rightDepth;
        }

        public int GetMaxDepth(Node node)
        {
            if (node != null)
                return GetMaxDepth(node, 0);

            return 0;
        }

        
        public int GetMaxDepth(Node node, int depth)
        {
            int right = 0;
            int left = 0;

            if (node.RightNode != null)
            {
                right = GetMaxDepth(node.RightNode, depth + 1);
            }

            if (node.LeftNode != null)
            {
                left = GetMaxDepth(node.LeftNode, depth + 1);
            }

            if (left > right)
            {
                return left;
            }
            else if (left <= right && right != 0)
            {
                return right;
            }
            
            return depth;
        }

        public Node LL(Node node)
        {
            Tree t = new Tree(node);
            //Move from the children to the root:
            //Get Children of Children
            var newRightLeftChild = t.GetTargetNode(new Queue<Tree.Direction>(new[] { Tree.Direction.Left, Tree.Direction.Right }));
            //Get Direct Children
            var newLeftChild = t.GetTargetNode(new Queue<Tree.Direction>(new[] { Tree.Direction.Left, Tree.Direction.Left }));
            var newRightChild = node;
            //Get New Root
            var newRoot = t.GetTargetNode(new Queue<Tree.Direction>(new[] { Tree.Direction.Left}));

            //Attach Children of Children to Direct Children
            newRightChild.LeftNode = newRightLeftChild;

            //Attach Children to Root
            newRoot.LeftNode = newLeftChild;
            newRoot.RightNode = newRightChild;

            return newRoot;
        }

        //TODO: Look at why this is creating a infinite Right Child when referencing RootNode rather than its output
        //My references are all messed up.
        public Node RR(Node node)
        {
            Tree t = new Tree(node);
            //Move from the children to the root:
            //Get Children of Children
            var newLeftRightChild = t.GetTargetNode(new Queue<Tree.Direction>(new[] { Tree.Direction.Right, Tree.Direction.Left }));
            //Get Direct Children
            var newLeftChild = node;
            var newRightChild = t.GetTargetNode(new Queue<Tree.Direction>(new[] { Tree.Direction.Right, Tree.Direction.Right }));
            //Get New Root
            var newRoot = t.GetTargetNode(new Queue<Tree.Direction>(new[] { Tree.Direction.Right }));
            
            //Attach Children of Children to Direct Children
            newLeftChild.RightNode = newLeftRightChild;
            //Attach Children to Root
            newRoot.LeftNode = newLeftChild;
            newRoot.RightNode = newRightChild;

            return newRoot;
        }
    }
}
