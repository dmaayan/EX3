using System.Collections.Generic;
using System.Linq;

namespace SearchAlgorithmsLib
{
    /// <summary>
    /// DFS, Best First search, is a generic algoritem for Search problems.
    /// inherit from NonPrioritySearcher, had an hasSet.
    /// </summary>
    /// <typeparam name="T">the type of object to search</typeparam>
    public class DFS<T> : NonPrioritySearcher<T>
    {
        /// <summary>
        /// the visited list
        /// </summary>
        private HashSet<State<T>> visited;

        /// <summary>
        /// Searcher's abstract method overriding 
        /// </summary>
        /// <param name="searchable"> to search on </param>
        /// <returns>the solution that DFS found </returns>
        public override Solution<T> Search(ISearchable<T> searchable)
        {
            visited = new HashSet<State<T>>();
            // get initial and goal states
            State<T> state = searchable.GetInitialState();
            State<T> goal = searchable.GetGoalState();
            Stack<State<T>> stack = new Stack<State<T>>();
            // add initial to the stack and update the parents
            stack.Push(state);
            state.CameFrom = null;
            Parents.Add(state, null);

            // while we have items and havn't reached the goal
            while (stack.Count > 0)
            {
                IncreaseEvaluatedNodes();
                state = stack.Pop();
                // check if reached the goal
                if (goal.Equals(state))
                {
                    return Backtrace(goal);
                }
                // checks if already seen this state
                if (!visited.Contains(state))
                {
                    // add the state to the visited hashset
                    visited.Add(state);
                    // for all neighbours of the state which havn't een visited yet
                    foreach (State<T> neighbour in searchable.GetAllPossibleStates(state).Where
                                                   (elem => !visited.Contains(elem)))
                    {
                        //add to the stack, update comefrom and add to parents
                        stack.Push(neighbour);
                        neighbour.CameFrom = state;
                        Parents.Add(neighbour, state);
                    }
                }
            }
            // didn't reach goal state
            return null;
        }

        /// <summary>
        /// BackTrace is a privat method that syndicate the Solution of the problem.
        /// </summary>
        /// <param name="goal">from where to go back</param>
        /// <returns>the path - the solution</returns>
        private Solution<T> Backtrace(State<T> goal)
        {
            List<State<T>> solution = new List<State<T>>();
            State<T> state = goal;
            // for each state, add the state that he came from, until the initial
            while (state != null)
            {
                solution.Add(state);
                state = Parents[state];
            }
            return new Solution<T>(solution, GetNumberOfNodesEvaluated());
        }
    }
}
