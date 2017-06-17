using EX3.Models;
using MazeGeneratorLib;
using MazeLib;
using Newtonsoft.Json.Linq;
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

        // GET: api/GenerateMaze/myName/1
        [Route("api/GenerateMaze/{name}/{algo}")]
        public JObject Get(string name, int algo)
        {
            JObject obj = JObject.Parse(model.Solve(name, algo).ToJson());
            return obj;
        }

        // GET: api/GenerateMaze/myName/10/20
        [Route("api/GenerateMaze/{name}/{rows}/{cols}")]
        public JObject Get(string name, int rows, int cols)
        {
            JObject obj = JObject.Parse(model.Generate(name, rows, cols).ToJSON());
            return obj;
        }
    }
}
