﻿using System;
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
            var dalitem = new DAL.Reservation();
            dalitem.MovieEventId = item.MovieEvent.MovieEventId;
            dalitem.SeatId = item.Seat.SeatId;
            dalitem.UserId = item.User.UserId;
            DAL.ReservationManager.AddReservation(dalitem);
            return Created("http://localhost:6544/api/reservation", dalitem);
        }
        [HttpGet]
        public ActionResult<List<DTO.Reservation>> Get()
        {
            List<DTO.Reservation> reservationlist = new List<DTO.Reservation>();
            List<DAL.Reservation> dallist = DAL.ReservationManager.ListReservations();
            foreach (var res in dallist)
            {
                DTO.Reservation dtoreservation = new DTO.Reservation();
                dtoreservation.ReservationId = res.ReservationId;
                dtoreservation.User = new DTO.User();
                DAL.User daluser = DAL.UserManager.GetUserById(res.UserId);
                dtoreservation.User.UserId = daluser.UserId;
                dtoreservation.User.Name = daluser.Name;
                dtoreservation.MovieEvent = new DTO.MovieEvent();
                dtoreservation.MovieEvent.Movie = new DTO.Movie();
                dtoreservation.MovieEvent.Movie.Title = res.MovieEvent.Movie.Title;
                dtoreservation.MovieEvent.Movie.Length = res.MovieEvent.Movie.Length;
                dtoreservation.MovieEvent.Movie.Img = res.MovieEvent.Movie.Img;
                dtoreservation.MovieEvent.Time = res.MovieEvent.TimeOfEvent;
                reservationlist.Add(dtoreservation);
            }
            return reservationlist;
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            DAL.ReservationManager.DeleteReservation(id);
            return NoContent();
        }
        [HttpGet("{id}")]
        public ActionResult<DTO.Reservation> GetById(int id)
        {
            DAL.Reservation dalreservation = DAL.ReservationManager.GetReservationById(id);
            DTO.Reservation dtoreservation = new DTO.Reservation();
            dtoreservation.ReservationId = dalreservation.ReservationId;
            return dtoreservation;
        }
       
        [HttpGet("resbyuser/{id}")]
        public ActionResult<List<DTO.Reservation>> GetResByUserId(int id)
        {
            List<DAL.Reservation> dalreservations = new List<DAL.Reservation>();
            dalreservations=DAL.UserManager.ListUserReservations(id);        
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
                dtoreservation.MovieEvent.Movie.Length = dalreservation.MovieEvent.Movie.Length;
                dtoreservation.MovieEvent.Movie.Img = dalreservation.MovieEvent.Movie.Img;
                dtoreservation.MovieEvent.Room.RoomId = dalreservation.MovieEvent.Room.RoomId;
                dtoreservation.MovieEvent.Room.RoomNumber = dalreservation.MovieEvent.Room.RoomNumber;
                DAL.User daluser = DAL.UserManager.GetUserById(dalreservation.UserId);
                dtoreservation.User = new DTO.User();
                dtoreservation.User.Name = daluser.Name;
                dtoreservation.User.UserId = daluser.UserId;
                DAL.Seat dalseatt =DAL.RoomManager.GetSeatById(dalreservation.SeatId); 
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