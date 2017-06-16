
namespace SearchAlgorithmsLib
{
    /// <summary>
    /// ISearcher is an interface for search problems solvers
    /// has two methods.
    /// </summary>
    /// <typeparam name="T">is the object to search</typeparam>
    public interface ISearcher<T>
    {
        /// <summary>
        /// the Search method
        /// </summary>
        /// <param name="searchable">to search on</param>
        /// <returns>the solution</returns>
        Solution<T> Search (ISearchable<T> searchable);

        /// <summary>
        /// get how many nodes were evaluated by the algorithm
        /// </summary>
        /// <returns> the number of the nodes</returns>
        int GetNumberOfNodesEvaluated();
    }
}
