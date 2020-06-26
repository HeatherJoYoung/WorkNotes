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
    public class PersonController : Controller
    {
        private NotesContext db = new NotesContext();

        // GET: Person
        public ActionResult Index(string sortOrder, int? page)
        {
            var persons = db.Persons.Include(p => p.Company);

            ViewBag.CurrentSort = sortOrder;
            ViewBag.FirstNameSortParam = String.IsNullOrEmpty(sortOrder) ? "firstname_desc" : "";
            ViewBag.LastNameSortParam = sortOrder == "LastName" ? "lastname_desc" : "LastName";
            ViewBag.CompanySortParam = sortOrder == "Company" ? "company_desc" : "Company";

            switch (sortOrder)
            {
                case "firstname_desc":
                    persons = persons.OrderByDescending(p => p.FirstName);
                    break;
                case "LastName":
                    persons = persons.OrderBy(p => p.LastName);
                    break;
                case "lastname_desc":
                    persons = persons.OrderByDescending(p => p.LastName);
                    break;
                case "Company":
                    persons = persons.OrderBy(p => p.Company.Name);
                    break;
                case "company_desc":
                    persons = persons.OrderByDescending(p => p.Company.Name);
                    break;
                default:
                    persons = persons.OrderBy(p => p.FirstName);
                    break;
            }

            int pageSize = 20;
            int pageNumber = (page ?? 1);
            return View(persons.ToPagedList(pageNumber, pageSize));
        }

        // GET: Person/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.Persons.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        // GET: Person/Create
        public ActionResult Create()
        {
            ViewBag.CompanyID = new SelectList(db.Companies, "ID", "Name");
            return View();
        }

        // POST: Person/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,CompanyID,FirstName,LastName,Phone,Email,Title")] Person person)
        {
            if (ModelState.IsValid)
            {
                db.Persons.Add(person);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CompanyID = new SelectList(db.Companies, "ID", "Name", person.CompanyID);
            return View(person);
        }

        // GET: Person/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.Persons.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            ViewBag.CompanyID = new SelectList(db.Companies, "ID", "Name", person.CompanyID);
            return View(person);
        }

        // POST: Person/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,CompanyID,FirstName,LastName,Phone,Email,Title")] Person person)
        {
            if (ModelState.IsValid)
            {
                db.Entry(person).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CompanyID = new SelectList(db.Companies, "ID", "Name", person.CompanyID);
            return View(person);
        }

        // GET: Person/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.Persons.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        // POST: Person/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Person person = db.Persons.Find(id);
            db.Persons.Remove(person);
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
