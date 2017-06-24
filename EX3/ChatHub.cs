using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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

        ///
        public void Send(string name, string message)
        {
            // Call the broadcastMessage method to update clients
            Clients.All.broadcastMessage(name, message);
        }

        public void StartGame(string name,int rows, int cols)
        {
            int stat = 0;
            JObject obj = null;
            string clientId = Context.ConnectionId;
            Maze maze = model.StartGame(name, rows, cols, clientId);
            if (maze != null)
            {
                stat = 1;
                obj = JObject.Parse(maze.ToJSON());

            } 
            Clients.Client(clientId).reciveMaze(obj, stat);
        }

        public void PlayMove(string pos)
        {
            int stat = 0;
            string clientId = Context.ConnectionId;
            // get other player and check that is not null
            Player otherPlayer = model.GetOtherPlayer(clientId);
            if (otherPlayer != null)
            {
                stat = 1;
                Clients.Client(otherPlayer.Client).move(pos, stat);
            }
        }

        public void JoinGame(string name) {

            int stat = 0;
            JObject obj = null;
            string clientId = Context.ConnectionId;

            Maze maze = model.JoinGame(name, clientId);            // get the maze
            if (maze != null)
            {
                stat = 1;
                obj = JObject.Parse(maze.ToJSON());

            }
            Clients.Client(clientId).reciveMaze(obj, stat);            
        }
        
        public void GetGames() {
            string clientId = Context.ConnectionId;
            //model.GetAllNames();
            Clients.Client(clientId).getGames(JsonConvert.SerializeObject(model.GetAllNames()));
        }

        public void GameOver(string mazeName) {
            string clientId = Context.ConnectionId;
            model.CloseGame(mazeName, clientId);

        }
    }
}