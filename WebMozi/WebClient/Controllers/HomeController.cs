using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebClient.Models;

namespace WebClient.Controllers
{
    public class HomeController : Controller
    {

        protected static IReservationManager ireservationmanager = ireservationmanager = ManagerProvider.Instance.GetReservationManager();
        protected static ICinemaManager icinemamanager = ManagerProvider.Instance.GetCinemaManager();

        protected readonly UserManager<IdentityUser> userManager;
        protected readonly SignInManager<IdentityUser> signInManager;
        protected readonly RoleManager<IdentityRole> roleManager;

        public HomeController(UserManager<IdentityUser> userMgr, SignInManager<IdentityUser> signInMgr, RoleManager<IdentityRole> roleMgr) { userManager = userMgr; signInManager = signInMgr; roleManager = roleMgr; }

        public async Task<ViewResult> Index()
        {
            if (User != null)
            {
                if (userManager.GetUserName(User) == null || userManager.GetUserName(User).Equals("admin")|| userManager.GetUserName(User).Equals(""))
                {
                    await signInManager.SignOutAsync();
                    return View("MainView");
                }
                else if (HttpContext.Session.GetString("_Name") == null|| HttpContext.Session.GetString("_Name").Equals(""))
                {
                    await signInManager.SignOutAsync();
                    return View("MainView");
                }
                    ViewBag.Name = HttpContext.Session.GetString("_Name");
                    return View("SignedMainView");
            }
            else
            {
                await signInManager.SignOutAsync();
                return View("MainView");
            }
        }
        public async Task<ViewResult> Logout() {
            await signInManager.SignOutAsync();
            return View("MainView");
        }

    }
}
