using System.Collections.Generic;
using System.Linq;

namespace JayTools.JayRandoms.Service
{
    public static class JayRandomExtensions
    {
        /// <summary>
        /// Returns a random item from the collection.
        /// </summary>
        /// <param name="collection"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetRandom<T>(this List<T> collection)
        {
            int random = Static.JayRandom.Random(0, collection.Count);
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
            int random = Static.JayRandom.Random(0, collection.Count);
            return collection.ElementAt(random);
        }
    }
}