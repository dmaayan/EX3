using EX3.Models;
using MazeGeneratorLib;
using MazeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EX3.Controllers
{
    public class GenerateMazeController : ApiController
    {
        private static IGenerateMazeModel model = new GenerateMazeModel();

        // GET: api/GenerateMaze/myName
        [Route("api/GenerateMaze/{algo}")]
        public string Get(int algo)
        {
            return model.Solve(algo).ToJson();
        }

        // GET: api/GenerateMaze/myName/10/20
        [Route("api/GenerateMaze/{name}/{rows}/{cols}")]
        public string Get(string name, int rows, int cols)
        {
            return model.Generate(name, rows, cols).ToJSON();
        }

        // POST: api/GenerateMaze
        public Maze Post([FromBody]MazeInfo mazeInfo)
        {
            //Maze maze = mazeGenerator.Generate(mazeInfo.Rows, mazeInfo.Cols);
            //maze.Name = mazeInfo.Name;
            //return maze;
            return null;
        }

        // PUT: api/GenerateMaze/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/GenerateMaze/5
        public void Delete(int id)
        {
        }
    }
}
