using JayTools.JayRandoms;
using UnityEngine;

namespace JayTools.Example
{
    public class PerlinNoise : MonoBehaviour
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
            var perlin = new Perlin();
            double value = perlin.GetPerlin(InitialOffset + Time.time * xScale, 0f, 0f);
            // float height = heightScale * Mathf.PerlinNoise(InitialOffset + Time.time * xScale, InitialOffset + Time.time * xScale);
            float height = heightScale * (float)value;
            Vector3 pos = transform.position;
            pos.y = baseHeight + height;
            transform.position = pos;
        }
    }
}
