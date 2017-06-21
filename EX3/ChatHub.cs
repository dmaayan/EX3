using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using EX3.Models;
using MazeLib;
using Newtonsoft.Json.Linq;

namespace EX3
{
    public class ChatHub : Hub
    {
        static IMultiPlayerModel model = new MultiPlayerModel();

        public void Send(string name, string message)
        {
            // Call the broadcastMessage method to update clients
            Clients.All.broadcastMessage(name, message);
        }

        public void start(string name,int rows, int cols)
        {
            int stat = 0;
            JObject obj = JObject.Parse("");
            string clientId = Context.ConnectionId;
            Maze maze = model.StartGame(name, rows, cols, clientId);
            if (maze != null)
            {
                stat = 1;
                obj = JObject.Parse(maze.ToJSON());

            } // TODO נקודת כשל אופציונלית
            Clients.Client(clientId).reciveMaze(obj, stat);
         
        }

    }
}