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
        public IActionResult CaseDetails(string code)
        {
            return View(_iwonder.CaseDetails(code));
        }
        public IActionResult GraphicsCardDetails(string code)
        {
            return View(_iwonder.GraphicsCardDetails(code));
        }
        public IActionResult HddDetails(string code)
        {
            return View(_iwonder.HddDetails(code));
        }
        public IActionResult MotherboardDetails(string code)
        {
            return View(_iwonder.MotherboardDetails(code));
        }
        public IActionResult PowerSupplyDetails(string code)
        {
            return View(_iwonder.PowerSupplyDetails(code));
        }
        public IActionResult ProcessorDetails(string code)
        {
            return View(_iwonder.ProcessorDetails(code));
        }
        public IActionResult RamDetails(string code)
        {
            return View(_iwonder.RamDetails(code));
        }
        public IActionResult SsdDetails(string code)
        {
            return View(_iwonder.SsdDetails(code));
        }


    }
}