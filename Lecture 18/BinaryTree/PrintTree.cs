using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryTree
{
    public class PrintTree
    {

        public int GetTreeDepth(Node node)
        {
            return getTreeDepth(0, node);
        }


        public int getTreeDepth(int currentDepth, Node node)
        {
            int? LeftDepth = null;
            int? RightDepth = null;

            if (node.LeftNode != null)
                LeftDepth = this.getTreeDepth(currentDepth + 1, node.LeftNode);

            if (node.RightNode != null)
                RightDepth = this.getTreeDepth(currentDepth + 1, node.RightNode);

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
