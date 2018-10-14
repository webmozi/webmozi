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

            return View("MainView");
        }
        [HttpGet]
        public ViewResult AMain()
        {
            return View("Admin/Main");
        }
        [HttpGet]
        public ViewResult UMain()
        {
            return View("User/Main");
        }

        [HttpGet]
        public ViewResult AListMovies()
        {
            return View("Admin/ListMovies", icinemamanager.ListMovies());
        }
        [HttpGet]
        public ViewResult UListMovies()
        {
            return View("User/ListMovies", icinemamanager.ListMovies());
        }
        [HttpGet]
        public ViewResult AAddMovie()
        {
            return View("Admin/AddMovie");
        }

        [HttpPost]
        public ViewResult AddMovie(DTO.Movie m)
        {
            icinemamanager.AddMovie(m);
            return AListMovies();
        }
        [HttpGet]
        public ViewResult ADelete(int ID)
        {
            TempData["message"] = $"{icinemamanager.SelectMovie(ID).Title} was removed";
            icinemamanager.DeleteMovie(ID);
            return AListMovies();
        }
        [HttpPost]
        public ActionResult Delete(int ID)
        {   
            return RedirectToAction("ADelete",ID);
        }
        [HttpGet]
        public ViewResult AEdit(int id)
        {
            DTO.Movie EditMovie = icinemamanager.SelectMovie(id);
            return View("Admin/Edit", EditMovie);
        }
        [HttpPost]
        public IActionResult Edit(DTO.Movie m)
        {  
                icinemamanager.EditMovie(m);
                TempData["message"] = $"{m.Title} has been saved";
                return AListMovies();
        }
        [HttpGet]
        public ViewResult UDateSelect(int MovieId)
        {
            DTO.Movie SelectedMovie = icinemamanager.SelectMovie(MovieId);
            return View("User/DateSelect", SelectedMovie);
        }
        //View hiányzik
        public ViewResult ChooseMovieEvent(DTO.MovieEvent m)
        {
            ReservationID = ireservationmanager.AddReservation(m);
            return View();
        }
        //View hiányzik
        public ViewResult CreateUser(DTO.User user) {
            UserID= ireservationmanager.AddUser(user).Id;
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
