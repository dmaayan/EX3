using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EX3.Models
{
    /// <summary>
    /// game between two players
    /// </summary>
    public class Game
    {
        /// <summary>
        /// player to create the game
        /// </summary>
        Player player;
        /// <summary>
        /// the other player that joined the game
        /// </summary>
        Player otherPlayer;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="creator">player that created the game</param>
        public Game(Player creator)
        {
            player = creator;
        }

        /// <summary>
        /// property of player
        /// </summary>
        public Player FirstPlayer
        {
            get { return player; }
        }

        /// <summary>
        /// property of otherPlayer
        /// </summary>
        public Player SecondPlayer
        {
            get { return otherPlayer; }
        }

        /// <summary>
        /// add a player to the game
        /// </summary>
        /// <param name="secondPlayer">to join the game</param>
        public void AddPlayer(Player secondPlayer)
        {
            otherPlayer = secondPlayer;
            player.StopWaiting();
        }

        /// <summary>
        /// creator of the game waits for a palyer to join
        /// </summary>
        public void waitForOtherPlayer()
        {
            player.WaitForPlayer();
        }

        /// <summary>
        /// get the other player
        /// </summary>
        /// <param name="tcc">client to get the other client</param>
        /// <returns>the other client</returns>
        public Player GetOtherPlayer(int tcc)
        {
            // compares this client with the plaer to get the other palyer
            if (player.Client == tcc)
            {
                return otherPlayer;
            }
            if (otherPlayer.Client == tcc)
            {
                return player;
            }
            // the client is not one of the players
            return null;
        }
    }
}