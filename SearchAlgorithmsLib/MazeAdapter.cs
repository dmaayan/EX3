using System.Collections.Generic;
using MazeLib;

namespace SearchAlgorithmsLib
{
    /// <summary>
    /// MazeAdapter, make adaptation from maze to ISearchable 
    /// </summary>
    public class MazeAdapter : ISearchable<Position>
    {
        /// <summary>
        /// the maze
        /// </summary>
        private Maze maze;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="m">is the maze to save</param>
        public MazeAdapter(Maze m)
        {
            maze = m;
        }

        /// <summary>
        /// get the Initial State, apdate the cost to zero.
        /// </summary>
        /// <returns>the state to start from</returns>
        public State<Position> GetInitialState()
        {
            State<Position> state = State<Position>.StatePool.GetState(maze.InitialPos);
            state.Cost = 0;
            state.CameFrom = null;
            return state;
        }

        /// <summary>
        /// get the goal state, apdate the cost to one.
        /// </summary>
        /// <returns>the state that beeing search</returns>
        public State<Position> GetGoalState()
        {
            State<Position> state = State<Position>.StatePool.GetState(maze.GoalPos);
            state.Cost = 1;
            return state;
        }

        /// <summary>
        /// check all the direction and found the possible states.
        /// </summary>
        /// <param name="s">the state to look from</param>
        /// <returns>list of Possible States from s </returns>
        public List<State<Position>> GetAllPossibleStates(State<Position> position)
        {
            List<State<Position>> neighbors = new List<State<Position>>();

            int row = position.GetState().Row;
            int col = position.GetState().Col;
            State<Position> state;

            // check down 
            if ((maze.Rows > row + 1) && (maze[row + 1, col] == CellType.Free))
            {
                state = State<Position>.StatePool.GetState(new Position(row + 1, col));
                state.Cost = position.Cost + 1;
                neighbors.Add(state);
            }
            // check up
            if ((row > 0) && (maze[row - 1, col] == CellType.Free))
            {
                state = State<Position>.StatePool.GetState(new Position(row - 1, col));
                state.Cost = position.Cost + 1;
                neighbors.Add(state);
            }
            // check left 
            if ((col > 0) && (maze[row, col - 1] == CellType.Free))
            {
                state = State<Position>.StatePool.GetState(new Position(row, col - 1));
                state.Cost = position.Cost + 1;
                neighbors.Add(state);
            }
            // check right 
            if ((maze.Cols > col + 1) && (maze[row, col + 1] == CellType.Free))
            {
                state = State<Position>.StatePool.GetState(new Position(row, col + 1));
                state.Cost = position.Cost + 1;
                neighbors.Add(state);
            }
            return neighbors;
        }
    }
}
