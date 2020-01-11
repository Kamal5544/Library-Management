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
    public class tblBorrowHistoriesController : Controller
    {
        private libraryManagemenDbContext db = new libraryManagemenDbContext();

        // GET: tblBorrowHistories
        public ActionResult Index()
        {
            var tblBorrowHistories = db.tblBorrowHistories.Include(t => t.tblBook).Include(t => t.tblMember);
            return View(tblBorrowHistories.ToList());
        }

        // GET: tblBorrowHistories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblBorrowHistory tblBorrowHistory = db.tblBorrowHistories.Find(id);
            if (tblBorrowHistory == null)
            {
                return HttpNotFound();
            }
            return View(tblBorrowHistory);
        }

        // GET: tblBorrowHistories/Create
        public ActionResult Create()
        {
            ViewBag.bookId = new SelectList(db.tblBooks, "bookId", "Title");
            ViewBag.memberId = new SelectList(db.tblMembers, "memberId", "memberName");
            return View();
        }

        // POST: tblBorrowHistories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BorrowHistoryId,bookId,memberId,BorrowDate,ReturnDate")] tblBorrowHistory tblBorrowHistory)
        {
            if (ModelState.IsValid)
            {
                db.tblBorrowHistories.Add(tblBorrowHistory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.bookId = new SelectList(db.tblBooks, "bookId", "Title", tblBorrowHistory.bookId);
            ViewBag.memberId = new SelectList(db.tblMembers, "memberId", "memberName", tblBorrowHistory.memberId);
            return View(tblBorrowHistory);
        }

        // GET: tblBorrowHistories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblBorrowHistory tblBorrowHistory = db.tblBorrowHistories.Find(id);
            if (tblBorrowHistory == null)
            {
                return HttpNotFound();
            }
            ViewBag.bookId = new SelectList(db.tblBooks, "bookId", "Title", tblBorrowHistory.bookId);
            ViewBag.memberId = new SelectList(db.tblMembers, "memberId", "memberName", tblBorrowHistory.memberId);
            return View(tblBorrowHistory);
        }

        // POST: tblBorrowHistories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BorrowHistoryId,bookId,memberId,BorrowDate,ReturnDate")] tblBorrowHistory tblBorrowHistory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblBorrowHistory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.bookId = new SelectList(db.tblBooks, "bookId", "Title", tblBorrowHistory.bookId);
            ViewBag.memberId = new SelectList(db.tblMembers, "memberId", "memberName", tblBorrowHistory.memberId);
            return View(tblBorrowHistory);
        }

        // GET: tblBorrowHistories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblBorrowHistory tblBorrowHistory = db.tblBorrowHistories.Find(id);
            if (tblBorrowHistory == null)
            {
                return HttpNotFound();
            }
            return View(tblBorrowHistory);
        }

        // POST: tblBorrowHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblBorrowHistory tblBorrowHistory = db.tblBorrowHistories.Find(id);
            db.tblBorrowHistories.Remove(tblBorrowHistory);
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
