using System;
using Spirals;

namespace SpiralGeneration
{
    /// <summary>
    /// Eighth algorithm -- Putting it all together.  This version extracts the shell functionality
    /// into a Shell class which provides random access as well as fast drawing capabilities.
    /// The generator just creates the relevant shells (either all of the shells needed to draw the
    /// spiral, or the shell on which the random-access coordinates fall) and calls them to 
    /// render or return the correct values.
    /// 
    /// I'm satisfied with this algorithm.  It's not quite as intuitive as LightCycleGeneratorImproved,
    /// and it's a bit "mathy" in places, but it's got an elegant design, it's very fast for both
    /// full spiral generation and random access, and it does minimal redundant calculations.
    /// 
    /// I chose not to cache the Shells in the generator, because it would constitute premature optimization.
    /// Depending on usage patterns, we should cache at the instance level or the class level, we should 
    /// use a synchronized (thread-safe) cache or a non-synchronized (non-thread-safe) cache, or we should
    /// not cache at all.  Adding caching is easy enough if the cost of creating Shell instances becomes 
    /// large relative to the cost of rendering spirals or calculating random-access values.
    /// </summary>
    public class SpiralGenerator : IRandomAccessSpiralGenerator
    {
        public Spiral Generate(int spiralTo)
        {
            return PopulateSpiral(new Spiral(spiralTo));
        }

        private Spiral PopulateSpiral(Spiral spiral)
        {
            for (int shellIndex = 1; shellIndex < spiral.Size/2 + 1; shellIndex++)
            {
                new Shell(shellIndex).DrawOn(spiral);
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
            return new Shell(shellIndex).GetValueAt(x, y);
        }
    }

    internal class Shell
    {
        private readonly int _index;
        private readonly int _sideLength;

        private readonly int _rightStartValue;
        private readonly int _rightStartY;

        private readonly int _bottomStartValue;
        private readonly int _bottomStartX;

        private readonly int _leftStartValue;
        private readonly int _leftStartY;

        private readonly int _topStartValue;
        private readonly int _topStartX;

        public Shell(int shellIndex)
        {
            _index = shellIndex;
            _sideLength = 2 * _index;
            
            _rightStartValue = (_sideLength - 1) * (_sideLength - 1);
            _rightStartY = -_index + 1;

            _bottomStartValue = _rightStartValue + _sideLength;
            _bottomStartX = _index - 1;

            _leftStartValue = _bottomStartValue + _sideLength;
            _leftStartY = _index - 1;

            _topStartValue = _leftStartValue + _sideLength;
            _topStartX = -_index + 1;
        }

        public void DrawOn(Spiral spiral)
        {
            DrawRightOn(spiral);
            DrawBottomOn(spiral);
            DrawLeftOn(spiral);
            DrawTopOn(spiral);
        }

        private void DrawRightOn(Spiral spiral)
        {
            for (int i = 0; i < _sideLength; i++)
            {
                spiral.SetValueRelative(_index, _rightStartY + i, _rightStartValue + i);
            }
        }

        private void DrawBottomOn(Spiral spiral)
        {
            for (int i = 0; i < _sideLength; i++)
            {
                spiral.SetValueRelative(_bottomStartX - i, _index, _bottomStartValue + i);
            }
        }

        private void DrawLeftOn(Spiral spiral)
        {
            for (int i = 0; i < _sideLength; i++)
            {
                spiral.SetValueRelative(-_index, _leftStartY - i, _leftStartValue + i);
            }
        }

        private void DrawTopOn(Spiral spiral)
        {
            for (int i = 0; i < _sideLength; i++)
            {
                spiral.SetValueRelative(_topStartX + i, -_index, _topStartValue + i);
            }
        }

        public int GetValueAt(int x, int y)
        {
            if (IsOnRight(x, y))
            {
                return _rightStartValue + (y - _rightStartY);
            }
            if (IsOnBottom(x, y))
            {
                return _bottomStartValue + (_bottomStartX - x);
            }
            if (IsOnLeft(x, y))
            {
                return _leftStartValue + (_leftStartY - y);
            }
            if (IsOnTop(x, y))
            {
                return _topStartValue + (x - _topStartX);
            }
            throw new Exception(string.Format("Coordinates ({0},{1}) are not in shell {2}.", x, y, _index));
        }

        /// <summary>
        /// Everything on the right-hand side except the top right corner, which is part of "top".
        /// </summary>
        private bool IsOnRight(int x, int y)
        {
            return ((x == _index) && (y != -_index));
        }

        /// <summary>
        /// Everything on the bottom side except the bottom right corner, which is part of "right".
        /// </summary>
        private bool IsOnBottom(int x, int y)
        {
            return ((y == _index) && (x != _index));
        }

        /// <summary>
        /// Everything on the left-hand side except the bottom left corner, which is part of "bottom".
        /// </summary>
        private bool IsOnLeft(int x, int y)
        {
            return ((x == -_index) && (y != _index));
        }

        /// <summary>
        /// Everything on the top side except the top left corner, which is part of "left side"
        /// </summary>
        private bool IsOnTop(int x, int y)
        {
            return ((y == -_index) && (x != -_index));
        }
    }
}
