﻿using UnityEngine;

namespace JayTools.JayRandom
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
        private Perlin perlin;

        private void Start()
        {
            basePosition = transform.position;
            perlin = new Perlin();
        }

        void Update()
        {
            double value = perlin.perlin(InitialOffset + Time.time * frequency, InitialOffset + Time.time * frequency, InitialOffset + Time.time * frequency);
            float height = amplitude * (float)value;
            transform.position = basePosition + new Vector3(height,height,height);
        }
    }
}