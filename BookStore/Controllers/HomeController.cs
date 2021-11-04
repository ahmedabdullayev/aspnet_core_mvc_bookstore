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
            // var client = new SmtpClient("smtp.mailtrap.io", 2525)
            // {
            //     Credentials = new NetworkCredential("9a403a46310b41", "16e28e18036665"),
            //     EnableSsl = true
            // };
            // client.Send("from@example.com", "to@example.com", "Hello world", "testbody");
            // Console.WriteLine("Sent");
            // Console.ReadLine();
            var smtpConfigModel = _smtpConfig;
            Console.WriteLine(smtpConfigModel);
            Console.WriteLine(smtpConfigModel.Host);
            Console.WriteLine(smtpConfigModel.Password);
            Console.WriteLine(smtpConfigModel.Port);
            Console.WriteLine(smtpConfigModel.SenderAddress);
            Console.WriteLine(smtpConfigModel.UserName);
            Console.WriteLine(smtpConfigModel.SenderDisplayName);
            Console.WriteLine(smtpConfigModel.UseDefaultCredentials);
            Console.WriteLine(smtpConfigModel.EnableSSL);
            Console.WriteLine(smtpConfigModel.IsBodyHTML);
            UserEmailOptions options = new UserEmailOptions
            {
                ToEmails = new List<string>() {"test@gmail.com"},
                PlaceHolders = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("{{UserName}}", "ahabdu")
                }
            };
            await _emailService.SendTestEmail(options);
            // var userId = _userService.GetUserId();
            // var isUserLoggedIn = _userService.IsAuthenticated();
            
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