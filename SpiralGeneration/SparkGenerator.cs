using System.Drawing;
using Spirals;

namespace SpiralGeneration
{
    /// <summary>
    /// Second algorithm -- I noticed that the number of elements written on each "side" of each square 
    /// increased by two for each successive shell of the spiral.  And algorithm based on this would be
    /// considerably faster and more comprehensible than the original, although it would not allow determination
    /// of the spiral value at a particular coordinate without calculating the spiral to that point.
    /// 
    /// The "Spark" class is named for the concept of a "spark" burning along the "fuse" of the spiral.
    /// It knows how to advance along the spiral, and internally tracks the state information needed to 
    /// calculate its next move.
    /// </summary>
    public class SparkGenerator : ISpiralGenerator
    {
        public Spiral Generate(int spiralTo)
        {
            return PopulateSpiral(new Spiral(spiralTo));
        }

        private enum Side
        {
            Top,
            Right,
            Bottom,
            Left
        }

        private Spiral PopulateSpiral(Spiral spiral)
        {
            Spark spark = new Spark(Side.Top, spiral.Origin, 0, 0, spiral);
            for (int i = 0; i < spiral.Numbers.Length; i++)
            {
                spark.Advance();
            }
            return spiral;
        }

        private class Spark
        {
            public Spark(Side side, Point position, int sideLength, int drawnThisSide, Spiral spiral)
            {
                Side = side;
                Position = position;
                SideLength = sideLength;
                AmountDrawnThisSide = drawnThisSide;
                Spiral = spiral;
                NextValue = 0;
            }

            private Side Side { get; set; }
            private Point Position { get; set; }
            private int SideLength { get; set; }
            private int AmountDrawnThisSide { get; set; }
            private Spiral Spiral { get; set; }
            private int NextValue { get; set; }

            public void Advance()
            {
                Spiral.SetValueAbsolute(Position.X, Position.Y, NextValue++);
                AmountDrawnThisSide++;

                if (AmountDrawnThisSide >= SideLength)
                {
                    SideLength = GetNextSideLength(Side, SideLength);
                    Position = GetStartingPositionOfNextSide(Side, Position);
                    Side = GetNextSide(Side);
                    AmountDrawnThisSide = 0;
                }
                else
                {
                    Position = GetNextPositionOnCurrentSide(Side, Position);
                }
            }

            private Point GetNextPositionOnCurrentSide(Side currentSide, Point position)
            {
                switch (currentSide)
                {
                    case Side.Top: return new Point(position.X + 1, position.Y);
                    case Side.Right: return new Point(position.X, position.Y + 1);
                    case Side.Bottom: return new Point(position.X - 1, position.Y);
                    default: return new Point(position.X, position.Y - 1);
                }
            }

            private static int GetNextSideLength(Side lastSide, int currentSideLength)
            {
                return (lastSide == Side.Top) ? currentSideLength + 2 : currentSideLength;
            }

            private Point GetStartingPositionOfNextSide(Side lastSide, Point position)
            {
                switch (lastSide)
                {
                    case Side.Top: return new Point(position.X + 1, position.Y);
                    case Side.Right: return new Point(position.X - 1, position.Y);
                    case Side.Bottom: return new Point(position.X, position.Y - 1);
                    default: return new Point(position.X + 1, position.Y);
                }
            }

            private Side GetNextSide(Side currentSide)
            {
                switch (currentSide)
                {
                    case Side.Top: return Side.Right;
                    case Side.Right: return Side.Bottom;
                    case Side.Bottom: return Side.Left;
                    default: return Side.Top;
                }
            }
        }        
    }
}
