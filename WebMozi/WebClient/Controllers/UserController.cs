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
        [HttpGet]
        public ViewResult Main()
        {
            if (ireservationmanager.SignedUserId()!=-1)
            {
                ViewBag.User = ireservationmanager.SelectUser(ireservationmanager.SignedUserId()).Name;
                return View("SignedMain");
            }
            else
            {
                return View("Login");
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
            return View("Login");
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

            int uid = ireservationmanager.SignedUserId();
            if (uid == -1)
            {
                TempData["message"] = $"Invalid username or password";
                return Login();
            }
            else
            {
                
                    TempData["message"] = $"Successfull log in!";
                    return Main();
            }
        }




        [HttpGet]
        public ViewResult ListEvents()
        {
            return View("ListMovieEvents", icinemamanager.ListMovieEventsWithoutSeats());
        }
        [HttpGet]
        public ViewResult SelectedMovieEvent(int meId)
        {
            ireservationmanager.SaveMovieEventForReservation(meId);
            DTO.MovieEvent me = icinemamanager.SelectMovieEvent(meId);
            Models.EnableAndDisableSeats seats = new Models.EnableAndDisableSeats();
            seats.AllSeats = icinemamanager.SelectMovieEvent(me.MovieEventId).Room.Seats;
            seats.EnableSeats = icinemamanager.getEnableSeats(me.MovieEventId);
            return View("ChooseSeats", seats);
        }
        [HttpGet]
        public ViewResult SelectMovieEvent(int id) {
            return SelectedMovieEvent(id);
        }


        [HttpGet]
        public ViewResult ListSeatsInMovieEvent()
        {
            Models.EnableAndDisableSeats seats = new Models.EnableAndDisableSeats();
            seats.AllSeats = icinemamanager.SelectMovieEvent(ireservationmanager.getChosedMovieEventId()).Room.Seats;
            seats.EnableSeats= icinemamanager.getEnableSeats(ireservationmanager.getChosedMovieEventId());
            return View("ListSeatsInMovieEvent",seats);
        }
        [HttpGet]
        public ViewResult SetSeatId(int id)
        {
            return ChooseSeat(id);
        }
        [HttpGet]
        public ViewResult ChooseSeat(int seatid)
        {
            ireservationmanager.SaveSeatForReservation(seatid);
            ireservationmanager.MakeReservation();
            return ChooseSeatMore();
        }
        [HttpGet]
        public ViewResult ChooseSeatMore()
        {
            int movieeventid = ireservationmanager.getChosedMovieEventId();
            Models.EnableAndDisableSeats seats = new Models.EnableAndDisableSeats();
            seats.AllSeats = icinemamanager.SelectMovieEvent(movieeventid).Room.Seats;
            seats.EnableSeats = icinemamanager.getEnableSeats(movieeventid);
            return View("ChooseSeats", seats);
        }

        
        [HttpGet]
        public ViewResult Reservation()
        {
            List<DTO.Reservation> reservationlist = new List<DTO.Reservation>();
            reservationlist = ireservationmanager.GetReservationsByUser(ireservationmanager.SignedUserId());
            return View("Reservation", reservationlist);
        }
    }
}