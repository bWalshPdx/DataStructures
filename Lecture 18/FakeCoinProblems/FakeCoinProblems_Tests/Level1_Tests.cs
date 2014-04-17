using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FakeCoinProblems;

namespace FakeCoinProblems_Tests
{
    using NUnit.Framework;

    [TestFixture]
    public class Level1_Tests
    {
        Level1 L1 = new Level1();

        [Test]
        public void RightIsLighter_MultipleCoins_ReturnsFalse()
        {
            int[] input = new int[] { L1.FakeCoin(), L1.RealCoin(), L1.RealCoin(), L1.RealCoin(), L1.RealCoin(), L1.RealCoin(), L1.RealCoin(), L1.RealCoin() };
            //int[] rightStack = new int[] { L1.RealCoin(), L1.RealCoin(), L1.RealCoin(), L1.RealCoin() };

            int begining = 0;
            int end = 7;
            int endFirstStack = 3; 

            var output = L1.RightIsLighter(input, begining, end, endFirstStack);

            Assert.IsFalse(output.Value);
        }

        
        [Test]
        public void RightIsLighter_MultipleCoins_ReturnsTrue()
        {
            int[] input = new int[] { L1.RealCoin(), L1.RealCoin(), L1.RealCoin(), L1.RealCoin(), L1.FakeCoin(), L1.RealCoin(), L1.RealCoin(), L1.RealCoin()};

            int begining = 0;
            int end = 7;
            int endFirstStack = 3;

            var output = L1.RightIsLighter(input, begining, end, endFirstStack);

            Assert.IsTrue(output.Value);
        }

        

        [Test]
        public void RightIsLighter_MultipleCoins_ReturnsNull()
        {
            int[] input = new int[] { L1.RealCoin(), L1.RealCoin(), L1.RealCoin(), L1.RealCoin(), L1.RealCoin(), L1.RealCoin(), L1.RealCoin(), L1.RealCoin() };
            
            int begining = 0;
            int end = 7;
            int endFirstStack = 3;

            var output = L1.RightIsLighter(input, begining, end, endFirstStack);

            Assert.IsNull(output);
        }

        

        [Test, Explicit]
        public void getInitialCoinStacks_OneCoinIsLighter()
        {
            Assert.That(L1.GetInitialCoinStacks(6).Where(c => c == 3).Count() == 1);
        }

        
        [Test]
        public void FindFakeCoinPosition_ExtraCoinIsReturned()
        {
            var input = new int[]{ L1.RealCoin(), L1.RealCoin(), L1.RealCoin(), L1.RealCoin(), L1.FakeCoin() };

            int begining = 0;
            int end = 4;
            
            var output = L1.FindFakeCoinPosition(input, begining, end);

            var expectedOutput = 4;

            Assert.AreEqual(expectedOutput, output);
        }


        
        [Test]
        public void FindFakeCoinPosition_FirstPosFakeCoinFound()
        {
            var input = new int[] {L1.FakeCoin(), L1.RealCoin(), L1.RealCoin(), L1.RealCoin(), L1.RealCoin()};
            int begining = 0;
            int end = 4;

            var output = L1.FindFakeCoinPosition(input);
            var expectedOutput = 0;

            Assert.AreEqual(expectedOutput, output);
        }


        
        [Test]
        public void FindFakeCoinPosition_FourthPosFakeCoinFound()
        {
            var input = new int[] { L1.RealCoin(), L1.RealCoin(), L1.RealCoin(), L1.FakeCoin(), L1.RealCoin() };

            var output = L1.FindFakeCoinPosition(input);
            var expectedOutput = 3;

            Assert.AreEqual(expectedOutput, output);
        }

        [Test]
        public void FindFakeCoinPosition_VerifyEveryPositionInStack()
        {
            int stackSize = 5;

            for (int i = 0; i < stackSize; i++)
            {
                var input = L1.GetInitialCoinStacks(stackSize, i);

                var output = L1.FindFakeCoinPosition(input);
                var expectedOutput = i;

                Console.WriteLine("Checking coin in position {0}", i);
                Assert.AreEqual(expectedOutput, output);
            }


            

            

            
        }


    }
}
