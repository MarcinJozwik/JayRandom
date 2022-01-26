using System.Text;
using JayTools.JayRandoms.Instance;
using UnityEngine;

namespace JayTools.Example
{
    public class RepeatRandomExample : MonoBehaviour
    {
       public int HowMany = 25;

       private void Start()
       {
           StringBuilder builder = new StringBuilder();
           RepeatRandom<int> random = new RepeatRandom<int>(3, 0,1);
           for (int i = 0; i < HowMany; i++)
           {
               builder.Append(random.Draw());
           }
           Debug.Log(builder.ToString());

           builder.Clear();
           float[] floatArray = new[] {4f, 5f, 6f, 10.5f};
           RepeatRandom<float> collectionRandom = new RepeatRandom<float>(2, floatArray);
           for (int i = 0; i < HowMany; i++)
           {
               builder.Append(collectionRandom.Draw());
           }
           Debug.Log(builder.ToString());
       }
    }
}