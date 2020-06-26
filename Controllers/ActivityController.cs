using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using WorkNotes.DAL;
using WorkNotes.Models;

namespace WorkNotes.Controllers
{
    public class ActivityController : Controller
    {
        private NotesContext db = new NotesContext();

        // GET: Activity
        public ActionResult Index(string sortOrder)
        {
            var activities = db.Activities
                .Include(a => a.Application)
                .Include(a => a.Contact)
                .Include(a => a.Job)
                .Include(a => a.Job.Company);

            ViewBag.IDSortParam = String.IsNullOrEmpty(sortOrder) ? "id_desc" : "";
            ViewBag.DateSortParam = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.TypeSortParam = sortOrder == "Type" ? "type_desc" : "Type";
            ViewBag.CompanySortParam = sortOrder == "Company" ? "company_desc" : "Company";

            switch (sortOrder)
            {
                case "id_desc":
                    activities = activities.OrderByDescending(a => a.ID);
                    break;
                case "Date":
                    activities = activities.OrderBy(a => a.Date);
                    break;
                case "date_desc":
                    activities = activities.OrderByDescending(a => a.Date);
                    break;
                case "Type":
                    activities = activities.OrderBy(a => a.Type);
                    break;
                case "type_desc":
                    activities = activities.OrderByDescending(a => a.Type);
                    break;
                case "Company":
                    activities = activities.OrderBy(a => a.Job.Company.Name);
                    break;
                case "company_desc":
                    activities = activities.OrderByDescending(a => a.Job.Company.Name);
                    break;
                default:
                    activities = activities.OrderBy(a => a.ID);
                    break;
            }

            return View(activities.ToList());
        }

        // GET: Activity/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity activity = db.Activities.Find(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            return View(activity);
        }

        // GET: Activity/Create
        public ActionResult Create()
        {
            ViewBag.ApplicationID = new SelectList(db.Applications, "ID", "ID");
            ViewBag.PersonID = new SelectList(db.Persons, "ID", "FullName");
            ViewBag.JobID = new SelectList(db.Jobs, "ID", "ID");
            return View();
        }

        // POST: Activity/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,PersonID,JobID,ApplicationID,Date,Type,Notes")] Activity activity)
        {
            if (ModelState.IsValid)
            {
                db.Activities.Add(activity);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ApplicationID = new SelectList(db.Applications, "ID", "ID", activity.ApplicationID);
            ViewBag.PersonID = new SelectList(db.Persons, "ID", "FullName", activity.PersonID);
            ViewBag.JobID = new SelectList(db.Jobs, "ID", "JobTitle", activity.JobID);
            return View(activity);
        }

        // GET: Activity/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity activity = db.Activities.Find(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            ViewBag.ApplicationID = new SelectList(db.Applications, "ID", "ID", activity.ApplicationID);
            ViewBag.PersonID = new SelectList(db.Persons, "ID", "FullName", activity.PersonID);
            ViewBag.JobID = new SelectList(db.Jobs, "ID", "JobTitle", activity.JobID);
            return View(activity);
        }

        // POST: Activity/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,PersonID,JobID,ApplicationID,Date,Type,Notes")] Activity activity)
        {
            if (ModelState.IsValid)
            {
                db.Entry(activity).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ApplicationID = new SelectList(db.Applications, "ID", "ID", activity.ApplicationID);
            ViewBag.PersonID = new SelectList(db.Persons, "ID", "FullName", activity.PersonID);
            ViewBag.JobID = new SelectList(db.Jobs, "ID", "JobTitle", activity.JobID);
            return View(activity);
        }

        // GET: Activity/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity activity = db.Activities.Find(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            return View(activity);
        }

        // POST: Activity/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Activity activity = db.Activities.Find(id);
            db.Activities.Remove(activity);
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
