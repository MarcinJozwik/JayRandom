using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace JayTools.JayRandoms.Instance
{
    /// <summary>
    /// An random class for drawing items from a collection based on the item's relative weight.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class WeightRandom <T> : IRandomDraw<T>
    {
        private readonly List<WeightedItem<T>> items;
        private float totalWeight = 0;

        /// <summary>
        /// Constructor of Weight Random
        /// </summary>
        public WeightRandom()
        {
            items = new List<WeightedItem<T>>();
        }

        /// <summary>
        /// Adds an item the drawing collection with a given weight.
        /// </summary>
        /// <param name="item">An item to add.</param>
        /// <param name="weight">A weight of the added item.</param>
        public void AddItem(T item, float weight)
        {
            if (!HasItem(item))
            {
                VerifyWeight(ref weight);
                items.Add(new WeightedItem<T>(item, weight));
                totalWeight += weight;
            }
        }

        /// <summary>
        /// Removes an item from the drawing collection.
        /// </summary>
        /// <param name="item">An item to remove.</param>
        public void RemoveItem(T item)
        {
            for (var i = 0; i < items.Count; i++)
            {
                WeightedItem<T> weightedItem = items[i];
                if (weightedItem.Item.Equals(item))
                {
                    totalWeight -= weightedItem.Weight;
                    items.RemoveAt(i);
                    return;
                }
            }
        }

        /// <summary>
        /// Changes a weight of an item existing in the collection. Throws an exception if the item
        /// is not there.
        /// </summary>
        /// <param name="item">An item to have its weight changed.</param>
        /// <param name="weight">A new weight for the item.</param>
        public void SetItemWeight(T item, float weight)
        {
            WeightedItem<T> existingItem = GetItem(item);
            if (existingItem != null)
            {
                VerifyWeight(ref weight);
                totalWeight -= existingItem.Weight;
                existingItem.Weight = weight;
                totalWeight += weight;
            }
            else
            {
                Debug.LogError("Item not found.");                
            }
        }
        
        /// <summary>
        /// Adds an additional weight to an item existing in the collection. Throws an exception if the item
        /// is not there.
        /// </summary>
        /// <param name="item">An item to have its weight changed.</param>
        /// <param name="weight">An additional weight for the item. Can be negative.</param>
        public void IncrementItemWeight(T item, float weight)
        {
            WeightedItem<T> existingItem = GetItem(item);
            if (existingItem != null)
            {
                IncrementWeight(existingItem, weight);
            }
            else
            {
                Debug.LogError("Item not found.");                
            }
        }

        /// <summary>
        /// Draws an item from the collection based on weights.
        /// </summary>
        /// <returns></returns>
        public T Draw()
        {
            int index = DrawItemIndex();
            return items[index].Item;
        }

        /// <summary>
        /// Draws an item from the collection based on weights. Removes the chosen item from
        /// the collection after the drawing.
        /// </summary>
        /// <returns></returns>
        public T DrawWithRemoval()
        {
            int index = DrawItemIndex();
            T item = items[index].Item;
            RemoveItem(item);
            return item;
        }

        /// <summary>
        /// Draws an item from the collection based on weights. Adjusts the weights of all items
        /// after the drawing.
        /// </summary>
        /// <param name="chosenItemWeightChange">A weight added to the item that has been chosen. Can be negative.</param>
        /// <param name="notChosenItemsWeightChange">A weight added to all the items that haven't been chosen. Can be negative.</param>
        /// <returns></returns>
        public T DrawWithWeightAdjustment(float chosenItemWeightChange, float notChosenItemsWeightChange)
        {
            int index = DrawItemIndex();

            if (chosenItemWeightChange != 0f || notChosenItemsWeightChange != 0f)
            {
                for (var i = 0; i < items.Count; i++)
                {
                    float change = index == i ? chosenItemWeightChange : notChosenItemsWeightChange;
                    IncrementWeight(items[i], change);
                }

                VerifyWeight(ref totalWeight);
            }

            return items[index].Item;
        }

        /// <summary>
        /// Picks an random index of an item from the collection based on weights.
        /// The internal logic of the weight-based drawing system. Throws an exception if the total
        /// weight of items is not positive.
        /// </summary>
        /// <returns></returns>
        private int DrawItemIndex()
        {
            if (totalWeight <= 0)
            {
                Debug.LogError("Trying to get item when the total weight is not positive");  
                return -1;
            }
            
            float random = Static.JayRandom.Random(0, totalWeight);
            
            for (int i = 0; i < items.Count; i++)
            {
                random -= items[i].Weight;

                if (random <= 0)
                {
                    return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// Verifies if the given weight is positive. Changes its value to zero if not.
        /// </summary>
        /// <param name="weight">A weight to verify.</param>
        private void VerifyWeight(ref float weight)
        {
            if (weight < 0)
            {
                Debug.LogError("Given weight is negative. Changing to 0");
                weight = 0;
            }
        }

        /// <summary>
        /// Checks if the collection has a specific item.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private bool HasItem(T item)
        {
            int count = items.Count;
            
            for (var i = 0; i < count; i++)
            {
                WeightedItem<T> weightedItem = items[i];
                if (weightedItem.Item.Equals(item))
                {
                    return true;
                }
            }

            return false;
        }
        
        /// <summary>
        /// Gets an item with its weight from the collection.
        /// </summary>
        /// <param name="item">An item to be searched for in the collection.</param>
        /// <returns></returns>
        public WeightedItem<T> GetItem(T item)
        {
            int count = items.Count;
            
            for (var i = 0; i < count; i++)
            {
                WeightedItem<T> weightedItem = items[i];
                if (weightedItem.Item.Equals(item))
                {
                    return weightedItem;
                }
            }

            return null;
        }

        /// <summary>
        /// Adds weight to the given item from the collection.
        /// </summary>
        /// <param name="item">An item to have its weight changed.</param>
        /// <param name="relativeWeightChange">An additional weight for the item. Can be negative.</param>
        private void IncrementWeight(WeightedItem<T> item, float relativeWeightChange)
        {
            totalWeight -= item.Weight;
            item.Weight += relativeWeightChange;
            VerifyWeight(ref item.Weight);
            totalWeight += item.Weight;
        }

        /// <summary>
        /// Prints information about items and their weights to the console. For debug purposes.
        /// </summary>
        public void Print()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append($"Total Weight: {totalWeight}\n");

            builder.Append($"Items:\n");
            
            for (var i = 0; i < items.Count; i++)
            {
                var item = items[i];
                builder.Append($"{i}. {item.Weight} {item.Item}\n");
            }
            
            Debug.Log(builder.ToString());
        }
    }
}