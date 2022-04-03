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
            IList<ProcessorVM> processorVMs = _iwonder.Paginations(pageNumber, PageSize).ToList();
            PagedResult<ProcessorVM> Data = new PagedResult<ProcessorVM>()
            {
                PageNumber = pageNumber,
                PageSize = PageSize,
                TotalItems = _iwonder.GetAllProcessors().Count(),
                Data = processorVMs.ToList(),
            };
            ViewBag.BrandNamesAndNumbers = _iwonder.GetBrandNamesAndNumbers();
            HttpContext.Session.SetString("PageSize", PageSize.ToString());
            HttpContext.Session.SetString("PageNumber", pageNumber.ToString());
            return View(Data);
        }
        [HttpGet]
        public JsonResult AscendingProdoucts(int Id)
        {
            string PageSize = HttpContext.Session.GetString("PageSize");
            string PageNumber = HttpContext.Session.GetString("PageNumber");
            IList<ProcessorVM> processor = null;
            if (Id != 0)
            {
                if (Id == 1)
                {
                    processor = _iwonder.Paginations(int.Parse(PageNumber), int.Parse(PageSize)).OrderByDescending(pvm => pvm.ProPrice).ToList();
                }
                else
                {
                    processor =_iwonder.Paginations(int.Parse(PageNumber), int.Parse(PageSize)).OrderBy(pvm => pvm.ProPrice).ToList();
                }
            }
            return Json(processor);
        }



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




