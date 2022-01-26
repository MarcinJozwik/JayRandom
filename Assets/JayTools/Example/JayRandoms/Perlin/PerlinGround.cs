using JayTools.JayRandoms;
using JayTools.JayRandoms.Static;
using UnityEngine;

namespace JayTools.Example
{
    public class PerlinGround : MonoBehaviour
    {
        [SerializeField] 
        private float repeatRate = 3f;
    
        private GameObject parent;
        public GameObject CubePrefab;
        public Vector3 StartPosition;
        public Vector2 Offset;
        public Vector2Int Size;

        public float MinHeight;
        public float MaxHeight;

        private GameObject[,] cubes;
        void Start()
        {
            InstantiateCubes();
            Randomize();
        
            InvokeRepeating(nameof(Randomize), 1f, repeatRate);
        }
    
        private void InstantiateCubes()
        {
            parent = new GameObject("CubesContainer");
            cubes = new GameObject[Size.x,Size.y];

            for (int i = 0; i < Size.x; i++)
            {
                for (int j = 0; j < Size.y; j++)
                {
                    var cube = Instantiate(CubePrefab, parent.transform);
                    cubes[i,j] = cube;
                    cube.transform.position = StartPosition + new Vector3(i * Offset.x, 0f, j * Offset.y);
                }
            }
        }

        public void Randomize()
        {
            int[] numbers = JayRandom.UniqueRandom(0, 7, 2);
            Color leftColor = GetColor(numbers[0]);
            Color rightColor = GetColor(numbers[1]);
        
            float initialOffsetHeight = JayRandom.Random01();
            float initialOffsetColor = JayRandom.Random01();

            for (int i = 0; i < Size.x; i++)
            {
                for (int j = 0; j < Size.y; j++)
                {
                    GameObject cube = cubes[i,j];
                    float sample = JayRandom.PerlinNoise(initialOffsetColor + ((i * 1f)/ Size.x), initialOffsetColor + ((j * 1f)/ Size.y), 0f);
                    cube.GetComponent<Renderer>().material.color = leftColor + sample * (rightColor - leftColor);

                    sample = JayRandom.PerlinNoise(initialOffsetHeight + ((i * 1f)/ Size.x), initialOffsetHeight + ((j * 1f)/ Size.y), 0f);
                    float newHeight = StartPosition.y + MinHeight + (MaxHeight - MinHeight) * sample;
            
                    Vector3 currentPosition = cube.transform.position;
                    cube.transform.position = new Vector3(currentPosition.x, newHeight, currentPosition.z);
                
                    cube.GetComponent<PerlinBounce>().SetBasePosition(cube.transform.position);
                }
            }
        }
    
        // public void Generate()
        //     {
        //         int[] numbers = JayRandom.UniqueRandom(0, 7, 2);
        //         Color leftColor = GetColor(numbers[0]);
        //         Color rightColor = GetColor(numbers[1]);
        //
        //         float frequency = JayRandom.Random(0.3f, 3f);
        //         float amplitude = JayRandom.Random(0.2f, 2f);
        //
        //         float scaleMax = JayRandom.Random(1f, 3f);
        //
        //         float initialOffset = JayRandom.Random01();
        //         int index = 0;
        //     
        //         for (int i = 0; i < Size.x; i++)
        //         {
        //             for (int j = 0; j < Size.y; j++)
        //             {
        //                 for (int k = 0; k < Size.z; k++)
        //                 {
        //                     GameObject cube = cubes[index];
        //                     cube.transform.position = StartPosition + new Vector3(i * Offset.x, j * Offset.y, k * Offset.z);
        //                     
        //                     // float sample = (float)perlin.perlin(initialOffset + i / Size.x,initialOffset + j / Size.y,initialOffset + k / Size.z);
        //                     float sample = Mathf.PerlinNoise(initialOffset + (index * 1f) / (Size.x* Size.y * Size.z), 0);
        //                     Debug.Log(sample);
        //                     // Debug.Log($"{leftColor}, {rightColor}");
        //                     cube.transform.localScale = Vector3.one * JayRandom.Map(sample, 0f, 1f,0.25f, scaleMax);
        //                     cube.GetComponent<Renderer>().material.color = leftColor + sample * (rightColor - leftColor);
        //                     
        //                     var perlinBounce = cube.GetComponent<PerlinBounce>();
        //                     perlinBounce.InitialOffset = index * 1000;
        //                     perlinBounce.frequency = frequency;
        //                     perlinBounce.amplitude = amplitude;
        //                     index++;
        //                 }
        //             }
        //         }
        //     }

        public Color DrawColor()
        {
            return GetColor(JayRandom.Random(0, 8));
        }
        
        public Color GetColor(int index)
        {
            switch (index)
            {
                case 0:
                    return Color.red;
                case 1:
                    return Color.magenta;
                case 2:
                    return Color.cyan;
                case 3:
                    return Color.blue;
                case 4:
                    return Color.green;
                case 5:
                    return Color.yellow;
                case 6:
                    return Color.black;
                case 7:
                    return Color.white;
                default:
                    return Color.white;

            }
        }
    }
}
