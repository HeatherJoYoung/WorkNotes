using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using PagedList;
using WorkNotes.DAL;
using WorkNotes.Models;

namespace WorkNotes.Controllers
{
    public class ApplicationController : Controller
    {
        private NotesContext db = new NotesContext();

        // GET: Application
        public ActionResult Index(string sortOrder, int? page)
        {
            var applications = db.Applications.Include(a => a.Job);

            ViewBag.CurrentSort = sortOrder;
            ViewBag.IDSortParam = String.IsNullOrEmpty(sortOrder) ? "ID" : "";
            ViewBag.CompanySortParam = sortOrder == "Company" ? "company_desc" : "Company";
            ViewBag.DateSortParam = sortOrder == "Date" ? "date_desc" : "Date";

            switch (sortOrder)
            {
                case "ID":
                    applications = applications.OrderBy(a => a.ID);
                    break;
                case "Company":
                    applications = applications.OrderBy(a => a.Job.Company.Name);
                    break;
                case "company_desc":
                    applications = applications.OrderByDescending(a => a.Job.Company.Name);
                    break;
                case "Date":
                    applications = applications.OrderBy(a => a.Date);
                    break;
                case "date_desc":
                    applications = applications.OrderByDescending(a => a.Date);
                    break;
                default:
                    applications = applications.OrderByDescending(a => a.ID);
                    break;
            }

            int pageSize = 20;
            int pageNumber = (page ?? 1);
            return View(applications.ToPagedList(pageNumber, pageSize));
        }

        // GET: Application/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Application application = db.Applications.Find(id);
            if (application == null)
            {
                return HttpNotFound();
            }
            return View(application);
        }

        // GET: Application/Create
        public ActionResult Create()
        {
            ViewBag.JobID = new SelectList(db.Jobs, "ID", "ID");
            return View();
        }

        // POST: Application/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,JobID,Date,Status")] Application application)
        {
            if (ModelState.IsValid)
            {
                db.Applications.Add(application);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.JobID = new SelectList(db.Jobs, "ID", "JobTitle", application.JobID);
            return View(application);
        }

        // GET: Application/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Application application = db.Applications.Find(id);
            if (application == null)
            {
                return HttpNotFound();
            }
            ViewBag.JobID = new SelectList(db.Jobs, "ID", "JobTitle", application.JobID);
            return View(application);
        }

        // POST: Application/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,JobID,Date,Status")] Application application)
        {
            if (ModelState.IsValid)
            {
                db.Entry(application).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.JobID = new SelectList(db.Jobs, "ID", "JobTitle", application.JobID);
            return View(application);
        }

        // GET: Application/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Application application = db.Applications.Find(id);
            if (application == null)
            {
                return HttpNotFound();
            }
            return View(application);
        }

        // POST: Application/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Application application = db.Applications.Find(id);
            db.Applications.Remove(application);
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
