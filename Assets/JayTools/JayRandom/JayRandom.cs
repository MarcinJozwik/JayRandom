using System.Collections.Generic;
using System.Linq;

namespace JayTools.JayRandom
{
    /// <summary>
    /// The main Random class
    /// Contains all methods for generating random values that don't require object instances.
    /// </summary>
    public static partial class JayRandom
    {
        // /// <summary>
        // /// Returns a random float number based on a given ease method.
        // /// </summary>
        // /// <param name="min"></param>
        // /// <param name="max"></param>
        // /// <param name="ease"></param>
        // /// <returns></returns>
        // public static float Random(float min, float max, Ease ease)
        // {
        //     float value = ease.Get(Random01());
        //     return min + (value * (max - min));
        // }

       /// <summary>
       /// Returns a random float number in the range [min,max], inclusive
       /// </summary>
       /// <param name="min"></param>
       /// <param name="max"></param>
       /// <returns></returns>
        public static float Random(float min, float max)
        {
            return min + (Random01() * (max - min));
        }

        /// <summary>
        /// Returns a random integer number in the range [min,max], exclusive.
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static int Random(int min, int max)
        {
            return RandomRange(min, max);
        }

        /// <summary>
        /// Returns a random item from a list of parameters.
        /// </summary>
        /// <param name="array"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetRandom<T>(params T[] array)
        {
            int random = RandomRange(0, array.Length);
            return array[random];
        }

        /// <summary>
        /// Returns a random item from the collection.
        /// </summary>
        /// <param name="collection"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetRandom<T>(this List<T> collection)
        {
            int random = RandomRange(0, collection.Count);
            return collection[random];
        }

        /// <summary>
        /// Returns a random item from the collection.
        /// </summary>
        /// <param name="collection"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetRandom<T>(this ICollection<T> collection)
        {
            int random = RandomRange(0, collection.Count);
            return collection.ElementAt(random);
        }

        /// <summary>
        /// Returns a random number in the range [0,1], inclusive.
        /// </summary>
        /// <returns></returns>
        public static float Random01()
        {
            return RandomRange(0f, 1f);
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
            float ratio = (value - currentMin) / (currentMax - currentMin);
            return targetMin + (ratio * (targetMax - targetMin));
        }
    }
}