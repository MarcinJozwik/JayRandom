namespace JayTools.JayRandoms.Instance
{
    /// <summary>
    /// Helper class used for storing data during Weight Random calculations.
    /// </summary>
    /// <see cref="WeightRandom{T}"/>
    /// <typeparam name="T"></typeparam>
    public class WeightedItem <T>
    {
        public T Item;
        public float Weight;

        /// <summary>
        /// A constructor of Weighted Item.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="weight"></param>
        public WeightedItem(T item, float weight)
        {
            Item = item;
            Weight = weight;
        }
    }
}