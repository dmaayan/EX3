using EX3.Models;
using Newtonsoft.Json.Linq;
using System.Web.Http;

namespace EX3.Controllers
{
    /// <summary>
    /// controller for generate maze and solve services
    /// </summary>
    public class GenerateMazeController : ApiController
    {
        /// <summary>
        /// the model for single player
        /// </summary>
        private static IGenerateMazeModel model = new GenerateMazeModel();

        // GET: api/GenerateMaze/myName/1
        [Route("api/GenerateMaze/{name}/{algo}")]
        public JObject Get(string name, int algo)
        {
            // return a solution object
            JObject obj = JObject.Parse(model.Solve(name, algo).ToJson());
            return obj;
        }

        // GET: api/GenerateMaze/myName/10/20
        [Route("api/GenerateMaze/{name}/{rows}/{cols}")]
        public JObject Get(string name, int rows, int cols)
        {
            // return a maze object
            JObject obj = JObject.Parse(model.Generate(name, rows, cols).ToJSON());
            return obj;
        }
    }
}
