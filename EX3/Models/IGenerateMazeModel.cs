using MazeLib;
using SearchAlgorithmsLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EX3.Models
{
    public interface IGenerateMazeModel
    {
        Maze Generate(string name, int rows, int cols);

        MazeSolution Solve(int algo);
    }
}