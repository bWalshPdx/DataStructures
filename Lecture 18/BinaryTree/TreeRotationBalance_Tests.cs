using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace BinaryTree
{

    [TestFixture]
    public class TreeRotationBalance_Tests
    {
        public Tree TestTree()
        {
            Tree t = new Tree(60);
            t.InsertValue(50);
            t.InsertValue(70);
            t.InsertValue(65);
            t.InsertValue(80);
            t.InsertValue(75);
            t.InsertValue(85);
            t.InsertValue(90);

            return t;
        }

        [Test]
        public void GetMaxDepth_GetCorrectDepth()
        {
            var t = TestTree();

            TreeRotationBalance trb = new TreeRotationBalance();

            var depth = trb.GetMaxDepth(t.RootNode);

            Console.WriteLine(depth);
        }

        [Test]
        public void GetSecondMaxDepth_GetCorrectDepth()
        {
            var t = TestTree();

            TreeRotationBalance trb = new TreeRotationBalance();

            var depth = trb.GetSecondMaxDepth(t.RootNode, 5);

            Console.WriteLine(depth);

        }
    }
}
