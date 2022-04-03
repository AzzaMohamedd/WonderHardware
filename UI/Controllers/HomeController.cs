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
using cloudscribe.Web.Pagination;
using cloudscribe.Pagination.Models;
using Microsoft.AspNetCore.Http;

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
        public IActionResult Store(int pageNumber = 1, int PageSize = 3)
        {
            HttpContext.Session.SetString("PageSize", PageSize.ToString()); 
            HttpContext.Session.SetString("PageNumber", pageNumber.ToString());
            int PNumber = int.Parse(HttpContext.Session.GetString("PageNumber")); // Session for PageNumber
            int SNumber = int.Parse(HttpContext.Session.GetString("PageSize")); // Session for PageSize
            IList<ProcessorVM> processorVMs = _iwonder.Paginations(PNumber, SNumber).ToList(); // Get Records
            PagedResult<ProcessorVM> Data = new PagedResult<ProcessorVM>() // To Pagination in View
            {
                PageNumber = PNumber,
                PageSize = SNumber,
                TotalItems = _iwonder.GetAllProcessors().Count(),
                Data = processorVMs.ToList(),
            };
            ViewBag.BrandNamesAndNumbers = _iwonder.GetBrandNamesAndNumbers(); // Get All Brands 
            ViewData["PageSize"] = PageSize;
            return View(Data);
        }
        [HttpGet]
        public JsonResult AscendingProdoucts(int Id)
        {
            int PageSize =int.Parse(HttpContext.Session.GetString("PageSize"));
            int PageNumber = int.Parse(HttpContext.Session.GetString("PageNumber"));
            IList<ProcessorVM> processor = null;
            if (Id != 0)
            {
              processor = _iwonder.GetProductsByPrice(_iwonder.Paginations(PageNumber, PageSize),Id).ToList();   
            }
            return Json(processor);
        }
        [HttpGet]
        public JsonResult Default(int PageSize=3)
        {
            HttpContext.Session.SetString("PageSize", PageSize.ToString());
            int PNumber = int.Parse(HttpContext.Session.GetString("PageNumber"));
            int SNumber = int.Parse(HttpContext.Session.GetString("PageSize"));
            IList<ProcessorVM> processorVMs = _iwonder.Paginations(PNumber, SNumber).ToList();
            return Json(processorVMs);
        }
        [HttpGet]
        public JsonResult ProductsOfBrand(string brand)
        {
            IList<ProcessorVM> processors = null;
            if (!String.IsNullOrEmpty(brand))
            {
                int PNumber = int.Parse(HttpContext.Session.GetString("PageNumber")); // Session for PageNumber
                int SNumber = int.Parse(HttpContext.Session.GetString("PageSize")); // Session for PageSize
                processors = _iwonder.GetProductsByBrand(brand, PNumber, SNumber).ToList();
            }
            return Json(processors);
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




