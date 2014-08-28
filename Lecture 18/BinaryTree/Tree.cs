using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryTree
{
    public class Tree<T> where T : IComparable
    {
        public Node<T> RootNode { get; set;}
        public Tree(T value)
        {
            RootNode = new Node<T>(value);
        }

        public Tree(Node<T> node)
        {
            RootNode = node;
        }

        public Tree()
        {
            
        }
        
        public void InsertValue(T value)
        {
            InsertValue(RootNode, value);
        }

        public void InsertValue(Node<T> currentNode, T value)
        {
            
            int comparison = value.CompareTo(currentNode.Value);
            
            if (0 > comparison)
            {
                if (currentNode.LeftNode == null)
                {
                    currentNode.LeftNode = new Node<T>(value);
                }
                else
                {
                    InsertValue(currentNode.LeftNode, value);
                }
            }
            else if (comparison >= 0)
            {
                if (currentNode.RightNode == null)
                {
                    currentNode.RightNode = new Node<T>(value);
                }
                else
                {
                    InsertValue(currentNode.RightNode, value);
                }
            }
        }

        public int GetMaxDepth(Node<T> node)
        {
            return GetMaxDepth(node, 1);
        }

        public int GetMaxDepth(Node<T> node, int depth)
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

        

    }

    public class Node<T>
    {
        public T Value { get; set; }
        public Node<T> LeftNode { get; set; }
        public Node<T> RightNode { get; set; }

        public Node()
        {
            
        }

        public Node(T value)
        {
            Value = value;
        }
    }

    public static class Utilities
    {

        public static Node<T> GetTargetNode<T>(Queue<bool> navigation, Node<T> node)
        {
            if (navigation.Count == 0)
                return node;

            if (node == null)
                throw new Exception("Unexpectedly encountered the end of the tree");

            var nextNode = navigation.Dequeue() ? node.RightNode : node.LeftNode;

            return GetTargetNode(navigation, nextNode);
        }
    }


}
