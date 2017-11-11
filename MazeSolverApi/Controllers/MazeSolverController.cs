using System;
using System.Web.Http;

using Solver = MazeSolver;

namespace MazeSolverApi.Controllers
{

    [RoutePrefix("MazeSolver")]
    public class MazeSolverController : ApiController
    {
        [HttpPost]
        [Route("solveMaze")]
        public IHttpActionResult Post([FromBody]string map)
        {
            try
            {
                var lines = (map ?? string.Empty).Split(new[] {'\r', '\n'}, StringSplitOptions.RemoveEmptyEntries);
                if (lines.Length < 1)
                {
                    return BadRequest("Invalid maze");
                }

                var maze = new Solver.Maze(lines);
                var solver = new Solver.MazeSolver();
                var solvedMaze = solver.Walk(maze);
                return solvedMaze == null
                    ? Ok(new {steps = solver.Steps, solution = "No solution found"})
                    : Ok(new {steps = solver.Steps, solution = solvedMaze.ToString()});
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
