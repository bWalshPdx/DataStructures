using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace BinaryTree
{

    [TestFixture]
    public class AVLSelfBalance_Tests
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

        public IEnumerable<TestCaseData> TreesAndExpectedDepths
        {
            get
            {
                var zeroDepthTree = new Node(10);

                var oneDepthTree = new Node(50);
                oneDepthTree.LeftNode = new Node(40);
                oneDepthTree.RightNode = new Node(60);

                var twoDepthTree = new Node(70);
                twoDepthTree.LeftNode = new Node(60);
                twoDepthTree.RightNode = new Node(80);
                twoDepthTree.RightNode.RightNode = new Node(90);

                var threeDepthTree = new Node(70);
                threeDepthTree.LeftNode = new Node(60);
                threeDepthTree.RightNode = new Node(80);
                threeDepthTree.RightNode.RightNode = new Node(90);
                threeDepthTree.RightNode.RightNode.LeftNode = new Node(85);
                threeDepthTree.RightNode.RightNode.RightNode = new Node(100);

                yield return new TestCaseData(zeroDepthTree, 0).SetName("Just the root tree");
                yield return new TestCaseData(oneDepthTree, 1).SetName("Tree with depth of one");
                yield return new TestCaseData(twoDepthTree, 2).SetName("Tree with depth of two");
                yield return new TestCaseData(threeDepthTree, 3).SetName("Tree with depth of three");
            }
        }



        [TestCaseSource("TreesAndExpectedDepths")]
        public void GetMaxDepth_GetCorrectDepth(Node testTree, int expectedDepth)
        {
            
            AVLSelfBalance trb = new AVLSelfBalance();

            var output = trb.GetMaxDepth(testTree);

            Assert.AreEqual(expectedDepth, output);
            
        }

        public IEnumerable<TestCaseData> DifferentTreesAndBalances
        {
            
            get
            {
                var postRotatedTree2 = new Node(60);
                postRotatedTree2.RightNode = new Node(70);
                postRotatedTree2.LeftNode = new Node(50);

                var postRotatedTree = new Node(40);
                postRotatedTree.LeftNode = new Node(30);
                postRotatedTree.RightNode = postRotatedTree2;


                var preRotatedTree2 = new Node(50);
                preRotatedTree2.RightNode = new Node(60);
                preRotatedTree2.RightNode.RightNode = new Node(70);

                var preRotatedTree = new Node(40);
                preRotatedTree.LeftNode = new Node(30);
                preRotatedTree.RightNode = preRotatedTree2;

                var balancedTree = new Node(50);
                balancedTree.RightNode = new Node(60);
                balancedTree.LeftNode = new Node(70);

                var threeRightTree = new Node(50);
                threeRightTree.RightNode = new Node(60);
                threeRightTree.RightNode.RightNode = new Node(70);

                yield return new TestCaseData(preRotatedTree, -2).SetName("Pre RR Shifted Tree");
                yield return new TestCaseData(postRotatedTree, -1).SetName("Post RR Shifted Tree");
                yield return new TestCaseData(balancedTree, 0).SetName("Balanced Tree");
                yield return new TestCaseData(threeRightTree, -2).SetName("Three Right Tree");
                
                
            }

        }


        [TestCaseSource("DifferentTreesAndBalances")]
        public void GetBalanceValue_GetsCorrectValue(Node tree, int expectedBalance)
        {

            AVLSelfBalance trb = new AVLSelfBalance();

            var output = trb.GetBalanceValue(tree);

            Assert.AreEqual(expectedBalance, output);
        }

        public Tree LLTestTree()
        {
            Tree t = new Tree(99);
            t.InsertValue(90);
            t.InsertValue(80);
            
            return t;
        }

        [Test]
        public void LL_RotatesCorrectly()
        {
            Tree t = this.LLTestTree();

            Node node = new Node(90);
            node.LeftNode = new Node(80);
            node.RightNode= new Node(99);

            AVLSelfBalance tr = new AVLSelfBalance();

            var output = tr.LL(t.RootNode);

            Assert.AreEqual(output.Value, node.Value);
            Assert.AreEqual(output.LeftNode.Value, node.LeftNode.Value);
            Assert.AreEqual(output.RightNode.Value, node.RightNode.Value);

        }

        public Tree RRTestTree()
        {
            Tree t = new Tree(50);
            t.InsertValue(51);
            t.InsertValue(53);

            return t;
        }

        [Test]
        public void RR_RotatesCorrectly_TheMainBranchOnly()
        {
            Tree t = RRTestTree();

            Node correctlyRotatedTree = new Node(51);
            correctlyRotatedTree.LeftNode = new Node(50);
            correctlyRotatedTree.RightNode = new Node(53);

            //TODO: Finish this test:
            AVLSelfBalance tr = new AVLSelfBalance();

            var output = tr.RR(t.RootNode);

            Assert.AreEqual(output.Value, correctlyRotatedTree.Value);
            Assert.AreEqual(output.RightNode.Value, correctlyRotatedTree.RightNode.Value);
            Assert.AreEqual(output.LeftNode.Value, correctlyRotatedTree.LeftNode.Value);
        }

        public Node RightLeaningTree()
        {
            Node tree3 = new Node(70);
            tree3.LeftNode = new Node(65);
            tree3.RightNode = new Node(75);

            Node tree2 = new Node(60);
            tree2.LeftNode = new Node(55);
            tree2.RightNode = tree3;

            Node rootNode = new Node(50);
            rootNode.LeftNode = new Node(40);
            rootNode.RightNode = tree2;

            return rootNode;
        }

        public Node RR_NewBalancedTree()
        {
            Node tree3 = new Node(70);
            tree3.LeftNode = new Node(65);
            tree3.RightNode = new Node(75);

            Node tree2 = new Node(50);
            tree2.LeftNode = new Node(40);
            tree2.RightNode = new Node(55);

            Node rootNode = new Node(60);
            rootNode.LeftNode = tree2;
            rootNode.RightNode = tree3;

            return rootNode;
        }


        [Test]
        public void RR_RotatesCorrectly_TreeMatchesPreconstructedBalancedTree()
        {
            var expectedOutput = this.RR_NewBalancedTree();
            
            AVLSelfBalance tr = new AVLSelfBalance();
            var output = tr.RR(RightLeaningTree());
            

            Assert.AreEqual(output.Value, expectedOutput.Value);
            
            Assert.AreEqual(output.RightNode.Value, expectedOutput.RightNode.Value);
            Assert.AreEqual(output.LeftNode.Value, expectedOutput.LeftNode.Value);

            Assert.AreEqual(output.LeftNode.LeftNode.Value, expectedOutput.LeftNode.LeftNode.Value);
            Assert.AreEqual(output.RightNode.RightNode.Value, expectedOutput.RightNode.RightNode.Value);

            Assert.AreEqual(output.RightNode.LeftNode.Value, expectedOutput.RightNode.LeftNode.Value);
            Assert.AreEqual(output.LeftNode.RightNode.Value, expectedOutput.LeftNode.RightNode.Value);//This is failing (Expected: 60 But was: 55)
        }


        [Test]
        public void RR_RotatesCorrectly_NoChildrenPastExpectedLeaves()
        {
            AVLSelfBalance tr = new AVLSelfBalance();
            var output = tr.RR(RightLeaningTree());
            //var output = RR_NewBalancedTree();

            Assert.IsNull(output.LeftNode.LeftNode.LeftNode);
            Assert.IsNull(output.LeftNode.LeftNode.RightNode);

            Assert.IsNull(output.LeftNode.RightNode.LeftNode);
            Assert.IsNull(output.LeftNode.RightNode.RightNode);

            Assert.IsNull(output.RightNode.LeftNode.LeftNode);
            Assert.IsNull(output.RightNode.LeftNode.RightNode);

            Assert.IsNull(output.RightNode.RightNode.LeftNode);
            Assert.IsNull(output.RightNode.RightNode.RightNode);
        }

        public Node LeftLeaningTree()
        {
            Node tree3 = new Node(30);
            tree3.LeftNode = new Node(20);
            tree3.RightNode = new Node(25);

            Node tree2 = new Node(40);
            tree2.LeftNode = tree3;
            tree2.RightNode = new Node(45);

            Node rootNode = new Node(50);
            rootNode.LeftNode = tree2;
            rootNode.RightNode = new Node(60);

            return rootNode;
        }

        public Node LL_NewBalancedTree()
        {
            Node tree3 = new Node(50);
            tree3.LeftNode = new Node(45);
            tree3.RightNode = new Node(60);

            Node tree2 = new Node(30);
            tree2.LeftNode = new Node(20);
            tree2.RightNode = new Node(25);

            Node rootNode = new Node(40);
            rootNode.LeftNode = tree2;
            rootNode.RightNode = tree3;

            return rootNode;
        }

        [Test]
        public void LL_RotatesCorrectly_TreeMatchesPreconstructedBalancedTree()
        {
            var expectedOutput = LL_NewBalancedTree();

            AVLSelfBalance tr = new AVLSelfBalance();
            var output = tr.LL(LeftLeaningTree());

            Assert.AreEqual(output.Value, expectedOutput.Value);

            Assert.AreEqual(output.RightNode.Value, expectedOutput.RightNode.Value);
            Assert.AreEqual(output.LeftNode.Value, expectedOutput.LeftNode.Value);

            Assert.AreEqual(output.LeftNode.LeftNode.Value, expectedOutput.LeftNode.LeftNode.Value);
            Assert.AreEqual(output.RightNode.RightNode.Value, expectedOutput.RightNode.RightNode.Value);

            Assert.AreEqual(output.RightNode.LeftNode.Value, expectedOutput.RightNode.LeftNode.Value);
            Assert.AreEqual(output.LeftNode.RightNode.Value, expectedOutput.LeftNode.RightNode.Value);//This is failing (Expected: 60 But was: 55)
        }


        [Test]
        public void LL_RotatesCorrectly_NoChildrenPastExpectedLeaves()
        {
            AVLSelfBalance tr = new AVLSelfBalance();
            var output = tr.RR(RightLeaningTree());
            //var output = RR_NewBalancedTree();

            Assert.IsNull(output.LeftNode.LeftNode.LeftNode);
            Assert.IsNull(output.LeftNode.LeftNode.RightNode);

            Assert.IsNull(output.LeftNode.RightNode.LeftNode);
            Assert.IsNull(output.LeftNode.RightNode.RightNode);

            Assert.IsNull(output.RightNode.LeftNode.LeftNode);
            Assert.IsNull(output.RightNode.LeftNode.RightNode);

            Assert.IsNull(output.RightNode.RightNode.LeftNode);
            Assert.IsNull(output.RightNode.RightNode.RightNode);
        }



        public Node RR_AVLTestTree()
        {
            Node tree2 = new Node(50);
            tree2.RightNode = new Node(60);

            Node root = new Node(40);
            root.RightNode = tree2;
            root.LeftNode = new Node(30);

            return root;
        }

        public Node RR_AVLBalancedTree()
        {
            
            Node tree2 = new Node(60);
            tree2.RightNode = new Node(70);
            tree2.LeftNode = new Node(50);

            Node root = new Node(40);
            root.RightNode = tree2;
            root.LeftNode = new Node(30);

            return root;
        }

        [Test]
        public void RR_AVLInsertValue_RotatesCorrectly_TreeMatchesPreconstructedBalancedTree()
        {
            var expectedOutput = RR_AVLBalancedTree();

            var avlTree = RR_AVLTestTree();
            AVLSelfBalance tr = new AVLSelfBalance(avlTree);

            tr.InsertValue(70);

            var output = tr.RootNode;

            Assert.AreEqual(output.Value, expectedOutput.Value);

            Assert.AreEqual(output.RightNode.Value, expectedOutput.RightNode.Value);
            Assert.AreEqual(output.LeftNode.Value, expectedOutput.LeftNode.Value);

            Assert.AreEqual(output.RightNode.RightNode.Value, expectedOutput.RightNode.RightNode.Value);
            Assert.AreEqual(output.RightNode.LeftNode.Value, expectedOutput.RightNode.LeftNode.Value);
            
        }
        //TODO: Write the test that checks if the root node get rotated correctly:

        [Test]
        public void InserValue_RootRotationHappens()
        {
            Node testTree = new Node(3);
            testTree.RightNode = new Node(5);

            Node expectedOutput = new Node(5);
            expectedOutput.RightNode = new Node(7);
            expectedOutput.LeftNode = new Node(3);

            AVLSelfBalance tr = new AVLSelfBalance(testTree);
            tr.InsertValue(7);

            var output = tr.RootNode;

            Assert.AreEqual(output.Value, expectedOutput.Value);
            Assert.AreEqual(output.RightNode.Value, expectedOutput.RightNode.Value);
            Assert.AreEqual(output.LeftNode.Value, testTree.LeftNode.Value);
        }
    }
}
