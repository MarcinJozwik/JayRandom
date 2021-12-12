using System;
using UnityEngine.Assertions;

namespace JayTools.JayRandom
{
    /// <summary>
    /// A random class used for multiple item drawing from the same collection.
    /// Ensures that a given item will not appear more than [maxRepeats] in a row.  
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RepeatRandom<T> : IRandomDraw<T>
    {
        private readonly T[] objectsToDrawFrom;
        private readonly int maxRepeats;
        
        private T lastObject;
        private T drawnObject;
        private int currentRepeats;
        
        /// <summary>
        /// A constructor of RepeatRandom class.
        /// Requires an instance because of keeping track of current repeat amount. 
        /// </summary>
        /// <param name="maxRepeats">Maximum number of drawings of the same value in a row.</param>
        /// <param name="array">Items to draw from.</param>
        public RepeatRandom(int maxRepeats, params T[] array)
        {
            this.maxRepeats = maxRepeats;
            this.objectsToDrawFrom = array;
            Assert.IsTrue(objectsToDrawFrom.Length > 1, "Size of the collection must be greater than zero!");
            Assert.IsTrue(maxRepeats > 0, "Max Repeats must be greater than zero!");
        }

        /// <summary>
        /// Draws an item from the collection. Counts the max repeats value and ensures that the item
        /// will not be drawn if the repeat limit is reached.
        /// </summary>
        /// <example>
        /// Example usage
        /// <code>
        /// var item = Draw();
        /// </code>
        /// </example>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public T Draw()
        {
            if (maxRepeats <= 0)
            {
                throw new Exception("Max Repeats must be greater than zero!");
            }

            if (objectsToDrawFrom.Length <= 1)
            {
                throw new Exception("Size of the collection must be greater than zero!");
            }
            
            do
            {
                drawnObject = objectsToDrawFrom.GetRandom();
                currentRepeats = drawnObject.Equals(lastObject) ? currentRepeats + 1 : 0;
            } 
            while (currentRepeats >= maxRepeats);

            lastObject = drawnObject;
            return drawnObject;
        }
    }
}