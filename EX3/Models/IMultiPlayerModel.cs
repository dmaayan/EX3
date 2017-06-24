using MazeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EX3.Models
{
    /// <summary>
    /// model interface
    /// </summary>
    public interface IMultiPlayerModel
    {

        /// <summary>
        /// start a game
        /// </summary>
        /// <param name="name">of the maze </param>
        /// <param name="rows">of the maze </param>
        /// <param name="cols">of the maze</param>
        /// <param name="client">that start this game</param>
        /// <returns>the maze</returns>
        Maze StartGame(string name, int rows, int cols, string client);

        /// <summary>
        /// join a game
        /// </summary>
        /// <param name="name"> of the game to join </param>
        /// <param name="client"> that requested to join</param>
        /// <returns>the maze of the game</returns>
        Maze JoinGame(string name, string client);

        /// <summary>
        /// close a multiplayer game
        /// </summary>
        /// <param name="name">of the maze  to close</param>
        /// <param name="client">that requested to close the game</param>
        /// <returns>the closed game</returns>
        void CloseGame(string name, string client);

        /// <summary>
        /// list all waiting games names
        /// </summary>
        /// <returns>array of waiting games names</returns>
        string[] GetAllNames();

        /// <summary>
        /// get the player that plays against the client
        /// </summary>
        /// <param name="client">to get the other player</param>
        /// <returns>the other player</returns>
        Player GetOtherPlayer(string client);

        /// <summary>
        /// get the game of a player
        /// </summary>
        /// <param name="client">to request its game</param>
        /// <returns>the game of the player</returns>
        Game GetGameOfPlayer(string client);
    }
}