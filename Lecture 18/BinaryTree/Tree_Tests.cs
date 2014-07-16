using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryTree
{
    using NUnit.Framework;

    [TestFixture]
    public class Tree_Tests
    {
        
        [Test]
        public void InsertValue_IntegrationTest()
        {


            Tree tree = new Tree(0);

            for (int i = 1; i < 5; i++)
            {
                tree.InsertValue(i);
            }

        }

        [Test]
        public void InsertValue_InsertRandom()
        {
            Random rnd = new Random();
            
            Tree tree = new Tree(0);

            for (int i = 0; i < 20; i++)
            {
                int randomNum = rnd.Next(1, 13);
                tree.InsertValue(randomNum);
            }

        }

        [Test]
        public void TreeIsValid()
        {
            Random rnd = new Random();

            Tree tree = new Tree(0);

            for (int i = 0; i < 20; i++)
            {
                int randomNum = rnd.Next(1, 13);
                tree.InsertValue(randomNum);
            }

            var output = tree.IsTreeValid();

            Assert.That(output);
        }

        [Test]
        public void DeleteNode()
        {
            Tree tree = new Tree(30);
            int[] nodesToAdd = new int[]{20, 40, 35, 45, 42, 47};

            foreach (var node in nodesToAdd)
            {
                tree.InsertValue(node);
            }

            tree.DeleteNode(42);
            tree.DeleteNode(45);

            Console.WriteLine("Stop");

        }


        //TODO: <NA> Finish this test for flattening tree's then test simple tree balance:
        [Test]
        public void FlattenTree_IntegrationTest()
        {
            NaiveTreeBalance to = new NaiveTreeBalance();
            Tree tree = new Tree(30);
            int[] nodesToAdd = new int[] { 20, 40, 35, 45, 42, 47 };

            foreach (var node in nodesToAdd)
            {
                tree.InsertValue(node);
            }

            int[] oracleList = new int[] { 20, 30, 40, 35, 45, 42, 47 };
            var oracle = oracleList.OrderBy(v => v).ToList();

            var treeList = to.FlattenTree(tree.RootNode).OrderBy(v => v).ToList();

            Console.WriteLine("Oracle: ");

            foreach (var i in oracle)
            {
                Console.Write(i + " ");
            }

            Console.WriteLine();
            Console.WriteLine("Flattened List: ");
            
            foreach (var i in treeList)
            {
                Console.Write(i + " ");
            }
        }

        [Test]
        public void MakeTree_IntegrationTest()
        {
            List<int> oracleList = new List<int>() { 20, 30, 40, 35, 45, 42, 47 };
            NaiveTreeBalance to = new NaiveTreeBalance();

            var newTree = to.MakeTree(oracleList.OrderBy(v => v).ToList());

            var stuff = "";

        }


    }
}
