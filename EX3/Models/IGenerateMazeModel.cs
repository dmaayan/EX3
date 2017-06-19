using MazeLib;
using SearchAlgorithmsLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EX3.Models
{
    /// <summary>
    /// interface for single player model
    /// </summary>
    public interface IGenerateMazeModel
    {
        /// <summary>
        /// Generate a Maze
        /// </summary>
        /// <param name="name">the name of the maze </param>
        /// <param name="rows">the rows of the maze </param>
        /// <param name="cols">the cols of the maze </param>
        /// <returns>the new maze</returns>
        Maze Generate(string name, int rows, int cols);

        /// <summary>
        /// solve a maze
        /// </summary>
        /// <param name="name">the name of the maze</param>
        /// <param name="algo">the algorithm to solve the maze with</param>
        /// <returns>the solution to the maze</returns>
        MazeSolution Solve(string name, int algo);
    }
}