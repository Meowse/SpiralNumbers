using System.Drawing;
using Spirals;

namespace SpiralGeneration
{
    /// <summary>
    /// Third algorithm -- I was unsatisfied with the clarity of the SparkGenerator, and wanted
    /// to create an even more intuitive algorithm.  It occurred to me that the process of drawing a
    /// spiral is a lot like the behavior of "light cycles" from the movie "Tron", and that the spiral
    /// generation algorithm simply needed to attempt to "turn right" constantly, and to go straight
    /// ahead only when the square "to the right" of the current position had already been filled.
    /// 
    /// Since filled squares can be easily identified by either (a) being the origin, or (b) having a
    /// non-zero value in them, it's simply to determine, for each move, whether it is possible to turn
    /// right, or necessary to go straight.
    /// 
    /// This algorithm has the advantage of trivially allowing the generation of anti-clockwise spirals,
    /// simply by extending the LightCycle class to accept a "turn direction" parameter and slightly modifying
    /// the move calculation logic.
    /// </summary>
    public class LightCycleGenerator : ISpiralGenerator
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
            LightCycle lightCycle = new LightCycle(spiral);
            for (int i = 0; i < spiral.Numbers.Length; i++)
            {
                lightCycle.DriveClockwise();
            }
            return spiral;
        }

        private class LightCycle
        {
            public LightCycle(Spiral spiral)
            {
                Side = Side.Top;
                Position = spiral.Origin;
                Spiral = spiral;
                NextValue = 0;
            }

            private Side Side { get; set; }
            private Point Position { get; set; }
            private Spiral Spiral { get; set; }
            private int NextValue { get; set; }

            public void DriveClockwise()
            {
                Spiral.SetValueAbsolute(Position.X, Position.Y, NextValue++);

                if ((NextValue == 1) || RightTurnIsBlocked())
                {
                    Position = GetStraightAheadPosition();
                }
                else
                {
                    Position = GetTurnRightPosition();
                    Side = GetNextSide();
                }
            }

            private bool RightTurnIsBlocked()
            {
                return !IsEmpty(GetTurnRightPosition());
            }

            private bool IsEmpty(Point position)
            {
                return ((Spiral.GetValueAbsolute(position.X, position.Y) == 0) && (position != Spiral.Origin));
            }

            private Point GetTurnRightPosition()
            {
                return GetMoveForSide(GetNextSide());
            }

            private Point GetStraightAheadPosition()
            {
                return GetMoveForSide(Side);
            }

            private Point GetMoveForSide(Side side)
            {
                switch (side)
                {
                    case Side.Top: return new Point(Position.X + 1, Position.Y);
                    case Side.Right: return new Point(Position.X, Position.Y + 1);
                    case Side.Bottom: return new Point(Position.X - 1, Position.Y);
                    default: return new Point(Position.X, Position.Y - 1);
                }
            }

            private Side GetNextSide()
            {
                switch (Side)
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
