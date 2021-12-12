using System.Collections.Generic;
using UnityEngine;

namespace JayTools.JayRandom
{
    /// <summary>
    /// Draws an item from the collection and excludes the item from it until all the items are drawn.
    /// When all the items are drawn, they become available for drawing again.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RemovalRandom<T> : IRandomDraw<T>
    {
        private readonly IList<T> collection;
        private readonly bool[] usedIndexes;

        private int index;
        
        /// <summary>
        /// A constructor of RemovalRandom class.
        /// </summary>
        /// <param name="collection">A collection to draw items from.</param>
        public RemovalRandom(IList<T> collection)
        {
            this.collection = collection;
            usedIndexes = new bool[collection.Count];
        }

        /// <summary>
        /// Draws an item from the collection and excludes the item from it until all the items are drawn.
        /// </summary>
        /// <returns></returns>
        public T Draw()
        {
            do
            {
                index = Random.Range(0, usedIndexes.Length);
            } 
            while (usedIndexes[index] == true);

            usedIndexes[index] = true;

            if (AreAllItemsUsed())
            {
                Reset();
            }
            
            return collection[index];
        }
        
        /// <summary>
        /// Resets the collection and makes all the items available for drawing again.
        /// </summary>
        public void Reset()
        {
            for (var i = 0; i < usedIndexes.Length; i++)
            {
                usedIndexes[i] = false;
            }  
        }

        /// <summary>
        /// Checks if all the items were used.
        /// </summary>
        /// <returns>Returns true if all the items were used.</returns>
        private bool AreAllItemsUsed()
        {
            for (var i = 0; i < usedIndexes.Length; i++)
            {
                if (!usedIndexes[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}