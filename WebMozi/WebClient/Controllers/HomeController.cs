﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebClient.Models;
namespace WebClient.Controllers
{
    public class HomeController : Controller
    {
        protected static IReservationManager ireservationmanager;
        protected static ICinemaManager icinemamanager ;

        public HomeController() {
            ireservationmanager = ManagerProvider.Instance.GetReservationManager();
             icinemamanager = ManagerProvider.Instance.GetCinemaManager();
        }
        public ViewResult Index() {

            return View("MainView");
        }
    }
}
