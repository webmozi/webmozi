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
        private IReservationManager irm = ManagerProvider.Instance.GetReservationManager();
        private ICinemaManager icm = ManagerProvider.Instance.GetCinemaManager();
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
        public ViewResult AddMovie(MovieEvent m)
        {
                icm.AddMovie(m);
                return View("MainView", m);
            
        }
        public ViewResult ListMovies()
        {
            return View(icm.ListMovies());
        }
        [HttpPost]
        public IActionResult Delete(int ID)
        {
            icm.DeleteMovie(ID);
            return RedirectToAction("ListMovies");
        }
       //View hiányzik
        public ViewResult ChooseMovie(MovieEvent m)
        {
            ReservationID = irm.MakeReservation(m);
            return View();
        }
        //View hiányzik
        public ViewResult CreateUser(User user) {
            UserID=irm.AddUser(user);
            return View();
        }
        //View hiányzik
        public ViewResult MakingReservation() {
            irm.ReservationToUser(UserID, ReservationID);
            return View();
        }
        //View hiányzik
        public ViewResult GetTicket() {
            User user = irm.GetUserInList(UserID);
            return View(user.Reservations);
        }
    }
}
