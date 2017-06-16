
namespace SearchAlgorithmsLib
{
    /// <summary>
    /// Searcher is an abstract class extends ISearcher.
    /// had int that represent evaluatedNodes.
    /// </summary>
    /// <typeparam name="T">is the object to search</typeparam>
    public abstract class Searcher<T> : ISearcher<T>
    {
        /// <summary>
        /// number of nodes evaluated
        /// </summary>
        private int evaluatedNodes;

        /// <summary>
        /// constuctor.
        /// </summary>
        public Searcher()
        {
            evaluatedNodes = 0;
        }

        /// <summary>
        /// increase Evaluated Nodes.
        /// </summary>
        protected void IncreaseEvaluatedNodes()
        {
            evaluatedNodes++;
        }

        /// <summary>
        /// GetNumberOfNodesEvaluated.
        /// ISearcher’s method.
        /// </summary>
        /// <returns>evaluatedNodes </returns>
        public int GetNumberOfNodesEvaluated()
        {
            return evaluatedNodes;
        }

        /// <summary>
        /// Search, ISearcher’s method.
        /// </summary>
        /// <param name="searchable">to search on</param>
        /// <returns>the solution</returns>
        public abstract Solution<T> Search(ISearchable<T> searchable);
    }
}
