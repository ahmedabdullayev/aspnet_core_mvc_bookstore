using Microsoft.AspNetCore.Mvc;
using System.Dynamic;
using BookStore.Models;
using Microsoft.Extensions.Configuration;

namespace BookStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;

        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

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
            var result = _configuration["Logging:LogLevel:Default"];
            var result2 = _configuration.GetValue<string>("Logging:LogLevel:Microsoft");
            Book = new BookModel() {Id = 32, Title = "Good book", Author = result2};
            
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