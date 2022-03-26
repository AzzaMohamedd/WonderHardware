using DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using UI.Models;
using BLL.ViewModel;

namespace UI.Controllers
{
    public class HomeController : Controller
    {
        readonly IWonder _iwonder;
        WonderHardwareContext db = new WonderHardwareContext();
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

        public IActionResult ProcessorDetails(string code)
        {
            ProcessorVM processor = _iwonder.ProcessorDetails(code);
            return View(processor);
        }

    }
}