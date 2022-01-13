using JayTools.JayRandoms.Instance;
using JayTools.JayRandoms.Static;
using NUnit.Framework;

namespace JayTools.JayRandoms.Tests.EditorTests
{
    public class EditorJayRandomTestSuite
    {
        private const int Samples = 10000;

        [Test]
        public void Random01WithinRange()
        {
            for (int i = 0; i < Samples; i++)
            {
                float value = JayRandom.Random01();
                Assert.GreaterOrEqual(value, 0f);
                Assert.LessOrEqual(value, 1f);
            }
        }
        
        [Test]
        public void RandomIntegerWithPositiveRangeWithinRange()
        {
            const int min = 1;
            const int max = 10;
            
            for (int i = 0; i < Samples; i++)
            {
                int value = JayRandom.Random(min, max);
                Assert.GreaterOrEqual(value, min);
                Assert.Less(value, max);
            }
        }
        
        [Test]
        public void RandomIntegerWithMixedRangeWithinRange()
        {
            const int min = -10;
            const int max = 10;
            
            for (int i = 0; i < Samples; i++)
            {
                int value = JayRandom.Random(min, max);
                Assert.GreaterOrEqual(value, min);
                Assert.Less(value, max);
            }
        }
        
        [Test]
        public void RepeatRandomSkipsValues()
        {
            const int maxRepeats = 1;
            
            RepeatRandom<int> random = new RepeatRandom<int>(maxRepeats, 0,1);
            int[] values = new int[Samples];
            
            for (int i = 0; i < Samples; i++)
            {
                values[i] = random.Draw();
            }

            int lastValue = values[0];
            int repeatCount = 1;
            
            for (int i = 1; i < values.Length; i++)
            {
                int value = values[i];
                if (value == lastValue)
                {
                    repeatCount++;
                }
                else
                {
                    lastValue = value;
                    repeatCount = 1;
                }
                
                Assert.LessOrEqual(repeatCount, maxRepeats);
            }
        }

        [Test]
        public void PerlinNoiseWithinRange()
        {
            for (int i = 0; i < Samples; i++)
            {
                float value = JayRandom.PerlinNoise((i * 1f)/Samples, 0f, 0f);
                Assert.GreaterOrEqual(value, 0f);
                Assert.LessOrEqual(value, 1f);
            }
        }
    }
}
