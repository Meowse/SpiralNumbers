using System;
using System.IO;
using System.Text;
using NUnit.Framework;
using SpiralGeneration;
using SpiralNumbers;
using SpiralRendering;
using Spirals;

namespace SpiralNumbersTest
{
    [TestFixture]
    public class SpiralNumbersProgramTest
    {
        private const string _USER_PROMPT = "Spiral to integer (blank line to exit): ";
        private const string _BAD_INPUT_PROMPT = "Please enter a positive integer to spiral out to, or a blank line to exit.";

        private MemoryStream _inputStream;
        private StreamReader _inReader;
        private StringWriter _outWriter;
        private MockSpiralGenerator _mockGenerator;
        private MockSpiralRenderer _mockRenderer;
        private SpiralNumbersProgram _program;

        [SetUp]
        public void BeforeEachTest()
        {
            _inputStream = new MemoryStream(100);
            _inReader = new StreamReader(_inputStream);
            _outWriter = new StringWriter();
            _mockGenerator = new MockSpiralGenerator();
            _mockRenderer = new MockSpiralRenderer();
            _program = new SpiralNumbersProgram(_inReader, _outWriter, _mockGenerator, _mockRenderer);
        }

        [Test]
        public void ShouldPromptUser()
        {
            _program.Run();
            Assert.That(_outWriter.ToString(), Is.EqualTo(_USER_PROMPT));
        }

        [Test]
        public void ShouldPromptUserAfterEachSpiral()
        {
            WriteToInput("3\n4\n");
            _program.Run();
            Assert.That(_outWriter.ToString(), Is.EqualTo(_USER_PROMPT + _USER_PROMPT + _USER_PROMPT));
        }

        [Test]
        public void ShouldExitOnNull()
        {
            _inputStream.WriteByte(0);
            _program.Run();
            Assert.That(_outWriter.ToString(), Is.EqualTo(_USER_PROMPT));
            Assert.That(_mockGenerator.Calls, Is.EqualTo(0));
            Assert.That(_mockRenderer.Calls, Is.EqualTo(0));
        }

        [Test]
        public void ShouldExitOnBlankLine()
        {
            WriteToInput("\n");
            _program.Run();
            Assert.That(_outWriter.ToString(), Is.EqualTo(_USER_PROMPT));
            Assert.That(_mockGenerator.Calls, Is.EqualTo(0));
            Assert.That(_mockRenderer.Calls, Is.EqualTo(0));
        }

        [Test]
        public void ShouldExitOnAllWhitespace()
        {
            WriteToInput("    \t\t  ");
            _program.Run();
            Assert.That(_outWriter.ToString(), Is.EqualTo(_USER_PROMPT));
            Assert.That(_mockGenerator.Calls, Is.EqualTo(0));
            Assert.That(_mockRenderer.Calls, Is.EqualTo(0));
        }

        [Test]
        public void ShouldExitOnWhitespaceAfterValidInputs()
        {
            WriteToInput("3\n\n");
            _program.Run();
            Assert.That(_outWriter.ToString(), Is.EqualTo(_USER_PROMPT + _USER_PROMPT));
            Assert.That(_mockGenerator.Calls, Is.EqualTo(1));
            Assert.That(_mockRenderer.Calls, Is.EqualTo(1));
        }


        [Test]
        public void ShouldGenerateAndRenderSpirals()
        {
            WriteToInput("3\n\n");
            Spiral expectedGeneratedSpiral = new Spiral(3);
            const string expectedRenderedSpiral = "[multi-line string\nrepresenting the\nrendered spiral]";
            _mockGenerator.GeneratedSpiral = expectedGeneratedSpiral;
            _mockRenderer.ExpectedSpiral = expectedGeneratedSpiral;
            _mockRenderer.RenderedSpiral = expectedRenderedSpiral;
            
            _program.Run();

            Assert.That(_outWriter.ToString(), Is.EqualTo(_USER_PROMPT + expectedRenderedSpiral + _USER_PROMPT));
        }

        [Test]
        public void ShouldPromptUserMoreSpecificallyOnInvalidInput()
        {
            WriteToInput("three\n3\n\n");

            Spiral expectedGeneratedSpiral = new Spiral(3);
            const string expectedRenderedSpiral = "[multi-line string\nrepresenting the\nrendered spiral]";
            _mockGenerator.GeneratedSpiral = expectedGeneratedSpiral;
            _mockRenderer.ExpectedSpiral = expectedGeneratedSpiral;
            _mockRenderer.RenderedSpiral = expectedRenderedSpiral;

            _program.Run();

            Assert.That(_outWriter.ToString(), Is.EqualTo(_USER_PROMPT + _BAD_INPUT_PROMPT + Environment.NewLine + _USER_PROMPT + expectedRenderedSpiral + _USER_PROMPT));
        }

        // For anything larger, I would have used a mocking framework, but hand-written mocks will suffice for a programming exercise.
        internal class MockSpiralGenerator : ISpiralGenerator
        {
            public Spiral GeneratedSpiral { get; set; }

            public int Calls { get; private set; }

            public Spiral Generate(int spiralTo)
            {
                Calls++;
                return GeneratedSpiral;
            }
        }

        internal class MockSpiralRenderer : ISpiralRenderer
        {
            public Spiral ExpectedSpiral { get; set; }
            public string RenderedSpiral { get; set; }

            public string Render(Spiral spiral)
            {
                Calls++;
                if (spiral != ExpectedSpiral)
                {
                    throw new Exception("Error: did not get expected spiral in call to Render(spiral).");
                }
                return RenderedSpiral;
            }

            public void Render(Spiral spiral, TextWriter writer)
            {
                Calls++;
                if (spiral != ExpectedSpiral)
                {
                    throw new Exception("Error: did not get expected spiral in call to Render(spiral, writer).");
                }
                writer.Write(RenderedSpiral);
            }

            public int Calls { get; private set; }
        }

        private void WriteToInput(string inputString)
        {
            byte[] bytes = new ASCIIEncoding().GetBytes(inputString);
            _inputStream.Write(bytes, 0, inputString.Length);
            _inputStream.Seek(0, SeekOrigin.Begin);
        }
    }
}
