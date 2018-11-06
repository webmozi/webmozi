using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebClient.Controllers
{
    public class UserController : HomeController
    {
        private int seatid = 0;

        [HttpGet]
        public ViewResult Main()
        {
            if (ireservationmanager.SignedUser()!=null)
            {
                ViewBag.User = ireservationmanager.SignedUser().Name;
                return View("SignedMain");
            }
            else
            {
                return View("Main");
            }
        }


        [HttpGet]
        public ViewResult RegisterUser()
        {
            return View("RegisterUser");
        }
        [HttpPost]
        public ViewResult RegisterUser(DTO.User u)
        {
            ireservationmanager.AddUser(u);
            return Login();
        }



        [HttpGet]
        public ViewResult Logout()
        {
            ireservationmanager.Loggingout();
            return View("Main");
        }
        [HttpGet]
        public ViewResult Login()
        {
            return View("Login");
        }
        [HttpPost]
        public ViewResult Login(DTO.User u)
        {

          /*  var claims = new List<Claim> { new Claim(ClaimTypes.Name, u.Name) };
            var claimsIdentity = new ClaimsIdentity (claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties { };
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);*/
            ireservationmanager.LogInUser(u);
            DTO.User user = ireservationmanager.SignedUser();
            if (user == null)
            {
                TempData["message"] = $"Invalid username or password";
                return Login();
            }
            else
            {
                if (ireservationmanager.getChosedReservationId()!=-1)
                {
                    ireservationmanager.AddUserToReservation(ireservationmanager.getChosedReservationId(), ireservationmanager.SignedUser());
                    return ListSeatsInMovieEvent();
                }
                else
                {
                    TempData["message"] = $"Successfull log in!";
                    return ListEvents();
                }
            }
        }



        [HttpGet]
        public ViewResult CreateUserOrLogin()
        {

            if (ireservationmanager.SignedUser()!=null)
            {
                ireservationmanager.AddUserToReservation(ireservationmanager.getChosedReservationId(), ireservationmanager.SignedUser());
                return ListSeatsInMovieEvent();
            }
            else
            {
                return View("CreateUserOrLogin");
            }
        }
        [HttpPost]
        public ViewResult CreateUserOrLogin(DTO.User u)
        {
            ireservationmanager.AddUser(u);
            ireservationmanager.AddUserToReservation(ireservationmanager.getChosedReservationId(), u);
            return ListSeatsInMovieEvent();
        }



        [HttpGet]
        public ViewResult ListEvents()
        {
            return View("ListMovieEvents", icinemamanager.ListMovieEventsWithoutSeats());
        }
        [HttpGet]
        public ViewResult SelectedMovieEvent(int meId)
        {
            DTO.MovieEvent me = icinemamanager.SelectMovieEvent(meId);
            ireservationmanager.CreateReservationOnlyWithMovieEvent(me);
            Models.EnableAndDisableSeats seats = new Models.EnableAndDisableSeats();
            seats.AllSeats = icinemamanager.SelectMovieEvent(me.MovieEventId).Room.Seats;
            seats.EnableSeats = icinemamanager.getEnableSeats(me.MovieEventId, ireservationmanager.ListReservations().ToList());
            return View("ChooseSeats", seats);
        }
        [HttpGet]
        public ViewResult SelectMovieEvent(int id) {
            return SelectedMovieEvent(id);
        }


        [HttpGet]
        public ViewResult ListSeatsInMovieEvent()
        {
            DTO.Reservation res = ireservationmanager.SelectReservation(ireservationmanager.getChosedReservationId());
            Models.EnableAndDisableSeats seats = new Models.EnableAndDisableSeats();
            seats.AllSeats = icinemamanager.SelectMovieEvent(res.MovieEvent.MovieEventId).Room.Seats;
            seats.EnableSeats= icinemamanager.getEnableSeats(res.MovieEvent.MovieEventId, ireservationmanager.ListReservations().ToList());
            return View("ListSeatsInMovieEvent",seats);
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
           
            DTO.Reservation res = ireservationmanager.SelectReservation(ireservationmanager.getChosedReservationId());
            for (int i = 0; i < res.MovieEvent.Room.Seats.Count; i++)
            {
                if (res.MovieEvent.Room.Seats.ElementAt(i).SeatId == seatid)
                {
                    res = ireservationmanager.AddSeatToReservation(ireservationmanager.getChosedReservationId(), res.MovieEvent.Room.Seats.ElementAt(i));
                }
            }
            return ChooseSeatMore();
        }
        [HttpGet]
        public ViewResult ChooseSeatMore()
        {
            int movieeventid = ireservationmanager.SelectReservation(ireservationmanager.getChosedReservationId()).MovieEvent.MovieEventId;
            Models.EnableAndDisableSeats seats = new Models.EnableAndDisableSeats();
            seats.AllSeats = icinemamanager.SelectMovieEvent(movieeventid).Room.Seats;
            seats.EnableSeats = icinemamanager.getEnableSeats(movieeventid, ireservationmanager.ListReservations().ToList());
            return View("ChooseSeats", seats);
        }

        
        [HttpGet]
        public ViewResult Reservation()
        {
            return View("Reservation", ireservationmanager.SelectReservation(ireservationmanager.getChosedReservationId()));
        }
    }
}