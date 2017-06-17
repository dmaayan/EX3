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
        private static KeyValuePair<string, Maze> singlePlayerMaze = new KeyValuePair<string, Maze>("", null);

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
            // if the maze exist return it
            if (singlePlayerMaze.Key.Equals(name))
            {
                return singlePlayerMaze.Value;
            }
            DFSMazeGenerator mazeGenerator = new DFSMazeGenerator();
            Maze maze = mazeGenerator.Generate(rows, cols);
            maze.Name = name;
            // create new maze
            singlePlayerMaze = new KeyValuePair<string, Maze>(name, maze);
            return maze;
        }

        /// <summary>
        /// solve a maze
        /// </summary>
        /// <param name="name">the name of the maze</param>
        /// <returns>the solution to the maze</returns>
        public MazeSolution Solve(int algo)
        {
            // solve the maze and return the solution
            Maze maze = singlePlayerMaze.Value;
            MazeAdapter mazeAdapter = new MazeAdapter(maze);
            if (algo == 0)
            {
                return new MazeSolution(new BFS<Position>().Search(mazeAdapter), maze.Name);
            }
            else
            {
                return new MazeSolution(new DFS<Position>().Search(mazeAdapter), maze.Name);
            }
        }
    }
}