using BookingAppStore.Models;
using System;
using System.Web.Mvc;

namespace BookingAppStore.Controllers
{
    public class HomeController : Controller
    {
        BookContext db = new BookContext();
        public ActionResult Index()
        {
            // Получаем список всех книг из БД
            var books = db.Books;
            // Передаём список всех книг в представление
            // ViewBag.Books = books;
            return View(books);
        }

        public ActionResult BookIndex()
        {
            // Получаем список всех книг из БД
            var books = db.Books;
            // Передаём список всех книг в представление
            // ViewBag.Books = books;
            return View(books);
        }

        [HttpGet] // По умолчанию метод HttpGet - его можно не указывать
        public ActionResult Buy(int id)
        {
            ViewBag.BookId = id;
            return View();
        }

        [HttpPost]
        public string Buy(Purchase purchase)
        {
            purchase.Date = DateTime.Now;
            // Добавляем информацию о покупке в БД
            db.Purchases.Add(purchase);
            // Сохраняем в БД все изменения
            db.SaveChanges();
            return "Спасибо, " + purchase.Person + ", за покупку!";
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