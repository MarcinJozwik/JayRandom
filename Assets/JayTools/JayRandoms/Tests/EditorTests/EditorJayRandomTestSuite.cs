using System;
using System.Collections.Generic;
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

        [Test]
        public void RemovalRandomUsedValues()
        {
            List<int> collection = new List<int>() {1, 4, 5, 7, 10};
            RemovalRandom<int> removalRandom = new RemovalRandom<int>(collection);
            int count = collection.Count;

            List<int> usedItems = new List<int>();
            for (int i = 0; i < count; i++)
            {
                int randomNumber = removalRandom.Draw();
            
                Assert.That(!usedItems.Contains(randomNumber));
                
                usedItems.Add(randomNumber);
            }
        }
        
        [Test]
        public void RemovalRandomResetsItself()
        {
            List<int> collection = new List<int>() {1, 1, 4, 5, 7, 10};
            RemovalRandom<int> removalRandom = new RemovalRandom<int>(collection);
            int count = collection.Count;

            List<int> usedItems = new List<int>();
            for (int i = 0; i < count; i++)
            {
                int randomNumber = removalRandom.Draw();
                usedItems.Add(randomNumber);
            }

            Assert.AreEqual(count, removalRandom.ItemsLeft());
        }
        
        [Test]
        public void RemovalRandomCatchesCollectionChange()
        {
            List<int> collection = new List<int>() {1, 1, 4, 5, 7, 10};
            RemovalRandom<int> removalRandom = new RemovalRandom<int>(collection);
            removalRandom.Draw();

            collection.Remove(1);

            var exception = Assert.Throws<Exception>(() => removalRandom.Draw());
            Assert.That(exception.Message, Is.EqualTo("The original collection was changed outside the Removal Random class!"));
        }

        [Test]
        public void WeightRandomDraws100Weight()
        {
            WeightRandom<string> weightRandom = new WeightRandom<string>();
            
            weightRandom.AddItem("A", 100);
            weightRandom.AddItem("B", 0);
            weightRandom.AddItem("C", 0);

            for (int i = 0; i < Samples; i++)
            {
                string item = weightRandom.Draw();
                Assert.AreEqual("A", item);
            }
        }
        
        [Test]
        public void WeightRandomWithRemovalDrawsCorrectly()
        {
            WeightRandom<string> weightRandom = new WeightRandom<string>();
            
            weightRandom.AddItem("A", 100);
            weightRandom.AddItem("B", 0);
            weightRandom.AddItem("C", 0);

            string item = weightRandom.DrawWithRemoval();
            Assert.AreEqual("A", item);
            
            weightRandom.SetItemWeight("C", 1);
            var cItem = weightRandom.GetItem("C");
            Assert.AreEqual(1, cItem.Weight);
            
            item = weightRandom.DrawWithRemoval();
            Assert.AreEqual("C", item);
        }
        
        [Test]
        public void WeightRandomAdjustWeightsCorrectly()
        {
            WeightRandom<string> weightRandom = new WeightRandom<string>();
            
            weightRandom.AddItem("A", 500);
            weightRandom.AddItem("B", 0);
            weightRandom.AddItem("C", 0);

            string item = weightRandom.DrawWithWeightAdjustment(-50, 20);

            Assert.AreEqual(450, weightRandom.GetItem("A").Weight);
            Assert.AreEqual(20, weightRandom.GetItem("B").Weight);
            Assert.AreEqual(20, weightRandom.GetItem("C").Weight);
        }
        
        [Test]
        public void WeightRandomPicksMoreProbableItem()
        {
            WeightRandom<string> weightRandom = new WeightRandom<string>();
            
            weightRandom.AddItem("A", 500);
            weightRandom.AddItem("B", 20);

            int aWins = 0;
            for (int i = 0; i < Samples; i++)
            {
                string item = weightRandom.Draw();
                aWins += (item == "A" ? 1 : -1);
            }
            
            Assert.Greater(aWins, 0);
        }
    }
}
