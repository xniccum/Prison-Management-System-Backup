using Prison_Managment_System;
using Prison_Managment_System_Site.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Prison_Managment_System_Site.Controllers
{
    public class UserController : Controller
    {
        private SQLhandler handler;

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User user)
        {
            ViewBag.Message = "";
            if (user.username!="" && user.password!="")
            {
                string username = Server.HtmlEncode(user.username);
                string password = Server.HtmlEncode(user.password);
                this.handler = new SQLhandler();
                this.handler.openConnection();
                if (handler.verifyUsernamePassword(username, password))
                {
                    Session["UserName"] = username;
                    Session["Password"] = password;
                    ViewBag.User = user.username;
                    return RedirectToAction("Index", "Prisoner");
                }
            }
            ViewBag.Message = "*User name or Password is Invalid";
            return View(user);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            Session["UserName"] = null;
            Session["Password"] = null;
            ViewBag.User = "";
            return RedirectToAction("Login", "User");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                user.fname = Server.HtmlEncode(user.fname);
                user.mname = Server.HtmlEncode(user.mname);
                user.lname = Server.HtmlEncode(user.lname);
                user.username = Server.HtmlEncode(user.username);
                user.password = Server.HtmlEncode(user.password);
                this.handler = new SQLhandler();
                this.handler.openConnection();
                handler.addUser(user);
                return RedirectToAction("Login", "User");
            }
            return View(user);
        }
    }
}