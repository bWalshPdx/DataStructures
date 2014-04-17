using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeCoinProblems
{
    public enum ScaleResult
    {
        LeftHeavy,
        RightHeavy,
        Equal,
    }

    public class JamesLevel
    {
        public int GetDefectiveCoin(int coins, int badCoin, bool isBadCoinHeavy)
        {
            List<int> stackOfCoins = Enumerable.Range(0, coins).ToList();
            int firstChunkEndIndex = (stackOfCoins.Count() / 3) - 1;
            int secondChunkEndIndex = (firstChunkEndIndex * 2);
            int thirdChunkEndIndex = stackOfCoins.Count() - 1;

            if (stackOfCoins.Count() % 3 == 0)
            {


                //Split up the stacks:
                var firstStack = stackOfCoins.Take(stackOfCoins.Count() / 3);
                var secondStack = stackOfCoins.Except(firstStack).Take(stackOfCoins.Count() / 3);
                var thirdStack = stackOfCoins.Except(firstStack).Take(stackOfCoins.Count() / 3)//YOu know like that >

            }
            else if (stackOfCoins.Count() % 3 == 1)
            {
                //THREE WEIGHTINGS stash one
            }
            else if (stackOfCoins.Count() % 3 == 2)
            {
                //THREE WEIGHTINGS stash two
            }
            throw new Exception("WTF");

        }

        public ScaleResult Weigh(int[] leftcoins, int[] rightcoins, int badCoin, bool isBadCoinHeavy)
        {
            if(leftcoins.Length != rightcoins.Length)
                throw new ArgumentException("Left and right must contain equal numbers of coins");

            if (leftcoins.Contains(badCoin)) return isBadCoinHeavy ? ScaleResult.LeftHeavy : ScaleResult.RightHeavy;
            if (rightcoins.Contains(badCoin)) return isBadCoinHeavy ? ScaleResult.RightHeavy : ScaleResult.LeftHeavy;
            return ScaleResult.Equal;
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
