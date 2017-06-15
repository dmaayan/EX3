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
        static DFSMazeGenerator mazeGenerator = new DFSMazeGenerator();

        private static List<Maze> mazes = new List<Maze>() {
            mazeGenerator.Generate(10, 10),
            mazeGenerator.Generate(10, 10)
        };

        // GET: api/GenerateMaze
        public IEnumerable<Maze> Get()
        {
            return mazes;
        }

        // GET: api/GenerateMaze/myName
        public string Get(string id)
        {
            Maze maze = mazeGenerator.Generate(10, 10);
            maze.Name = id;
            return maze.ToJSON();
        }

        // POST: api/GenerateMaze
        public Maze Post([FromBody]MazeInfo mazeInfo)
        {
            Maze maze = mazeGenerator.Generate(mazeInfo.Rows, mazeInfo.Cols);
            maze.Name = mazeInfo.Name;
            return maze;
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
