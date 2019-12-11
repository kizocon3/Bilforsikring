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
using WebApi.Models.DB;

namespace WebApi.Controllers
{
    public class AvtalersController : ApiController
    {
        private FAGSYSTEMDBEntities db = new FAGSYSTEMDBEntities();

        // GET: api/Avtalers
        public IQueryable<Avtaler> GetAvtalers()
        {
            return db.Avtalers;
        }

        // GET: api/Avtalers/5
        [ResponseType(typeof(Avtaler))]
        public async Task<IHttpActionResult> GetAvtaler(Guid id)
        {
            Avtaler avtaler = await db.Avtalers.FindAsync(id);
            if (avtaler == null)
            {
                return NotFound();
            }

            return Ok(avtaler);
        }

        // PUT: api/Avtalers/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutAvtaler(Guid id, Avtaler avtaler)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != avtaler.Id)
            {
                return BadRequest();
            }

            db.Entry(avtaler).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AvtalerExists(id))
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

        // POST: api/Avtalers
        [ResponseType(typeof(Avtaler))]
        public async Task<IHttpActionResult> PostAvtaler(Avtaler avtaler)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Avtalers.Add(avtaler);

            try
            {
               await db.SaveChangesAsync();          
            }
            catch (DbUpdateException)
            {
                if (AvtalerExists(avtaler.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = avtaler.Id }, avtaler);
        }

        // DELETE: api/Avtalers/5
        [ResponseType(typeof(Avtaler))]
        public async Task<IHttpActionResult> DeleteAvtaler(Guid id)
        {
            Avtaler avtaler = await db.Avtalers.FindAsync(id);
            if (avtaler == null)
            {
                return NotFound();
            }

            db.Avtalers.Remove(avtaler);
            await db.SaveChangesAsync();

            return Ok(avtaler);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AvtalerExists(Guid id)
        {
            return db.Avtalers.Count(e => e.Id == id) > 0;
        }
    }
}