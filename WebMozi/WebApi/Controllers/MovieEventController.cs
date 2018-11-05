using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/movieevents")]
    [ApiController]
    public class MovieEventController : Controller
    {
        public MovieEventController()
        {
        }
        [HttpGet]
        public ActionResult<List<DTO.MovieEvent>> Get()
        {
            DAL.CinemaManager cinemamanager = new DAL.CinemaManager();
            List<DTO.MovieEvent> dtomovieevents = new List<DTO.MovieEvent>();
            List<DAL.MovieEvent> dalmovieevents = cinemamanager.ListMovieEventsWithRoomAndMovie();
         
            foreach (var dalme in dalmovieevents)
            {
                DTO.MovieEvent movieevent = new DTO.MovieEvent();
                movieevent.MovieEventId = dalme.MovieEventId;
                movieevent.Time = dalme.TimeOfEvent;
                movieevent.Movie = new DTO.Movie();
                movieevent.Room = new DTO.Room();
                movieevent.Room.Seats = new List<DTO.MovieEventSeat>();
                movieevent.Movie.Director = dalme.Movie.Director;
                movieevent.Movie.MovieId = dalme.Movie.MovieId;
                movieevent.Movie.Title = dalme.Movie.Title;
                movieevent.Room.RoomId = dalme.Room.RoomId;
                movieevent.Room.RoomNumber = dalme.Room.RoomNumber;
                movieevent.Room.Capacity = dalme.Room.Capacity;
                List<DAL.Seat> enableseats = cinemamanager.ListFreeSeatsForMovieEvent(dalme.MovieEventId);
                //Hiányzik a tényleges
                DTO.MovieEventSeat s = new DTO.MovieEventSeat();
                movieevent.Room.Seats.Add(s);

                dtomovieevents.Add(movieevent);
            }
            return dtomovieevents;
        }
        [HttpGet("{id}")]
        public ActionResult<DTO.MovieEvent> GetById(int id)
        {
            DAL.CinemaManager cinemamanager = new DAL.CinemaManager();
            var dalme=cinemamanager.GetMovieEventById(id);
            DTO.MovieEvent movieevent = new DTO.MovieEvent();
            movieevent.MovieEventId = dalme.MovieEventId;
            movieevent.Time = dalme.TimeOfEvent;
            movieevent.Movie = new DTO.Movie();
            movieevent.Room = new DTO.Room();
            movieevent.Room.Seats = new List<DTO.MovieEventSeat>();
            movieevent.Movie.Director = dalme.Movie.Director;
            movieevent.Movie.MovieId = dalme.Movie.MovieId;
            movieevent.Movie.Title = dalme.Movie.Title;
            movieevent.Room.RoomId = dalme.Room.RoomId;
            movieevent.Room.RoomNumber = dalme.Room.RoomNumber;
            movieevent.Room.Capacity = dalme.Room.Capacity;
            List<DAL.Seat> enableseats = cinemamanager.ListFreeSeatsForMovieEvent(dalme.MovieEventId);
            //Hiányzik a tényleges
            DTO.MovieEventSeat s = new DTO.MovieEventSeat();
            movieevent.Room.Seats.Add(s);

            return movieevent;
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            DAL.CinemaManager cinemamanager = new DAL.CinemaManager();
            cinemamanager.DeleteMovieEvent(id);
            return NoContent();
        }

        [HttpPost]
        public IActionResult Create(DTO.MovieEvent item)
        {
            DAL.CinemaManager cinemamanager = new DAL.CinemaManager();
            var dalitem = new DAL.MovieEvent();
            dalitem.TimeOfEvent = item.Time;
            dalitem.RoomId = item.Room.RoomId;
            dalitem.MovieId = item.Movie.MovieId;
            cinemamanager.AddMovieEvent(dalitem);
            return Created("http://localhost:6544/api/movieevents", item);
        }

    }
}