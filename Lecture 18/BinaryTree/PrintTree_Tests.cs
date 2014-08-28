using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;


namespace BinaryTree
{
    

    [TestFixture]
    public class PrintTree_Tests
    {
        [Test]
        public void IntegrationTest()
        {
            
            var tree = new Tree<int>(5);

            tree.InsertValue(0);
            tree.InsertValue(1);
            tree.InsertValue(6);

            var pt = new PrintTree<int>();

            int depth = pt.GetTreeDepth(tree.RootNode);

            Console.WriteLine(depth);
        }

    }
}
