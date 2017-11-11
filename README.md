# Maze-Solver-Api
WebApi endpoint allows solving of user supplied mazes with a starting point and a destination ending point
### Prerequisites
Consists of a VS 2017 solution and three projects.
1) A maze class for handling the maze object
2) A Web Api class containing the MazeSolver/solveMaze endpoint on port 8080
3) A console application that can optionally be used to test the Web Api
The Web Api project uses the Microsoft Web Api Client package,
which may require download and installation from Nuget,
as well as the NewtonSoft Json package.

## Testing

Run the MazeSolverApi project in VS 2017.
It will load up in the default browser as localhost on port 8080.

Run the MazeTest console application.
It will accept a command line parameter indicating a maze file name to load.
Currently, Maze1.txt, Maze2.txt and Maze3.txt are available.

The console application will load the text file, call the API
and display the results.

## Authors

* **Michael Folden**

## Acknowledgments

* Various maze solving algorithms were examined.
* The one chosen was located on GitHub and works exceptionally well.

