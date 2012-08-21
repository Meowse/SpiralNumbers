using System;
using System.Diagnostics;
using System.Drawing;

namespace Spirals
{
    public class Spiral
    {
        public int[,] Numbers { get; set; }
        public int SpiralTo { get; set; }
        public Point Origin { get; private set; }
        public int Size { get; set; }

        public Spiral(int spiralTo)
        {
            Size = GetSpiralSize(spiralTo);
            Numbers = new int[Size,Size];
            Origin = new Point(Size/2, Size/2);
            SpiralTo = spiralTo;
        }

        public Spiral(int[,] numbers, int spiralTo)
        {
            Debug.Assert(numbers.Rank == 2, "Spiral can only be initialized with a two-dimensional array.");
            Debug.Assert(numbers.GetLength(0) == numbers.GetLength(1),
                         "Spiral can only be initialized with a square, two-dimensional array.");
            Numbers = numbers;
            Size = numbers.GetLength(0);
            Origin = new Point(Size/2, Size/2);
            SpiralTo = spiralTo;
        }

        private static int GetSpiralSize(int spiralTo)
        {
            int width = (int) Math.Floor(Math.Sqrt(spiralTo)) + 1;
            if (width%2 == 0)
            {
                width++;
            }
            return width;
        }


        /// <summary>
        /// Gets the value in absolute coordinates, where 0,0 is the upper left corner
        /// of the Numbers array.  Also handles the conversion between (x,y) and the 
        /// [row, column] indexing of the Numbers array.
        /// </summary>
        public int GetValueAbsolute(int absoluteX, int absoluteY)
        {
            return Numbers[absoluteY, absoluteX];
        }

        /// <summary>
        /// Sets the value in absolute coordinates, where 0,0 is the upper left corner
        /// of the Numbers array.  Also handles the conversion between (x,y) and the 
        /// [row, column] indexing of the Numbers array, and writes -1 to the array
        /// for values greater than SpiralTo.
        /// </summary>
        public void SetValueAbsolute(int absoluteX, int absoluteY, int value)
        {
            Numbers[absoluteY, absoluteX] = UpToLimit(value);
        }

        /// <summary>
        /// Gets the value in relative coordinates, where 0,0 is the origin of the spiral
        /// (spiral value == 0).  Also handles the conversion between (x,y) and the 
        /// [row, column] indexing of the Numbers array.
        /// </summary>
        public int GetValueRelative(int x, int y)
        {
            return Numbers[y + Origin.Y, x + Origin.X];
        }

        /// <summary>
        /// Gets the value in relative coordinates, where 0,0 is the origin of the spiral
        /// (spiral value == 0).  Also handles the conversion between (x,y) and the 
        /// [row, column] indexing of the Numbers array, and writes -1 to the array
        /// for values greater than SpiralTo.
        /// </summary>
        public void SetValueRelative(int x, int y, int value)
        {
            Numbers[y + Origin.Y, x + Origin.X] = UpToLimit(value);
        }

        private int UpToLimit(int i)
        {
            return ((i <= SpiralTo) ? i : -1);
        }
    }
}