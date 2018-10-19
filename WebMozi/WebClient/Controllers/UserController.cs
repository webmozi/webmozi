using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebClient.Controllers
{
    public class UserController : HomeController
    {
        private static int ReservationID;
        private int seatid = 0;
        
        [HttpGet]
        public ViewResult Main()
        {
            return View("Main");
        }



        [HttpGet]
        public ViewResult CreateUser()
        {
            return View("CreateUser");
        }
        [HttpPost]
        public ViewResult CreateUser(DTO.User u)
        {
            ireservationmanager.AddUser(u);
            ireservationmanager.AddUserToReservation(ReservationID, u);
            return ListSeatsInMovieEvent();
        }



        [HttpGet]
        public ViewResult ListEvents()
        {
            return View("ListMovieEvents", icinemamanager.ListMovieEvents());
        }
        [HttpGet]
        public ViewResult SelectedMovieEvent(int meId)
        {
            DTO.MovieEvent me = icinemamanager.SelectMovieEvent(meId);
            ReservationID = ireservationmanager.CreateReservationOnlyWithMovieEvent(me);
            return View("ChooseSeats", me.Room.Seats.ToList());
        }



        [HttpGet]
        public ViewResult ListSeatsInMovieEvent()
        {
            DTO.Reservation res = ireservationmanager.GetReservation(ReservationID);
            return View("ListSeatsInMovieEvent", res.MovieEvent.Room.Seats);
        }
        [HttpGet]
        public ViewResult SetSeatId(int id)
        {
            seatid = id;
            return ChooseSeat(seatid);
        }
        [HttpGet]
        public ViewResult ChooseSeat(int seatid)
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
            return ChooseSeatMore();
        }
        [HttpGet]
        public ViewResult ChooseSeatMore()
        {
            return View("ChooseSeats", ireservationmanager.GetReservation(ReservationID).MovieEvent.Room.Seats);
        }



        [HttpGet]
        public ViewResult Reservation()
        {
            return View("Reservation", ireservationmanager.GetReservation(ReservationID));
        }
    }
}