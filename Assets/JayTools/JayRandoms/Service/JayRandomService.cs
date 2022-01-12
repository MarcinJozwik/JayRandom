using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JayTools.JayRandoms.Service
{
    /// <summary>
    /// The main Random class
    /// Contains all methods for generating random values that don't require object instances.
    /// Distributions based on https://bitbucket.org/Superbest/superbest-random/src/master/Superbest%20random/RandomExtensions.cs
    /// </summary>
    public class JayRandomService : IRandomService
    {
        private readonly IRandomCoreService randomCoreService;
        private readonly PerlinNoise perlinNoise;

        public JayRandomService(IRandomCoreService randomCoreService)
        {
            this.randomCoreService = randomCoreService;
            perlinNoise = new PerlinNoise();
        }
        
        public float Random(float min, float max)
        {
            return min + (Random01() * (max - min));
        }

        public int Random(int min, int max)
        {
            return randomCoreService.RandomRange(min, max);
        }

        public T GetRandom<T>(params T[] array)
        {
            int random = randomCoreService.RandomRange(0, array.Length);
            return array[random];
        }

        public float Random01()
        {
            return randomCoreService.RandomRange(0f, 1f);
        }

        public float Map(float value, float currentMin, float currentMax, float targetMin, float targetMax)
        {
            float ratio = (value - currentMin) / (currentMax - currentMin);
            return targetMin + (ratio * (targetMax - targetMin));
        }

        public float NextNormal(float mean = 0, float sigma = 1)
        {
            float u1 = Random01();
            float u2 = Random01();
            double randStdNormal = Math.Sqrt(-2.0f * Math.Log(u1)) *
                                   Math.Sin(2.0f * Math.PI * u2); //random normal(0,1)

            double randNormal = mean + sigma * randStdNormal; //random normal(mean,stdDev^2)

            return (float) randNormal;
        }

        public float NextTriangular(float a, float b, float c)
        {
            float u = Random01();

            return (float) (u < (c - a) / (b - a)
                ? a + Math.Sqrt(u * (b - a) * (c - a))
                : b - Math.Sqrt((1 - u) * (b - a) * (b - c)));
        }

        public bool NextBoolean()
        {
            return Random01() > 0.5f;
        }

        public void Shuffle(IList list)
        {
            for (var i = 0; i < list.Count; i++)
            {
                var j = randomCoreService.RandomRange(0, i + 1);

                var temp = list[j];
                list[j] = list[i];
                list[i] = temp;
            }
        }

        public int[] UniqueRandom(int min, int max, int amount)
        {
            int maxAmount = max - min + 1;
            if ( maxAmount < amount)
            {
                Debug.LogWarning($"Unique Random: Not enough unique numbers. Changing amount to max possible value (from: {amount} to {maxAmount})");
                amount = maxAmount;
            }
            
            var result = new List<int>();
            var sorted = new SortedSet<int>();

            for (int i = 0; i < amount; i++)
            {
                int randomValue = randomCoreService.RandomRange(min, max + 1 - i);

                foreach (int item in sorted)
                {
                    if (randomValue >= item)
                    {
                        randomValue++;
                    }
                }

                result.Add(randomValue);
                sorted.Add(randomValue);
            }

            return result.ToArray();
        }

        public float PerlinNoise(float x, float y, float z, int repeat = -1)
        {
            perlinNoise.repeat = repeat;
            return (float) perlinNoise.Perlin(x, y, z);
        }

        public float OctavePerlinNoise(float x, float y, float z, int octaves, float persistence, int repeat = -1)
        {
            perlinNoise.repeat = repeat;
            return (float) perlinNoise.OctavePerlin(x, y, z, octaves, persistence);
        }
    }
}