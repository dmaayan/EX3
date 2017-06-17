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
            // if the maze exist return it
            if (singlePlayerMazes.ContainsKey(name))
            {
                singlePlayerMazes.Remove(name);
            }
            // create new maze
            singlePlayerMazes.Add(name, maze);
            return maze;
        }

        /// <summary>
        /// solve a maze
        /// </summary>
        /// <param name="name">the name of the maze</param>
        /// <param name="algo">the algorithm to solve the maze with</param>
        /// <returns>the solution to the maze</returns>
        public MazeSolution Solve(string name, int algo)
        {
            // solve the maze and return the solution
            Maze maze = singlePlayerMazes[name];
            MazeAdapter mazeAdapter = new MazeAdapter(maze);
            MazeSolution ms;
            if (algo == 0)
            {
                ms = new MazeSolution(new BFS<Position>().Search(mazeAdapter), maze.Name);
            }
            else
            {
                ms = new MazeSolution(new DFS<Position>().Search(mazeAdapter), maze.Name);
            }
            return ms;
        }
    }
}