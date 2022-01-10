using JayTools.JayRandoms.Instance;
using UnityEngine;

namespace JayTools.Example
{
    public class RandomTest : MonoBehaviour
    {
       public int HowMany = 100;

       private void Start()
       {
           RepeatRandom<int> random = new RepeatRandom<int>(3, 0,1);
           for (int i = 0; i < HowMany; i++)
           {
               Debug.Log(random.Draw());
           }

           float[] superArray = new[] {4f, 5f, 6};
           RepeatRandom<float> collectionRandom = new RepeatRandom<float>(5, superArray);
       }
    }
}