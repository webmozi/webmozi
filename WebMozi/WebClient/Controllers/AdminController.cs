using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebClient.Controllers
{
    [Authorize(Policy = "Admin")]
    public class AdminController : HomeController
    {
        public AdminController(UserManager<IdentityUser> userMgr, SignInManager<IdentityUser> signInMgr, RoleManager<IdentityRole> roleMgr)
           : base(userMgr, signInMgr, roleMgr)
        {
        }

        [AllowAnonymous]
        [HttpGet]
        public ViewResult Main()
        {
            signInManager.SignOutAsync();
            ViewBag.FirstWasUser = 1;
            return View("Login");
        }
        [HttpGet]
        public ViewResult AdminMain()
        {
            return View("Main");
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<ViewResult> Login(DTO.User u)
        {
            bool adminrole = await roleManager.RoleExistsAsync("Admin");
            if (!adminrole)
            {
                var role = new IdentityRole();
                role.Name = "Admin";
                await roleManager.CreateAsync(role);
                var user = new IdentityUser { UserName = "admin" };
                string adminpassword = "admin";
                var checkUser = await userManager.CreateAsync(user, adminpassword);
                if (checkUser.Succeeded)
                {
                    var res = await userManager.AddToRoleAsync(user, "Admin");
                }
            }
            var ress = await userManager.CheckPasswordAsync(new IdentityUser() { UserName = u.Name }, u.Password);
            if (!ress)
            {
                var user = new IdentityUser { UserName = u.Name };
                var resss = await userManager.CreateAsync(user, "admin");
                if (resss.Succeeded)
                {
                    var res = await userManager.AddToRoleAsync(user, "Admin");
                }
            }
            var result = await signInManager.PasswordSignInAsync(u.Name, u.Password, false, false);
            if (result.Succeeded)
            {
                var signeduser = userManager.FindByNameAsync(u.Name);
                bool isAdmin = await userManager.IsInRoleAsync(signeduser.Result, "Admin");
                if (isAdmin)
                {
                    TempData["successfull"] = $"Successfull Sign In as Admin";
                    ViewBag.IsSignedIn = 1;
                    ViewBag.AdminName = u.Name;
                    return AdminMain();
                }
                else
                {
                    TempData["incorrect"] = $"You don't have Admin permission";
                    return Main();
                }
            }
            else
            {
                TempData["incorrect"] = $"Invalid username or password as Admin";
                return Main();
            }
        }
        public ViewResult MovieMain()
        {
            return View("MovieMain");
        }
        [HttpGet]
        public ViewResult RoomMain()
        {
            return View("RoomMain");
        }
        [HttpGet]
        public ViewResult MovieEventMain()
        {
            return View("MovieEventMain");
        }
        [HttpGet]
        public ViewResult UserReservationMain()
        {
            return View("UserReservationMain");
        }
        [HttpGet]
        public ViewResult ListMovies()
        {
            return View("ListMovies", icinemamanager.ListMovies());
        }
        [HttpGet]
        public ViewResult AddMovie()
        {
            return View("AddMovie");
        }
        [HttpPost]
        public ViewResult AddMovie(DTO.Movie m)
        {
            TempData["add"] = $"{m.Title} added!";
            icinemamanager.AddMovie(m);
            return ListMovies();
        }
        [HttpGet]
        public ViewResult DeleteMovie(int ID)
        {
            TempData["remove"] = $"{icinemamanager.SelectMovie(ID).Title} was removed!";
            icinemamanager.DeleteMovie(ID);
            return ListMovies();
        }
        [HttpGet]
        public ViewResult EditMovie(int id)
        {
            DTO.Movie editmovie = icinemamanager.SelectMovie(id);
            return View("EditMovie", editmovie);
        }
        [HttpPost]
        public IActionResult EditMovie(DTO.Movie m)
        {
            DTO.Movie movie = icinemamanager.EditMovie(m);
            TempData["edit"] = $"{movie.Title} has been saved!";
            return ListMovies();
        }



        [HttpGet]
        public ViewResult ListRooms()
        {
            return View("ListRooms", icinemamanager.ListRooms());
        }
        [HttpGet]
        public ViewResult AddRoom()
        {
            return View("AddRoom");
        }
        [HttpPost]
        public ViewResult AddRoom(DTO.Room r)
        {
            TempData["add"] = $"{r.RoomNumber} added!";
            icinemamanager.AddRoom(r);
            return ListRooms();
        }
        [HttpGet]
        public ViewResult DeleteRoom(int ID)
        {
            TempData["delete"] = $"{icinemamanager.SelectRoom(ID).RoomNumber} was removed!";
            icinemamanager.DeleteRoom(ID);
            return ListRooms();
        }



        [HttpGet]
        public ViewResult ListEvents()
        {
            return View("ListMovieEvents", icinemamanager.ListMovieEventsWithoutSeats());
        }
        [HttpGet]
        public ViewResult CreateEvent()
        {
            SelectList listmovie = new SelectList(icinemamanager.ListMovies(), "MovieId", "Title");
            SelectList listroom = new SelectList(icinemamanager.ListRooms(), "RoomId", "RoomNumber");
            ViewBag.movielist = listmovie;
            ViewBag.roomlist = listroom;
            return View("CreateMovieEvent");
        }
        [HttpPost]
        public IActionResult CreateEvent(DTO.MovieEvent me)
        {
            DTO.MovieEvent mevent = new DTO.MovieEvent();
            mevent.Movie = icinemamanager.SelectMovie(me.Movie.MovieId);
            mevent.Room = icinemamanager.SelectRoom(me.Room.RoomId);
            mevent.Time = me.Time;
            icinemamanager.AddMovieEvent(mevent);
            TempData["add"] = $"{mevent.Movie.Title} added at {mevent.Time} in Room {mevent.Room.RoomNumber}!";
            return ListEvents();
        }
        [HttpGet]
        public ViewResult DeleteMovieEvent(int Id)
        {
            DTO.MovieEvent mevent = icinemamanager.SelectMovieEvent(Id);
            TempData["delete"] = $"{mevent.Movie.Title} at {mevent.Time} in Room {mevent.Room.RoomNumber} was removed !";
            icinemamanager.DeleteMovieEvent(Id);
            return ListEvents();
        }



        [HttpGet]
        public ViewResult ListUsers()
        {
            return View("ListUsers", ireservationmanager.ListUsers());
        }
        [HttpGet]
        public ViewResult DeleteUser(int ID)
        {
            TempData["delete"] = $"{ireservationmanager.SelectUser(ID).Name} was removed!";
            ireservationmanager.DeleteUser(ID);
            return ListUsers();
        }
        [HttpGet]
        public ViewResult EditUser(int id)
        {
            DTO.User edituser = ireservationmanager.SelectUser(id);
            return View("EditUser", edituser);
        }
        [HttpPost]
        public IActionResult EditUser(DTO.User u)
        {
            DTO.User user = ireservationmanager.EditUser(u);
            TempData["edit"] = $"{user.Name} has been saved!";
            return ListUsers();
        }


        [HttpGet]
        public ViewResult ListSeats(int id)
        {
            IEnumerable<DTO.MovieEventSeat> seatslist = icinemamanager.ListSeatsInRoom(id);
            return View("ListSeats", seatslist);
        }

        [HttpGet]
        public ViewResult ListReservation()
        {
            return View("ListReservation", ireservationmanager.ListReservations().ToList());
        }
        [HttpGet]
        public ViewResult DeleteReservation(int id)
        {
            TempData["delete"] = $"{ireservationmanager.SelectReservation(id).ReservationId} was removed!";
            ireservationmanager.DeleteReservation(id);
            return ListReservation();
        }
    }
}