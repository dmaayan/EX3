using Microsoft.AspNet.SignalR;
using EX3.Models;
using MazeLib;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace EX3
{
    /// <summary>
    /// ChatHub is the hub of the game, has a model.
    /// </summary>
    public class ChatHub : Hub
    {
        /// <summary>
        /// the model of the game
        /// </summary>
        private static IMultiPlayerModel model = new MultiPlayerModel();

        /// <summary>
        /// StartGame function gets from the model a new maze
        /// by the clients dimands, and open the game.
        /// </summary>
        /// <param name="name">of the maze</param>
        /// <param name="rows">the number of the row of the maze</param>
        /// <param name="cols">the number of the colmns of the maze</param>
        public void StartGame(string name,int rows, int cols)
        {
            int stat = 0;
            JObject obj = null;
            string clientId = Context.ConnectionId;     // the client ID
            Maze maze = model.StartGame(name, rows, cols, clientId);
            // if didn't get maze, update the maze's stat
            if (maze != null)
            {
                stat = 1;
                obj = JObject.Parse(maze.ToJSON());

            } 
            // send the maze to the client
            Clients.Client(clientId).reciveMaze(obj, stat);
        }

        /// <summary>
        /// PlayMove function get the player move and update the rival
        /// </summary>
        /// <param name="pos"> is the player's new position</param>
        public void PlayMove(string pos)
        {
            int stat = 0;
            string clientId = Context.ConnectionId;     // the client ID
            // get other player and check that is not null
            Player otherPlayer = model.GetOtherPlayer(clientId);
            // if didn't get the rival, update the stat
            if (otherPlayer != null)
            {
                stat = 1;
                // send the move to the client - rival
                Clients.Client(otherPlayer.Client).move(pos, stat);
            }
        }

        /// <summary>
        /// JoinGame function get the game that a player want to join to,
        /// update the model.
        /// </summary>
        /// <param name="name"> of the maze</param>
        public void JoinGame(string name) {
            int stat = 0;
            JObject obj = null;
            string clientId = Context.ConnectionId;
            Maze maze = model.JoinGame(name, clientId);            // get the maze
            // if didn't get maze, update the maze's stat
            if (maze != null)
            {
                stat = 1;
                obj = JObject.Parse(maze.ToJSON());

            }
            // send the maze to the client
            Clients.Client(clientId).reciveMaze(obj, stat);            
        }

        /// <summary>
        /// GetGames function gets from the model a list of games
        /// that waiting for someone to join them, and send it to the client
        /// </summary>
        public void GetGames() {
            string clientId = Context.ConnectionId;
            Clients.Client(clientId).getGames(JsonConvert.SerializeObject(model.GetAllNames()));
        }

        /// <summary>
        /// GameOver function close the game that send to it
        /// </summary>
        /// <param name="mazeName"> to close </param>
        public void GameOver(string mazeName) {

            string clientId = Context.ConnectionId;   // the client ID
            model.CloseGame(mazeName, clientId);

        }
    }
}