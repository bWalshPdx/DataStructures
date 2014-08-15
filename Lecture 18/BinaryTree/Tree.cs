using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryTree
{
    public class Tree
    {
        public Node RootNode;
        public Tree(int value)
        {
            this.RootNode = new Node(value);
        }

        public Tree(Node node)
        {
            RootNode = node;
        }

        public Tree()
        {
            RootNode = new Node();
        }
        
        public void InsertValue(int value)
        {
            InsertValue(RootNode, value);
        }

        public void InsertValue(Node currentNode, int value)
        {
            
            int comparison = value.CompareTo(currentNode.Value);
            
            if (comparison == -1)
            {
                if (currentNode.LeftNode == null)
                {
                    currentNode.LeftNode = new Node(value);
                }
                else
                {
                    InsertValue(currentNode.LeftNode, value);
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
                    InsertValue(currentNode.RightNode, value);
                }
            }
            //Equals zero in which cas it has already been inserted

        }

        public bool IsTreeValid()
        {
            return IsTreeValid(this.RootNode);
        }

        private bool IsTreeValid(Node tree)
        {
            bool leftIsValid = (tree.LeftNode == null || (tree.LeftNode.Value > tree.Value && IsTreeValid(tree.LeftNode)) );
            bool rightIsValid = (tree.RightNode == null || (tree.Value > tree.RightNode.Value && IsTreeValid(tree.RightNode)) );

            return leftIsValid && rightIsValid;
        }


        public Node DeleteNode(int value)
        {
            return DeleteNode(value, this.RootNode);
        }

        
        public Node DeleteNode(int value, Node node)
        {
            if (node == null)
                return null;

            if (value == node.Value)
            {
                return performDelete(value, node);
            }
            //Else recurse on the subtree's:

            node.LeftNode = DeleteNode(value, node.LeftNode);
            node.RightNode = DeleteNode(value, node.RightNode);
            
            return node;
        }

        private Node performDelete(int value, Node node)
        {
            //The node is a leaf:
            //delete the leaf node

            if (node.RightNode == null && node.LeftNode == null)
                return new Node();

            //The node to be deleted only has one sub tree:
            //link the child sub tree to the deleted parent

            if (node.RightNode != null && node.LeftNode == null)
                return node.RightNode;
            
            if(node.LeftNode != null && node.RightNode == null)
                return node.LeftNode;

            //The node has two sub tree's:
               //traverse tree
               //find the right sub nodes smallest entry and replace the newly deleted node

            if(node.RightNode != null && node.LeftNode != null)
            {
                int valueToMove = findSmallestValue(node.RightNode).Value;
                Node newSubTree = DeleteNode(valueToMove, node.RightNode);

                return new Node(valueToMove) {LeftNode = node.LeftNode, RightNode = newSubTree};
            }

            throw new Exception("Case not descriped in delete function");
        }

        
        private int? findSmallestValue(Node node)
        {
            if (node.LeftNode == null && node.RightNode == null)
                return node.Value;

            if(node.LeftNode != null || node.LeftNode.Value < node.Value)
                return findSmallestValue(node.LeftNode);

            if (node.RightNode != null || node.RightNode.Value < node.Value)
                return findSmallestValue(node.RightNode);

            return node.Value.Value;
        }

        public int GetSecondMaxDepth(Node node, int maxDepth)
        {
            return GetSecondMaxDepth(node, maxDepth, 1);
        }

        public int GetSecondMaxDepth(Node node, int maxDepth, int currentDepth)
        {
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

        public int GetMaxDepth(Node node)
        {
            return GetMaxDepth(node, 1);
        }

        public int GetMaxDepth(Node node, int depth)
        {
            if (node.RightNode == null && node.LeftNode == null)
                return depth;

            int right = 0;
            if (node.RightNode != null)
                right = GetMaxDepth(node.RightNode, depth + 1);

            int left = 0;
            if (node.LeftNode != null)
                left = GetMaxDepth(node.LeftNode, depth + 1);

            if (right < left)
            {
                return left;
            }
            else
            {
                return right;
            }
        }

        public enum Direction { Right, Left };

        public Node GetTargetNode(Queue<Direction> nav)
        {
            var temp = getTargetNode(nav, RootNode);
            return temp;
        }

        private Node getTargetNode(Queue<Direction> navigation, Node node)
        {
            if (node == null || navigation.Count == 0)
                return node;

            if (navigation.Dequeue() == Direction.Right)
            {
                var temp = getTargetNode(navigation, node.RightNode);
                return temp;
            }
            else
            {
                var temp = getTargetNode(navigation, node.LeftNode);
                return temp;
            }
        }

    }

    public class Node
    {
        public int? Value;
        public Node LeftNode;
        public Node RightNode;

        public Node()
        {
            
        }

        public Node(int? value)
        {
            Value = value;
        }
    }
}
