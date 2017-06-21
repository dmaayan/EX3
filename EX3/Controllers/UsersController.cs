using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using EX3.Models;
using System.Security.Cryptography;
using System.Text;

namespace EX3.Controllers
{
    public class UsersController : ApiController
    {
        private EX3Context db = new EX3Context();

        // GET: api/Users
        public IQueryable<User> GetUsers()
        {
            return db.Users.Include(stat => stat.StatisticsUserName);
        }

        // GET: api/Users/qq/qwee
        [ResponseType(typeof(User))]
        [Route("api/Users/{id}/{password}")]
        public async Task<IHttpActionResult> GetUser(string id, string password)
        {
            // hash the password
            string hashedPassword = ComputeHash(password);
            User user = await db.Users.FindAsync(id);
            // if failed to find user
            if (user == null)
            {
                return NotFound();
            }
            // check password
            if (user.password != hashedPassword)
            {
                return BadRequest();
            }
            // return the user
            return Ok(user);
        }

        // GET: api/Users/qq
        [ResponseType(typeof(User))]
        [Route("api/Users/{id}")]
        public async Task<IHttpActionResult> GetUser(string id)
        {
            User user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        // PUT: api/Users/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutUser(string id, User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // user id does not match user
            if (id != user.userName)
            {
                return BadRequest();
            }

            db.Entry(user).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Users
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> PostUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return Content(HttpStatusCode.BadRequest, "Db");
            }
            // cant add user that already in the system
            if (UserExists(user.userName))
            {
                return Content(HttpStatusCode.Conflict, "UserName");
            }
            // hash the password
            user.password = ComputeHash(user.password);
            // add user to data base
            db.Users.Add(user);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = user.userName }, user);
        }

        // DELETE: api/Users/5
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> DeleteUser(string id)
        {
            // get the user to delete
            User user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            // delete the user
            db.Users.Remove(user);
            await db.SaveChangesAsync();

            return Ok(user);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// checks if user exist
        /// </summary>
        /// <param name="id">of the user to check</param>
        /// <returns>true if user in data base, false otherwise</returns>
        private bool UserExists(string id)
        {
            return db.Users.Count(e => e.userName == id) > 0;
        }

        /// <summary>
        /// hash a password
        /// </summary>
        /// <param name="input">string to hash</param>
        /// <returns>hashed string</returns>
        private string ComputeHash(string input)
        {
            SHA1 sha = SHA1.Create();
            byte[] buffer = Encoding.ASCII.GetBytes(input);
            byte[] hash = sha.ComputeHash(buffer);
            string hash64 = Convert.ToBase64String(hash);
            return hash64;
        }
    }
}