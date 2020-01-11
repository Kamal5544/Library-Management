using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CrystalDecisions.CrystalReports.Engine;
using MVC_Project.Models;

namespace MVC_Project.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddImage()
        {
            Image img = new Image();
            return View(img);
        }
        [HttpPost]
        public ActionResult AddImage(Image model, HttpPostedFileBase image1)
        {
            var db = new libraryManagemenEntities();
            if (image1 != null)
            {

                model.picture = new byte[image1.ContentLength];
                image1.InputStream.Read(model.picture, 0, image1.ContentLength);
            }
            image1.SaveAs(Server.MapPath("~/Images/" + image1.FileName));
            db.Images.Add(model);
            db.SaveChanges();
            return RedirectToAction("Index1");
        }
        public ActionResult Index1()
        {
            var db = new libraryManagemenEntities();
            var item = (from d in db.Images select d).ToList();
            return View(item);
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        libraryManagemenDbContext db = new libraryManagemenDbContext();

        public ActionResult Index2()
        {
            return View();
        }
        public ActionResult PartialViews()
        {
            return View(db.tblBorrowHistories.ToList());
        }

        // Return all students
        public PartialViewResult All()
        {
            List<tblBorrowHistory> model = db.tblBorrowHistories.ToList();
            return PartialView("_BorrowRecord", model);
        }

        // Return Top3 students
        public PartialViewResult LastBorrowRecord()
        {
            List<tblBorrowHistory> model = db.tblBorrowHistories.OrderByDescending(x => x.BorrowDate).Take(3).ToList();
            return PartialView("_BorrowRecord", model);
        }

        // Return Bottom3 students
        public PartialViewResult NewBorrowRecord()
        {
            List<tblBorrowHistory> model = db.tblBorrowHistories.OrderBy(x => x.BorrowDate).Take(3).ToList();
            return PartialView("_BorrowRecord", model);
        }
        public ActionResult BookList()
        {
            return View(db.tblBooks.ToList());
        }

        public ActionResult exportReport()
        {
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Report"), "CrystalReport.rpt"));
            rd.SetDataSource(db.tblBooks.ToList());
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            try
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "application/pdf", "Book_List.pdf");
            }
            catch
            {
                throw;
            }
        }
    }
}
