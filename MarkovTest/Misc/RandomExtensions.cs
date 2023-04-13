using System;
using System.Collections.Generic;

namespace Markov.MarkovTest.Misc
{
    public static class RandomExtensions
    {
        /// <summary>
        /// Fills HashSet with random numbers from given range
        /// </summary>
        /// <param name="random">Random instance that will generate random numbers </param>
        /// <param name="collection">Collection, that will be filled</param>
        /// <param name="count"> Count of generated numbers</param>
        /// <param name="min">Min generated number (inclusive)</param>
        /// <param name="max">Max generated number (exclusive)</param>
        public static void FillWithRandomIndexes(this Random random, HashSet<int> collection, int count, int min, int max)
        {
            collection.Clear();
            for (var i = 0; i < count; i++)
            {
                int num;
                do
                {
                    num = random.Next(min, max);
                } while (collection.Contains(num));

                collection.Add(num);
            }
        }
    }
}