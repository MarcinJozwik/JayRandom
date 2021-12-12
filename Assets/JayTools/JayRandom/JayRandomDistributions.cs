using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JayTools.JayRandom
{
    // Distributions based on https://bitbucket.org/Superbest/superbest-random/src/master/Superbest%20random/RandomExtensions.cs
    public static partial class JayRandom
    {
        /// <summary>
        /// Generates values from a normal distribution.
        /// About 68% of values drawn from a normal distribution are within one standard deviation (sigma) away from the mean;
        /// About 95% of the values lie within two standard deviations;
        /// About 99.7% are within three standard deviations.
        /// The 68-95-99.7 (empirical) rule, or the 3-sigma rule.
        /// </summary>
        /// <param name="mean">Center of the distribution</param>
        /// <param name="sigma">Standard deviation (68-95-99.7)</param>
        /// <returns></returns>
        public static float NextNormal(float mean = 0, float sigma = 1)
        {
            float u1 = Random01();
            float u2 = Random01();
            double randStdNormal = Math.Sqrt(-2.0f * Math.Log(u1)) *
                                     Math.Sin(2.0f * Math.PI * u2); //random normal(0,1)

            double randNormal = mean + sigma * randStdNormal; //random normal(mean,stdDev^2)

            return (float) randNormal;
        }

        /// <summary>
        /// Generates values from a triangular distribution.
        /// </summary>
        /// <remarks>
        /// See http://en.wikipedia.org/wiki/Triangular_distribution for a description of the triangular probability distribution and the algorithm for generating one.
        /// </remarks>
        /// <param name = "a">Minimum</param>
        /// <param name = "b">Maximum</param>
        /// <param name = "c">Mode (most frequent value)</param>
        /// <returns></returns>
        public static float NextTriangular(float a, float b, float c)
        {
            float u = Random01();

            return (float) (u < (c - a) / (b - a)
                ? a + Math.Sqrt(u * (b - a) * (c - a))
                : b - Math.Sqrt((1 - u) * (b - a) * (b - c)));
        }

        /// <summary>
        /// Equally likely to return true or false./>.
        /// </summary>
        /// <returns></returns>
        public static bool NextBoolean()
        {
            return Random01() > 0.5f;
        }

        /// <summary>
        /// Shuffles a list in O(n) time by using the Fisher-Yates/Knuth algorithm.
        /// </summary>
        /// <param name="r"></param>
        /// <param name = "list"></param>
        public static void Shuffle(IList list)
        {
            for (var i = 0; i < list.Count; i++)
            {
                var j = RandomRange(0, i + 1);

                var temp = list[j];
                list[j] = list[i];
                list[i] = temp;
            }
        }

        /// <summary>
        /// Returns [amount] unique random numbers in the range [min, max], inclusive. 
        /// This is equivalent to getting the first n numbers of some random permutation of the sequential numbers from 1 to max. 
        /// Runs in O(amount^2) time.
        /// </summary>
        /// <param name="min">Minimum number possible</param>
        /// <param name="max">Maximum number possible.</param>
        /// <param name="amount">How many numbers to return.</param>
        /// <returns></returns>
        public static int[] UniqueRandom(int min, int max, int amount)
        {
            int maxAmount = max - min + 1;
            if ( maxAmount < amount)
            {
                Debug.LogWarning($"Unique Random: Not enough unique numbers. Changing amount to max possible value (from: {amount} to {maxAmount})");
                amount = maxAmount;
            }
            
            var result = new List<int>();
            var sorted = new SortedSet<int>();

            for (int i = 0; i < amount; i++)
            {
                int randomValue = RandomRange(min, max + 1 - i);

                foreach (int item in sorted)
                {
                    if (randomValue >= item)
                    {
                        randomValue++;
                    }
                }

                result.Add(randomValue);
                sorted.Add(randomValue);
            }

            return result.ToArray();
        }
    }
}