using Spirals;

namespace SpiralGeneration
{
    /// <summary>
    /// First algorithm -- I noticed, when I started thinking about this problem, that the starting point
    /// (one down from the upper right corner of the shell) and the lower left corner of each "shell" in 
    /// the spiral (each successive hollow square, moving outward from the origin) were square numbers 
    /// (e.g. 1 at coordinate (1,0) and 4 at coordinate (-1,1), or 9 at coordinate (2,-1) and 16 at coordinate (-2,2)).  
    /// Since all spiral numbers in a "shell" differ from one of those two "corner" coordinates on only one axis, 
    /// and the difference between the "corner" value and the spiral number is the same as the horizontal or vertical 
    /// distance between the referenced corner and the spiral number, it is easy to calculate the spiral number for 
    /// any set of coordinates.
    /// 
    /// This algorithm suffers from two flaws:
    ///    1) It is less efficient than an algorithm which builds the spiral iteratively, since it performs
    /// redundant calculations on adjacent spiral cells, and
    ///    2) It is almost incomprehensible, even with reasonable method names and variable names.
    /// 
    /// However, it has the advantage of random access -- it is possible to determine the spiral value at coordinates
    /// (x,y) without building the entire spiral.  Should the application of this spiral numbers algorith 
    /// require knowing a few points in a very large spiral without building said very large spiral, this is the 
    /// algorithm to use.
    /// </summary>
    public class RandomAccessGenerator : IRandomAccessSpiralGenerator
    {
        public Spiral Generate(int spiralTo)
        {
            return PopulateSpiral(new Spiral(spiralTo));
        }

        private Spiral PopulateSpiral(Spiral spiral)
        {
            var offset = spiral.Origin.X;
            for (int x = -offset; x <= offset; x++)
            {
                for (int y = -offset; y <= offset; y++)
                {
                    int valueAtCoordinates = GetValueAt(x, y);
                    spiral.SetValueRelative(x, y, valueAtCoordinates);
                }
            }
            return spiral;
        }

        public int GetValueAt(int x, int y)
        {
            if ((x == 0) && (y == 0))
            {
                return 0;
            }
            if (IsOnRightSide(x, y))
            {
                int topRightValue = (((x*2) - 1)*((x*2) - 1));
                int topRightValueYCoord = - (x - 1);
                int yDistanceFromTopRight = y - topRightValueYCoord;
                return topRightValue + yDistanceFromTopRight;
            }
            if (IsOnBottom(x, y))
            {
                int leftBottomValue = ((y * 2)*(y * 2));
                int leftBottomXCoord = -y;
                int xDistanceFromLeftBottom = x - leftBottomXCoord;
                return leftBottomValue - xDistanceFromLeftBottom;
            }
            if (IsOnLeftSide(x, y))
            {
                int leftBottomValue = (x * 2) * (x * 2);
                int leftBottomYCoord = -x;
                int yDistanceFromLeftBottom = leftBottomYCoord - y;
                return leftBottomValue + yDistanceFromLeftBottom;
            }
            if (IsOnTop(x, y))
            {
                int topRightValueOfNextShell = (((y*2) - 1)*((y*2) - 1)) - 1;
                int xCoordOfTopRightValueOfNextShell = -y;
                int xDistanceFromTopRightValueOfNextShell = xCoordOfTopRightValueOfNextShell - x;
                return topRightValueOfNextShell - xDistanceFromTopRightValueOfNextShell;
            }
            return -1;
        }

        private bool IsOnRightSide(int x, int y)
        {
            return (x > 0) && (y >= -(x-1)) && (y <= x);
        }

        private bool IsOnBottom(int x, int y)
        {
            return (y > 0) && (x < y) && (x >= -y);
        }

        private bool IsOnLeftSide(int x, int y)
        {
            return (x < 0) && (y >= x) && (y < -x);
        }

        private bool IsOnTop(int x, int y)
        {
            return (y < 0) && (x > y) && (x <= -y);
        }
    }
}
