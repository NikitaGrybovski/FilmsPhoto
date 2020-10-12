using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using FilmsPhoto.Models;

namespace FilmsPhoto.Controllers
{
    public class HomeController : Controller
    {
        private MyFilmDBEntities db = new MyFilmDBEntities();
        public ActionResult Index()
        {
            var allFilms = db.Films.OrderByDescending(v => v.Id).Take(3).ToList();
            return View(allFilms);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public FileContentResult GetImage(int id)
        {
            Film film = db.Films.FirstOrDefault(g => g.Id == id);
            if (film != null)
            {
                return File(film.Cover, film.ImageMiMeType);
            }
            return null;

        }

    }
}