using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/movieeventheader")]
    [ApiController]
    public class MovieEventHeaderController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<DTO.MovieEventHeader>> Get()
        {
            DAL.CinemaManager cinemamanager = new DAL.CinemaManager();
            List<DTO.MovieEventHeader> dtomovieevents = new List<DTO.MovieEventHeader>();
            List<DAL.MovieEvent> dalmovieevents = cinemamanager.ListMovieEventsWithRoomAndMovie();

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
                movieevent.Room.RoomId = dalme.Room.RoomId;
                movieevent.Room.RoomNumber = dalme.Room.RoomNumber;
                dtomovieevents.Add(movieevent);
            }
            return dtomovieevents;
        }
    }
}