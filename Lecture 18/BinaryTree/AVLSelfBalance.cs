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
               currentNode = Rotate(currentNode, true);

            if (2 <= balanceValue)
                currentNode = Rotate(currentNode, false);

            return currentNode;
        }

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
       
        public Node Rotate(Node node, bool rotateRight)
        {
            bool goRight = false;
            
            if (rotateRight)
                goRight = rotateRight;

            Tree t = new Tree(node);
            //Move from the children to the root:
            //Get Children of Children
            var orphanChild = t.GetTargetNode(new Queue<bool>(new[] { goRight, !goRight}));
            //Get Direct Children
            var newLeftChild = node;
            var tailOfRoot = t.GetTargetNode(new Queue<bool>(new[] { goRight, goRight}));
            //Get New Root
            var newRoot = t.GetTargetNode(new Queue<bool>(new[] { goRight}));
            

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
