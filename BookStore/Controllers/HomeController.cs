using Microsoft.AspNetCore.Mvc;
using System.Dynamic;
using BookStore.Models;

namespace BookStore.Controllers
{
    public class HomeController : Controller
    {
        public ViewResult Index()
        {
            
            ViewData["book"] = new BookModel() {Id = 5, Author = "Ahmed Abdullajev"};

            return View();
        }

        public ViewResult AboutUs()
        {
            return View();
        }
    }
}