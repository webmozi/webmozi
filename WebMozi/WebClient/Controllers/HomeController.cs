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
        private  static int ReservationID;
        private int idd;
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
        public ViewResult UCreateUser()
        {
            return View("User/CreateUser");
        }

        [HttpPost]
        public ViewResult CreateUser(DTO.User u)
        {
            ireservationmanager.AddUser(u);
            ireservationmanager.AddUserToReservation(ReservationID, u);
            return UListSeatsInMovieEvent();
        }
        [HttpGet]
        public ViewResult AAddRoom()
        {
            return View("Admin/AddRoom");
        }
        [HttpGet]
        public ViewResult UListSeatsInMovieEvent()
        {
            DTO.Reservation res=ireservationmanager.GetReservation(ReservationID);
            return View("User/ListSeatsInMovieEvent",res.MovieEvent.Room.Seats);
        }
        [HttpGet]
        public ViewResult UReservation()
        {
            return View("User/Reservation", ireservationmanager.GetReservation(ReservationID));
        }
        [HttpPost]
        public ViewResult AddMovie(DTO.Movie m)
        {
            icinemamanager.AddMovie(m);
            return AListMovies();
        }
        [HttpGet]
        public ViewResult ADeleteUser(int ID)
        {
            TempData["message"] = $"{ireservationmanager.SelectUser(ID).Name} was removed";
            ireservationmanager.DeleteUser(ID);
            return AListUsers();
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
        public ViewResult AEditUser(int id)
        {
            DTO.User edituser = ireservationmanager.SelectUser(id);
            return View("Admin/EditUser", edituser);
        }
        [HttpGet]
        public ViewResult AEditMovie(int id)
        {
            DTO.Movie editmovie = icinemamanager.SelectMovie(id);
            return View("Admin/EditMovie", editmovie);
        }
        [HttpGet]
        public ViewResult UID(int id)
        {
            idd = id;
            return UChooseSeat(idd);
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

        [HttpGet]
        public ViewResult USelectedMovieEvent(int meId)
        {
            DTO.MovieEvent me = icinemamanager.SelectMovieEvent(meId);
            ReservationID = ireservationmanager.CreateReservationOnlyWithMovieEvent(me);
            return View("User/ChooseSeats", me.Room.Seats.ToList());
        }
        [HttpGet]
        public ViewResult UChooseSeatMore()
        {
            return View("User/ChooseSeats", ireservationmanager.GetReservation(ReservationID).MovieEvent.Room.Seats);
        }
        [HttpGet]
        public ViewResult UChooseSeat(int seatid)
        {
            DTO.Reservation res = ireservationmanager.GetReservation(ReservationID);
            for (int i = 0; i < res.MovieEvent.Room.Seats.Count; i++)
            {
                if (res.MovieEvent.Room.Seats.ElementAt(i).ID == seatid)
                {
                    res.MovieEvent.Room.Seats.ElementAt(i).IsEnable = false;
                    res = ireservationmanager.AddSeatToReservation(ReservationID, res.MovieEvent.Room.Seats.ElementAt(i));
                }
            }
            return UChooseSeatMore();
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
        public ViewResult AListUsers()
        {
            return View("Admin/ListUsers", ireservationmanager.ListUsers());
        }
        [HttpGet]
        public ViewResult AEvent(int id)
        {
            DTO.MovieEvent selectedevent = icinemamanager.SelectMovieEvent(id);
            return View("Admin/ListMovieEvents", selectedevent);
        }
        [HttpPost]
        public IActionResult EditMovie(DTO.Movie m)
        {  
                DTO.Movie movie=icinemamanager.EditMovie(m);
                TempData["message"] = $"{movie.Title} has been saved";
                return AListMovies();
        }
       
        [HttpPost]
        public IActionResult EditUser(DTO.User u)
        {
            DTO.User user=ireservationmanager.EditUser(u);
            TempData["message"] = $"{user.Name} has been saved";
            return AListUsers();
        }
       
      
        
       /* //View hiányzik
        public ViewResult MakingReservation() {
            ireservationmanager.ReservationToUser(UserID, ReservationID);
            return View();
        }
        //View hiányzik
        public ViewResult GetTicket() {
            DTO.User user = ireservationmanager.SelectUser(UserID);
            return View(user.Reservations);
        }*/
    }
}
