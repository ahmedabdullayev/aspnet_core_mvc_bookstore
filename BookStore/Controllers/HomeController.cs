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

        [Route("about-us/{id?}/{name?}")]
        public ViewResult AboutUs(int? id, string name)
        {
            Title = "About Us " + id + name;
            return View();
        }
        
        [Route("test/a{a}")]
        public string Test(string a)
        {
            return a;
        }
        
        [Route("test/b{a}")]
        public string Test1(string a)
        {
            return a;
        }
        
        
    }
}