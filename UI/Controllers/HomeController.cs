using DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DataModel.Models;
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
        [HttpGet]
        public IActionResult Store()
        {
            ViewBag.BrandNamesAndNumbers = _iwonder.GetBrandNamesAndNumbers();
           
            return View(_iwonder.GetAllProcessors());
        }
        [HttpGet]
        public JsonResult ArrangeProdouct(int Id)
        {
            if (Id != 0)
            {
                return Json(_iwonder.GetProcessorByPrice(Id));
            }
            return Json("false");
        }
        [HttpGet]
        public JsonResult DisplayProducts(int id)
        {
            if (id != 0)
            {
                return Json(_iwonder.TakeProcessor(id));
            }
            return Json("false");
        }
        [HttpGet]
        public JsonResult DisplayBrand(string BName)
        {
            if (!String.IsNullOrEmpty(BName))
            {
              return Json(_iwonder.AllBrands(BName));
            }
            return Json("false");
        }
        [HttpGet]
        public JsonResult HiddenBrand(string BName)
        {
            if (!String.IsNullOrEmpty(BName))
            {
                return Json(_iwonder.HiddenBrands(BName));
            }
            return Json("false");
        }
        #endregion
        // End Store Action
        public IActionResult Cart()
        {
            return View();
        }
        [HttpGet]
        public IActionResult CheckOut()
        {
            return View();
        }
        [HttpPost]
        public JsonResult CheckOut(CheckOutVM checkOut)
        {
            string check;
            if (checkOut.FName != null)
            {
                //create Acc + checkout
                check = _iwonder.CheckOrderCreateAcc(checkOut);
            }
            else
            {
                check = _iwonder.CheckOrder(checkOut);
            }

            return Json(check);
        }

        public IActionResult ProcessorDetails(string code)
        {
            ProcessorVM processor = _iwonder.ProcessorDetails(code);
            return View(processor);
        }
    }
}




