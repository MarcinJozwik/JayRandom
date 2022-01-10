using System.Collections;
using JayTools.JayRandoms.Service;

namespace JayTools.JayRandoms.Static
{
    /// <summary>
    /// Provides static access to Jay Random Service Class
    /// </summary>
    public static class JayRandom
    {
        #region Variables

        private static IRandomService randomService;

        private static IRandomService RandomService
        {
            get => randomService ?? (randomService = new JayRandomService(new UnityRandomCoreService()));
        }

        #endregion
        
        #region Public Methods

        /// <summary>
        /// Returns a random float number in the range [min,max], inclusive
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static float Random(float min, float max)
        {
            return RandomService.Random(min, max);
        }

        /// <summary>
        /// Returns a random integer number in the range [min,max], exclusive.
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static int Random(int min, int max)
        {
            return RandomService.Random(min, max);
        }

        /// <summary>
        /// Returns a random item from a list of parameters.
        /// </summary>
        /// <param name="array"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetRandom<T>(params T[] array)
        {
            return RandomService.GetRandom(array);
        }

        /// <summary>
        /// Returns a random number in the range [0,1], inclusive.
        /// </summary>
        /// <returns></returns>
        public static float Random01()
        {
            return RandomService.Random01();
        }

        /// <summary>
        /// Maps a given value from a current range to a different one.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="currentMin"></param>
        /// <param name="currentMax"></param>
        /// <param name="targetMin"></param>
        /// <param name="targetMax"></param>
        /// <returns></returns>
        public static float Map(float value, float currentMin, float currentMax, float targetMin, float targetMax)
        {
            return RandomService.Map(value, currentMin, currentMax, targetMin, targetMax);
        }

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
            return RandomService.NextNormal(mean, sigma);
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
            return RandomService.NextTriangular(a, b, c);
        }

        /// <summary>
        /// Equally likely to return true or false./>.
        /// </summary>
        /// <returns></returns>
        public static bool NextBoolean()
        {
            return RandomService.NextBoolean();
        }

        /// <summary>
        /// Shuffles a list in O(n) time by using the Fisher-Yates/Knuth algorithm.
        /// </summary>
        /// <param name = "list"></param>
        public static void Shuffle(IList list)
        {
            RandomService.Shuffle(list);
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
            return RandomService.UniqueRandom(min, max, amount);
        }
        
        #endregion
    }
}