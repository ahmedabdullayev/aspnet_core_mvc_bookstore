using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Areas.Admin.Controllers
{
    [Area("admin")]
    // [Route("admin/[controller]/[action]")]
    [Route("admin")]
    public class HomeController : Controller
    {
        // GET: Home
        [Route("")]
        public ActionResult Index()
        {
            return View();
        }

        // GET: Home/Details/5
        [Route("details/{id}")]
        public ActionResult Details(int id)
        {
            
            return View(id);
        }
        
        
    }
}