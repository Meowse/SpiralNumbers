using System.IO;
using NUnit.Framework;
using SpiralRendering;
using Spirals;

namespace SpiralRenderingTest
{
    class HtmlSpiralRendererTest
    {
        private HtmlSpiralRenderer _renderer;
        private TextWriter _textWriter;

        [SetUp]
        public void Setup()
        {
            _renderer = new HtmlSpiralRenderer();
            _textWriter = new StringWriter();
        }

        [Test]
        public void RendersAsHtmlTableRows()
        {
            Spiral spiral = new Spiral(new[,] { {-1, -1, -1}, {-1,  0, 1}, {-1, -1, 2} }, 0);
            _renderer.Render(spiral, _textWriter);
            Assert.That(_textWriter.ToString(), Is.EqualTo("<tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr><tr><td>&nbsp;</td><td>0</td><td>1</td></tr><tr><td>&nbsp;</td><td>&nbsp;</td><td>2</td></tr>"));
        }

        // To display in a web page with proper formatting:
        //<html>
        //  <style>
        //  .spiral td {
        //	  text-align:right;
        //  }
        //  </style>
        //  <body>
        //    <table class="spiral">
        //      <!-- ...rows go here... -->
        //    </table>
        //  </body>
        //</html>
    }
}
