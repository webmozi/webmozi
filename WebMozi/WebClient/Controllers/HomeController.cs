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

            return ListMovies();
        }
        [HttpGet]
        public ViewResult AddMovie()
        {
            return View();
        }
        [HttpPost]
        public ViewResult AddMovie(DTO.Movie m)
        {
            icinemamanager.AddMovie(m);
            return View("ListMovies", icinemamanager.ListMovies());
        }

        [HttpGet]
        public ViewResult Edit(int MovieId)
        {
            DTO.Movie EditingMovie = icinemamanager.SelectMovie(MovieId);

            return View(EditingMovie);
        }
     
        [HttpPost]
        public IActionResult Edit(DTO.Movie m)
        {
            if (ModelState.IsValid)
            {
                icinemamanager.DeleteMovie(m.MovieId);
                icinemamanager.AddMovie(m);
                return RedirectToAction("ListMovies", icinemamanager.ListMovies());
            }
            else
            {
                return View();
            }
        }
       
        public ViewResult ListMovies()
        {
            return View("ListMovies", icinemamanager.ListMovies());
        }
        [HttpPost]
        public IActionResult Delete(int ID)
        {
            icinemamanager.DeleteMovie(ID);
            return RedirectToAction("ListMovies");
        }
       //View hiányzik
        public ViewResult ChooseMovieEvent(DTO.MovieEvent m)
        {
            ReservationID = ireservationmanager.AddReservation(m);
            return View();
        }
        //View hiányzik
        public ViewResult CreateUser(DTO.User user) {
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
            DTO.User user = ireservationmanager.GetUser(UserID);
            return View(user.Reservations);
        }
    }
}
