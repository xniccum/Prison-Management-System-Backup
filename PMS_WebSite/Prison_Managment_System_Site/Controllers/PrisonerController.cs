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
    public class PrisonerController : Controller
    {
        private SQLhandler handler;

        private void initilizeHandler()
        {
            this.handler = new SQLhandler();
            this.handler.openConnection();
            this.handler.verifyUsernamePassword(Session["UserName"].ToString(), Session["Password"].ToString());
        }

        private List<Prisoner> initilize_prisoners()
        {
            this.initilizeHandler();
            return new List<Prisoner>();
        }

        private Prisoner constructPrisoner(Object[] prisoner_data)
        {
            Prisoner p = new Prisoner();
            p.ID = int.Parse(prisoner_data[0].ToString());
            p.first_name = prisoner_data[1].ToString();
            p.middle_name = prisoner_data[2].ToString();
            p.last_name = prisoner_data[3].ToString();
            p.crime = prisoner_data[4].ToString();
            return p;
        }

        private List<Prisoner> getAllPrisoners()
        {
            this.initilizeHandler();
            List<Prisoner> prisoners = new List<Prisoner>();
            foreach (DataRow dr in this.handler.getPrisonersTable().Rows)
            {
                Object[] prisoner_data = this.handler.getPrisoner(int.Parse(dr.ItemArray[0].ToString()));
                prisoners.Add(constructPrisoner(prisoner_data));
            }
            return prisoners;
        }

        [HttpGet]
        public ActionResult Index()
        {

            if (Session["UserName"]!=null)
            {
                return View(getAllPrisoners());
            }
            return RedirectToAction("Login","User");
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Index(FormCollection collection)
        {
            if (Session["UserName"] != null)
            {
                List<Prisoner> prisoners = initilize_prisoners();
                if (collection.Get("filterRelation") == "on" || collection.Get("Search") != "")
                {
                    bool check = (collection.Get("filterRelation") == "on");
                    string searchString = Server.HtmlEncode(collection.Get("Search"));

                    DataTable temp = null;
                    if (searchString != "")
                    {
                        foreach (string s in searchString.Split(new Char[] { ' ' }))
                        {
                            if (temp == null)
                            {
                                temp = this.handler.getFilteredPrisoners(Session["UserName"].ToString(),Session["Password"].ToString(), check, s);
                            }
                            else
                                temp.Merge(this.handler.getFilteredPrisoners((string)Session["UserName"], (string)Session["Password"], check, s));
                        }
                    }
                    else
                    {
                        temp = this.handler.getRelations();
                    }
                    foreach (DataRow dr in temp.Rows)
                    {
                        prisoners.Add(constructPrisoner(dr.ItemArray));
                    }
                    return View(prisoners);
                }
                return View(getAllPrisoners());
            }
            return RedirectToAction("Login", "User");
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            if (Session["UserName"]!=null)
            {
                this.initilizeHandler();
                if (!this.handler.verifyRelation(id) && this.handler.checkUsernamePermissions(Session["UserName"].ToString())<1)
                {
                    return View("Error");
                }
                Object[] temp = handler.getPrisoner(id);
                Prisoner p = constructPrisoner(temp);
                p.crime_description = temp[5].ToString();
                return View(p);
            }
            return RedirectToAction("Login", "User");
        }
    }
}
