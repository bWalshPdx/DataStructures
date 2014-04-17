using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace SelfOrganizingList_Tests
{
    using SelfOrganizingList;

    [TestFixture]
    public class LinkedList_Tests
    {
        MyLinkedList<int> _linkedList = new MyLinkedList<int>();

        [Test]
        public void AddToBack_GetProperCountAndOrder()
        {
            var expectedOutput = new List<int>() { 0, 1, 2 };

            _linkedList.AddToBack(0);
            _linkedList.AddToBack(1);
            _linkedList.AddToBack(2);

            var output = _linkedList.WalkValues().ToList();

            Assert.That(output.SequenceEqual(expectedOutput));
        }

        [Test]
        public void AddToFront_ElementInTheProperPosition()
        {
            var expectedOutput = new List<int>() { 2, 1, 0 };

            _linkedList.AddToFront(0);
            _linkedList.AddToFront(1);
            _linkedList.AddToFront(2);

            var output = _linkedList.WalkValues().ToList();

            Assert.That(output.SequenceEqual(expectedOutput));
        }

        [Test]
        public void RemoveAtValue_ElementInTheProperPosition()
        {
            var list = new List<int>() { 0, 1, 2, 3, 4 };
            var expectedOutput = new List<int>() { 0, 1, 2, 4 };

            MyLinkedList<int> input = new MyLinkedList<int>();

            foreach (var i in list)
                input.AddToBack(i);

            input.RemoveValue(3);

            foreach (var i in expectedOutput)
            {
                Console.WriteLine(i);
            }

            Console.WriteLine("========");

            foreach (var i in input.WalkValues())
            {
                Console.WriteLine(i);
            }
        }


        
        [Test]
        public void FindIndex_ReturnsTheProperElementPosition()
        {
            var list = new List<int>() { 0, 1, 2, 3, 4 };
            
            MyLinkedList<int> input = new MyLinkedList<int>();
            
            foreach (var i in list)
                input.AddToBack(i);

            var output = input.FindIndex(4);

            Assert.AreEqual(4, output);

        }

    }



    [TestFixture]
    public class MoveToFront_Tests
    {
        [Test]
        public void MoveToFront_ValueNotFound()
        {
            var list = new List<int>() { 0, 1, 2, 3, 4 };
            
            MoveToFront<int> mtf = new MoveToFront<int>(list);

            var output = mtf.Find(12);
            
            Assert.IsFalse(output);
        }

        [Test]
        public void MoveToFront_SearchForValueIsInFront()
        {
            var list = new List<int>() { 0, 1, 2, 3, 4 };

            MoveToFront<int> mtf = new MoveToFront<int>(list);

            mtf.Find(4);

            Assert.That(mtf.Walk.First() == 4);

            Assert.That(mtf.Walk.Skip(1).All(v => v != 4));
        }

        



    }
}
