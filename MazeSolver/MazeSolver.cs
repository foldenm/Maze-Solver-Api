namespace MazeSolver
{
    public class MazeSolver
    {

        private static int[][] positionsToCheck =
        {
            new []{-1, 0}, new[]{1, 0}, new[]{0, -1}, new[]{0, 1}
        };

        public int Steps { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputMaze">The maze to be solved</param>
        /// <param name=""></param>
        /// <returns></returns>
        public Maze Walk(Maze inputMaze)
        {

            Steps = 0;

            // Clone the maze to avoid modifying the original
            var maze = (Maze)inputMaze.Clone();

            while (!maze.Get(0, 0).Equals(Maze.END))
            {

                var moved = false;

                foreach (var positionToCheck in positionsToCheck)
                {
                    var c = maze.Get(positionToCheck);
                    if (!c.Equals(Maze.OPEN) && !c.Equals(Maze.END))
                    {
                        continue;
                    }
                    maze.Move(positionToCheck);
                    moved = true;
                    break;
                }

                if (moved)
                {
                    Steps++;
                }
                else if (!maze.UnMove())
                {
                    return null;
                }
            }

            return maze;

        }
    }
}
