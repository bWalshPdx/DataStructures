using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeCoinProblems
{

    /*Problem 2: (Difficult)

     * We are given 4 coins, out of which only one coin may be defective. 
     * We don’t know, whether all coins are genuine or any defective one is present. 
     * How many number of weighing are required in worst case to figure out the odd coin, if present? We also need to tell whether it is heavier or lighter.
    */

    public class Level2
    {
        public int[] GetInitialCoinStacks(int totalCoins, bool fakeIsLighter, int? fakeCoinPos = null)
        {
            var output = new int[totalCoins];

            if (fakeCoinPos == null)
            {
                Random r = new Random(totalCoins);
                fakeCoinPos = r.Next(totalCoins);
            }

            for (int i = 0; i < totalCoins; i++)
            {
                if (i == fakeCoinPos.Value)
                {
                    if (fakeIsLighter)
                    {
                        output[i] = FakeCoin(1);
                    }
                    else
                    {
                        output[i] = FakeCoin(3);
                    }
                }
                else
                {
                    output[i] = RealCoin();
                }
            }

            return output;
        }

        
        public int? FindFakeCoinPosition(int[] coinInput, int begining = 0, int end = -1)
        {
            int middle = -1;

            if (end == -1)
                end = coinInput.Count() - 1;

            int currentStackLength = (end - begining) + 1;

            //Success:
            if (currentStackLength == 1)
            {
                int goodCoinPos = -1;

                if (begining < coinInput.Count() / 2)
                {
                    goodCoinPos = coinInput.Count() - 1;
                }
                else
                {
                    goodCoinPos = 0;
                }
                
                var lastStack = new int[] { coinInput[goodCoinPos], coinInput[begining]};

                if (RightIsLighter(lastStack, 0, 1, 0) != null)
                {
                    return begining;
                }
                else
                {
                    return null;
                }
                
            }
                

            //Make Stacks:
            middle = begining + ((currentStackLength / 2) - 1);

            
            bool? goRight = RightIsLighter(coinInput, begining, end, middle);

            if (goRight == null)
                return null;
            
            int? outcome = -1;

            for (int i = 0; i < 2; i++)
            {
                if (outcome == null)
                    goRight = !goRight;

                if (!goRight.Value)
                {
                    outcome = FindFakeCoinPosition(coinInput, begining, middle);
                }
                else if (goRight.Value)
                {
                    outcome = FindFakeCoinPosition(coinInput, middle + 1, end);
                }
            }

            return outcome;
        }

        public int RealCoin()
        {
            return 5;
        }

        Random seed = new Random();

        public int FakeCoin(int? stub = null)
        {
            int randomValue = -1;

            if (stub == null)
            {
                randomValue = seed.Next(1,4);
            }
            else
            {
                randomValue = stub.Value;
            }

            switch (randomValue)
            {
                case 1:
                    return 4;
                case 2:
                    return 5;
                case 3:
                    return 6;
                default:
                    throw new Exception("Error in Fake Coin Generator");
            }   
        }

        public bool? RightIsLighter(int[] input, int beg, int end, int mid)
        {
            List<int> leftArray = new List<int>();
            List<int> rightArray = new List<int>();

            //Sort coins to piles:
            for (int i = beg; i <= mid; i++)
            {
                leftArray.Add(input[i]);
            }

            for (int i = mid + 1; i <= end; i++)
            {
                rightArray.Add(input[i]);
            }

            if (rightArray.Sum() == leftArray.Sum())
            {
                return null;
            }
            else
            {
                return leftArray.Sum() > rightArray.Sum();
            }

        }
    }
}
