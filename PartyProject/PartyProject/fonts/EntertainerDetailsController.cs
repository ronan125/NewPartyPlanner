using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PartyProject.Models;

namespace PartyProject.Controllers
{
    public class EntertainerDetailsController : Controller
    {
        private TeamProjectEntities db = new TeamProjectEntities();

        // GET: EntertainerDetails
        public ActionResult Index()
        {

            if((!(Session["EntertainerID"] == null)))
            {
                return RedirectToAction("Index", "Home");
            }

           else if ((!(Session["CustomerID"] == null)))
            {
                ViewBag.Message = "You are already Logged in " + Session["Firstname"];
                return RedirectToAction("Login", "EntertainerDetails");
            }


            return View("Login");
        }

        //Get: Entertainer Portal
        public ActionResult EntertainerPortal()
        {
            return View("EntertainerPortal");
        }

        // GET: EntertainerDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EntertainerDetail entertainerDetail = db.EntertainerDetails.Find(id);
            if (entertainerDetail == null)
            {
                return HttpNotFound();
            }
            if ((string)Session["EntertainerID"] == entertainerDetail.EntertainerID.ToString())
            {
                return View(entertainerDetail);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        // GET: EntertainerDetails/Create
        public ActionResult Create()
        {
            ViewBag.County = new SelectList(db.tblLocations, "LocationID", "Location");
            ViewBag.Skill = new SelectList(db.tblSkills, "SkillID", "Skill");
            return View();
        }

        // POST: EntertainerDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EntertainerID,Fistname,Lastname,StreetAddress,Town,County,Phone,Email,Password,Skill")] EntertainerDetail entertainerDetail)
        {
            if (ModelState.IsValid)
            {
                db.EntertainerDetails.Add(entertainerDetail);
                db.SaveChanges();
                Session["CustomerID"] = null;

                foreach (EntertainerDetail e in db.EntertainerDetails)
                {
                    if ((e.Email == entertainerDetail.Email) && (e.Password == entertainerDetail.Password))
                    {
                        Session["Firstname"] = e.Fistname.ToString();
                        Session["EntertainerID"] = e.EntertainerID.ToString();
                        return RedirectToAction("Index", "Home");
                    }
                }
                return View("EntertainerPortal");
            }

            ViewBag.County = new SelectList(db.tblLocations, "LocationID", "Location", entertainerDetail.County);
            ViewBag.Skill = new SelectList(db.tblSkills, "SkillID", "Skill", entertainerDetail.Skill);
            return View(entertainerDetail);
        }

        // GET: EntertainerDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EntertainerDetail entertainerDetail = db.EntertainerDetails.Find(id);
            if (entertainerDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.County = new SelectList(db.tblLocations, "LocationID", "Location", entertainerDetail.County);
            ViewBag.Skill = new SelectList(db.tblSkills, "SkillID", "Skill", entertainerDetail.Skill);
            return View(entertainerDetail);
        }

        // POST: EntertainerDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EntertainerID,Fistname,Lastname,StreetAddress,Town,County,Phone,Email,Password,Skill")] EntertainerDetail entertainerDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(entertainerDetail).State = EntityState.Modified;
                db.SaveChanges();
                return Redirect(entertainerDetail.EntertainerID.ToString());
            }
            ViewBag.County = new SelectList(db.tblLocations, "LocationID", "Location", entertainerDetail.County);
            ViewBag.Skill = new SelectList(db.tblSkills, "SkillID", "Skill", entertainerDetail.Skill);
            return View(entertainerDetail);
        }

        // GET: EntertainerDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EntertainerDetail entertainerDetail = db.EntertainerDetails.Find(id);
            if (entertainerDetail == null)
            {
                return HttpNotFound();
            }
            if ((string)Session["EntertainerID"] == entertainerDetail.EntertainerID.ToString())
            {
                return View(entertainerDetail);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        // POST: EntertainerDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EntertainerDetail entertainerDetail = db.EntertainerDetails.Find(id);
            db.EntertainerDetails.Remove(entertainerDetail);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Login()
        {
            return View();
        }



        [HttpPost]
        public ActionResult Login(EntertainerDetail ent)
        {

            foreach (EntertainerDetail e in db.EntertainerDetails)
            {
                if ((e.Email == ent.Email) && (e.Password == ent.Password))
                {
                    Session["Firstname"] = e.Fistname.ToString();
                    Session["EntertainerID"] = e.EntertainerID.ToString();
                    Session["CustomerID"] = null; // incase a Customer is logged in already
                    return RedirectToAction("Index", "Home");
                }
            }
            return Redirect("Login");
            ModelState.AddModelError("", "Username or Password is Incorrect!");
        }
    }
}
