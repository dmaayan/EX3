using MazeGeneratorLib;
using MazeLib;
using SearchAlgorithmsLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EX3.Models
{
    public class GenerateMazeModel : IGenerateMazeModel
    {
        /// <summary>
        /// all the single game mazes
        /// </summary>
        private static Dictionary<string, Maze> singlePlayerMazes = new Dictionary<string, Maze>();

        /// <summary>
        /// all the solutions to the single game mazes
        /// </summary>
        private static Dictionary<string, MazeSolution> 
            singlePlayerSolved = new Dictionary<string, MazeSolution>();

        /// <summary>
        /// Generate a Maze, if the maze exist close it and create new maze
        /// </summary>
        /// <param name="name">the name of the maze </param>
        /// <param name="rows">the rows of the maze </param>
        /// <param name="cols">the cols of the maze </param>
        /// <returns>the new maze</returns>
        public Maze Generate(string name, int rows, int cols)
        {
            DFSMazeGenerator mazeGenerator = new DFSMazeGenerator();
            Maze maze = mazeGenerator.Generate(rows, cols);
            maze.Name = name;
            // if the maze exist close it
            if (singlePlayerMazes.ContainsKey(name))
            {
                CloseSingleGame(name);
            }
            // create new maze
            singlePlayerMazes.Add(name, maze);
            return maze;
        }

        public MazeSolution Solve(string name, int algo)
        {
            return null;
        }
    }
}