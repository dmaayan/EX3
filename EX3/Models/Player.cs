using MazeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EX3.Models
{
    /// <summary>
    /// Player has client, list of Directions, and bool ( if wait )
    /// </summary>
    public class Player
    {
        /// <summary>
        /// the client
        /// </summary>
        private int client;
        /// <summary>
        /// the way that this player have played
        /// </summary>
        private List<Direction> way;
        /// <summary>
        /// statues to wait for another player
        /// </summary>
        private bool wait;

        /// <summary>
        /// constuctor
        /// </summary>
        /// <param name="c">the client</param>
        public Player(int c)
        {
            wait = false;
            client = c;
            way = new List<Direction>();
        }

        /// <summary>
        /// a property of client 
        /// </summary>
        public int Client
        {
            get { return client; }
        }

        /// <summary>
        /// a property of way 
        /// </summary>
        public List<Direction> Way
        {
            get { return way; }
        }

        /// <summary>
        /// WaitForPlayer make the thread wait if the boolean wait i true
        /// </summary>
        public void WaitForPlayer()
        {
            wait = true;
            while (wait)
            {
                System.Threading.Thread.Sleep(1);
            }
        }

        /// <summary>
        /// StopWaiting
        /// </summary>
        public void StopWaiting()
        {
            wait = false;
        }
    }
}