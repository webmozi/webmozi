using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/reservation")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<DTO.Reservation>> Get()
        {
            List<DTO.Reservation> reservationlist = new List<DTO.Reservation>();
            DAL.CinemaManager cinemamanager = new DAL.CinemaManager();
            List<DAL.Reservation> dallist = cinemamanager.ListReservations();
            foreach (var res in dallist)
            {
                reservationlist.Add(new DTO.Reservation
                {
                    ReservationId = res.ReservationId
                });
            }
            return reservationlist;
        }
        [HttpPost("onlywithmovieevent")]
        public IActionResult ReservationWithMovieEvent(DTO.Reservation res)
        {
            DAL.CinemaManager cinemamanager = new DAL.CinemaManager();
            var dalitem = new DAL.Reservation();
            dalitem.MovieEventId = res.MovieEvent.MovieEventId;
            cinemamanager.AddReservationOnlyWithMovieEvent(dalitem);
            return Created("http://localhost:6544/api/reservation/onlywithmovieevent", dalitem);
        }
        [HttpPost("seattoreservation")]
        public IActionResult SeatToReservation(DTO.Reservation res)
        {
            DAL.CinemaManager cinemamanager = new DAL.CinemaManager();
            var dalitem = new DAL.Reservation();
            dalitem.ReservationId = res.ReservationId;
            dalitem.MovieEventId = res.MovieEvent.MovieEventId;
            dalitem.SeatId = res.Seat.SeatId;
            cinemamanager.AddSeatToReservation(dalitem);
            return Created("http://localhost:6544/api/reservation/seattoreservation", dalitem);
        }
        
        [HttpPost("usertoreservation")]
        public IActionResult UserToReservation(DTO.Reservation res)
        {
            DAL.CinemaManager cinemamanager = new DAL.CinemaManager();
            var dalitem = new DAL.Reservation();
            dalitem.ReservationId = res.ReservationId;
            dalitem.MovieEventId = res.MovieEvent.MovieEventId;
            dalitem.SeatId = res.Seat.SeatId;
            dalitem.UserId = res.User.UserId;
            cinemamanager.AddUserToReservation(dalitem);
            return Created("http://localhost:6544/api/reservation/usertoreservation", dalitem);
        }
        [HttpGet("{id}")]
        public ActionResult<DTO.Reservation> GetById(int id)
        {
            DAL.CinemaManager cinemamanager = new DAL.CinemaManager();
            DAL.Reservation dalreservation = cinemamanager.GetReservationById(id);
            DTO.Reservation dtoreservation = new DTO.Reservation();
            dtoreservation.ReservationId = dalreservation.ReservationId;
            return dtoreservation;
        }
        [HttpGet("withmovieevent/{id}")]
        public ActionResult<DTO.Reservation> GetByIdWithMovieEvent(int id)
        {
            DAL.CinemaManager cinemamanager = new DAL.CinemaManager();
            DAL.Reservation dalreservation = cinemamanager.GetReservationByIdWithMovieEvent(id);
            DTO.Reservation dtoreservation = new DTO.Reservation();
            dtoreservation.ReservationId = dalreservation.ReservationId;
            dtoreservation.MovieEvent = new DTO.MovieEvent();
            dtoreservation.MovieEvent.Movie = new DTO.Movie();
            dtoreservation.MovieEvent.Room = new DTO.Room();
            dtoreservation.MovieEvent.MovieEventId = dalreservation.MovieEventId;
            dtoreservation.MovieEvent.Time = dalreservation.MovieEvent.TimeOfEvent;
            dtoreservation.MovieEvent.Movie.Director = dalreservation.MovieEvent.Movie.Director;
            dtoreservation.MovieEvent.Movie.MovieId = dalreservation.MovieEvent.Movie.MovieId;
            dtoreservation.MovieEvent.Movie.Title = dalreservation.MovieEvent.Movie.Title;
            dtoreservation.MovieEvent.Room.Capacity = dalreservation.MovieEvent.Room.Capacity;
            dtoreservation.MovieEvent.Room.RoomId = dalreservation.MovieEvent.Room.RoomId;
            dtoreservation.MovieEvent.Room.RoomNumber = dalreservation.MovieEvent.Room.RoomNumber;
            dtoreservation.MovieEvent.Room.Seats = new List<DTO.MovieEventSeat>();
             foreach (var dalseat in dalreservation.MovieEvent.Room.Seats)
                {
                DTO.MovieEventSeat dtoseat = new DTO.MovieEventSeat();
                dtoseat.RowNumber = dalseat.RowNumber;
                dtoseat.SeatId = dalseat.SeatId;
                dtoseat.SeatNumber = dalseat.SeatNumber;
                dtoreservation.MovieEvent.Room.Seats.Add(dtoseat);
                }
            return dtoreservation;
        }
        [HttpGet("withmeandseat/{id}")]
        public ActionResult<DTO.Reservation> GetByIdWithMovieEventAndSeat(int id)
        {
            DAL.CinemaManager cinemamanager = new DAL.CinemaManager();
            DAL.Reservation dalreservation = cinemamanager.GetReservationByIdWithMovieEvent(id);
            DTO.Reservation dtoreservation = new DTO.Reservation();
            dtoreservation.ReservationId = dalreservation.ReservationId;
            dtoreservation.MovieEvent = new DTO.MovieEvent();
            dtoreservation.MovieEvent.Movie = new DTO.Movie();
            dtoreservation.MovieEvent.Room = new DTO.Room();
            dtoreservation.MovieEvent.MovieEventId = dalreservation.MovieEventId;
            dtoreservation.MovieEvent.Time = dalreservation.MovieEvent.TimeOfEvent;
            dtoreservation.MovieEvent.Movie.Director = dalreservation.MovieEvent.Movie.Director;
            dtoreservation.MovieEvent.Movie.MovieId = dalreservation.MovieEvent.Movie.MovieId;
            dtoreservation.MovieEvent.Movie.Title = dalreservation.MovieEvent.Movie.Title;
            dtoreservation.MovieEvent.Room.Capacity = dalreservation.MovieEvent.Room.Capacity;
            dtoreservation.MovieEvent.Room.RoomId = dalreservation.MovieEvent.Room.RoomId;
            dtoreservation.MovieEvent.Room.RoomNumber = dalreservation.MovieEvent.Room.RoomNumber;
            dtoreservation.MovieEvent.Room.Seats = new List<DTO.MovieEventSeat>();
            foreach (var dalseat in dalreservation.MovieEvent.Room.Seats)
            {
                DTO.MovieEventSeat dtoseat = new DTO.MovieEventSeat();
                dtoseat.RowNumber = dalseat.RowNumber;
                dtoseat.SeatId = dalseat.SeatId;
                dtoseat.SeatNumber = dalseat.SeatNumber;
                dtoreservation.MovieEvent.Room.Seats.Add(dtoseat);
            }
            DAL.Seat dalseatt =cinemamanager.GetSeatById(dalreservation.SeatId);
            dtoreservation.Seat= new DTO.MovieEventSeat();
            dtoreservation.Seat.SeatId = dalseatt.SeatId;
            dtoreservation.Seat.SeatNumber = dalseatt.SeatNumber;
            dtoreservation.Seat.RowNumber = dalseatt.RowNumber;
            return dtoreservation;
        }
        [HttpGet("allin/{id}")]
        public ActionResult<DTO.Reservation> GetByIdAllIn(int id)
        {
            DAL.CinemaManager cinemamanager = new DAL.CinemaManager();
            DAL.Reservation dalreservation = cinemamanager.GetReservationByIdWithMovieEvent(id);
            DTO.Reservation dtoreservation = new DTO.Reservation();
            dtoreservation.ReservationId = dalreservation.ReservationId;
            dtoreservation.MovieEvent = new DTO.MovieEvent();
            dtoreservation.MovieEvent.Movie = new DTO.Movie();
            dtoreservation.MovieEvent.Room = new DTO.Room();
            dtoreservation.MovieEvent.MovieEventId = dalreservation.MovieEventId;
            dtoreservation.MovieEvent.Time = dalreservation.MovieEvent.TimeOfEvent;
            dtoreservation.MovieEvent.Movie.MovieId = dalreservation.MovieEvent.Movie.MovieId;
            dtoreservation.MovieEvent.Movie.Title = dalreservation.MovieEvent.Movie.Title;
            dtoreservation.MovieEvent.Room.RoomId = dalreservation.MovieEvent.Room.RoomId;
            dtoreservation.MovieEvent.Room.RoomNumber = dalreservation.MovieEvent.Room.RoomNumber;
            DAL.User daluser = cinemamanager.GetUserById(dalreservation.UserId);
            dtoreservation.User = new DTO.User();
            dtoreservation.User.Name = daluser.Name;
            dtoreservation.User.UserId = daluser.UserId;
            DAL.Seat dalseatt = cinemamanager.GetSeatById(dalreservation.SeatId);
            dtoreservation.Seat = new DTO.MovieEventSeat();
            dtoreservation.Seat.SeatId = dalseatt.SeatId;
            dtoreservation.Seat.SeatNumber = dalseatt.SeatNumber;
            dtoreservation.Seat.RowNumber = dalseatt.RowNumber;
            return dtoreservation;
        }
    }
}