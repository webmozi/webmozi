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
        [HttpPost]
        public IActionResult Create(DTO.Reservation item)
        {
            DAL.CinemaManager cinemamanager = new DAL.CinemaManager();
            var dalitem = new DAL.Reservation();
            dalitem.MovieEventId = item.MovieEvent.MovieEventId;
            dalitem.SeatId = item.Seat.SeatId;
            dalitem.UserId = item.User.UserId;
            cinemamanager.AddReservation(dalitem);
            return Created("http://localhost:6544/api/reservation", dalitem);
        }
        [HttpGet]
        public ActionResult<List<DTO.Reservation>> Get()
        {
            List<DTO.Reservation> reservationlist = new List<DTO.Reservation>();
            DAL.CinemaManager cinemamanager = new DAL.CinemaManager();
            List<DAL.Reservation> dallist = cinemamanager.ListReservations();
            foreach (var res in dallist)
            {
                DTO.Reservation dtoreservation = new DTO.Reservation();
                dtoreservation.ReservationId = res.ReservationId;
                dtoreservation.User = new DTO.User();
                DAL.User daluser = cinemamanager.GetUserById(res.UserId);
                dtoreservation.User.UserId = daluser.UserId;
                dtoreservation.User.Name = daluser.Name;
                dtoreservation.MovieEvent = new DTO.MovieEvent();
                dtoreservation.MovieEvent.Movie = new DTO.Movie();
                dtoreservation.MovieEvent.Movie.Title = res.MovieEvent.Movie.Title;
                dtoreservation.MovieEvent.Time = res.MovieEvent.TimeOfEvent;
                reservationlist.Add(dtoreservation);
            }
            return reservationlist;
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            DAL.CinemaManager cinemamanager = new DAL.CinemaManager();
            cinemamanager.DeleteReservation(id);
            return NoContent();
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
       
        [HttpGet("resbyuser/{id}")]
        public ActionResult<List<DTO.Reservation>> GetResByUserId(int id)
        {
            DAL.CinemaManager cinemamanager = new DAL.CinemaManager();
            List<DAL.Reservation> dalreservations = new List<DAL.Reservation>();
            dalreservations=cinemamanager.ListUserReservations(id);        
            List<DTO.Reservation> dtoreservations = new List<DTO.Reservation>();
            foreach (var dalreservation in dalreservations)
            {
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
                DAL.Seat dalseatt =cinemamanager.GetSeatById(dalreservation.SeatId); 
                dtoreservation.Seat = new DTO.MovieEventSeat();
                dtoreservation.Seat.SeatId = dalseatt.SeatId;
                dtoreservation.Seat.SeatNumber = dalseatt.SeatNumber;
                dtoreservation.Seat.RowNumber = dalseatt.RowNumber;
                dtoreservations.Add(dtoreservation);
            }
            return dtoreservations;
        }
    }
}