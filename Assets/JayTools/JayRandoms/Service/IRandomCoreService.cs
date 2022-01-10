namespace JayTools.JayRandoms.Service
{
    public interface IRandomCoreService
    {
        /// <summary>
        /// Internal method for getting random integer number.
        /// The core of all the other random methods.
        /// Changing this method will modify the behaviour of all integer-based random methods.
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        int RandomRange(int min, int max);

        /// <summary>
        /// Internal method for getting random float number.
        /// The core of all the other random methods.
        /// Changing this method will modify the behaviour of all float-based random methods.
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        float RandomRange(float min, float max);
    }
}