﻿using MazeGeneratorLib;
using MazeLib;
using SearchAlgorithmsLib;
using System.Collections.Generic;

namespace EX3.Models
{
    public class GenerateMazeModel : IGenerateMazeModel
    {
        /// <summary>
        /// all the single game mazes
        /// </summary>
        private static Dictionary<string, Maze> singlePlayerMazes = new Dictionary<string, Maze>();

        /// <summary>
        /// Generate a Maze
        /// </summary>
        /// <param name="name">the name of the maze </param>
        /// <param name="rows">the rows of the maze </param>
        /// <param name="cols">the cols of the maze </param>
        /// <returns>the new maze</returns>
        public Maze Generate(string name, int rows, int cols)
        { 
            // generate a maze
            DFSMazeGenerator mazeGenerator = new DFSMazeGenerator();
            Maze maze = mazeGenerator.Generate(rows, cols);
            maze.Name = name;
            // if the maze exist close it
            if (singlePlayerMazes.ContainsKey(name))
            {
                singlePlayerMazes.Remove(name);
            }
            // add the new maze
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
            // solve with the correct algorithm
            if (algo == 0)
            {
                ms = new MazeSolution(new BFS<Position>().Search(mazeAdapter), maze.Name);
            }
            else
            {
                ms = new MazeSolution(new DFS<Position>().Search(mazeAdapter), maze.Name);
            }
            // return the solution
            return ms;
        }
    }
}