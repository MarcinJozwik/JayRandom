using JayTools.JayRandoms;
using JayTools.JayRandoms.Static;
using UnityEngine;

namespace JayTools.Example
{
    public class PerlinFloating : MonoBehaviour
    {
        // "Bobbing" animation from 1D Perlin noise.

        public float InitialOffset = 0.0f;
    
        // Range over which height varies.
        public float heightScale = 1.0f;

        // Distance covered per second along X axis of Perlin plane.
        public float xScale = 1.0f;
    
        private float baseHeight;

        private void Start()
        {
            baseHeight = transform.position.y;
        }

        void Update()
        {
            double value = JayRandom.PerlinNoise(InitialOffset + Time.time * xScale, 0f, 0f);
            float height = heightScale * (float)value;
            Vector3 pos = transform.position;
            pos.y = baseHeight + height;
            transform.position = pos;
        }
    }
}
