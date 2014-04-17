using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeCoinProblems
{
    /*We are given 5 coins, a group of 4 coins out of one is defective (we DONT KNOW whether it is heavier or lighter), 
     * and one coin is genuine. How many weighing are required in worst case to figure out the odd coin whether it is 
     * heavier or lighter?
     */
    
    public class Level3
    {
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

                var lastStack = new int[] { coinInput[goodCoinPos], coinInput[begining] };

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
