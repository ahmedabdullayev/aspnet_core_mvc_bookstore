using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using BookStore.Models;
using BookStore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace BookStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;
        private readonly SMTPConfigModel _smtpConfig;

        public HomeController(IConfiguration configuration, IUserService userService, IEmailService emailService, IOptions<SMTPConfigModel> smtpConfig)
        {
            _configuration = configuration;
            _userService = userService;
            _emailService = emailService;
            _smtpConfig = smtpConfig.Value;
        }

        [ViewData]
        public string CustomProperty { get; set; }

        [ViewData]
        public string Title { get; set; }

        [ViewData]
        public BookModel Book { get; set; }
        public async Task<ViewResult> Index()
        {
            // var userId = _userService.GetUserId();
            // var isUserLoggedIn = _userService.IsAuthenticated();
            
            Title = "Home page";
            CustomProperty = "Custom value";
            var result = _configuration["Logging:LogLevel:Default"];
            var result2 = _configuration.GetValue<string>("Logging:LogLevel:Microsoft");
            Book = new BookModel() {Id = 32, Title = "Good book", Author = result2};
            
            return View();
        }
        [Authorize(Roles = "user")]
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