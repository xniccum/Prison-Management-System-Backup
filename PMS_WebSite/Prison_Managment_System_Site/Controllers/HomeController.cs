using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Prison_Managment_System_Site.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.user = "";
            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}