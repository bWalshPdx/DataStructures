using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FakeCoinProblems;
using NUnit.Framework;

namespace FakeCoinProblems_Tests
{
    

    [TestFixture]
    public class Level2_Tests
    {
        Level2 L2 = new Level2();

        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        public void FakeCoin_GetBothValues(int weightCheck)
        {
            List<int> fakeCoinCollection = new List<int>();

            for (int i = 0; i < 1000; i++)
            {
                fakeCoinCollection.Add(L2.FakeCoin());
            }

            Assert.IsTrue(fakeCoinCollection.Contains(weightCheck));
        }

        [Test]
        public void FindFakeCoinPosition_AllRealCoinsReturnsNull()
        {
            var input = new int[] {L2.RealCoin(), L2.RealCoin(), L2.RealCoin(), L2.RealCoin()};

            int begining = 0;
            int end = 3;

            var output = L2.FindFakeCoinPosition(input, begining, end);

            Assert.IsNull(output);
        }

        [Test]
        public void FindFakeCoinPosition_FindsFirstFakeCoin()
        {
            var input = new int[] { L2.FakeCoin(1), L2.RealCoin(), L2.RealCoin(), L2.RealCoin() };

            int begining = 0;
            int end = 3;

            var expectedOutput = 0;
            var output = L2.FindFakeCoinPosition(input, begining, end);

            Assert.AreEqual(expectedOutput, output);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void FindFakeCoinPosition_FindsHeavierCoinInAllPositions2(int fakeCoinPos)
        {
            int stackSize = 4;

            var input = L2.GetInitialCoinStacks(stackSize, false, fakeCoinPos);

            var output = L2.FindFakeCoinPosition(input);
                
            Assert.AreEqual(fakeCoinPos, output);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void FindFakeCoinPosition_FindsLighterCoinInAllPositions2(int fakeCoinPos)
        {
            int stackSize = 4;

            var input = L2.GetInitialCoinStacks(stackSize, true, fakeCoinPos);

            var output = L2.FindFakeCoinPosition(input);

            Assert.AreEqual(fakeCoinPos, output);
        }


        [Test, Explicit]
        public void FindFakeCoinPosition_FindsHeavierCoinInAllPositions()
        {
            int stackSize = 4;

            for (int i = 0; i < stackSize; i++)
            {
                var input = L2.GetInitialCoinStacks(stackSize, false, i);

                var output = L2.FindFakeCoinPosition(input);
                var expectedOutput = i;

                Console.WriteLine("Checking coin in position {0}", i);
                Assert.AreEqual(expectedOutput, output);
            }
        }

        [Test, Explicit]
        public void FindFakeCoinPosition_FindsLigherCoinInAllPositions()
        {
            int stackSize = 4;

            for (int i = 0; i < stackSize; i++)
            {
                var input = L2.GetInitialCoinStacks(stackSize, true, i);

                var output = L2.FindFakeCoinPosition(input);
                var expectedOutput = i;

                Console.WriteLine("Checking coin in position {0}", i);
                Assert.AreEqual(expectedOutput, output);
            }
        }
    }
}
