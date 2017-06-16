
using System.Collections.Generic;

namespace SearchAlgorithmsLib
{
    /// <summary>
    /// ISearchable is an interface for problems that need to be solve with search in them
    /// has three methods.
    /// </summary>
    /// <typeparam name="T">is the object to search</typeparam>
    public interface ISearchable<T>
    {
        /// <summary>
        /// GetInitialState
        /// </summary>
        /// <returns>the state to start from</returns>
        State<T> GetInitialState();

        /// <summary>
        /// GetGoalState
        /// </summary>
        /// <returns>the state that beeing search</returns>
        State<T> GetGoalState();

        /// <summary>
        /// GetAllPossibleStates
        /// </summary>
        /// <param name="s">the state to look from</param>
        /// <returns>list of Possible States from s </returns>
        List<State<T>> GetAllPossibleStates(State<T> s);

    }
}
