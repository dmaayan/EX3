using System.Collections.Generic;
using System.Linq;
using Priority_Queue;

namespace SearchAlgorithmsLib
{
    /// <summary>
    /// PrioritySearcher is an abstract class inherit from Searcher.
    /// had Dictionary of updated states, and priority queue.
    /// </summary>
    /// <typeparam name="T">the type of object to search</typeparam>
    public abstract class PrioritySearcher<T> : Searcher<T>
    {
        /// <summary>
        /// the open list that states have not been closed yet
        /// </summary>
        private SimplePriorityQueue<State<T>> openList;
        /// <summary>
        /// updated came from and cost for each state
        /// </summary>
        private Dictionary<State<T>, KeyValuePair<State<T>, double>> updatedStates;

        /// <summary>
        ///  constuctor
        /// </summary>
        public PrioritySearcher()
        {
            openList = new SimplePriorityQueue<State<T>>();
            updatedStates = new Dictionary<State<T>, KeyValuePair<State<T>, double>>();
        }

        /// <summary>
        /// a property of openList 
        /// </summary>
        public SimplePriorityQueue<State<T>> OpenList
        {
            get { return openList; }
        }

        // a property of openList Count
        public int OpenListSize
        {
            get { return openList.Count; }
        }

        /// <summary>
        /// a property of updatedStates
        /// </summary>
        public Dictionary<State<T>, KeyValuePair<State<T>, double>> UpdatedStates
        {
            get { return updatedStates; }
        }

        /// <summary>
        /// pop state from open list, increase the evaluated nodes
        /// </summary>
        /// <returns>the state that poped</returns>
        protected State<T> PopOpenList()
        {
            IncreaseEvaluatedNodes();
            return openList.Dequeue();
        }

        /// <summary>
        /// add state to UpdatedStates, enqueue from openList
        /// </summary>
        /// <param name="state">is the state to add</param>
        public void AddToOpenList(State<T> state)
        {
            openList.Enqueue(state, (float)(state.Cost));
            UpdatedStates.Add(state, new KeyValuePair<State<T>, double>(state.CameFrom, state.Cost));
        }

        /// <summary>
        /// UpdateStateIfPathBetter.
        /// </summary>
        /// <param name="current">to check</param>
        public void UpdateStateIfPathBetter(State<T> current)
        {
            // find the state at the open list
            State<T> state = openList.Where(elem => current.Equals(elem)).First();

            double costOfState = updatedStates[state].Value;
            // if the currend cost is better, update his cost
            if (current.Cost < costOfState)
            {
                updatedStates[state] = new KeyValuePair<State<T>, double>
                                       (current.CameFrom, current.Cost);
                openList.UpdatePriority(state, (float)current.Cost);
            }

        }
        /// <summary>
        /// Searcher's abstract method overriding
        /// </summary>
        /// <param name="searchable">to search on</param>
        /// <returns>the solution</returns>
        public override abstract Solution<T> Search(ISearchable<T> searchable);
    }
}
