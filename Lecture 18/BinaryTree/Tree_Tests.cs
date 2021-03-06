﻿using System;
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
            var tree = new Tree<int>(0);

            for (int i = 1; i < 5; i++)
            {
                tree.InsertValue(i);
            }

        }

        [Test]
        public void InsertValue_InsertRandom()
        {
            Random rnd = new Random();
            
            var tree = new Tree<int>(0);

            for (int i = 0; i < 20; i++)
            {
                int randomNum = rnd.Next(1, 13);
                tree.InsertValue(randomNum);
            }

        }

        //[Test,Ignore]
        //public void TreeIsValid()
        //{
        //    Random rnd = new Random();

        //    Tree tree = new Tree(0);

        //    for (int i = 0; i < 20; i++)
        //    {
        //        int randomNum = rnd.Next(1, 13);
        //        tree.InsertValue(randomNum);
        //    }

        //    var output = tree.IsTreeValid();

        //    Assert.That(output);
        //}

        //[Test]
        //public void DeleteNode()
        //{
        //    Tree tree = new Tree(30);
        //    int[] nodesToAdd = new int[]{20, 40, 35, 45, 42, 47};

        //    foreach (var node in nodesToAdd)
        //    {
        //        tree.InsertValue(node);
        //    }

        //    tree.DeleteNode(42);
        //    tree.DeleteNode(45);

        //    Console.WriteLine("Stop");

        //}

        [Test]
        public void FlattenTree_IntegrationTest()
        {
            var to = new NaiveTreeBalance<int>();
            var tree = new Tree<int>(30);
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
            var to = new NaiveTreeBalance<int>();

            var newTree = to.MakeTree(oracleList.OrderBy(v => v).ToList());

            var stuff = "";

        }

        [Test]
        public void GetTargetNode_IntegrationTest()
        {
            var lowChild = new Node<int>(30);
            var midChild = new Node<int>(20) {LeftNode = null, RightNode = lowChild};
            var root = new Node<int>() { LeftNode = null, RightNode = midChild };

            Queue<bool> path = new Queue<bool>();
            path.Enqueue(true);
            path.Enqueue(true);

            var output = Utilities.GetTargetNode(path, root);

            Assert.AreEqual(30, output.Value);
        }


    }
}
