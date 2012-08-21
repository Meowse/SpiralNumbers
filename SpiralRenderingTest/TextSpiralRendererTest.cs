using System;
using System.IO;
using NUnit.Framework;
using SpiralRendering;
using Spirals;

namespace SpiralRenderingTest
{
    internal class TextSpiralRendererTest
    {
        private TextSpiralRenderer _renderer;
        private TextWriter _textWriter;

        [SetUp]
        public void Setup()
        {
            _renderer = new TextSpiralRenderer();
            _textWriter = new StringWriter();
        }

        [Test]
        public void DoesNotRenderNegativeOne()
        {
            Spiral spiral = new Spiral(new[,]
            {
                {-1, -1, -1},
                {-1, 0, -1},
                {-1, -1, -1}
            }, 0);
            _renderer.Render(spiral, _textWriter);
            Assert.That(_textWriter.ToString(), Is.Not.StringContaining("-1"));
        }

        [Test]
        public void RendersSpacesBetweenNumbers()
        {
            Spiral spiral = new Spiral(new[,]
            {
                {-1, -1, -1},
                {-1, 0, 1},
                {-1, -1, -1}
            }, 0);
            _renderer.Render(spiral, _textWriter);
            Assert.That(_textWriter.ToString(), Is.EqualTo(
                  "     " + Environment.NewLine 
                + "  0 1" + Environment.NewLine
                + "     " + Environment.NewLine));
        }

        [Test]
        public void RendersExtraSpacesIfTwoDigitNumbersArePresent()
        {
            Spiral spiral = new Spiral(new[,]
            {
                {-1, -1, -1, -1, -1},
                {-1, 6, 7, 8, 9},
                {-1, 5, 0, 1, 10},
                {-1, 4, 3, 2, 11},
                {-1, -1, 14, 13, 12}
            }, 14);
            _renderer.Render(spiral, _textWriter);
            Assert.That(_textWriter.ToString(), Is.EqualTo(
                  "              " + Environment.NewLine
                + "    6  7  8  9" + Environment.NewLine
                + "    5  0  1 10" + Environment.NewLine
                + "    4  3  2 11" + Environment.NewLine
                + "      14 13 12" + Environment.NewLine));
        }

        [Test]
        public void DoesNotRenderExtraSpacesIfTwoDigitNumbersAreNotPresent()
        {
            Spiral spiral = new Spiral(new[,]
            {
                {-1, -1, -1, -1, -1},
                {-1, 6, 7, 8, 9},
                {-1, 5, 0, 1, -1},
                {-1, 4, 3, 2, -1},
                {-1, -1, -1, -1, -1}
            }, 9);
            _renderer.Render(spiral, _textWriter);
            Assert.That(_textWriter.ToString(), Is.EqualTo(
                  "         " + Environment.NewLine
                + "  6 7 8 9" + Environment.NewLine
                + "  5 0 1  " + Environment.NewLine
                + "  4 3 2  " + Environment.NewLine
                + "         " + Environment.NewLine));
        }

        [Test]
        public void RendersUpToThreeDigitNumbers()
        {
            Spiral spiral = new Spiral(new[,]
            {
                {110, 111, 112, 113, 114, 115, 116, 117, 118, -1, -1},
                {109, 72, 73, 74, 75, 76, 77, 78, 79, 80, 81},
                {108, 71, 42, 43, 44, 45, 46, 47, 48, 49, 82},
                {107, 70, 41, 20, 21, 22, 23, 24, 25, 50, 83},
                {106, 69, 40, 19, 6, 7, 8, 9, 26, 51, 84},
                {105, 68, 39, 18, 5, 0, 1, 10, 27, 52, 85},
                {104, 67, 38, 17, 4, 3, 2, 11, 28, 53, 86},
                {103, 66, 37, 16, 15, 14, 13, 12, 29, 54, 87},
                {102, 65, 36, 35, 34, 33, 32, 31, 30, 55, 88},
                {101, 64, 63, 62, 61, 60, 59, 58, 57, 56, 89},
                {100, 99, 98, 97, 96, 95, 94, 93, 92, 91, 90}
            }, 118);
            _renderer.Render(spiral, _textWriter);
            Assert.That(_textWriter.ToString(), Is.EqualTo(
                  "110 111 112 113 114 115 116 117 118        " + Environment.NewLine
                + "109  72  73  74  75  76  77  78  79  80  81" + Environment.NewLine
                + "108  71  42  43  44  45  46  47  48  49  82" + Environment.NewLine
                + "107  70  41  20  21  22  23  24  25  50  83" + Environment.NewLine
                + "106  69  40  19   6   7   8   9  26  51  84" + Environment.NewLine
                + "105  68  39  18   5   0   1  10  27  52  85" + Environment.NewLine
                + "104  67  38  17   4   3   2  11  28  53  86" + Environment.NewLine
                + "103  66  37  16  15  14  13  12  29  54  87" + Environment.NewLine
                + "102  65  36  35  34  33  32  31  30  55  88" + Environment.NewLine
                + "101  64  63  62  61  60  59  58  57  56  89" + Environment.NewLine
                + "100  99  98  97  96  95  94  93  92  91  90" + Environment.NewLine
            ));
        }
    }
}

