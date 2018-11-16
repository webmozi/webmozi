using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebClient.Models;

namespace WebClient.Controllers
{
    public class UserController : HomeController
    {
        public UserController(UserManager<IdentityUser> userMgr, SignInManager<IdentityUser> signInMgr, RoleManager<IdentityRole> roleMgr)
            : base(userMgr, signInMgr, roleMgr)
        {
        }

        [HttpGet]
        public ViewResult Main(string name)
        {
            ViewBag.IsSignedIn = 1;
            ViewBag.Name = HttpContext.Session.GetString(name + "_Name");
            return ListEvents();
        }

        [HttpGet]
        public ViewResult ListMovies()
        {
            ViewBag.Name = HttpContext.Session.GetString(userManager.GetUserName(User) + "_Name");
            return View("ListMovies", icinemamanager.ListMovies());
        }
        [HttpGet]
        public ViewResult RegisterUser()
        {
            return View("RegisterUser");
        }
        [HttpPost]
        public async Task<ViewResult> RegisterUser(User u)
        {
            DTO.User dtouser = new DTO.User();
            dtouser.Email = u.Email;
            dtouser.Name = u.Name;
            dtouser.TelephoneNumber = u.TelephoneNumber;
            dtouser.UserId = u.UserId;
            ireservationmanager.AddUser(dtouser);
            var user = new IdentityUser { UserName = dtouser.Email };
            var result = await userManager.CreateAsync(user, u.Password);
            if (result.Succeeded)
            {
                return Login();
            }
            else { return RegisterUser(); }
        }
        
      
        [HttpGet]
        public ViewResult Login()
        {
            return View("Login");
        }
        [HttpPost]
        public async Task<ViewResult> Login(User u)
        {
            var result = await signInManager.PasswordSignInAsync(u.Email, u.Password, false, false);
            if (result.Succeeded)
            {
                var identity = new ClaimsIdentity();
                identity.AddClaim(new Claim(ClaimTypes.Name, u.Email));
                Thread.CurrentPrincipal = new ClaimsPrincipal(identity);
                if (u.Email == "admin")
                {
                    await signInManager.SignOutAsync();
                    TempData["invalid"] = $"You can't Sign In with admin";
                    return Login();
                }
                DTO.User dtouser = new DTO.User();
                dtouser.Email = u.Email;
                dtouser.Name = u.Name;
                dtouser.TelephoneNumber = u.TelephoneNumber;
                dtouser.UserId = u.UserId;
                int uid = ireservationmanager.GetIdByUser(dtouser);
                HttpContext.Session.SetString(Thread.CurrentPrincipal.Identity.Name + "_Name", ireservationmanager.SelectUser(uid).Name);
                HttpContext.Session.SetInt32(Thread.CurrentPrincipal.Identity.Name, uid);
                TempData["success"] = $"Successfull log in!";
                return Main(Thread.CurrentPrincipal.Identity.Name);
            }
            else if (result.IsLockedOut)
            {
                TempData["invalid"] = $"User account locked out! Wait 1 minute";
                return Login();
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
            if (userManager.GetUserName(User) != null) { 
            ViewBag.Name = HttpContext.Session.GetString(userManager.GetUserName(User) + "_Name");
            }
            return View("ListMovieEvents", icinemamanager.ListMovieEventsWithoutSeats());
        }
        [HttpGet]
        public ViewResult SelectedMovieEvent(int meId)
        {
            ViewBag.Name = HttpContext.Session.GetString(userManager.GetUserName(User) + "_Name");
            HttpContext.Session.SetInt32(userManager.GetUserName(User) + "_meId", meId);
            DTO.MovieEvent me = icinemamanager.SelectMovieEvent(meId);
            Models.EnableAndDisableSeats seats = new Models.EnableAndDisableSeats();
            seats.AllSeats = icinemamanager.SelectMovieEvent(me.MovieEventId).Room.Seats;
            seats.EnableSeats = icinemamanager.getEnableSeats(me.MovieEventId);
            return View("ChooseSeats", seats);
        }
        [HttpGet]
        public ViewResult SelectMovieEvent(int id)
        {
            return SelectedMovieEvent(id);
        }


        [HttpGet]
        public ViewResult ListSeatsInMovieEvent()
        {
            ViewBag.Name = HttpContext.Session.GetString(userManager.GetUserName(User) + "_Name");
            Models.EnableAndDisableSeats seats = new Models.EnableAndDisableSeats();
            int movieeventid = (int)HttpContext.Session.GetInt32(userManager.GetUserName(User) + "_meId");
            seats.AllSeats = icinemamanager.SelectMovieEvent(movieeventid).Room.Seats;
            seats.EnableSeats = icinemamanager.getEnableSeats(movieeventid);
            return View("ListSeatsInMovieEvent", seats);
        }
        [HttpGet]
        public ViewResult SetSeatId(int id)
        {
            return ChooseSeat(id);
        }
        [HttpGet]
        public ViewResult ChooseSeat(int seatid)
        {
            ViewBag.Name = HttpContext.Session.GetString(userManager.GetUserName(User) + "_Name");
            ireservationmanager.MakeReservation((int)HttpContext.Session.GetInt32(userManager.GetUserName(User) + "_meId"), seatid, (int)HttpContext.Session.GetInt32(userManager.GetUserName(User)));
            return ChooseSeatMore();
        }
        [HttpGet]
        public ViewResult ChooseSeatMore()
        {
            ViewBag.Name = HttpContext.Session.GetString(userManager.GetUserName(User) + "_Name");
            int movieeventid = (int)HttpContext.Session.GetInt32(userManager.GetUserName(User) + "_meId");
            Models.EnableAndDisableSeats seats = new Models.EnableAndDisableSeats();
            seats.AllSeats = icinemamanager.SelectMovieEvent(movieeventid).Room.Seats;
            seats.EnableSeats = icinemamanager.getEnableSeats(movieeventid);
            return View("ChooseSeats", seats);
        }


        [HttpGet]
        public ViewResult Reservation()
        {
            ViewBag.Name = HttpContext.Session.GetString(userManager.GetUserName(User) + "_Name");
            List<DTO.Reservation> reservationlist = new List<DTO.Reservation>();
            reservationlist = ireservationmanager.GetReservationsByUser((int)HttpContext.Session.GetInt32(userManager.GetUserName(User)));
            return View("Reservation", reservationlist);
        }
    }
}