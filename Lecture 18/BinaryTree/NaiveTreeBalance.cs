using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryTree
{
    class NaiveTreeBalance
    {
        public List<int> FlattenTree(Node node)
        {
            List<int> flatTree = new List<int>();

            //Add value to list
            if (node.Value != null)
            {
                flatTree.Add(node.Value.Value);
            }

            if (node.LeftNode != null)
                flatTree.AddRange(FlattenTree(node.LeftNode));

            if (node.RightNode != null)
                flatTree.AddRange(FlattenTree(node.RightNode));

            //return that list
            return flatTree;
        }

        public Node MakeTree(List<int> valueList)
        {
            if (valueList.Count == 0)
                return new Node();

            int midElement = valueList.Count / 2;

            return new Node(valueList[midElement])
            {
                LeftNode = MakeTree(valueList.Take(midElement).ToList()),
                RightNode = MakeTree(valueList.Skip(midElement + 1).ToList())
            };
        }
    }
}
