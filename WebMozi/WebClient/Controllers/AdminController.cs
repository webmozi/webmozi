using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebClient.Controllers
{
    public class AdminController : HomeController
    {
        [HttpGet]
        public ViewResult Main()
        {
            return View("Main");
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
            icinemamanager.AddMovie(m);
            return ListMovies();
        }
        [HttpGet]
        public ViewResult DeleteMovie(int ID)
        {
            TempData["message"] = $"{icinemamanager.SelectMovie(ID).Title} was removed";
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
            TempData["message"] = $"{movie.Title} has been saved";
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
        public ViewResult AddRoom(DTO.Room m)
        {
            icinemamanager.CreateRoom(m);
            return ListRooms();
        }
        [HttpGet]
        public ViewResult DeleteRoom(int ID)
        {
            TempData["message"] = $"{icinemamanager.SelectRoom(ID).RoomNumber} was removed";
            icinemamanager.DeleteRoom(ID);
            return ListRooms();
        }



        [HttpGet]
        public ViewResult ListEvents()
        {
            return View("ListMovieEvents", icinemamanager.ListMovieEvents());
        }
        [HttpGet]
        public ViewResult CreateEvent()
        {
            SelectList listmovie = new SelectList(icinemamanager.ListMovies(), "MovieId", "Title");
            SelectList listroom = new SelectList(icinemamanager.ListRooms(), "Id", "RoomNumber");
            ViewBag.movielist = listmovie;
            ViewBag.roomlist = listroom;
            return View("CreateMovieEvent");
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
            return ListEvents();
        }
        [HttpGet]
        public ViewResult DeleteMovieEvent(int ID)
        {
            TempData["message"] = $"{icinemamanager.SelectMovieEvent(ID).Movie.Title} was removed";
            icinemamanager.DeleteMovieEvent(ID);
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
            TempData["message"] = $"{ireservationmanager.SelectUser(ID).Name} was removed";
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
            TempData["message"] = $"{user.Name} has been saved";
            return ListUsers();
        }



        [HttpGet]
        public ViewResult ListSeats(int id)
        {
            IEnumerable<DTO.Seat> seatslist = icinemamanager.ListSeatsInRoom(id);
            return View("ListSeats", seatslist);
        }
    }
}