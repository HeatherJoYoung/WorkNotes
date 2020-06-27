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
    public class JobController : Controller
    {
        private NotesContext db = new NotesContext();

        // GET: Job
        public ActionResult Index(string sortOrder, int? page)
        {
            var jobs = db.Jobs.Include(j => j.Company);

            ViewBag.CurrentSort = sortOrder;
            ViewBag.IDSortParam = String.IsNullOrEmpty(sortOrder) ? "ID" : "";
            ViewBag.CompanySortParam = sortOrder == "Company" ? "company_desc" : "Company";

            switch (sortOrder)
            {
                case "ID":
                    jobs = jobs.OrderBy(c => c.ID);
                    break;
                case "Company":
                    jobs = jobs.OrderBy(c => c.Company.Name);
                    break;
                case "company_desc":
                    jobs = jobs.OrderByDescending(c => c.Company.Name);
                    break;
                default:
                    jobs = jobs.OrderByDescending(c => c.ID);
                    break;
            }

            int pageSize = 20;
            int pageNumber = (page ?? 1);
            return View(jobs.ToPagedList(pageNumber, pageSize));
        }

        // GET: Job/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = db.Jobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        // GET: Job/Create
        public ActionResult Create()
        {
            ViewBag.CompanyId = new SelectList(db.Companies, "ID", "Name");
            ViewBag.PersonID = new SelectList(db.Persons, "ID", "FullName");
            return View();
        }

        // POST: Job/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,CompanyId,PersonID,JobTitle,Description,Qualifications,PostingDate,PostingSite")] Job job)
        {
            if (ModelState.IsValid)
            {
                db.Jobs.Add(job);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CompanyId = new SelectList(db.Companies, "ID", "Name", job.CompanyId);
            return View(job);
        }

        // GET: Job/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = db.Jobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            ViewBag.CompanyId = new SelectList(db.Companies, "ID", "Name", job.CompanyId);
            return View(job);
        }

        // POST: Job/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,CompanyId,PersonID,JobTitle,Description,Qualifications,PostingDate,PostingSite")] Job job)
        {
            if (ModelState.IsValid)
            {
                db.Entry(job).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CompanyId = new SelectList(db.Companies, "ID", "Name", job.CompanyId);
            return View(job);
        }

        // GET: Job/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = db.Jobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        // POST: Job/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Job job = db.Jobs.Find(id);
            db.Jobs.Remove(job);
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
