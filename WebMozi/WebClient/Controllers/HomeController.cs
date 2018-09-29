using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebClient.Models;
namespace WebClient.Controllers
{
    public class HomeController : Controller
    {
        private IReservationManager irm = ReservationManagerProvider.Instance.GetReservationManager();
        public ViewResult Index()
        {
            return View("MainView");
        }
        public ViewResult MainView()
        {
            return View("MainView");
        }
        [HttpGet]
        public ViewResult AddMovie()
        {
            return View();
        }
        [HttpPost]
        public ViewResult AddMovie(MovieEvent m)
        {
            if (ModelState.IsValid)
            {
                irm.AddMovie(m);
                return View("MainView", m);
            }
            else
            { return View(); }
        }
        public ViewResult ListMovies()
        {
            return View(irm.ListMovies());
        }
        [HttpPost]
        public IActionResult Delete(int ID)
        {
            irm.DeleteMovie(ID);
            return RedirectToAction("ListMovies");
        }
    }
}
