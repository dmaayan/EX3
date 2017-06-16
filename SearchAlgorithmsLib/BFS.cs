using System;
using System.Collections.Generic;

namespace SearchAlgorithmsLib
{
    /// <summary>
    /// BFS, Best First search, is a generic algoritem for Search problems.
    /// inherit from PrioritySearcher, had a locker.
    /// </summary>
    /// <typeparam name="T">the type of object to search</typeparam>
    public class BFS<T> : PrioritySearcher<T>
    {
        /// <summary>
        /// locker for threads
        /// </summary>
        private Object locker = new Object();
        /// <summary>
        /// locker for threads
        /// </summary>
        private Object locker2 = new Object();

        /// <summary>
        /// Searcher's abstract method overriding 
        /// </summary>
        /// <param name="searchable"> to search on </param>
        /// <returns>the solution that BFS found</returns>
        public override Solution<T> Search(ISearchable<T> searchable)
        {
            lock (locker2)
            {
                AddToOpenList(searchable.GetInitialState());
            }
            HashSet<State<T>> closed = new HashSet<State<T>>();
            State<T> goal = searchable.GetGoalState();

            while (OpenListSize > 0)
            {
                State<T> state = PopOpenList();         // removes the best state
                closed.Add(state);                      // add it to the closed hash

                if (state.Equals(goal))
                {
                    return BackTrace(goal);             // back traces through the parents
                }
                
                lock (locker)
                {
                    // returns a list of states with state as a parent
                    List<State<T>> succerssors = searchable.GetAllPossibleStates(state);
                    foreach (State<T> s in succerssors)
                    {
                        if (!closed.Contains(s) && !OpenList.Contains(s))
                        {
                            s.CameFrom = state;
                            AddToOpenList(s);
                        }
                        // the cost of the new way is better
                        else if (OpenList.Contains(s))
                        {
                            s.CameFrom = state;
                            UpdateStateIfPathBetter(s);
                        }
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// BackTrace is a privat method that calc the Solution of the problem.
        /// </summary>
        /// <param name="goal">from where to go back </param>
        /// <returns>the path - the solution </returns>
        private Solution<T> BackTrace(State<T> goal)
        {
            List<State<T>> solution = new List<State<T>>();
            State<T> state = goal;
            // for each state, add the state that he came from, until the surce
            while (state != null)
            {
                solution.Add(state);
                state = UpdatedStates[state].Key;
            }
            return new Solution<T>(solution, GetNumberOfNodesEvaluated());
        }
    }
}

