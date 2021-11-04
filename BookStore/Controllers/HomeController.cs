using Microsoft.AspNetCore.Mvc;
using System.Dynamic;
using BookStore.Models;
using BookStore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;

namespace BookStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;

        public HomeController(IConfiguration configuration, IUserService userService)
        {
            _configuration = configuration;
            _userService = userService;
        }

        [ViewData]
        public string CustomProperty { get; set; }

        [ViewData]
        public string Title { get; set; }

        [ViewData]
        public BookModel Book { get; set; }
        public ViewResult Index()
        {
            var userId = _userService.GetUserId();
            var isUserLoggedIn = _userService.IsAuthenticated();
            
            Title = "Home page";
            CustomProperty = "Custom value";
            var result = _configuration["Logging:LogLevel:Default"];
            var result2 = _configuration.GetValue<string>("Logging:LogLevel:Microsoft");
            Book = new BookModel() {Id = 32, Title = "Good book", Author = result2};
            
            return View();
        }
        [Authorize]
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