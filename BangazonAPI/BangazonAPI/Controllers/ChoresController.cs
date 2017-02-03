//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.Entity;
//using System.Data.Entity.Infrastructure;
//using System.Linq;
//using System.Net;
//using System.Net.Http;
//using System.Web.Http;
//using System.Web.Http.Description;
//using BangazonAPI.DAL;
//using BangazonAPI.Providers.Models;

//namespace BangazonAPI.Controllers
//{
//    public class ChoresController : ApiController
//    {
//        ChoreRepository Repo = new ChoreRepository();

//        // GET: api/Chores
//        public List<Chore> GetChores()
//        {
//            return Repo.GetAllChores().ToList();
//        }

//        // GET: api/Chores/5
//        [ResponseType(typeof(Chore))]
//        public IHttpActionResult GetChore(int id)
//        {
//            Chore chore = db.Chores.Find(id);
//            if (chore == null)
//            {
//                return NotFound();
//            }

//            return Ok(chore);
//        }

//        // PUT: api/Chores/5
//        [ResponseType(typeof(void))]
//        public IHttpActionResult PutChore(int id, Chore chore)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }

//            if (id != chore.ChoreID)
//            {
//                return BadRequest();
//            }

//            db.Entry(chore).State = EntityState.Modified;

//            try
//            {
//                db.SaveChanges();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!ChoreExists(id))
//                {
//                    return NotFound();
//                }
//                else
//                {
//                    throw;
//                }
//            }

//            return StatusCode(HttpStatusCode.NoContent);
//        }

//        // POST: api/Chores
//        [ResponseType(typeof(Chore))]
//        public IHttpActionResult PostChore(Chore chore)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }

//            Repo.AddNewChore(chore);

//            return CreatedAtRoute("DefaultApi", new { id = chore.ChoreID }, chore);
//        }

//        // DELETE: api/Chores/5
//        [ResponseType(typeof(Chore))]
//        public IHttpActionResult DeleteChore(int id)
//        {
//            Chore chore = db.Chores.Find(id);
//            if (chore == null)
//            {
//                return NotFound();
//            }

//            db.Chores.Remove(chore);
//            db.SaveChanges();

//            return Ok(chore);
//        }

//        protected override void Dispose(bool disposing)
//        {
//            if (disposing)
//            {
//                db.Dispose();
//            }
//            base.Dispose(disposing);
//        }

//        private bool ChoreExists(int id)
//        {
//            return db.Chores.Count(e => e.ChoreID == id) > 0;
//        }
//    }
//}