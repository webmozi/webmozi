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

        public ViewResult Index() {
            refreshViewBag();
            return View("ListMovies");
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
                refreshViewBag();
                return View("ListMovies",m);
        }
        public ViewResult Edit(int MovieId)
        {
            Movie EditingMovie = icinemamanager.SelectMovie(MovieId);
  
           return View(EditingMovie);
        }
        [HttpPost]
        public IActionResult Edit(Movie m)
        {
            if (ModelState.IsValid)
            {
                icinemamanager.DeleteMovie(m.MovieId);
                icinemamanager.AddMovie(m);
                refreshViewBag();
                return RedirectToAction("ListMovies");
            }
            else
            {
                return View(m);
            }
        }
            public void refreshViewBag() {
            ViewBag.movies= icinemamanager.ListMovies();
        }
        public ViewResult ListMovies()
        {
            refreshViewBag();
            return View();
        }
        [HttpPost]
        public IActionResult Delete(int ID)
        {
            refreshViewBag();
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
