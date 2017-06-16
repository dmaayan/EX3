using MazeLib;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SearchAlgorithmsLib
{
    /// <summary>
    /// MazeSolution holds list of directions that represent the path of the soulution,
    /// the number of evaluated nodes, the name of the maze.
    /// </summary>
    public class MazeSolution
    {
        /// <summary>
        /// a list of directions
        /// </summary>
        private List<Direction> directions;
        /// <summary>
        /// nodes Evaluated
        /// </summary>
        private int nodesEvaluated;
        /// <summary>
        /// maze Name
        /// </summary>
        private string mazeName;

        /// <summary>
        /// constructor.
        /// </summary>
        /// <param name="s"> list of position </param>
        /// <param name="name">the name of the maze</param>
        public MazeSolution(Solution<Position> s, string name)
        {
            directions = new List<Direction>();
            SolutionToDirections(s);
            nodesEvaluated = s.NodesEvaluated;
            mazeName = name;
        }

        /// <summary>
        /// get a list of positions and transfer to a list of directions
        /// </summary>
        /// <param name="solution"> to transfer</param>
        private void SolutionToDirections(Solution<Position> solution)
        {
            List<State<Position>> positions = solution.GetSolution;
            State<Position> state = positions.First();
            Position pos = state.GetState();
            positions.Remove(state);
            // for each position at the list, check his direction
            while (positions.Count > 0)
            {
                state = state.CameFrom;
                positions.Remove(state);
                Position otherPos = state.GetState();
                // check right 
                if (pos.Col > otherPos.Col)
                {
                    directions.Add(Direction.Right);
                }
                // check left
                else if (pos.Col < otherPos.Col)
                {
                    directions.Add(Direction.Left);
                }
                // check down
                else if (pos.Row < otherPos.Row)
                {
                    directions.Add(Direction.Up);
                }
                // check up
                else if (pos.Row > otherPos.Row)
                {
                    directions.Add(Direction.Down);
                }
                // Unknown direction
                else
                {
                    directions.Add(Direction.Unknown);
                }
                pos = otherPos;
            }
            directions.Reverse();
        }

        /// <summary>
        /// transfer the maze solutions to json
        /// </summary>
        /// <returns> string in json </returns>
        public string ToJson()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("{");
            // add name
            sb.AppendLine("  \"Name\": \"" + mazeName + "\",");
            string direct = "";
            // add all directions to a string
            foreach(Direction d in directions)
            {
                direct += (int)d;
            }
            sb.AppendLine("  \"Solution\": \"" + direct + "\",");
            // add nodes Evaluated
            sb.AppendLine("  \"NodesEvaluated\" :" + nodesEvaluated);
            sb.AppendLine("}");
            return sb.ToString();
        }

        /// <summary>
        /// transfer the maze solutions from json
        /// </summary>
        /// <returns> string in json </returns>
        public static string FromJson(string solution)
        {
            Match match = Regex.Match(solution, "\"Solution\": \"(\\d*)", RegexOptions.Multiline);
            if (match.Success)
            {
                return match.Groups[1].ToString();
            }
            return null;
        }
    }
}
