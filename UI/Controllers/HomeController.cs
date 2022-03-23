using DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace UI.Controllers
{
    public class HomeController : Controller
    {
        readonly IWonder _iwonder;

        public HomeController(IWonder iwonder)
        {
            _iwonder = iwonder;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Store()
        {
            ViewBag.BrandNamesAndNumbers = _iwonder.GetBrandNamesAndNumbers();
            return View(_iwonder.GetAll());
        }

        public IActionResult CheckOut()
        {
            return View();
        }
    }
}