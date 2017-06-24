using MazeGeneratorLib;
using MazeLib;
using SearchAlgorithmsLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EX3.Models
{
    /// <summary>
    /// MultiPlayerModel is an IMultiPlayerModel. the model of the games
    /// </summary>
    public class MultiPlayerModel : IMultiPlayerModel
    {
        /// <summary>
        /// all the games waiting for a second player
        /// </summary>
        private static Dictionary<string, Maze> waitingList;

        /// <summary>
        /// all games currently active
        /// </summary>
        private static Dictionary<string, Maze> activeGames;

        /// <summary>
        /// all the multiplayer games
        /// </summary>
        private static Dictionary<string, Maze> multiPlayerMazes;

        /// <summary>
        /// maze name to game dictionary
        /// </summary>
        private static Dictionary<string, Game> mazeNameToGame;

        /// <summary>
        /// client to maze dictionary
        /// </summary>
        private static Dictionary<string, string> clientToMazeName;

        /// <summary>
        /// constructor
        /// </summary>
        public MultiPlayerModel()
        {
            multiPlayerMazes = new Dictionary<string, Maze>();
            activeGames = new Dictionary<string, Maze>();
            waitingList = new Dictionary<string, Maze>();
            mazeNameToGame = new Dictionary<string, Game>();
            clientToMazeName = new Dictionary<string, string>();
        }

        /// <summary>
        /// start a game
        /// </summary>
        /// <param name="name">of the maze </param>
        /// <param name="rows">of the maze </param>
        /// <param name="cols">of the maze</param>
        /// <param name="client">that start this game</param>
        /// <returns>the maze</returns>
        public Maze StartGame(string name, int rows, int cols, string client)
        {
            // create a multiplay game - the maze
            Maze maze = MultiGameGenerateMaze(name, rows, cols);
            // checks that the maze is ok
            if (maze == null)
            {
                return null;
            }
            // add the maze to the waiting list
            string mazeName = maze.Name;
            waitingList.Add(name, maze);

            // create the game with the player
            Player p = new Player(client);
            mazeNameToGame.Add(mazeName, new Game(p));

            // wait for a player to join the game
            mazeNameToGame[mazeName].waitForOtherPlayer();
            clientToMazeName.Add(client, mazeName);
            return maze;
        }

        /// <summary>
        /// Generate a Maze
        /// </summary>
        /// <param name="name">the name of the maze </param>
        /// <param name="rows">the rows of the maze </param>
        /// <param name="cols">the cols of the maze </param>
        /// <returns>the new maze,if the maze already return null</returns>
        private Maze MultiGameGenerateMaze(string name, int rows, int cols)
        {
            // if the maze exist return null
            if (multiPlayerMazes.ContainsKey(name))
            {
                return null;
            }
            DFSMazeGenerator mazeGenerator = new DFSMazeGenerator();
            Maze maze = mazeGenerator.Generate(rows, cols);
            maze.Name = name;

            // add the new maze
            multiPlayerMazes.Add(name, maze);
            return maze;
        }

        /// <summary>
        /// join a game
        /// </summary>
        /// <param name="name"> of the game to join </param>
        /// <param name="client"> that requested to join</param>
        /// <returns>the maze of the game</returns>
        public Maze JoinGame(string name, string client)
        {
            // check for available maze with that name
            if ((name!= null) && (waitingList.ContainsKey(name)))
            {
                Maze maze = waitingList[name];
                string mazeName = maze.Name;

                // put the game in the active games list and remove from waiting
                activeGames.Add(name, maze);
                waitingList.Remove(name);

                // add the player to the game
                Player player = new Player(client);
                mazeNameToGame[mazeName].AddPlayer(player);
                clientToMazeName.Add(client, mazeName);
                return maze;
            }
            return null;
        }

        /// <summary>
        /// close a multiplayer game
        /// </summary>
        /// <param name="name">of the maze to close</param>
        /// <param name="client">that requested to close the game</param>
        public void CloseGame(string name, string client)
        {
            // if the game is on
            if ((name!= null) && (activeGames.ContainsKey(name)))
            {
                //remove from active list and multiplayer mazes list
                activeGames.Remove(name);
                multiPlayerMazes.Remove(name);

                // remove the players and clients
                Game game = mazeNameToGame[name];
                mazeNameToGame.Remove(name);
                clientToMazeName.Remove(game.FirstPlayer.Client);
                clientToMazeName.Remove(game.SecondPlayer.Client);
                //TODO  לא אמורים למחוק פה גם את המשחק עצמו ? (game)?
            }
        }

        /// <summary>
        /// list all waiting games names
        /// </summary>
        /// <returns>array of waiting games names</returns>
        public string[] GetAllNames()
        {
            return waitingList.Keys.ToArray();
        }

        /// <summary>
        /// get the player that plays against the client
        /// </summary>
        /// <param name="client">to get the other player</param>
        /// <returns>the other player</returns>
        public Player GetOtherPlayer(string client)
        {
            // checks for avtive game
            if (!clientToMazeName.ContainsKey(client))
            {
                return null;
            }
            Game game = mazeNameToGame[clientToMazeName[client]];
            return game.GetOtherPlayer(client);
        }

        /// <summary>
        /// get the game of a player
        /// </summary>
        /// <param name="client">to request its game</param>
        /// <returns>the game of the player</returns>
        public Game GetGameOfPlayer(string client)
        {
            // checks for active game with this client
            if (!clientToMazeName.ContainsKey(client))
            {
                return null;
            }
            return mazeNameToGame[clientToMazeName[client]];
        }
    }
}