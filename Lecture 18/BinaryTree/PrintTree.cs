using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryTree
{
    public class PrintTree<T> where T : IComparable
    {

        public int GetTreeDepth(Node<T> node)
        {
            return getTreeDepth(0, node);
        }


        public int getTreeDepth(int currentDepth, Node<T> node)
        {
            int? LeftDepth = null;
            int? RightDepth = null;

            if (node.LeftNode != null)
                LeftDepth = getTreeDepth(currentDepth + 1, node.LeftNode);

            if (node.RightNode != null)
                RightDepth = getTreeDepth(currentDepth + 1, node.RightNode);

            if (RightDepth == null && LeftDepth == null)
                return currentDepth;

            if (node.LeftNode == null || LeftDepth < RightDepth)
                return RightDepth.Value;

            if (node.RightNode == null || RightDepth <= LeftDepth)
                return LeftDepth.Value;

            throw new Exception("Problem with getTreeDepth");
        }

    }
}
