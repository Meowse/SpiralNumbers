using System.IO;
using Spirals;

namespace SpiralRendering
{
    public class CodeSpiralRenderer : ISpiralRenderer
    {
        public void Render(Spiral spiral, TextWriter outWriter)
        {
            outWriter.WriteLine("new[,]");
            outWriter.WriteLine("{");
            int lastRowOrColumn = spiral.Size - 1;
            for (int row = 0; row < spiral.Size; row++)
            {
                outWriter.Write("{");
                for (int column = 0; column < spiral.Size; column++)
                {
                    outWriter.Write(spiral.Numbers[row, column]);
                    if (column < lastRowOrColumn)
                    {
                        outWriter.Write(", ");
                    }
                }
                outWriter.WriteLine((row < lastRowOrColumn) ? "}," : "}");
            }
            outWriter.WriteLine("}");
        }
    }
}
