using Microsoft.AspNetCore.Mvc;
using System.Dynamic;
using BookStore.Models;

namespace BookStore.Controllers
{
    public class HomeController : Controller
    {
        [ViewData]
        public string CustomProperty { get; set; }

        [ViewData]
        public string Title { get; set; }

        [ViewData]
        public BookModel Book { get; set; }
        public ViewResult Index()
        {
            Title = "Home page";
            CustomProperty = "Custom value";
            Book = new BookModel() {Id = 32, Title = "Good book"};
            
            return View();
        }

        public ViewResult AboutUs()
        {
            Title = "About Us";
            return View();
        }
    }
}