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
            List<DTO.MovieEvent> dtomovieevents = new List<DTO.MovieEvent>();
            List<DAL.MovieEvent> dalmovieevents = DAL.MovieEventManager.ListMovieEventsWithRoomAndMovie();
         
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
                movieevent.Movie.Length = dalme.Movie.Length;
                movieevent.Movie.Img = dalme.Movie.Img;
                movieevent.Room.RoomId = dalme.Room.RoomId;
                movieevent.Room.RoomNumber = dalme.Room.RoomNumber;
                movieevent.Room.Capacity = dalme.Room.Capacity;
                List<DAL.Seat> seats = DAL.MovieEventManager.ListSeatsInMovieEvent(dalme.MovieEventId);
                foreach (var dalseat in seats)
                {
                    DTO.MovieEventSeat dtoseat = new DTO.MovieEventSeat();
                    dtoseat.RowNumber = dalseat.RowNumber;
                    dtoseat.SeatNumber = dalseat.SeatNumber;
                    dtoseat.SeatId = dalseat.SeatId;
                    movieevent.Room.Seats.Add(dtoseat);
                }
                dtomovieevents.Add(movieevent);
            }
            return dtomovieevents;
        }
        [HttpGet("movieeventheader")]
        public ActionResult<List<DTO.MovieEventHeader>> GetMovieEventHeader()
        {
            List<DTO.MovieEventHeader> dtomovieevents = new List<DTO.MovieEventHeader>();
            List<DAL.MovieEvent> dalmovieevents = DAL.MovieEventManager.ListMovieEventsWithRoomAndMovie();
            foreach (var dalme in dalmovieevents)
            {
                DTO.MovieEventHeader movieevent = new DTO.MovieEventHeader();
                movieevent.MovieEventId = dalme.MovieEventId;
                movieevent.Time = dalme.TimeOfEvent;
                movieevent.Movie = new DTO.Movie();
                movieevent.Room = new DTO.RoomHeader();
                movieevent.Movie.Director = dalme.Movie.Director;
                movieevent.Movie.MovieId = dalme.Movie.MovieId;
                movieevent.Movie.Title = dalme.Movie.Title;
                movieevent.Movie.Length = dalme.Movie.Length;
                movieevent.Movie.Img = dalme.Movie.Img;
                movieevent.Room.RoomId = dalme.Room.RoomId;
                movieevent.Room.RoomNumber = dalme.Room.RoomNumber;
                dtomovieevents.Add(movieevent);
            }
            return dtomovieevents;
        }
        [HttpGet("enableseats/{id}")]
        public ActionResult<List<DTO.MovieEventSeat>> GetEnableSeatsInMovieEvent(int id)
        {
            List<DAL.Seat> dalenableevents = DAL.MovieEventManager.ListFreeSeatsForMovieEvent(id);
            List<DTO.MovieEventSeat> dtoenableseats = new List<DTO.MovieEventSeat>();
            foreach (var dales in dalenableevents)
            {
                  DTO.MovieEventSeat dtoseat = new DTO.MovieEventSeat();
                  dtoseat.RowNumber = dales.RowNumber;
                  dtoseat.SeatNumber = dales.SeatNumber;
                  dtoseat.SeatId = dales.SeatId;
                 dtoenableseats.Add(dtoseat);
            }
            return dtoenableseats;

        }
        [HttpGet("{id}")]
        public ActionResult<DTO.MovieEvent> GetById(int id)
        {
            var dalme=DAL.MovieEventManager.GetMovieEventById(id);
            DTO.MovieEvent movieevent = new DTO.MovieEvent();
            movieevent.MovieEventId = dalme.MovieEventId;
            movieevent.Time = dalme.TimeOfEvent;
            movieevent.Movie = new DTO.Movie();
            movieevent.Room = new DTO.Room();
            movieevent.Room.Seats = new List<DTO.MovieEventSeat>();
            movieevent.Movie.Director = dalme.Movie.Director;
            movieevent.Movie.MovieId = dalme.Movie.MovieId;
            movieevent.Movie.Title = dalme.Movie.Title;
            movieevent.Movie.Length = dalme.Movie.Length;
            movieevent.Movie.Img = dalme.Movie.Img;
            movieevent.Room.RoomId = dalme.Room.RoomId;
            movieevent.Room.RoomNumber = dalme.Room.RoomNumber;
            movieevent.Room.Capacity = dalme.Room.Capacity;
            List<DAL.Seat> seats = DAL.MovieEventManager.ListSeatsInMovieEvent(dalme.MovieEventId);
            foreach (var dalseat in seats)
            {
                DTO.MovieEventSeat dtoseat = new DTO.MovieEventSeat();
                dtoseat.RowNumber = dalseat.RowNumber;
                dtoseat.SeatNumber = dalseat.SeatNumber;
                dtoseat.SeatId = dalseat.SeatId;
                movieevent.Room.Seats.Add(dtoseat);
            }
            return movieevent;
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            DAL.MovieEventManager.DeleteMovieEvent(id);
            return NoContent();
        }
        [HttpPost]
        public IActionResult Create(DTO.MovieEvent item)
        {
            var dalitem = new DAL.MovieEvent();
            dalitem.TimeOfEvent = item.Time;
            dalitem.RoomId = item.Room.RoomId;
            dalitem.MovieId = item.Movie.MovieId;
            DAL.MovieEventManager.AddMovieEvent(dalitem);
            return Created("http://localhost:6544/api/movieevents", item);
        }

    }
}