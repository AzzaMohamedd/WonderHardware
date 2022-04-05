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
       
        // Start  Processors
        #region
        [HttpGet]
        public IActionResult Processor(int pageNumber = 1, int PageSize = 3)
        {
            HttpContext.Session.SetString("PageSize", PageSize.ToString()); 
            HttpContext.Session.SetString("PageNumber", pageNumber.ToString());
            int PNumber = int.Parse(HttpContext.Session.GetString("PageNumber")); // Session for PageNumber
            int SNumber = int.Parse(HttpContext.Session.GetString("PageSize")); // Session for PageSize
            IList<ProcessorVM> processorVMs = _iwonder.ProcessorPaginations(PNumber, SNumber).ToList(); // Get Records
            PagedResult<ProcessorVM> Data = new PagedResult<ProcessorVM>() // To Pagination in View
            {
                PageNumber = PNumber,
                PageSize = SNumber,
                TotalItems = _iwonder.GetAllProcessors().Count(),
                Data = processorVMs.ToList(),
            };
            ViewBag.BrandNamesAndNumbers = _iwonder.GetProcessorBrandNamesAndNumbers(); // Get All Brands 
            ViewData["PageSize"] = PageSize;
            return View(Data);
        }
        [HttpGet]
        public JsonResult AscendingProcessorProdoucts(int Id)
        {
            int PageSize =int.Parse(HttpContext.Session.GetString("PageSize"));
            int PageNumber = int.Parse(HttpContext.Session.GetString("PageNumber"));
            IList<ProcessorVM> processor = null;
            if (Id != 0)
            {
              processor = _iwonder.GetProcessorProductsByPrice(_iwonder.ProcessorPaginations(PageNumber, PageSize),Id).ToList();   
            }
            return Json(processor);
        }
        [HttpGet]
        public JsonResult DefaultProcessor(int PageSize=3)
        {
            HttpContext.Session.SetString("PageSize", PageSize.ToString());
            int PNumber = int.Parse(HttpContext.Session.GetString("PageNumber"));
            int SNumber = int.Parse(HttpContext.Session.GetString("PageSize"));
            IList<ProcessorVM> processorVMs = _iwonder.ProcessorPaginations(PNumber, SNumber).ToList();
            return Json(processorVMs);
        }
        [HttpGet]
        public JsonResult ProductsOfProcessorBrand(string brand)
        {
            IList<ProcessorVM> processors = null;
            if (!String.IsNullOrEmpty(brand))
            {
                int PNumber = int.Parse(HttpContext.Session.GetString("PageNumber")); // Session for PageNumber
                int SNumber = int.Parse(HttpContext.Session.GetString("PageSize")); // Session for PageSize
                processors = _iwonder.GetProcessorProductsByBrand(brand, PNumber, SNumber).ToList();
            }
            return Json(processors);
        }
        #endregion
        // End Processors

        // Start Motherboards
        #region
        [HttpGet]
        public IActionResult Motherboard(int pageNumber = 1, int PageSize = 3)
        {
            HttpContext.Session.SetString("PageSize", PageSize.ToString());
            HttpContext.Session.SetString("PageNumber", pageNumber.ToString());
            int PNumber = int.Parse(HttpContext.Session.GetString("PageNumber")); // Session for PageNumber
            int SNumber = int.Parse(HttpContext.Session.GetString("PageSize")); // Session for PageSize
            IList<MotherboardVM> MotherboardVMs = _iwonder.MotherboardPaginations(PNumber, SNumber).ToList(); // Get Records
            PagedResult<MotherboardVM> Data = new PagedResult<MotherboardVM>() // To Pagination in View
            {
                PageNumber = PNumber,
                PageSize = SNumber,
                TotalItems = _iwonder.GetAllMotherboard().Count(),
                Data = MotherboardVMs.ToList(),
            };
            ViewBag.BrandNamesAndNumbers = _iwonder.GetMotherboardBrandNamesAndNumbers(); // Get All Brands 
            ViewData["PageSize"] = PageSize;
            return View(Data);
        }
        [HttpGet]
        public JsonResult AscendingMotherboardProdoucts(int Id)
        {
            int PageSize = int.Parse(HttpContext.Session.GetString("PageSize"));
            int PageNumber = int.Parse(HttpContext.Session.GetString("PageNumber"));
            IList<MotherboardVM> motherboards = null;
            if (Id != 0)
            {
                motherboards = _iwonder.GetMotherboardProductsByPrice(_iwonder.MotherboardPaginations(PageNumber, PageSize), Id).ToList();
            }
            return Json(motherboards);
        }
        [HttpGet]
        public JsonResult DefaultMotherboard(int PageSize = 3)
        {
            HttpContext.Session.SetString("PageSize", PageSize.ToString());
            int PNumber = int.Parse(HttpContext.Session.GetString("PageNumber"));
            int SNumber = int.Parse(HttpContext.Session.GetString("PageSize"));
            IList<MotherboardVM> MotherBoardVMs = _iwonder.MotherboardPaginations(PNumber, SNumber).ToList();
            return Json(MotherBoardVMs);
        }
        [HttpGet]
        public JsonResult ProductsOfMotherboardBrand(string brand)
        {
            IList<MotherboardVM> motherboards = null;
            if (!String.IsNullOrEmpty(brand))
            {
                int PNumber = int.Parse(HttpContext.Session.GetString("PageNumber")); // Session for PageNumber
                int SNumber = int.Parse(HttpContext.Session.GetString("PageSize")); // Session for PageSize
                motherboards = _iwonder.GetMotherboardProductsByBrand(brand, PNumber, SNumber).ToList();
            }
            return Json(motherboards);
        }
        #endregion
        // End Motherboards

        // Start HDD
        
        #region
        [HttpGet]
        public IActionResult HDD(int pageNumber = 1, int PageSize = 3)
        {
            HttpContext.Session.SetString("PageSize", PageSize.ToString());
            HttpContext.Session.SetString("PageNumber", pageNumber.ToString());
            int PNumber = int.Parse(HttpContext.Session.GetString("PageNumber")); // Session for PageNumber
            int SNumber = int.Parse(HttpContext.Session.GetString("PageSize")); // Session for PageSize
            IList<HddVM> HddVMs = _iwonder.HDDPaginations(PNumber, SNumber).ToList(); // Get Records
            PagedResult<HddVM> Data = new PagedResult<HddVM>() // To Pagination in View
            {
                PageNumber = PNumber,
                PageSize = SNumber,
                TotalItems = _iwonder.GetAllHDD().Count(),
                Data = HddVMs.ToList(),
            };
            ViewBag.BrandNamesAndNumbers = _iwonder.GetHDDBrandNamesAndNumbers(); // Get All Brands 
            ViewData["PageSize"] = PageSize;
            return View(Data);
        }
        [HttpGet]
        public JsonResult AscendingHDDProdoucts(int Id)
        {
            int PageSize = int.Parse(HttpContext.Session.GetString("PageSize"));
            int PageNumber = int.Parse(HttpContext.Session.GetString("PageNumber"));
            IList<HddVM> hddvm = null;
            if (Id != 0)
            {
                hddvm = _iwonder.GetHDDProductsByPrice(_iwonder.HDDPaginations(PageNumber, PageSize), Id).ToList();
            }
            return Json(hddvm);
        }
        [HttpGet]
        public JsonResult DefaultHDD(int PageSize = 3)
        {
            HttpContext.Session.SetString("PageSize", PageSize.ToString());
            int PNumber = int.Parse(HttpContext.Session.GetString("PageNumber"));
            int SNumber = int.Parse(HttpContext.Session.GetString("PageSize"));
            IList<HddVM> hdds = _iwonder.HDDPaginations(PNumber, SNumber).ToList();
            return Json(hdds);
        }
        [HttpGet]
        public JsonResult ProductsOfHDDBrand(string brand)
        {
            IList<HddVM> Hdds = null;
            if (!String.IsNullOrEmpty(brand))
            {
                int PNumber = int.Parse(HttpContext.Session.GetString("PageNumber")); // Session for PageNumber
                int SNumber = int.Parse(HttpContext.Session.GetString("PageSize")); // Session for PageSize
                Hdds = _iwonder.GetHDDProductsByBrand(brand, PNumber, SNumber).ToList();
            }
            return Json(Hdds);
        }
        #endregion
        // End HDD
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




