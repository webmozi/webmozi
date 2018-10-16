using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        [HttpGet]
        public ViewResult AListRooms()
        {
            return View("Admin/ListRooms",icinemamanager.ListRooms());
        }
        [HttpGet]
        public ViewResult AListSeats(int id)
        {
           IEnumerable<DTO.Seat> seatslist = icinemamanager.ListSeatsInRoom(id);
            return View("Admin/ListSeats", seatslist);
        }
        [HttpPost]
        public ViewResult AddRoom(DTO.Room m)
        {
            icinemamanager.CreateRoom(m);
            return AListRooms();
        }
        [HttpGet]
        public ViewResult AAddRoom()
        {
            return View("Admin/AddRoom");
        }

        [HttpPost]
        public ViewResult AddMovie(DTO.Movie m)
        {
            icinemamanager.AddMovie(m);
            return AListMovies();
        }
        [HttpGet]
        public ViewResult ADeleteMovie(int ID)
        {
            TempData["message"] = $"{icinemamanager.SelectMovie(ID).Title} was removed";
            icinemamanager.DeleteMovie(ID);
            return AListMovies();
        }
        [HttpGet]
        public ViewResult ADeleteRoom(int ID)
        {
            TempData["message"] = $"{icinemamanager.SelectRoom(ID).RoomNumber} was removed";
            icinemamanager.DeleteRoom(ID);
            return AListRooms();
        }
        [HttpGet]
        public ViewResult ADeleteMovieEvent(int ID)
        {
            TempData["message"] = $"{icinemamanager.SelectMovieEvent(ID).Movie.Title} was removed";
            icinemamanager.DeleteMovieEvent(ID);
            return AListEvents();
        }
        [HttpGet]
        public ViewResult AEdit(int id)
        {
            DTO.Movie editmovie = icinemamanager.SelectMovie(id);
            return View("Admin/EditMovie", editmovie);
        }
        [HttpGet]
        public ViewResult AListEvents()
        {
            return View("Admin/ListMovieEvents", icinemamanager.ListMovieEvents());
        }
        [HttpGet]
        public ViewResult ACreateEvent()
        {
            SelectList listmovie = new SelectList(icinemamanager.ListMovies(), "MovieId","Title");
            SelectList listroom = new SelectList(icinemamanager.ListRooms(),"Id","RoomNumber");
            ViewBag.movielist = listmovie;
            ViewBag.roomlist = listroom;
            return View("Admin/CreateMovieEvent");
        }
        [HttpPost]
        public IActionResult CreateEvent(DTO.MovieEvent me)
        {
            DTO.MovieEvent mevent = new DTO.MovieEvent();
            mevent.Movie = icinemamanager.SelectMovie(me.Movie.MovieId);
            mevent.Room = icinemamanager.SelectRoom(me.Room.Id);
            mevent.Time = me.Time;
            icinemamanager.AddMovieEvent(mevent);
            TempData["message"] = $"{mevent.Movie.Title} has been saved";
            return AListEvents();
        }
        [HttpGet]
        public ViewResult UListEvents()
        {
            return View("User/ListMovieEvents", icinemamanager.ListMovieEvents());
        }
        [HttpGet]
        public ViewResult AEvent(int id)
        {
            DTO.MovieEvent selectedevent = icinemamanager.SelectMovieEvent(id);
            return View("Admin/ListMovieEvents", selectedevent);
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
        public ViewResult ChooseMovieEvent(DTO.MovieEvent m,int seatID)
        {
            ReservationID = ireservationmanager.AddReservation(m, seatID);
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
