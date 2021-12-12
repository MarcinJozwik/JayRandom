namespace JayTools.JayRandom
{
    public partial class JayRandom
    {
        /// <summary>
        /// Internal method for getting random integer number.
        /// The core of all the other random methods.
        /// Changing this method will modify the behaviour of all integer-based random methods.
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        private static int RandomRange(int min, int max)
        {
            return UnityEngine.Random.Range(min, max);
        }

        /// <summary>
        /// Internal method for getting random float number.
        /// The core of all the other random methods.
        /// Changing this method will modify the behaviour of all float-based random methods.
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        private static float RandomRange(float min, float max)
        {
            return UnityEngine.Random.Range(min, max);
        }
    }
}