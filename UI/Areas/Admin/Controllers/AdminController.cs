using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UI.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            //hamza or ragab 
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Users()
        {
            return View();
        }
    }
}
