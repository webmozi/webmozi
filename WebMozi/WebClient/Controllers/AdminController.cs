using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebClient.Models;

namespace WebClient.Controllers
{
    public class AdminController : HomeController
    {
        [HttpGet]
        public ViewResult Main()
        {
            return View("Login");
        }
        [HttpGet]
        public ViewResult AdminMain()
        {
            return View("Main");
        }
        [HttpPost]
        public ViewResult Login(DTO.User u)
        {
           ireservationmanager.LogInAdmin(u);

            int aid = ireservationmanager.SignedAdminId();
            if (aid == -1)
            {
                TempData["invalid"] = $"Invalid username or password";
                return Main();
            }
            else
            {
                TempData["success"] = $"Successfull log in!";
                return AdminMain();
            }
        }
        public ViewResult MovieMain()
        {
            return View("MovieMain");
        }
        [HttpGet]
        public ViewResult RoomMain()
        {
            return View("RoomMain");
        }
        [HttpGet]
        public ViewResult MovieEventMain()
        {
            return View("MovieEventMain");
        }
        [HttpGet]
        public ViewResult UserReservationMain()
        {
            return View("UserReservationMain");
        }
        [HttpGet]
        public ViewResult ListMovies()
        {
            return View("ListMovies", icinemamanager.ListMovies());
        }
        [HttpGet]
        public ViewResult AddMovie()
        {
            return View("AddMovie");
        }
        [HttpPost]
        public ViewResult AddMovie(DTO.Movie m)
        {
            TempData["add"] = $"{m.Title} added!";
            icinemamanager.AddMovie(m);
            return ListMovies();
        }
        [HttpGet]
        public ViewResult DeleteMovie(int ID)
        {
            TempData["remove"] = $"{icinemamanager.SelectMovie(ID).Title} was removed!";
            icinemamanager.DeleteMovie(ID);
            return ListMovies();
        }
        [HttpGet]
        public ViewResult EditMovie(int id)
        {
            DTO.Movie editmovie = icinemamanager.SelectMovie(id);
            return View("EditMovie", editmovie);
        }
        [HttpPost]
        public IActionResult EditMovie(DTO.Movie m)
        {
            DTO.Movie movie = icinemamanager.EditMovie(m);
            TempData["edit"] = $"{movie.Title} has been saved!";
            return ListMovies();
        }



        [HttpGet]
        public ViewResult ListRooms()
        {
            return View("ListRooms", icinemamanager.ListRooms());
        }
        [HttpGet]
        public ViewResult AddRoom()
        {
            return View("AddRoom");
        }
        [HttpPost]
        public ViewResult AddRoom(DTO.Room r)
        {
            TempData["add"] = $"{r.RoomNumber} added!";
            icinemamanager.AddRoom(r);
            return ListRooms();
        }
        [HttpGet]
        public ViewResult DeleteRoom(int ID)
        {
            TempData["delete"] = $"{icinemamanager.SelectRoom(ID).RoomNumber} was removed!";
            icinemamanager.DeleteRoom(ID);
            return ListRooms();
        }



        [HttpGet]
        public ViewResult ListEvents()
        {
            return View("ListMovieEvents", icinemamanager.ListMovieEventsWithoutSeats());
        }
        [HttpGet]
        public ViewResult CreateEvent()
        {
            SelectList listmovie = new SelectList(icinemamanager.ListMovies(), "MovieId", "Title");
            SelectList listroom = new SelectList(icinemamanager.ListRooms(), "RoomId", "RoomNumber");
            ViewBag.movielist = listmovie;
            ViewBag.roomlist = listroom;
            return View("CreateMovieEvent");
        }
        [HttpPost]
        public IActionResult CreateEvent(DTO.MovieEvent me)
        {
            DTO.MovieEvent mevent = new DTO.MovieEvent();
            mevent.Movie = icinemamanager.SelectMovie(me.Movie.MovieId);
            mevent.Room = icinemamanager.SelectRoom(me.Room.RoomId);
            mevent.Time = me.Time;
            icinemamanager.AddMovieEvent(mevent);
            TempData["add"] = $"{mevent.Movie.Title} added at {mevent.Time} in Room {mevent.Room.RoomNumber}!";
            return ListEvents();
        }
        [HttpGet]
        public ViewResult DeleteMovieEvent(int Id)
        {
            DTO.MovieEvent mevent = icinemamanager.SelectMovieEvent(Id);
            TempData["delete"] = $"{mevent.Movie.Title} at {mevent.Time} in Room {mevent.Room.RoomNumber} was removed !";
            icinemamanager.DeleteMovieEvent(Id);
            return ListEvents();
        }



        [HttpGet]
        public ViewResult ListUsers()
        {
            return View("ListUsers", ireservationmanager.ListUsers());
        }
        [HttpGet]
        public ViewResult DeleteUser(int ID)
        {
            TempData["delete"] = $"{ireservationmanager.SelectUser(ID).Name} was removed!";
            ireservationmanager.DeleteUser(ID);
            return ListUsers();
        }
        [HttpGet]
        public ViewResult EditUser(int id)
        {
            DTO.User edituser = ireservationmanager.SelectUser(id);
            return View("EditUser", edituser);
        }
        [HttpPost]
        public IActionResult EditUser(DTO.User u)
        {
            DTO.User user = ireservationmanager.EditUser(u);
            TempData["edit"] = $"{user.Name} has been saved!";
            return ListUsers();
        }
        

        [HttpGet]
        public ViewResult ListSeats(int id)
        {
            IEnumerable<DTO.MovieEventSeat> seatslist = icinemamanager.ListSeatsInRoom(id);
            return View("ListSeats", seatslist);
        }

        [HttpGet]
        public ViewResult ListReservation()
        {
            return View("ListReservation", ireservationmanager.ListReservations().ToList());
        }
        [HttpGet]
        public ViewResult DeleteReservation(int id)
        {
            TempData["delete"] = $"{ireservationmanager.SelectReservation(id).ReservationId} was removed!";
            ireservationmanager.DeleteReservation(id);
            return ListReservation();
        }
    }
}