using System;
using System.Collections.Generic;

namespace SearchAlgorithmsLib
{
    /// <summary>
    /// state has represention, cost, an state cameFrom.
    /// </summary>
    /// <typeparam name="T">is the type of the state </typeparam>
    public class State<T>
    {
        /// <summary>
        /// the state
        /// </summary>
        private T state;            
        /// <summary>
        /// cost to get to this state
        /// </summary>
        private double cost;
        /// <summary>
        /// the state we came from to this state
        /// </summary>
        private State<T> cameFrom;

        /// <summary>
        /// constuctor
        /// </summary>
        /// <param name="state">the represention to save</param>
        public State(T state)
        {
            this.state = state;
        }

        /// <summary>
        /// a property of Cost 
        /// </summary>
        public double Cost
        {
            get { return cost; }
            set { cost = value; }
        }

        /// <summary>
        /// a property of CameFrom 
        /// </summary>
        public State<T> CameFrom
        {
            get { return cameFrom; }
            set { cameFrom = value; }
        }

        /// <summary>
        /// represention getter
        /// </summary>
        /// <returns>the represention </returns>
        public T GetState()
        {
            return state;
        }

        /// <summary>
        /// overload Object's Equals method
        /// </summary>
        /// <param name="s">state to comper</param>
        /// <returns>true if equal, else false </returns>
        public bool Equals(State<T> s) 
        {
            return state.Equals(s.GetState());
        }

        /// <summary>
        /// GetHashCode
        /// </summary>
        /// <returns>int in the hashCode</returns>
        public override int GetHashCode()
        {
            return String.Intern(state.ToString()).GetHashCode();
        }

        /// <summary>
        /// StatePool is intern class, has Dictionary
        /// </summary>
        public static class StatePool
        {
            /// <summary>
            /// the pool of states
            /// </summary>
            private static Dictionary<T, State<T>> pool = new Dictionary<T, State<T>>();

            /// <summary>
            /// GetState creates State if doesn't exist, else return T's State
            /// </summary>
            /// <param name="state">T to search of exist </param>
            /// <returns>the State </returns>
            public static State<T> GetState(T state)
            {
                // if the state doesn't exist, add it to the pool  
                if (!pool.ContainsKey(state))
                {
                    pool.Add(state, new State<T>(state));
                }
                return pool[state];
            }
        }
    }
}