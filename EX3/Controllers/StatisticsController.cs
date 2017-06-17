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

namespace EX3.Controllers
{
    public class StatisticsController : ApiController
    {
        private EX3Context db = new EX3Context();

        // GET: api/Statistics
        public IQueryable<Statistics> GetStatistics()
        {
            return db.Statistics;
        }

        // GET: api/Statistics/5
        [ResponseType(typeof(Statistics))]
        public async Task<IHttpActionResult> GetStatistics(string id)
        {
            Statistics statistics = await db.Statistics.FindAsync(id);
            if (statistics == null)
            {
                return NotFound();
            }

            return Ok(statistics);
        }

        // PUT: api/Statistics/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutStatistics(string id, Statistics statistics)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != statistics.UserName)
            {
                return BadRequest();
            }

            db.Entry(statistics).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StatisticsExists(id))
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

        // POST: api/Statistics
        [ResponseType(typeof(Statistics))]
        public async Task<IHttpActionResult> PostStatistics(Statistics statistics)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Statistics.Add(statistics);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (StatisticsExists(statistics.UserName))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = statistics.UserName }, statistics);
        }

        // DELETE: api/Statistics/5
        [ResponseType(typeof(Statistics))]
        public async Task<IHttpActionResult> DeleteStatistics(string id)
        {
            Statistics statistics = await db.Statistics.FindAsync(id);
            if (statistics == null)
            {
                return NotFound();
            }

            db.Statistics.Remove(statistics);
            await db.SaveChangesAsync();

            return Ok(statistics);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StatisticsExists(string id)
        {
            return db.Statistics.Count(e => e.UserName == id) > 0;
        }
    }
}