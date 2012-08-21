using System.Drawing;
using Spirals;

namespace SpiralGeneration
{
    /// <summary>
    /// Fifth algorithm -- treat each shell as four lines, and build each shell in turn by
    /// computing its size and starting value.
    /// 
    /// It's probably Faster than LightCycleGenerator, and arguably simpler than SparkGenerator.
    /// And it could probably be modified to provide random access, without being as overcomplicated 
    /// as RandomAccessGenerator.
    /// 
    /// But it's not really a big improvement over any of the above, 
    /// </summary>
    public class ShellGenerator : ISpiralGenerator
    {
        public Spiral Generate(int spiralTo)
        {
            return PopulateSpiral(new Spiral(spiralTo));
        }

        private Spiral PopulateSpiral(Spiral spiral)
        {
            for (int shellIndex = 1; shellIndex <= spiral.Size / 2; shellIndex++)
            {
                DrawShell(spiral, shellIndex);
            }
            return spiral;
        }

        private void DrawShell(Spiral spiral, int shellIndex)
        {
            int shellDimension = shellIndex*2 + 1;
            int previousShellDimension = (shellIndex - 1)*2 + 1;
            int nextValue = previousShellDimension * previousShellDimension;
            Rectangle shellCorners = new Rectangle(-shellIndex, -shellIndex, shellDimension - 1, shellDimension - 1);
            DrawRectangle(spiral, nextValue, shellCorners);
        }

        private void DrawRectangle(Spiral spiral, int firstValue, Rectangle shellCorners)
        {
            int nextValue = DrawLine(spiral, firstValue, new Point(shellCorners.Right, shellCorners.Top + 1), new Point(shellCorners.Right, shellCorners.Bottom));
            nextValue = DrawLine(spiral, nextValue, new Point(shellCorners.Right - 1, shellCorners.Bottom), new Point(shellCorners.Left, shellCorners.Bottom));
            nextValue = DrawLine(spiral, nextValue, new Point(shellCorners.Left, shellCorners.Bottom - 1), new Point(shellCorners.Left, shellCorners.Top));
            DrawLine(spiral, nextValue, new Point(shellCorners.Left + 1, shellCorners.Top), new Point(shellCorners.Right, shellCorners.Top));
        }

        private int DrawLine(Spiral spiral, int nextValue, Point start, Point end)
        {
            if (start.X < end.X)
            {
                for (int i = start.X; i <= end.X; i++)
                {
                    spiral.SetValueRelative(i, start.Y, nextValue++);
                }
            }
            else if (start.X > end.X)
            {
                for (int i = start.X; i >= end.X; i--)
                {
                    spiral.SetValueRelative(i, start.Y, nextValue++);
                }
            }
            if (start.Y < end.Y)
            {
                for (int i = start.Y; i <= end.Y; i++)
                {
                    spiral.SetValueRelative(start.X, i, nextValue++);
                }
            }
            else if (start.Y > end.Y)
            {
                for (int i = start.Y; i >= end.Y; i--)
                {
                    spiral.SetValueRelative(start.X, i, nextValue++);
                }
            }
            return nextValue;
        }
    }
}
