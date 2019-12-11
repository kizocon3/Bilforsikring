using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WebApi.Models.DB;

namespace WebApi.Controllers
{
    public class KundersController : ApiController
    {
        private FAGSYSTEMDBEntities db = new FAGSYSTEMDBEntities();

        // GET: api/Kunders
        public IQueryable<Kunder> GetKunders()
        {
            return db.Kunders;
        }

        // GET: api/Kunders/5
        [ResponseType(typeof(Kunder))]
        public async Task<IHttpActionResult> GetKunder(Guid id)
        {
            Kunder kunder = await db.Kunders.FindAsync(id);
            if (kunder == null)
            {
                return NotFound();
            }

            return Ok(kunder);
        }

        // PUT: api/Kunders/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutKunder(Guid id, Kunder kunder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != kunder.Id)
            {
                return BadRequest();
            }

            db.Entry(kunder).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KunderExists(id))
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

        // POST: api/Kunders
        [ResponseType(typeof(Kunder))]
        public async Task<IHttpActionResult> PostKunder(Kunder kunder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dt = DateTime.Now;

            var newKunder = new Kunder()
            {
                Id = Guid.NewGuid(),
                Bonus = kunder.Bonus,
                Email = kunder.Email,
                ForNavn = kunder.ForNavn,
                Etternavn = kunder.Etternavn,
                PersonNummer = kunder.PersonNummer,
                BilensRegistreringsNummer = kunder.BilensRegistreringsNummer,
                Created = dt,
            };

            db.Kunders.Add(newKunder);
            try
            {
                int i = await db.SaveChangesAsync();
                if (i > 0)
                {
                    WebApi.CustomHelp.CustomHelper.BuildEmailTemplate(kunder);
                }
            }
            catch (DbUpdateException)
            {
                if (KunderExists(kunder.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = kunder.Id }, kunder);
        }
        // DELETE: api/Kunders/5
        [ResponseType(typeof(Kunder))]
        public async Task<IHttpActionResult> DeleteKunder(Guid id)
        {
            Kunder kunder = await db.Kunders.FindAsync(id);
            if (kunder == null)
            {
                return NotFound();
            }

            db.Kunders.Remove(kunder);
            await db.SaveChangesAsync();

            return Ok(kunder);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool KunderExists(Guid id)
        {
            return db.Kunders.Count(e => e.Id == id) > 0;
        }
    }
}