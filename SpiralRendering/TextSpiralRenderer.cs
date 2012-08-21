using System;
using System.Globalization;
using System.IO;
using Spirals;

namespace SpiralRendering
{
    public class TextSpiralRenderer : ISpiralRenderer
    {
        public void Render(Spiral spiral, TextWriter outWriter)
        {
            int places = Math.Max(0, (int)Math.Floor(Math.Log10(spiral.SpiralTo))) + 1;
            for (int row = 0; row < spiral.Size; row++)
            {
                for (int column = 0; column < spiral.Size; column++)
                {
                    if (column != 0) { outWriter.Write(" "); }
                    outWriter.Write(FormatForDisplay(spiral.Numbers[row, column], places));
                }
                outWriter.WriteLine();
            }
        }

        private static string FormatForDisplay(int value, int places)
        {
            return ((value == -1) ? " " : value.ToString(CultureInfo.InvariantCulture)).PadLeft(places);
        }
    }
}
