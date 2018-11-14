using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebClient.Models;
namespace WebClient.Controllers
{
    public class HomeController : Controller
    {
        protected static IReservationManager ireservationmanager = ireservationmanager = ManagerProvider.Instance.GetReservationManager();
        protected static ICinemaManager icinemamanager = ManagerProvider.Instance.GetCinemaManager();

        protected readonly SignInManager<IdentityUser> signInManager;
        protected readonly UserManager<IdentityUser> userManager;

        public HomeController() {
        }
        public ViewResult Index() {
            return View("MainView");
        }
    }
}
