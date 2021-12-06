using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using PhotoStudioAPI.Models;

namespace PhotoStudioAPI.Controllers
{
    public class PhotographsController : ApiController
    {
        private db_a74fb8_photostudioEntities db = new db_a74fb8_photostudioEntities();

        // GET: api/Photographs
        public IQueryable<Photograph> GetPhotograph()
        {
            return db.Photograph;
        }

        [Route("api/GetPhotographPhotoSessions/{id}")]
        public IHttpActionResult GetPhotographPhotoSessions(int id)
        {
            var currentDate = DateTime.Now.Date;
            return Ok(db.PhotoSession.Where(p => p.PhotographId == id && p.StatusId != 2 && p.StatusId != 3 && p.PhotoSessionDate >= currentDate));
        }
        [Route("api/PutPhotosesion/{id}")]
        public IHttpActionResult PutPhotograph(int id, PhotoSession photoSession)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != photoSession.Id)
            {
                return BadRequest();
            }

            db.Entry(photoSession).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PhotographExists(id))
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

        // GET: api/Photographs/5
        [ResponseType(typeof(Photograph))]
        public IHttpActionResult GetPhotograph(int id)
        {
            Photograph photograph = db.Photograph.Find(id);
            if (photograph == null)
            {
                return NotFound();
            }

            return Ok(photograph);
        }

        [ResponseType(typeof(Photograph))]
        public IHttpActionResult GetPhotograph(string login)
        {
            return Ok(db.Photograph.FirstOrDefault(p => p.Login == login));
        }
        // PUT: api/Photographs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPhotograph(int id, Photograph photograph)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != photograph.Id)
            {
                return BadRequest();
            }

            db.Entry(photograph).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PhotographExists(id))
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

        // POST: api/Photographs
        [ResponseType(typeof(Photograph))]
        public IHttpActionResult PostPhotograph(Photograph photograph)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Photograph.Add(photograph);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = photograph.Id }, photograph);
        }

        // DELETE: api/Photographs/5
        [ResponseType(typeof(Photograph))]
        public IHttpActionResult DeletePhotograph(int id)
        {
            Photograph photograph = db.Photograph.Find(id);
            if (photograph == null)
            {
                return NotFound();
            }

            db.Photograph.Remove(photograph);
            db.SaveChanges();

            return Ok(photograph);
        }
        [Route("api/DeletePhotoSession/{id}")]
        public IHttpActionResult DeletePhotoSession(int id)
        {
            PhotoSession photoSession = db.PhotoSession.Find(id);
            if (photoSession == null)
            {
                return NotFound();
            }

            db.PhotoSession.Remove(photoSession);
            db.SaveChanges();

            return Ok(photoSession);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PhotographExists(int id)
        {
            return db.Photograph.Count(e => e.Id == id) > 0;
        }
    }
}