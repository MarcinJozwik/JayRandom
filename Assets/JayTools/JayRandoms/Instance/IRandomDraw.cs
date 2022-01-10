namespace JayTools.JayRandoms.Instance
{
    /// <summary>
    /// An interface for all the instance-based random classes.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRandomDraw <T>
    {
        T Draw();
    }
}