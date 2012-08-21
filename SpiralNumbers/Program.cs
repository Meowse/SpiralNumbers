using System;
using SpiralGeneration;
using SpiralRendering;

namespace SpiralNumbers
{
    public class Program
    {
        static void Main(string[] args)
        {
            new SpiralNumbersProgram(Console.In, Console.Out, new SpiralGenerator(), new TextSpiralRenderer()).Run();
        }
    }
}
