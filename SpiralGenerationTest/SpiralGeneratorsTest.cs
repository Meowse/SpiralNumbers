using System;
using NUnit.Framework;
using SpiralGeneration;
using Spirals;

namespace SpiralGenerationTest
{
    [TestFixture]
    public class SpiralGeneratorsTest
    {
        private ISpiralGenerator[] _generators;

        [SetUp]
        public void Setup()
        {
            _generators = new ISpiralGenerator[]
            {
                new RandomAccessGenerator(), 
                new SparkGenerator(), 
                new LightCycleGenerator(), 
                new LightCycleGeneratorImproved(), 
                new ShellGenerator(), 
                new ShellGeneratorImproved(),
                new RandomAccessGeneratorImproved(),
                new SpiralGenerator()
            };
        }

        private void DoForGenerators(Action<ISpiralGenerator, string> generatorAction)
        {
            foreach (ISpiralGenerator generator in _generators)
            {
                generatorAction(generator, GetFailureMessage(generator));
            }
        }

        private string GetFailureMessage(ISpiralGenerator generator)
        {
            return string.Format("Failed for generator {0}.", generator.GetType().Name);
        }

        [Test]
        public void ReturnsSingleElementSpiralForZero()
        {
            DoForGenerators((generator, message) =>
            {
                Spiral spiral = generator.Generate(0);
                Assert.That(spiral.Numbers, Is.EqualTo(new[,] {{0}}), message);
            });
        }

        [Test]
        public void ReturnsOddWidthSpiralForEvenWidthResult()
        {
            DoForGenerators((generator, message) =>
            {
                Spiral spiral = generator.Generate(1);
                Assert.That(spiral.Numbers.Length, Is.EqualTo(9));                                    
            });
        }

        [Test]
        public void ReturnsOddWidthSpiralForOddWidthResult()
        {
            DoForGenerators((generator, message) =>
            {
                Spiral spiral = generator.Generate(4);
                Assert.That(spiral.Numbers.Length, Is.EqualTo(9));
            });
        }

        [Test]
        public void ReturnsCorrectSpiralForRightEdge()
        {
            DoForGenerators((generator, message) =>
            {
                Spiral spiral = generator.Generate(2);
                Assert.That(spiral.Numbers, Is.EqualTo(new[,] {
                    { -1, -1, -1 }, 
                    { -1,  0,  1 },
                    { -1, -1,  2 }
                }));
            });
        }

        [Test]
        public void ReturnsCorrectSpiralForLowerEdge()
        {
            DoForGenerators((generator, message) =>
            {
                Spiral spiral = generator.Generate(4);
                Assert.That(spiral.Numbers, Is.EqualTo(new[,] {
                    { -1, -1, -1 }, 
                    { -1,  0,  1 },
                    {  4,  3,  2 }
                }));
            });
        }

        [Test]
        public void ReturnsCorrectSpiralForLeftEdge()
        {
            DoForGenerators((generator, message) =>
            {
                Spiral spiral = generator.Generate(6);
                Assert.That(spiral.Numbers, Is.EqualTo(new[,] {
                    {  6, -1, -1 }, 
                    {  5,  0,  1 },
                    {  4,  3,  2 }
                }));
            });
        }

        [Test]
        public void ReturnsCorrectSpiralForUpperEdge()
        {
            DoForGenerators((generator, message) =>
            {
                Spiral spiral = generator.Generate(8);
                Assert.That(spiral.Numbers, Is.EqualTo(new[,] {
                    {  6,  7,  8 }, 
                    {  5,  0,  1 },
                    {  4,  3,  2 }
                }));
            });
        }

        [Test]
        public void ReturnsCorrectSpiralForMiddleOfLowerRightEdges()
        {
            DoForGenerators((generator, message) =>
            {
                Spiral spiral = generator.Generate(14);
                Assert.That(spiral.Numbers, Is.EqualTo(new[,] {
                    { -1, -1, -1, -1, -1 }, 
                    { -1,  6,  7,  8,  9 }, 
                    { -1,  5,  0,  1, 10 },
                    { -1,  4,  3,  2, 11 },
                    { -1, -1, 14, 13, 12 } 
                }));
            });
        }

        [Test]
        public void ReturnsCorrectSpiralForMiddleOfUpperLeftEdges()
        {
            DoForGenerators((generator, message) =>
            {
                Spiral spiral = generator.Generate(22);
                Assert.That(spiral.Numbers, Is.EqualTo(new[,] {
                    { 20, 21, 22, -1, -1 }, 
                    { 19,  6,  7,  8,  9 }, 
                    { 18,  5,  0,  1, 10 },
                    { 17,  4,  3,  2, 11 },
                    { 16, 15, 14, 13, 12 } 
                }));
            });
        }

        [Test]
        public void HandlesExampleCase()
        {
            DoForGenerators((generator, message) =>
            {
                Spiral spiral = generator.Generate(24);
                Assert.That(spiral.Numbers, Is.EqualTo(new[,] {
                    { 20, 21, 22, 23, 24 }, 
                    { 19,  6,  7,  8,  9 }, 
                    { 18,  5,  0,  1, 10 },
                    { 17,  4,  3,  2, 11 },
                    { 16, 15, 14, 13, 12 } 
                }));
            });
        }

        [Test]
        public void HandlesThreeDigitNumbers()
        {
            DoForGenerators((generator, message) =>
            {
                Spiral spiral = generator.Generate(118);
                Assert.That(spiral.Numbers, Is.EqualTo(new[,]
                {
                    {110, 111, 112, 113, 114, 115, 116, 117, 118,  -1,  -1},
                    {109,  72,  73,  74,  75,  76,  77,  78,  79,  80,  81},
                    {108,  71,  42,  43,  44,  45,  46,  47,  48,  49,  82},
                    {107,  70,  41,  20,  21,  22,  23,  24,  25,  50,  83},
                    {106,  69,  40,  19,   6,   7,   8,   9,  26,  51,  84},
                    {105,  68,  39,  18,   5,   0,   1,  10,  27,  52,  85},
                    {104,  67,  38,  17,   4,   3,   2,  11,  28,  53,  86},
                    {103,  66,  37,  16,  15,  14,  13,  12,  29,  54,  87},
                    {102,  65,  36,  35,  34,  33,  32,  31,  30,  55,  88},
                    {101,  64,  63,  62,  61,  60,  59,  58,  57,  56,  89},
                    {100,  99,  98,  97,  96,  95,  94,  93,  92,  91,  90}
                }));
            });
        }
    }
}
