using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Mvc;
using PagedList;
using WorkNotes.DAL;
using WorkNotes.Models;

namespace WorkNotes.Controllers
{
    public class ActivityController : Controller
    {
        private NotesContext db = new NotesContext();

        // GET: Activity
        public ActionResult Index(string sortOrder, int? page)
        {
            var activities = db.Activities
                .Include(a => a.Application)
                .Include(a => a.Contact)
                .Include(a => a.Job)
                .Include(a => a.Job.Company);

            ViewBag.CurrentSort = sortOrder;
            ViewBag.IDSortParam = String.IsNullOrEmpty(sortOrder) ? "ID" : "";
            ViewBag.DateSortParam = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.TypeSortParam = sortOrder == "Type" ? "type_desc" : "Type";
            ViewBag.CompanySortParam = sortOrder == "Company" ? "company_desc" : "Company";

            switch (sortOrder)
            {
                case "ID":
                    activities = activities.OrderBy(a => a.ID);
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
                    activities = activities.OrderByDescending(a => a.ID);
                    break;
            }

            int pageSize = 20;
            int pageNumber = (page ?? 1);
            return View(activities.ToPagedList(pageNumber, pageSize));
        }

        [HttpPost]
        public FileResult Export()
        {
            var activities = db.Activities
                .Include(a => a.Contact.FullName)
                .Include(a => a.Job.JobTitle)
                .Include(a => a.Job.Company); 

            List<List<string>> rows = (from a in activities
                                 select new List<string>
                                 {
                                     a.Date.ToString(),
                                     a.Type.ToString(),
                                     a.Contact.LastName,
                                     a.Job.JobTitle,
                                     a.Job.Company.Name,
                                     a.Notes
                                 }).ToList<List<string>>();

            //Insert the Column Names.
            rows.Insert(0, new List<string> { "Date", "Type", "Contact", "Job Title", "Company", "Notes" });

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < rows.Count; i++)
            {
                List<string> row = rows[i];
                for (int j = 0; j < row.Count; j++)
                {
                    //Append data with separator.
                    sb.Append(row[j] + ',');
                }

                //Append new line character.
                sb.Append("\r\n");
            }

            return File(Encoding.UTF8.GetBytes(sb.ToString()), "text/csv", "Grid.csv");
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
