using System;
using NUnit.Framework;
using SpiralGeneration;
using Spirals;

namespace SpiralGenerationTest
{
    [TestFixture]
    public class RandomAccessSpiralGeneratorsTest
    {
        private IRandomAccessSpiralGenerator[] _generators;

        [SetUp]
        public void Setup()
        {
            _generators = new IRandomAccessSpiralGenerator[]
            {
                new RandomAccessGenerator(), 
                new RandomAccessGeneratorImproved(),
                new SpiralGenerator()
            };
        }

        private void DoForGenerators(Action<IRandomAccessSpiralGenerator, string> generatorAction)
        {
            foreach (IRandomAccessSpiralGenerator generator in _generators)
            {
                generatorAction(generator, GetFailureMessage(generator));
            }
        }

        private string GetFailureMessage(IRandomAccessSpiralGenerator generator)
        {
            return string.Format("Failed for generator {0}.", generator.GetType().Name);
        }

        [Test]
        public void ReturnsCorrectThreeByThreeSpiral()
        {
            DoForGenerators((generator, message) =>
            {
                Assert.That(generator.GetValueAt(0, 0), Is.EqualTo(0), message);
                Assert.That(generator.GetValueAt(1, 0), Is.EqualTo(1), message);
                Assert.That(generator.GetValueAt(1, 1), Is.EqualTo(2), message);
                Assert.That(generator.GetValueAt(0, 1), Is.EqualTo(3), message);
                Assert.That(generator.GetValueAt(-1, 1), Is.EqualTo(4), message);
                Assert.That(generator.GetValueAt(-1, 0), Is.EqualTo(5), message);
                Assert.That(generator.GetValueAt(-1, -1), Is.EqualTo(6), message);
                Assert.That(generator.GetValueAt(0, -1), Is.EqualTo(7), message);
                Assert.That(generator.GetValueAt(1, -1), Is.EqualTo(8), message);
            });
        }

        [Test]
        public void ReturnsExpectedValuesForLargeCoordinates()
        {
            DoForGenerators((generator, message) =>
            {
                Spiral spiral = generator.Generate(10000);
                var bounds = spiral.Size/2;
                for (int x = -bounds; x <= bounds; x++)
                {
                    for (int y = -bounds; y <= bounds; y++)
                    {
                        var expectedValue = spiral.GetValueRelative(x, y);
                        int actualValue = generator.GetValueAt(x, y);
                        if (expectedValue == -1)
                        {
                            Assert.That(actualValue, Is.GreaterThan(10000), message);
                        }
                        else
                        {
                            Assert.That(actualValue, Is.EqualTo(expectedValue), message);                            
                        }
                    }
                }
            });
        }
    }
}
