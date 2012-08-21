using System.Globalization;
using System.IO;
using Spirals;

namespace SpiralRendering
{
    public class HtmlSpiralRenderer : ISpiralRenderer
    {
        public void Render(Spiral spiral, TextWriter outWriter)
        {
            for (int row = 0; row < spiral.Size; row++)
            {
                outWriter.Write("<tr>");
                for (int column = 0; column < spiral.Size; column++)
                {
                    int value = spiral.Numbers[row, column];
                    outWriter.Write(string.Format("<td>{0}</td>", ((value == -1) ? "&nbsp;" : value.ToString(CultureInfo.InvariantCulture))));
                }
                outWriter.Write("</tr>");
            }
        }
    }
}
