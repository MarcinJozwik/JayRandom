using System;
using UnityEngine;

namespace JayTools.JayRandom
{
    public class GeneratePerlinCubes : MonoBehaviour
    {
        [SerializeField] 
        private float repeatRate = 3f;
        
        private GameObject parent;
        public GameObject CubePrefab;
        public GameObject CenterCubePrefab;
        public Vector3 StartPosition;
        public Vector3 Offset;
        public Vector3Int Size;

        private GameObject[] cubes;
        private GameObject centralCube;
        private Perlin perlin;
    
        void Start()
        {
            perlin = new Perlin();

            InstantiateCubes();
        
            Generate();
        
            InvokeRepeating(nameof(Generate), 1f, repeatRate);
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // Generate();
                DrawSample();
            }
        }

        private void DrawSample()
        {
            int initialOffset = JayRandom.Random(0, 10000);
            int index = 0;
            
            for (int i = 0; i < Size.x; i++)
            {
                for (int j = 0; j < Size.y; j++)
                {
                    for (int k = 0; k < Size.z; k++)
                    {
                        float sample = Mathf.PerlinNoise(index + Time.time, 0);
                        Debug.Log(sample);
                        index++;
                    }
                }
            }
        }

        private void InstantiateCubes()
        {
            parent = new GameObject("CubesContainer");
            int length = Size.x * Size.y * Size.z;
            cubes = new GameObject[length];
            centralCube = Instantiate(CenterCubePrefab, parent.transform);

            for (int i = 0; i < length; i++)
            {
                cubes[i] = Instantiate(CubePrefab, parent.transform);
            }
        }

        public void Generate()
        {
            int[] numbers = JayRandom.UniqueRandom(0, 7, 2);
            Color leftColor = GetColor(numbers[0]);
            Color rightColor = GetColor(numbers[1]);

            float frequency = JayRandom.Random(0.3f, 3f);
            float amplitude = JayRandom.Random(0.2f, 2f);

            float scaleMax = JayRandom.Random(1f, 3f);

            float initialOffset = JayRandom.Random01();
            int index = 0;
        
            centralCube.transform.position = StartPosition + new Vector3((Size.x / 2f) * Offset.x, (Size.y / 2f) * Offset.y, (Size.z / 2f) * Offset.z);
        
            for (int i = 0; i < Size.x; i++)
            {
                for (int j = 0; j < Size.y; j++)
                {
                    for (int k = 0; k < Size.z; k++)
                    {
                        GameObject cube = cubes[index];
                        cube.transform.position = StartPosition + new Vector3(i * Offset.x, j * Offset.y, k * Offset.z);
                        
                        // float sample = (float)perlin.perlin(initialOffset + i / Size.x,initialOffset + j / Size.y,initialOffset + k / Size.z);
                        float sample = Mathf.PerlinNoise(initialOffset + (index * 1f) / (Size.x* Size.y * Size.z), 0);
                        Debug.Log(sample);
                        // Debug.Log($"{leftColor}, {rightColor}");
                        cube.transform.localScale = Vector3.one * JayRandom.Map(sample, 0f, 1f,0.25f, scaleMax);
                        cube.GetComponent<Renderer>().material.color = leftColor + sample * (rightColor - leftColor);
                        
                        var perlinBounce = cube.GetComponent<PerlinBounce>();
                        perlinBounce.InitialOffset = index * 1000;
                        perlinBounce.frequency = frequency;
                        perlinBounce.amplitude = amplitude;
                        index++;
                    }
                }
            }
        }

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
