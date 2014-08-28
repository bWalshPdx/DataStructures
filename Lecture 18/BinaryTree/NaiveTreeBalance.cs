using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryTree
{
    class NaiveTreeBalance<T>
    {
        
        public List<T> FlattenTree(Node<T> node)
        {
            List<T> flatTree = new List<T>();

            //Add value to list
            if (node.Value != null)
            {
                flatTree.Add(node.Value);
            }

            if (node.LeftNode != null)
                flatTree.AddRange(FlattenTree(node.LeftNode));

            if (node.RightNode != null)
                flatTree.AddRange(FlattenTree(node.RightNode));

            //return that list
            return flatTree;
        }

        public Node<T> MakeTree(List<T> valueList)
        {
            if (valueList.Count == 0)
                return new Node<T>();

            int midElement = valueList.Count / 2;

            return new Node<T>(valueList[midElement])
            {
                LeftNode = MakeTree(valueList.Take(midElement).ToList()),
                RightNode = MakeTree(valueList.Skip(midElement + 1).ToList())
            };
        }
    }
}
