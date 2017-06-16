using System.Collections.Generic;

namespace SearchAlgorithmsLib

{
    /// <summary>
    /// NonPrioritySearcher is an abstract class inherit from Searcher.
    /// had Dictionary of parents
    /// </summary>
    /// <typeparam name="T">the type of object to search</typeparam>
    public abstract class NonPrioritySearcher<T> : Searcher<T>
    {
        /// <summary>
        /// the updated parents of each state
        /// </summary>
        private Dictionary<State<T>, State<T>> parents;

        /// <summary>
        ///  constuctor
        /// </summary>
        public NonPrioritySearcher()
        {
            parents = new Dictionary<State<T>, State<T>>();
        }

        /// <summary>
        /// getter and setter to Parents
        /// </summary>
        public Dictionary<State<T>, State<T>> Parents
        {
            get { return parents; }
        }

        /// <summary>
        /// Searcher's abstract method overriding
        /// </summary>
        /// <param name="searchable">to search on</param>
        /// <returns>the solution</returns>
        public override abstract Solution<T> Search(ISearchable<T> searchable);
    }
}
    