namespace JayTools.JayRandoms.Service
{
    public class UnityRandomCoreService : IRandomCoreService
    {
        public int RandomRange(int min, int max)
        {
            return UnityEngine.Random.Range(min, max);
        }

        public float RandomRange(float min, float max)
        {
            return UnityEngine.Random.Range(min, max);
        }
    }
}