using Spirals;

namespace SpiralGeneration
{
    /// <summary>
    /// Sixth algorithm -- refining ShellGenerator.  
    /// 
    /// ShellGeneratorImproved and SparkGenerator seem to be converging on a common implementation, 
    /// with distinct shades of RandomAccessGenerator.  This makes me suspect that there is some 
    /// underlying model of spirals that I'm getting close to, but also that I'm approaching the
    /// point of diminishing returns.
    /// 
    /// At this point, in a production system, I'd stop working on the problem for a few days or 
    /// weeks to give myself time to mull it over, and come back to it when my subconscious handed
    /// the solution to me.  I've got a range of "good enough for now" solutions, tuned to different
    /// uses, and further refinements can wait on inspiration.
    /// </summary>
    public class ShellGeneratorImproved : ISpiralGenerator
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
            int nextValue = DrawDown(spiral, GetShellStartingValue(shellIndex), shellIndex, -shellIndex + 1, shellIndex);
            nextValue = DrawLeft(spiral, nextValue, shellIndex, shellIndex - 1, -shellIndex);
            nextValue = DrawUp(spiral, nextValue, -shellIndex, shellIndex - 1, -shellIndex);
            DrawRight(spiral, nextValue, -shellIndex, -shellIndex + 1, shellIndex);
        }        

        private int DrawDown(Spiral spiral, int nextValue, int x, int startY, int endY)
        {
            for (int y = startY; y <= endY; y++)
            {
                spiral.SetValueRelative(x, y, nextValue++);
            }
            return nextValue;
        }

        private int DrawLeft(Spiral spiral, int nextValue, int y, int startX, int endX)
        {
            for (int x = startX; x >= endX; x--)
            {
                spiral.SetValueRelative(x, y, nextValue++);
            }
            return nextValue;
        }

        private int DrawUp(Spiral spiral, int nextValue, int x, int startY, int endY)
        {
            for (int y = startY; y >= endY; y--)
            {
                spiral.SetValueRelative(x, y, nextValue++);
            }
            return nextValue;
        }

        private void DrawRight(Spiral spiral, int nextValue, int y, int startX, int endX)
        {
            for (int x = startX; x <= endX; x++)
            {
                spiral.SetValueRelative(x, y, nextValue++);
            }
        }

        private static int GetShellStartingValue(int shellIndex)
        {
            int previousShellDimension = (shellIndex - 1)*2 + 1;
            return previousShellDimension*previousShellDimension;
        }
    }
}
