using Microsoft.AspNetCore.Mvc;
using System.Dynamic;
using BookStore.Models;

namespace BookStore.Controllers
{
    public class HomeController : Controller
    {
        public ViewResult Index()
        {
            ViewBag.Title = "Home Page";

            dynamic data = new ExpandoObject();
            data.Id = 1;
            data.Name = "Ahma";

            ViewBag.Data = data;

            ViewBag.Type = new BookModel() {Id = 5, Author = "Ahmed Abdullajev"};

            return View();
        }

        public ViewResult AboutUs()
        {
            return View();
        }
    }
}