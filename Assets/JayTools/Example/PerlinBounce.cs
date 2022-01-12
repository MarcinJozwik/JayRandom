using JayTools.JayRandoms;
using JayTools.JayRandoms.Static;
using UnityEngine;

namespace JayTools.Example
{
    public class PerlinBounce : MonoBehaviour
    {
        // "Bobbing" animation from 1D Perlin noise.

        public float InitialOffset = 0.0f;
    
        // Range over which height varies.
        public float amplitude = 1.0f;

        // Distance covered per second along X axis of Perlin plane.
        public float frequency = 1.0f;
    
        private Vector3 basePosition;

        public bool RandomInitialOffset = false;

        private void Start()
        {
            basePosition = transform.position;

            if (RandomInitialOffset)
            {
                InitialOffset = JayRandom.Random01();
            }
        }

        void Update()
        {
            double value = JayRandom.PerlinNoise(InitialOffset + Time.time * frequency, InitialOffset + Time.time * frequency, InitialOffset + Time.time * frequency);
            float height = amplitude * (float)value;
            transform.position = basePosition + new Vector3(height,height,height);
        }

        public void SetBasePosition(Vector3 position)
        {
            basePosition = position;
        }
    }
}
