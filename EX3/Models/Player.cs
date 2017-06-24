using MazeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EX3.Models
{
    /// <summary>
    /// Player has client and bool - wait to other player
    /// </summary>
    public class Player
    {
        /// <summary>
        /// the client
        /// </summary>
        private string client;

        /// <summary>
        /// statues to wait for another player
        /// </summary>
        private bool wait;

        /// <summary>
        /// constuctor
        /// </summary>
        /// <param name="c">the client</param>
        public Player(string c)
        {
            wait = false;
            client = c;
        }

        /// <summary>
        /// a property of client 
        /// </summary>
        public string Client
        {
            get { return client; }
        }

        /// <summary>
        /// WaitForPlayer make the thread wait if the boolean wait is true
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