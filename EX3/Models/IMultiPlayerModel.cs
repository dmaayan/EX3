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
        /// Generate a Maze, if the maze exist close it and create new maze
        /// </summary>
        /// <param name="name">the name of the maze </param>
        /// <param name="rows">the rows of the maze </param>
        /// <param name="cols">the cols of the maze </param>
        /// <returns>the new maze</returns>
        Maze MultiGameGenerateMaze(string name, int rows, int cols);

        /// <summary>
        /// start a game
        /// </summary>
        /// <param name="name">of the maze </param>
        /// <param name="rows">of the maze </param>
        /// <param name="cols">of the maze</param>
        /// <param name="client">that start this game</param>
        /// <returns>the maze</returns>
        Maze StartGame(string name, int rows, int cols, int client);

        /// <summary>
        /// join a game
        /// </summary>
        /// <param name="name"> of the game to join </param>
        /// <param name="client"> that requested to join</param>
        /// <returns>the maze of the game</returns>
        Maze JoinGame(string name, int client);

        /// <summary>
        /// play a move in the game
        /// </summary>
        /// <param name="move"> Direction to play</param>
        /// <param name="client">that moved</param>
        /// <returns>the maze played by this client</returns>
        Maze PlayGame(Direction move, int client);

        /// <summary>
        /// close a multiplayer game
        /// </summary>
        /// <param name="name">of the maze  to close</param>
        /// <param name="client">that requested to close the game</param>
        /// <returns>the closed game</returns>
        Game CloseGame(string name, int client);

        /// <summary>
        /// checks if the client had permision to close the game
        /// </summary>
        /// <param name="name">of the maze  to close</param>
        /// <param name="client">that requested to close the game</param>
        /// <returns>true if legal else false</returns>
        bool IsLegalToClose(string name, int client);

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
        Player GetOtherPlayer(int client);

        /// <summary>
        /// get the game of a player
        /// </summary>
        /// <param name="client">to request its game</param>
        /// <returns>the game of the player</returns>
        Game GetGameOfPlayer(int client);
    }
}