using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FilmsPhoto.Models;

namespace FilmsPhoto.Controllers
{
    public class FilmsController : Controller
    {
        private MyFilmDBEntities db = new MyFilmDBEntities();

        // GET: Films
        public ActionResult Index()
        {
            
            return View(db.Films.ToList());
        }

       

        // GET: Films/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Film film = db.Films.Find(id);
            if (film == null)
            {
                return HttpNotFound();
            }
            return View(film);
        }

        // GET: Films/Create
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Films/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Year,Description,Cover,ImageMiMeType,Country")] Film film, HttpPostedFileBase uploadImage)
        {
            if (ModelState.IsValid)
            {
                if (uploadImage != null)
                {
                    film.ImageMiMeType = uploadImage.ContentType;
                    film.Cover = new byte[uploadImage.ContentLength];
                    uploadImage.InputStream.Read(film.Cover, 0, uploadImage.ContentLength);
                }

                db.Films.Add(film);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(film);
        }

        public FileContentResult GetImage(int id) {
            Film film = db.Films.FirstOrDefault(g => g.Id == id);
            if(film != null)
            {
                return File(film.Cover, film.ImageMiMeType);
            }
            return null;

        }


        // GET: Films/Edit/5
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Film film = db.Films.Find(id);
            if (film == null)
            {
                return HttpNotFound();
            }
            return View(film);
        }

        // POST: Films/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Year,Description,Cover,ImageMiMeType,Country")] Film film, HttpPostedFileBase uploadImage)
        {
            if (ModelState.IsValid)
            {
                if (uploadImage != null)
                {
                    film.ImageMiMeType = uploadImage.ContentType;
                    film.Cover = new byte[uploadImage.ContentLength];
                    uploadImage.InputStream.Read(film.Cover, 0, uploadImage.ContentLength);
                }
                db.Entry(film).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(film);
        }

        public ActionResult Search(string Country)
        {
            var countryFilm = db.Films.Where(b => b.Country.Contains(Country)).ToList();
            if (countryFilm.Count <= 0)
            {
                return HttpNotFound();
            }
            return PartialView(countryFilm);
        }



        // GET: Films/Delete/5
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Film film = db.Films.Find(id);
            if (film == null)
            {
                return HttpNotFound();
            }
            return View(film);
        }
        

        // POST: Films/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Film film = db.Films.Find(id);
            db.Films.Remove(film);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
