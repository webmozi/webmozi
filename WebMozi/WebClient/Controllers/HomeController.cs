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
        private static IReservationManager ireservationmanager = ManagerProvider.Instance.GetReservationManager();
        private static ICinemaManager icinemamanager = ManagerProvider.Instance.GetCinemaManager();
        private int UserID;
        private int ReservationID;

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
        public ViewResult AddMovie(Movie m)
        {
                icinemamanager.AddMovie(m);
                return View("MainView", m);
        }
        public ViewResult ListMovies()
        {
            ViewBag.movies = icinemamanager.ListMovies();
            return View();
        }
        [HttpPost]
        public IActionResult Delete(int ID)
        {
            icinemamanager.DeleteMovie(ID);
            return RedirectToAction("ListMovies");
        }
       //View hiányzik
        public ViewResult ChooseMovieEvent(MovieEvent m)
        {
            ReservationID = ireservationmanager.AddReservation(m);
            return View();
        }
        //View hiányzik
        public ViewResult CreateUser(User user) {
            UserID= ireservationmanager.AddUser(user);
            return View();
        }
        //View hiányzik
        public ViewResult MakingReservation() {
            ireservationmanager.ReservationToUser(UserID, ReservationID);
            return View();
        }
        //View hiányzik
        public ViewResult GetTicket() {
            User user = ireservationmanager.GetUser(UserID);
            return View(user.Reservations);
        }
    }
}
