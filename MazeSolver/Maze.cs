using System;
using System.Collections.Generic;
using System.Text;

namespace MazeSolver
{
    /// <summary>
    /// Maze object contains the maze itself and methods for moving within it.
    /// </summary>
    public class Maze : ICloneable
    {

        public const char START = 'A';
        public const char END = 'B';
        public const char OPEN = '.';
        public const char CLOSED = '#';
        public const char GOOD = '@';
        public const char BAD = 'x';

        private readonly List<List<char>> _maze = new List<List<char>>();
        private readonly Stack<int[]> _positions = new Stack<int[]>();
        private int[] _position = { 0, 0 };

        /// <summary>
        /// Default constructor creates an empty maze object
        /// </summary>
        public Maze()
        {
        }

        /// <summary>
        /// Constructor 
        /// </summary>
        /// <param name="lines"></param>
        public Maze(IReadOnlyCollection<string> lines)
        {
            if (lines.Count <= 0)
            {
                return;
            }
            _maze.Capacity = lines.Count; // Preallocate capacity because we know how large the list will be.
            var currentRow = 0;
            foreach (var line in lines)
            {
                var n = line.Length;
                _maze.Add(new List<char>(n)); // Preallocate capacity because we know how long the line is.
                for (var i = 0; i < n; i++)
                {
                    var c = line[i];
                    _maze[currentRow].Add(c);
                    if (c.Equals(START))
                    {
                        _position = new[] { currentRow, i };
                    }
                }
                currentRow++;
            }
        }

        public object Clone()
        {
            return MemberwiseClone();
        }

        private void _Set(int x, int y, char c)
        {
            _maze[x][y] = c;
        }

        private char _GetAbsolute(int x, int y)
        {
            return x < 0 ||
                   y < 0 ||
                   x > _maze.Count - 1 ||
                   y > _maze[x].Count - 1
                   ? CLOSED
                   : _maze[x][y];
        }

        public char Get(int[] pair)
        {
            return Get(pair[0], pair[1]);
        }

        public char Get(int offsetX, int offsetY)
        {
            return _GetAbsolute(_position[0] + offsetX, _position[1] + offsetY);
        }

        public bool Move(int[] pair)
        {
            return _Move(pair[0], pair[1]);
        }

        private bool _Move(int offsetX, int offsetY)
        {
            _positions.Push(_position);
            _position = new[] { _position[0] + offsetX, _position[1] + offsetY };
            _CheckState();
            if (Get(0, 0).Equals(END))
            {
                return true;
            }
            _Set(_position[0], _position[1], GOOD);
            return false;
        }

        public bool UnMove()
        {
            _Set(_position[0], _position[1], BAD);
            if (_positions.Count == 0)
            {
                return false;
            }
            _position = _positions.Pop();
            return true;
        }

        private void _CheckState()
        {
            var currentChar = Get(0, 0);
            if (!(currentChar.Equals(START) ||
                  currentChar.Equals(END) ||
                  currentChar.Equals(OPEN)))
            {
                throw new Exception("Position " + _position[0] + "," + _position[1] + " is occupied");
            }
        }

        public override string ToString()
        {
            var n = _maze.Count;
            var result = new StringBuilder(n * n + 1);
            foreach (var line in _maze)
            {
                foreach (var c in line)
                {
                    result.Append(c.Equals(BAD) ? OPEN : c);
                }
                result.AppendLine();
            }
            return result.ToString();
        }
    }
}
