using BookingAppStore.Models;
using System;
using System.Web.Mvc;

namespace BookingAppStore.Controllers
{
    public class HomeController : Controller
    {
        BookContext db = new BookContext();
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
        public ActionResult Index()
        {
            // Получаем список всех книг из БД
            var books = db.Books;
            // Заголовок для частичного преставления _GetList
            ViewBag.Message = "Это частичное представление 2";
            // Передаём список всех книг в представление
            // ViewBag.Books = books;
            // Список опций для DropDownList
            SelectList authors = new SelectList(db.Books, "Author", "Name");
            ViewBag.Authors = authors;
            return View(books);
        }

        // Для частичных представлений имена обычно начинают со знака подчёркивания чтобы
        // подчеркнуть, что они не предназначены для прямого обращения
        // Через этот метод можно вызвать частичное представление как обычное
        // При встраивании частичного представления этот метод не вызыватся
        public ActionResult _GetList()
        {
            ViewBag.Message = "Это частичное представление";
            string[] countries = new string[] { "Ukraine", "USA", "France" };
            return PartialView(countries);
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
        public ActionResult Buy(int? id)
        {
            // ViewBag.BookId = id;
            Purchase purchase = new Purchase { BookId = (id != null ? (int)id : 0), Person = "неизвестно" };
            return View(purchase);
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
        [HttpPost]
        public string GetForm(string text, string color, bool set, string author, string[] countries)
        {
            string result = "";
            foreach (string country in countries)
            {
                result += (result == "" ? "" : ", ") + country;
            }
            return $"text: {text} color: {color} set: {set} author: {author} countries: {result}";
        }
        public ActionResult GetBook(int id)
        {
            Book b = db.Books.Find(id);
            if (b == null)
                return HttpNotFound();
            return View(b);
        }
    }
}