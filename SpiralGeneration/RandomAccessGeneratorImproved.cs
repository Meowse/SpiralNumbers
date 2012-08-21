using System;
using Spirals;

namespace SpiralGeneration
{
    /// <summary>
    /// Seventh algorithm -- Revisiting RandomAccessGenerator with the insight gained from writing
    /// ShellGeneratorImproved.
    /// </summary>
    public class RandomAccessGeneratorImproved : IRandomAccessSpiralGenerator
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

            int shellIndex = Math.Max(Math.Abs(x), Math.Abs(y));
            if (IsOnRightOfShell(x, y, shellIndex))
            {
                return GetShellStart(shellIndex) + (y - GetShellStartYCoord(shellIndex));
            }
            if (IsOnBottomOfShell(x, y, shellIndex))
            {
                return GetShellMiddle(shellIndex) - (x - GetShellMiddleXCoord(shellIndex));
            }
            if (IsOnLeftOfShell(x, y, shellIndex))
            {
                return GetShellMiddle(shellIndex) + (GetShellMiddleYCoord(shellIndex) - y);
            }
            if (IsOnTopOfShell(x, y, shellIndex))
            {
                return GetShellEnd(shellIndex) - (GetShellEndXCoord(shellIndex) - x);
            }
            throw new Exception("Should not be possible to reach this code.");
        }

        private int GetShellStart(int shellIndex)
        {
            return (2*shellIndex - 1)*(2*shellIndex - 1);
        }

        private int GetShellMiddle(int shellIndex)
        {
            return 4*shellIndex*shellIndex;
        }

        private int GetShellEnd(int shellIndex)
        {
            int shellDimension = shellIndex*2 + 1;
            return shellDimension*shellDimension - 1;
        }

        private int GetShellStartYCoord(int shellIndex)
        {
            return -shellIndex + 1;
        }

        private int GetShellMiddleXCoord(int shellIndex)
        {
            return -shellIndex;
        }

        private int GetShellMiddleYCoord(int shellIndex)
        {
            return shellIndex;
        }

        private int GetShellEndXCoord(int shellIndex)
        {
            return shellIndex;
        }

        /// <summary>
        /// Everything on the right-hand side except the top right corner, which is part of "top".
        /// </summary>
        private bool IsOnRightOfShell(int x, int y, int shellIndex)
        {
            return ((x == shellIndex) && (y != -shellIndex));
        }

        /// <summary>
        /// Everything on the bottom side except the bottom right corner, which is part of "right".
        /// </summary>
        private bool IsOnBottomOfShell(int x, int y, int shellIndex)
        {
            return ((y == shellIndex) && (x != shellIndex));
        }

        /// <summary>
        /// Everything on the left-hand side except the bottom left corner, which is part of "bottom".
        /// </summary>
        private bool IsOnLeftOfShell(int x, int y, int shellIndex)
        {
            return ((x == -shellIndex) && (y != shellIndex));
        }

        /// <summary>
        /// Everything on the top side except the top left corner, which is part of "left side"
        /// </summary>
        private bool IsOnTopOfShell(int x, int y, int shellIndex)
        {
            return ((y == -shellIndex) && (x != -shellIndex));
        }
    }
}
