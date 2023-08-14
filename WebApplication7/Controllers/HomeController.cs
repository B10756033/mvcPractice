using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication7.Models;

namespace WebApplication7.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            DBmanager dbmanager = new DBmanager();
            List<Models.DBmanager.Book> books = dbmanager.GetBooks();
            ViewBag.books = books;
            return View();
        }
        public ActionResult CreateBook()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateBook(DBmanager.Book book)
        {
            DBmanager dBmanager = new DBmanager();
            try {
                dBmanager.NewBook(book);
            } 
            catch (Exception e){
                Console.WriteLine(e.ToString());
            }
            return RedirectToAction("Index"); 
        }
        public ActionResult EditBook(string id)
        {
            DBmanager dBmanager = new DBmanager();
            DBmanager.Book book = dBmanager.GetBooks().Where(m => m.Barcode == id).FirstOrDefault(); 
            return View(book);
        }
        public ActionResult DelBook(string id)
        {
            DBmanager dBmanager = new DBmanager();
            dBmanager.delBookById(id);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult EditBook(DBmanager.Book book)
        {
            DBmanager dBmanager = new DBmanager();
            dBmanager.updBook(book);
            return RedirectToAction("Index");
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
    }
}