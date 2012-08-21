using System.Drawing;
using Spirals;

namespace SpiralGeneration
{
    /// <summary>
    /// Fourth algorithm -- Akin to LightCycleGenerator, but replaces the "Side" concept (which
    /// originated with SparkGenerator) with a "Direction" concept more naturally suited to its 
    /// role in the algorithm.
    /// 
    /// I also implemented the generation of anti-clockwise spirals, to test my intuition that it
    /// would be straightforward.  It was.
    /// 
    /// I am still unhappy with the name "Bearings" for the combination of position and heading 
    /// (although it is technically correct).  Were this a real project, I would re-visit that name
    /// in a couple of weeks to see if I could improve either the name, or the object factoring 
    /// leading to the existence of a concept requiring such a name.
    /// </summary>
    public class LightCycleGeneratorImproved : ISpiralGenerator
    {
        public Spiral Generate(int spiralTo)
        {
            return PopulateSpiral(new Spiral(spiralTo));
        }

        private Spiral PopulateSpiral(Spiral spiral)
        {
            LightCycle lightCycle = new LightCycle(spiral);
            for (int i = 0; i < spiral.Numbers.Length; i++)
            {
                lightCycle.Drive();
            }
            return spiral;
        }

        internal enum Rotation
        {
            Left,
            Right
        }

        private class LightCycle
        {
            private Bearings Bearings { get; set; }
            private Rotation Rotation { get; set; }
            private Spiral Spiral { get; set; }
            private int NextValue { get; set; }

            public LightCycle(Spiral spiral, Direction leaveOriginDirection = null, Rotation rotation = Rotation.Right)
            {
                Spiral = spiral;
                Bearings = new Bearings(leaveOriginDirection ?? Direction.RIGHT, spiral.Origin);
                NextValue = 0;
                Rotation = rotation;
            }

            public void Drive()
            {
                Spiral.SetValueAbsolute(Bearings.Position.X, Bearings.Position.Y, NextValue++);

                if (Bearings.Position == Spiral.Origin)
                {
                    Bearings = Bearings.Move();
                }
                else
                {
                    Bearings newBearingsIfTurning = Bearings.MoveWithRotation(Rotation);
                    Bearings = IsOccupied(newBearingsIfTurning.Position) ? Bearings.Move() : newBearingsIfTurning;
                }
            }

            private bool IsOccupied(Point position)
            {
                return ((Spiral.GetValueAbsolute(position.X, position.Y) != 0) || (position == Spiral.Origin));
            }
        }

        internal class Bearings
        {
            public Point Position;
            public Direction Direction;

            public Bearings(Direction direction, Point position)
            {
                Direction = direction;
                Position = position;
            }

            public Bearings Move()
            {
                return new Bearings(Direction, Direction.MoveAhead(Position));
            }

            public Bearings MoveWithRotation(Rotation rotation)
            {
                Direction newDirection = Direction.Rotate(rotation);
                return new Bearings(newDirection, newDirection.MoveAhead(Position));
            }
        }

        internal class Direction
        {
            static Direction()
            {
                RIGHT = new Direction();
                LEFT = new Direction();
                DOWN = new Direction();
                UP = new Direction();
                
                RIGHT._deltaX = 1;
                RIGHT._deltaY = 0;
                RIGHT._leftTurn = UP;
                RIGHT._rightTurn = DOWN;

                LEFT._deltaX = -1;
                LEFT._deltaY = 0;
                LEFT._leftTurn = DOWN;
                LEFT._rightTurn = UP;

                DOWN._deltaX = 0;
                DOWN._deltaY = 1;
                DOWN._leftTurn = RIGHT;
                DOWN._rightTurn = LEFT;

                UP._deltaX = 0;
                UP._deltaY = -1;
                UP._leftTurn = LEFT;
                UP._rightTurn = RIGHT;
            }

            public static readonly Direction RIGHT;
            public static readonly Direction LEFT;
            public static readonly Direction DOWN;
            public static readonly Direction UP;

            private int _deltaX;
            private int _deltaY;
            private Direction _leftTurn;
            private Direction _rightTurn;

            private Direction()
            {
            }

            public Point MoveAhead(Point point)
            {
                return new Point(point.X + _deltaX, point.Y + _deltaY);
            }
            
            public Direction Rotate(Rotation rotation)
            {
                return ((rotation == Rotation.Left) ? _leftTurn : _rightTurn);
            }
        }
    }
}
