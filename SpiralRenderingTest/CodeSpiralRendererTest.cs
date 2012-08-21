using System;
using System.IO;
using NUnit.Framework;
using SpiralRendering;
using Spirals;

namespace SpiralRenderingTest
{
    class CodeSpiralRendererTest
    {
        private CodeSpiralRenderer _renderer;
        private TextWriter _textWriter;

        [SetUp]
        public void Setup()
        {
            _renderer = new CodeSpiralRenderer();
            _textWriter = new StringWriter();
        }

        [Test]
        public void RendersAsMultiDimensionalArrayDeclaration()
        {
            Spiral spiral = new Spiral(new[,] { {-1, -1, -1}, {-1,  0, 1}, {-1, -1, 2} }, 0);
            _renderer.Render(spiral, _textWriter);
            Assert.That(_textWriter.ToString(), Is.EqualTo("new[,]" + Environment.NewLine + "{" + Environment.NewLine + "{-1, -1, -1}," + Environment.NewLine + "{-1, 0, 1}," + Environment.NewLine + "{-1, -1, 2}" + Environment.NewLine + "}" + Environment.NewLine));
        }
    }
}
