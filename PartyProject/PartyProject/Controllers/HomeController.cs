using PartyProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PartyProject.Controllers
{
    public class HomeController : Controller
    {
       
        public ActionResult Index()
        {
            TeamProjectEntities db = new TeamProjectEntities();

            ViewBag.Locs = new SelectList(db.tblLocations, "LocationID", "Location");
            ViewBag.Skil = new SelectList(db.tblSkills, "SkillID", "Skill");

            return View("Index", db);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult Search()
        {

            if ((!(Session["Firstname"] == null))) { 

                int locationID;
            int.TryParse(Request.Form["Locs"], out locationID);

            int skillID;
            int.TryParse(Request.Form["Skil"], out skillID);

            TeamProjectEntities db = new TeamProjectEntities();



            return View("Search",  db.EntViews.Where(element => (locationID == 0) ? true : element.LocationID == locationID).
                                     Where(element => (skillID == 0) ? true : element.SkillID == skillID).ToList());
            }
            else
            { 
                return RedirectToAction("Login", "CustomerDetails");
            }
        }

        public ActionResult Logoff() {
            Session["Firstname"] = null;
            return RedirectToAction("Index");
        }
    }
}