using System.IO;
using SpiralGeneration;
using SpiralRendering;

namespace SpiralNumbers
{
    public class SpiralNumbersProgram
    {
        private TextReader InReader { get; set; }
        private TextWriter OutWriter { get; set; }
        private ISpiralGenerator Generator { get; set; }
        private ISpiralRenderer Renderer { get; set; }

        public SpiralNumbersProgram(TextReader inReader, TextWriter outWriter, ISpiralGenerator generator, ISpiralRenderer renderer)
        {
            InReader = inReader;
            OutWriter = outWriter;
            Generator = generator;
            Renderer = renderer;
        }

        public void Run()
        {
            string input = GetUserInput();
            while (!ShouldExit(input))
            {
                ProcessUserInput(input);
                input = GetUserInput();
            }
        }

        private bool ShouldExit(string input)
        {
            return ((input == null) || input.Trim().Equals(string.Empty));
        }

        private string GetUserInput()
        {
            OutWriter.Write("Spiral to integer (blank line to exit): ");
            return InReader.ReadLine();
        }

        private void ProcessUserInput(string userInput)
        {
            int spiralTo;
            if (int.TryParse(userInput, out spiralTo) && (spiralTo >= 0))
            {
                Renderer.Render(Generator.Generate(spiralTo), OutWriter);
            }
            else
            {
                OutWriter.WriteLine("Please enter a positive integer to spiral out to, or a blank line to exit.");
            }
        }
    }
}
