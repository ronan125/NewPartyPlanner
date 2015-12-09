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
    public class CustomerDetailsController : Controller
    {
        private TeamProjectEntities db = new TeamProjectEntities();

        // GET: CustomerDetails

        public ActionResult Index()
        {

            var customerDetails = db.CustomerDetails.Include(c => c.tblLocation);


            return View(customerDetails);
        }

        //Get: Customer Portal
        public ActionResult CustomerPortal()
        {
            return View("CustomerPortal");
        }

        // GET: CustomerDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerDetail customerDetail = db.CustomerDetails.Find(id);
            if (customerDetail == null)
            {
                return HttpNotFound();
            }

            if ((string)Session["CustomerID"] == customerDetail.CustomerID.ToString())
            {
                return View(customerDetail);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        // GET: CustomerDetails/Create
        public ActionResult Create()
        {
            if ((!(Session["Firstname"] == null)))
            {
                ViewBag.Message = "You are already Logged in " + Session["Firstname"];
                return RedirectToAction("Index", "CustomerDetails");
            }

                ViewBag.County = new SelectList(db.tblLocations, "LocationID", "Location");
            return View();
        }

        // POST: CustomerDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerID,Firstname,Lastname,StreetAddress,Town,County,Email,Password, Phone")] CustomerDetail customerDetail)
        {
            if (ModelState.IsValid)
            {
                db.CustomerDetails.Add(customerDetail);
                db.SaveChanges();

                // Log in with the new record

                foreach (CustomerDetail c in db.CustomerDetails)
                {
                    if ((c.Email == customerDetail.Email) && (c.Password == customerDetail.Password))
                    {
                        Session["Firstname"] = c.Firstname.ToString();
                        return RedirectToAction("Index", "Home");
                    }
                }
                return View("CustomerPortal");
            }

            ViewBag.County = new SelectList(db.tblLocations, "LocationID", "Location", customerDetail.County);
            return View(customerDetail);
        }

        // GET: CustomerDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerDetail customerDetail = db.CustomerDetails.Find(id);
            if (customerDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.County = new SelectList(db.tblLocations, "LocationID", "Location", customerDetail.County);
            return View(customerDetail);
        }

        // POST: CustomerDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]

        public ActionResult Edit([Bind(Include = "CustomerID,Firstname,Lastname,StreetAddress,Town,County,Email,Password,Phone")] CustomerDetail customerDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customerDetail).State = EntityState.Modified;
                db.SaveChanges();
                return Redirect(customerDetail.CustomerID.ToString());
            }
            ViewBag.County = new SelectList(db.tblLocations, "LocationID", "Location", customerDetail.County);
            return View(customerDetail);
        }

        // GET: CustomerDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerDetail customerDetail = db.CustomerDetails.Find(id);
            if (customerDetail == null)
            {
                return HttpNotFound();
            }
            return View(customerDetail);
        }

        // POST: CustomerDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CustomerDetail customerDetail = db.CustomerDetails.Find(id);
            db.CustomerDetails.Remove(customerDetail);
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
        public ActionResult Login(CustomerDetail customer)
        {

            foreach (CustomerDetail c in db.CustomerDetails) {
                if ((c.Email == customer.Email) && (c.Password == customer.Password)){
                    Session["Firstname"] = c.Firstname.ToString();
                    Session["CustomerID"] = c.CustomerID.ToString();
                    return RedirectToAction("Index", "Home");
                }
            }
            return View("CustomerPortal");
        }
    }
}
