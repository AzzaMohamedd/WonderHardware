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
using Microsoft.AspNetCore.Mvc.Rendering;

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
            ViewBag.NewMotherBoards = _iwonder.GetNewMotherBoards();
            ViewBag.NewProcessors = _iwonder.GetNewProcessors();
            ViewBag.NewRam = _iwonder.GetNewRam();
            ViewBag.NewVGA = _iwonder.GetNewVGA();
            ViewBag.NewHDD = _iwonder.GetNewHDD();
            ViewBag.NewSSD = _iwonder.GetNewSSD();
            ViewBag.NewPSU = _iwonder.GetNewPSU();
            ViewBag.NewCase = _iwonder.GetNewCase();
            return View();
        }
        // Start Store Action
        #region
        [HttpGet("Home/Store")]
        public IActionResult Store()
        {
            ViewBag.BrandNamesAndNumbers = _iwonder.GetBrandNamesAndNumbers();
            return View(_iwonder.GetAllProcessors());
        }
        #endregion
        // End Store Action
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