using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace WebClient.Controllers
{
    public class UserController : HomeController
    {

        [HttpGet]
        public ViewResult Main()
        {
            if (ireservationmanager.SignedUserId()!=-1)
            {
                DTO.User u=ireservationmanager.SelectUser(ireservationmanager.SignedUserId());
                HttpContext.Session.SetString("_Name", u.Name);
                ViewBag.User = HttpContext.Session.GetString("_Name");
                return View("SignedMain");
            }
            else
            {
                return View("Main");
            }
        }
         [HttpGet]
        public ViewResult ListMovies()
        {
            if (ireservationmanager.SignedUserId() != -1) {
                ViewBag.User = HttpContext.Session.GetString("_Name");
                return View("SignedListMovies", icinemamanager.ListMovies());
            }
            else
            {
                return View("ListMovies", icinemamanager.ListMovies());
            }
       }

        [HttpGet]
        public ViewResult RegisterUser()
        {
            return View("RegisterUser");
        }
        [HttpPost]
        public async Task<ViewResult> RegisterUserAsync(DTO.User u)
        {
            ireservationmanager.AddUser(u);
            var user = new IdentityUser { UserName = u.Name, Email = u.Email,PhoneNumber=u.TelephoneNumber};
            var result = await userManager.CreateAsync(user, u.Password);
          
            return Login();
        }



        [HttpGet]
        public async Task<ViewResult> Logout()
        {
            await signInManager.SignOutAsync();
            return View("Login");
        }
        [HttpGet]
        public ViewResult Login()
        {
           
            return View("Login");
        }
        [HttpPost]
        public async Task<ViewResult> Login(DTO.User u)
        {
            var user = new IdentityUser { UserName = u.Name};
            var result = await signInManager.PasswordSignInAsync(user,u.Password,false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                ViewBag.User = HttpContext.Session.GetString("_Name");
                TempData["success"] = $"Successfull log in!";
                return Main();
            }
            else
            {
                TempData["invalid"] = $"Invalid username or password";
                return Login();
            }
        }




        [HttpGet]
        public ViewResult ListEvents()
        {
            if (ireservationmanager.SignedUserId() == -1) {
                return View("Login");
            }
            ViewBag.User = HttpContext.Session.GetString("_Name");
            return View("ListMovieEvents", icinemamanager.ListMovieEventsWithoutSeats());
        }
        [HttpGet]
        public ViewResult SelectedMovieEvent(int meId)
        {
            ViewBag.User = HttpContext.Session.GetString("_Name");
            HttpContext.Session.SetInt32("_meId", meId);
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
            ViewBag.User = HttpContext.Session.GetString("_Name");
            Models.EnableAndDisableSeats seats = new Models.EnableAndDisableSeats();
            int movieeventid =(int) HttpContext.Session.GetInt32("_meId");
            seats.AllSeats = icinemamanager.SelectMovieEvent(movieeventid).Room.Seats;
            seats.EnableSeats= icinemamanager.getEnableSeats(movieeventid);
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
            ViewBag.User = HttpContext.Session.GetString("_Name");
            ireservationmanager.MakeReservation((int)HttpContext.Session.GetInt32("_meId"),seatid);
            return ChooseSeatMore();
        }
        [HttpGet]
        public ViewResult ChooseSeatMore()
        {
            ViewBag.User = HttpContext.Session.GetString("_Name");
            int movieeventid = (int)HttpContext.Session.GetInt32("_meId");
            Models.EnableAndDisableSeats seats = new Models.EnableAndDisableSeats();
            seats.AllSeats = icinemamanager.SelectMovieEvent(movieeventid).Room.Seats;
            seats.EnableSeats = icinemamanager.getEnableSeats(movieeventid);
            return View("ChooseSeats", seats);
        }

        
        [HttpGet]
        public ViewResult Reservation()
        {
            ViewBag.User = HttpContext.Session.GetString("_Name");
            List<DTO.Reservation> reservationlist = new List<DTO.Reservation>();
            reservationlist = ireservationmanager.GetReservationsByUser(ireservationmanager.SignedUserId());
            return View("Reservation", reservationlist);
        }
    }
}