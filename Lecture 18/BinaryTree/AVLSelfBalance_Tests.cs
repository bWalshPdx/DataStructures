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
        public Tree<int> TestTree()
        {
            Tree<int> t = new Tree<int>(60);
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
                var zeroDepthTree = new Node<int>(10);

                var oneDepthTree = new Node<int>(50);
                oneDepthTree.LeftNode = new Node<int>(40);
                oneDepthTree.RightNode = new Node<int>(60);

                var twoDepthTree = new Node<int>(70);
                twoDepthTree.LeftNode = new Node<int>(60);
                twoDepthTree.RightNode = new Node<int>(80);
                twoDepthTree.RightNode.RightNode = new Node<int>(90);

                var threeDepthTree = new Node<int>(70);
                threeDepthTree.LeftNode = new Node<int>(60);
                threeDepthTree.RightNode = new Node<int>(80);
                threeDepthTree.RightNode.RightNode = new Node<int>(90);
                threeDepthTree.RightNode.RightNode.LeftNode = new Node<int>(85);
                threeDepthTree.RightNode.RightNode.RightNode = new Node<int>(100);

                yield return new TestCaseData(zeroDepthTree, 0).SetName("Just the root tree");
                yield return new TestCaseData(oneDepthTree, 1).SetName("Tree with depth of one");
                yield return new TestCaseData(twoDepthTree, 2).SetName("Tree with depth of two");
                yield return new TestCaseData(threeDepthTree, 3).SetName("Tree with depth of three");
            }
        }



        [TestCaseSource("TreesAndExpectedDepths")]
        public void GetMaxDepth_GetCorrectDepth(Node<int> testTree, int expectedDepth)
        {
            
            AVLTree<int> trb = new AVLTree<int>();

            var output = trb.GetMaxDepth(testTree);

            Assert.AreEqual(expectedDepth, output);
            
        }

        public IEnumerable<TestCaseData> DifferentTreesAndBalances
        {
            
            get
            {
                var postRotatedTree2 = new Node<int>(60);
                postRotatedTree2.RightNode = new Node<int>(70);
                postRotatedTree2.LeftNode = new Node<int>(50);

                var postRotatedTree = new Node<int>(40);
                postRotatedTree.LeftNode = new Node<int>(30);
                postRotatedTree.RightNode = postRotatedTree2;


                var preRotatedTree2 = new Node<int>(50);
                preRotatedTree2.RightNode = new Node<int>(60);
                preRotatedTree2.RightNode.RightNode = new Node<int>(70);

                var preRotatedTree = new Node<int>(40);
                preRotatedTree.LeftNode = new Node<int>(30);
                preRotatedTree.RightNode = preRotatedTree2;

                var balancedTree = new Node<int>(50);
                balancedTree.RightNode = new Node<int>(60);
                balancedTree.LeftNode = new Node<int>(70);

                var threeRightTree = new Node<int>(50);
                threeRightTree.RightNode = new Node<int>(60);
                threeRightTree.RightNode.RightNode = new Node<int>(70);

                yield return new TestCaseData(preRotatedTree, -2).SetName("Pre Rotate Shifted Tree");
                yield return new TestCaseData(postRotatedTree, -1).SetName("Post Rotate Shifted Tree");
                yield return new TestCaseData(balancedTree, 0).SetName("Balanced Tree");
                yield return new TestCaseData(threeRightTree, -2).SetName("Three Right Tree");
                
                
            }

        }


        [TestCaseSource("DifferentTreesAndBalances")]
        public void GetBalanceValue_GetsCorrectValue(Node<int> tree, int expectedBalance)
        {

            AVLTree<int> trb = new AVLTree<int>();

            var output = trb.GetBalanceValue(tree);

            Assert.AreEqual(expectedBalance, output);
        }

        public Tree<int> LLTestTree()
        {
            var t = new Tree<int>(99);
            t.InsertValue(90);
            t.InsertValue(80);
            
            return t;
        }

        [Test]
        public void LL_RotatesCorrectly()
        {
            Tree<int> t = LLTestTree();

            Node<int> expectedOutput = new Node<int>(90);
            expectedOutput.LeftNode = new Node<int>(80);
            expectedOutput.RightNode= new Node<int>(99);

            AVLTree<int> tr = new AVLTree<int>();

            var output = tr.Rotate(t.RootNode, false);

            Assert.AreEqual(output.Value, expectedOutput.Value);
            Assert.AreEqual(output.LeftNode.Value, expectedOutput.LeftNode.Value);
            Assert.AreEqual(output.RightNode.Value, expectedOutput.RightNode.Value);

        }

        public Tree<int> RRTestTree()
        {
            var t = new Tree<int>(50);
            t.InsertValue(51);
            t.InsertValue(53);

            return t;
        }

        [Test]
        public void RR_RotatesCorrectly_TheMainBranchOnly()
        {
            Tree<int> t = RRTestTree();

            Node<int> correctlyRotatedTree = new Node<int>(51);
            correctlyRotatedTree.LeftNode = new Node<int>(50);
            correctlyRotatedTree.RightNode = new Node<int>(53);

            AVLTree<int> tr = new AVLTree<int>();

            var output = tr.Rotate(t.RootNode, true);

            Assert.AreEqual(output.Value, correctlyRotatedTree.Value);
            Assert.AreEqual(output.RightNode.Value, correctlyRotatedTree.RightNode.Value);
            Assert.AreEqual(output.LeftNode.Value, correctlyRotatedTree.LeftNode.Value);
        }

        public Node<int> RightLeaningTree()
        {
            var tree3 = new Node<int>(70);
            tree3.LeftNode = new Node<int>(65);
            tree3.RightNode = new Node<int>(75);

            var tree2 = new Node<int>(60);
            tree2.LeftNode = new Node<int>(55);
            tree2.RightNode = tree3;

            var rootNode = new Node<int>(50);
            rootNode.LeftNode = new Node<int>(40);
            rootNode.RightNode = tree2;

            return rootNode;
        }

        public Node<int> RR_NewBalancedTree()
        {
            var tree3 = new Node<int>(70);
            tree3.LeftNode = new Node<int>(65);
            tree3.RightNode = new Node<int>(75);

            var tree2 = new Node<int>(50);
            tree2.LeftNode = new Node<int>(40);
            tree2.RightNode = new Node<int>(55);

            var rootNode = new Node<int>(60);
            rootNode.LeftNode = tree2;
            rootNode.RightNode = tree3;

            return rootNode;
        }


        [Test]
        public void RR_RotatesCorrectly_TreeMatchesPreconstructedBalancedTree()
        {
            var expectedOutput = RR_NewBalancedTree();
            
            var tr = new AVLTree<int>();
            var output = tr.Rotate(RightLeaningTree(), true);
            

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
            var tr = new AVLTree<int>();
            var output = tr.Rotate(RightLeaningTree(), true);
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

        public Node<int> LeftLeaningTree()
        {
            var tree3 = new Node<int>(30);
            tree3.LeftNode = new Node<int>(20);
            tree3.RightNode = new Node<int>(25);

            var tree2 = new Node<int>(40);
            tree2.LeftNode = tree3;
            tree2.RightNode = new Node<int>(45);

            var rootNode = new Node<int>(50);
            rootNode.LeftNode = tree2;
            rootNode.RightNode = new Node<int>(60);

            return rootNode;
        }

        public Node<int> LL_NewBalancedTree()
        {
            var tree3 = new Node<int>(50);
            tree3.LeftNode = new Node<int>(45);
            tree3.RightNode = new Node<int>(60);

            var tree2 = new Node<int>(30);
            tree2.LeftNode = new Node<int>(20);
            tree2.RightNode = new Node<int>(25);

            var rootNode = new Node<int>(40);
            rootNode.LeftNode = tree2;
            rootNode.RightNode = tree3;

            return rootNode;
        }

        [Test]
        public void LL_RotatesCorrectly_TreeMatchesPreconstructedBalancedTree()
        {
            var expectedOutput = LL_NewBalancedTree();

            var tr = new AVLTree<int>();
            var output = tr.Rotate(LeftLeaningTree(), false);

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
            var tr = new AVLTree<int>();
            var output = tr.Rotate(RightLeaningTree(), true);
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



        public Node<int> RR_AVLTestTree()
        {
            var tree2 = new Node<int>(50);
            tree2.RightNode = new Node<int>(60);

            var root = new Node<int>(40);
            root.RightNode = tree2;
            root.LeftNode = new Node<int>(30);

            return root;
        }

        public Node<int> RR_AVLBalancedTree()
        {
            
            var tree2 = new Node<int>(60);
            tree2.RightNode = new Node<int>(70);
            tree2.LeftNode = new Node<int>(50);

            var root = new Node<int>(40);
            root.RightNode = tree2;
            root.LeftNode = new Node<int>(30);

            return root;
        }

        [Test]
        public void RR_AVLInsertValue_RotatesCorrectly_TreeMatchesPreconstructedBalancedTree()
        {
            var expectedOutput = RR_AVLBalancedTree();

            var avlTree = RR_AVLTestTree();
            var tr = new AVLTree<int>(avlTree);

            tr.InsertValue(70);

            var output = tr.RootNode;

            Assert.AreEqual(output.Value, expectedOutput.Value);

            Assert.AreEqual(output.RightNode.Value, expectedOutput.RightNode.Value);
            Assert.AreEqual(output.LeftNode.Value, expectedOutput.LeftNode.Value);

            Assert.AreEqual(output.RightNode.RightNode.Value, expectedOutput.RightNode.RightNode.Value);
            Assert.AreEqual(output.RightNode.LeftNode.Value, expectedOutput.RightNode.LeftNode.Value);
            
        }
        

        [Test]
        public void InserValue_RootRotationHappens()
        {
            var testTree = new Node<int>(3);
            testTree.RightNode = new Node<int>(5);

            var expectedOutput = new Node<int>(5);
            expectedOutput.RightNode = new Node<int>(7);
            expectedOutput.LeftNode = new Node<int>(3);

            var tr = new AVLTree<int>(testTree);
            tr.InsertValue(7);

            var output = tr.RootNode;

            Assert.AreEqual(output.Value, expectedOutput.Value);
            Assert.AreEqual(output.RightNode.Value, expectedOutput.RightNode.Value);
            Assert.AreEqual(output.LeftNode.Value, expectedOutput.LeftNode.Value);
        }

        [Test]
        public void GetTargetNode_GetsTheRightNode()
        {
            var testTree = new Node<int>(50);
            testTree.LeftNode = new Node<int>(40);
            testTree.RightNode = new Node<int>(60);

            var output = Utilities.GetTargetNode(new Queue<bool>(new[] { false}), testTree);

            Assert.AreEqual(40, output.Value);
        }
    }
}
