using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeCoinProblems
{
    //http://www.geeksforgeeks.org/decision-trees-fake-coin-puzzle/
    

    //Given 5 coins out of which one coin is lighter. In the worst case, how many minimum number of weighing are required to figure out the odd coin?
    public class Level1
    {
        public int[] GetInitialCoinStacks(int totalCoins, int? fakeCoinPos = null)
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
                    output[i] = FakeCoin();
                }
                else
                {
                    output[i] = RealCoin();
                }
            }

            return output;
        }


        //TODO: Look at index out of the array exception:
        public int FindFakeCoinPosition(int[] coinInput,int begining = 0, int end = -1)
        {
            
            int? extraCoinPos = -1;
            int middle = -1;

            if (end == -1)
                end = coinInput.Count() - 1;

            int currentStackLength = (end - begining) + 1;


            //Success:
            if (currentStackLength == 1)
                return end;

            //Make Stacks:
            if (currentStackLength % 2 != 0)
            {
                extraCoinPos = end;
                end = end - 1;
                currentStackLength = (end - begining) + 1;
                middle = begining + ((currentStackLength / 2) - 1);
            }
            else
                middle = begining + ((currentStackLength / 2) - 1);
            

            var measureResult = RightIsLighter(coinInput, begining, end, middle);

            if (measureResult == null)
            {
                return extraCoinPos.Value;
            }
            else if (!measureResult.Value)
            {
                
                return FindFakeCoinPosition(coinInput, begining, middle);
            }
            else if (measureResult.Value)
            {
                return FindFakeCoinPosition(coinInput, middle + 1, end);
            }    

            return -1;
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

        
        public int RealCoin()
        {
            return 5;
        }

        public int FakeCoin()
        {
            return 3;
        }
    }

    
}
