﻿using System.Web.Mvc;

namespace QueueDash.Controllers
{
    public class HomeController : Controller
    {

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Hello()
        {
            return View("HelloWorld");
        }
    }
}