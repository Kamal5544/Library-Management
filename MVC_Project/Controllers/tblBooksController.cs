using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC_Project.Models;

namespace MVC_Project.Controllers
{
    public class tblBooksController : Controller
    {
        private libraryManagemenDbContext db = new libraryManagemenDbContext();

        // GET: tblBooks
        public ActionResult Index()
        {
            return View(db.tblBooks.ToList());
        }

        // GET: tblBooks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblBook tblBook = db.tblBooks.Find(id);
            if (tblBook == null)
            {
                return HttpNotFound();
            }
            return View(tblBook);
        }
        [Authorize]
        // GET: tblBooks/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: tblBooks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "bookId,Title,serialNumber,Author,publisher")] tblBook tblBook)
        {
            if (ModelState.IsValid)
            {
                db.tblBooks.Add(tblBook);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return Json(tblBook, JsonRequestBehavior.AllowGet);
        }

        // GET: tblBooks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblBook tblBook = db.tblBooks.Find(id);
            if (tblBook == null)
            {
                return HttpNotFound();
            }
            return View(tblBook);
        }

        // POST: tblBooks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "bookId,Title,serialNumber,Author,publisher")] tblBook tblBook)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblBook).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblBook);
        }

        // GET: tblBooks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblBook tblBook = db.tblBooks.Find(id);
            if (tblBook == null)
            {
                return HttpNotFound();
            }
            return View(tblBook);
        }

        // POST: tblBooks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblBook tblBook = db.tblBooks.Find(id);
            db.tblBooks.Remove(tblBook);
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
