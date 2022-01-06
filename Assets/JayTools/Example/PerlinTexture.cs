﻿using UnityEngine;
using UnityEngine.UI;

namespace JayTools.JayRandom
{
    public class PerlinTexture : MonoBehaviour
    {
        // Width and height of the texture in pixels.
        public int pixWidth;
        public int pixHeight;

        // The origin of the sampled area in the plane.
        public float xOrg;
        public float yOrg;

        // The number of cycles of the basic noise pattern that are repeated
        // over the width and height of the texture.
        public float scale = 1.0F;

        private Texture2D noiseTex;
        private Color[] pix;
        
        public bool mode2D = true;

        void Start()
        {
            // Set up the texture and a Color array to hold pixels during processing.
            noiseTex = new Texture2D(pixWidth, pixHeight);
            pix = new Color[noiseTex.width * noiseTex.height];
            
            if (mode2D)
            {
                RawImage rawImage = GetComponent<RawImage>();
                rawImage.texture = noiseTex;
            }
            else
            {
                Renderer rend = GetComponent<Renderer>();
                rend.material.mainTexture = noiseTex;
            }
        }

        void CalcNoise()
        {
            // For each pixel in the texture...
            float y = 0.0F;

            while (y < noiseTex.height)
            {
                float x = 0.0F;
                while (x < noiseTex.width)
                {
                    float xCoord = xOrg + (x + Time.time * 50f) / noiseTex.width * scale;// * Mathf.Sin(Time.time);
                    float yCoord = yOrg + (y + Time.time * 50f) / noiseTex.height * scale;// * Mathf.Sin(Time.time);
                    float sample = Mathf.PerlinNoise(xCoord, yCoord);
                    pix[(int)y * noiseTex.width + (int)x] = new Color(sample, sample, sample);
                    x++;
                }
                y++;
            }

            // Copy the pixel data to the texture and load it into the GPU.
            noiseTex.SetPixels(pix);
            noiseTex.Apply();
        }

        void Update()
        {
            CalcNoise();
        }
    }
}
