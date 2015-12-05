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
            var entertainerDetails = db.EntertainerDetails.Include(e => e.Location).Include(e => e.Skill);
            return View(entertainerDetails.ToList());
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
            return View(entertainerDetail);
        }

        // GET: EntertainerDetails/Create
        public ActionResult Create()
        {
            ViewBag.County = new SelectList(db.Locations, "LocationID", "Location1");
            ViewBag.Skill1 = new SelectList(db.Skills, "SkillID", "Skill1");
            return View();
        }

        // POST: EntertainerDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EntertainerID,Fistname,Lastname,StreetAddress,Town,County,Phone,Email,Skill1")] EntertainerDetail entertainerDetail)
        {
            if (ModelState.IsValid)
            {
                db.EntertainerDetails.Add(entertainerDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.County = new SelectList(db.Locations, "LocationID", "Location1", entertainerDetail.County);
            ViewBag.Skill1 = new SelectList(db.Skills, "SkillID", "Skill1", entertainerDetail.Skill1);
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
            ViewBag.County = new SelectList(db.Locations, "LocationID", "Location1", entertainerDetail.County);
            ViewBag.Skill1 = new SelectList(db.Skills, "SkillID", "Skill1", entertainerDetail.Skill1);
            return View(entertainerDetail);
        }

        // POST: EntertainerDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EntertainerID,Fistname,Lastname,StreetAddress,Town,County,Phone,Email,Skill1")] EntertainerDetail entertainerDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(entertainerDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.County = new SelectList(db.Locations, "LocationID", "Location1", entertainerDetail.County);
            ViewBag.Skill1 = new SelectList(db.Skills, "SkillID", "Skill1", entertainerDetail.Skill1);
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
            return View(entertainerDetail);
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
    }
}
