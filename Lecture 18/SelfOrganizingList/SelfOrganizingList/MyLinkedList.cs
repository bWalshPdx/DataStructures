using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace SelfOrganizingList
{
    public class MyLinkedList<T>
    {
        private Node<T> _rootNode;

        public MyLinkedList()
        {
            
        }

        public MyLinkedList(IEnumerable<T> input)
        {
            foreach (var i in input)
                AddToBack(i);
        }

        public void AddToBack(T value)
        {
            if (_rootNode == null)
            {
                _rootNode = new Node<T>(value);
                return;
            }

            foreach (var curNode in WalkNodes())
            {
                if (curNode.Child == null)
                {
                    curNode.Child = new Node<T>(value);
                    break;
                }

            }
        }

        public void AddToFront(T value)
        {
            if (_rootNode == null)
            {
                _rootNode = new Node<T>(value);
                return;
            }

            _rootNode = new Node<T>(value) { Child = _rootNode };
        }
        
        public void RemoveValue(T value)
        {
            removeNode(value, _rootNode);
        }

        public Node<T> removeNode(T value, Node<T> currentNode)
        {
            if (currentNode.value.Equals(value))
            {
                return currentNode.Child;
            }
            else
            {
                currentNode.Child = removeNode(value, currentNode.Child);

                return currentNode;
            }
        }

        public int FindIndex(T value)
        {
            return findIndex(value, 0, _rootNode);
        }

        private int findIndex(T value, int index, Node<T> currentNode)
        {
            if (currentNode == null)
                return -1;

            if (currentNode.value.Equals(value))
            {
                currentNode.Hits++;
                return index;
            }
            
            return findIndex(value, index + 1, currentNode.Child);
        }

        public void InsertNode(int index)
        {
            
        }
        

        public void Swap(int index1, int index2)
        {
            
        }

        public IEnumerable<Node<T>> WalkNodes()
        {
            Node<T> currentNode = _rootNode;

            do
            {
                yield return currentNode;
                currentNode = currentNode.Child;
            }
            while (currentNode != null);

        }

        public IEnumerable<T> WalkValues()
        {
            Node<T> currentNode = _rootNode;

            do
            {
                yield return currentNode.value;
                currentNode = currentNode.Child;
            }
            while (currentNode != null);
        }

    }
}
