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
        // Start RAM
        #region
        [HttpGet]
        public IActionResult RAM(int pageNumber = 1, int PageSize = 3)
        {
            HttpContext.Session.SetString("PageSize", PageSize.ToString());
            HttpContext.Session.SetString("PageNumber", pageNumber.ToString());
            int PNumber = int.Parse(HttpContext.Session.GetString("PageNumber")); // Session for PageNumber
            int SNumber = int.Parse(HttpContext.Session.GetString("PageSize")); // Session for PageSize
            IList<RamVM> RamVMs =_iwonder.RAMPaginations(PNumber, SNumber).ToList(); // Get Records
            PagedResult<RamVM> Data = new PagedResult<RamVM>() // To Pagination in View
            {
                PageNumber = PNumber,
                PageSize = SNumber,
                TotalItems = _iwonder.GetAllHDD().Count(),
                Data = RamVMs.ToList(),
            };
            ViewBag.BrandNamesAndNumbers = _iwonder.GetRAMBrandNamesAndNumbers(); // Get All Brands 
            ViewData["PageSize"] = PageSize;
            return View(Data);
        }
        [HttpGet]
        public JsonResult AscendingRAMProdoucts(int Id)
        {
            int PageSize = int.Parse(HttpContext.Session.GetString("PageSize"));
            int PageNumber = int.Parse(HttpContext.Session.GetString("PageNumber"));
            IList<RamVM> ramVMs = null;
            if (Id != 0)
            {
                ramVMs = _iwonder.GetRAMProductsByPrice(_iwonder.RAMPaginations(PageNumber, PageSize), Id).ToList();
            }
            return Json(ramVMs);
        }
        [HttpGet]
        public JsonResult DefaultRAM(int PageSize = 3)
        {
            HttpContext.Session.SetString("PageSize", PageSize.ToString());
            int PNumber = int.Parse(HttpContext.Session.GetString("PageNumber"));
            int SNumber = int.Parse(HttpContext.Session.GetString("PageSize"));
            IList<RamVM> rams = _iwonder.RAMPaginations(PNumber, SNumber).ToList();
            return Json(rams);
        }
        [HttpGet]
        public JsonResult ProductsOfRAMBrand(string brand)
        {
            IList<RamVM> rams = null;
            if (!String.IsNullOrEmpty(brand))
            {
                int PNumber = int.Parse(HttpContext.Session.GetString("PageNumber")); // Session for PageNumber
                int SNumber = int.Parse(HttpContext.Session.GetString("PageSize")); // Session for PageSize
                rams = _iwonder.GetRAMProductsByBrand(brand, PNumber, SNumber).ToList();
            }
            return Json(rams);
        }
        #endregion
        // End RAM
        // Start SSD
        #region
        [HttpGet]
        public IActionResult SSD(int pageNumber = 1, int PageSize = 3)
        {
            HttpContext.Session.SetString("PageSize", PageSize.ToString());
            HttpContext.Session.SetString("PageNumber", pageNumber.ToString());
            int PNumber = int.Parse(HttpContext.Session.GetString("PageNumber")); // Session for PageNumber
            int SNumber = int.Parse(HttpContext.Session.GetString("PageSize")); // Session for PageSize
            IList<SsdVM> ssdVMs = _iwonder.SSDPaginations(PNumber, SNumber).ToList(); // Get Records
            PagedResult<SsdVM> Data = new PagedResult<SsdVM>() // To Pagination in View
            {
                PageNumber = PNumber,
                PageSize = SNumber,
                TotalItems = _iwonder.GetAllSSD().Count(),
                Data = ssdVMs.ToList(),
            };
            ViewBag.BrandNamesAndNumbers = _iwonder.GetSSDBrandNamesAndNumbers(); // Get All Brands 
            ViewData["PageSize"] = PageSize;
            return View(Data);
        }
        [HttpGet]
        public JsonResult AscendingSSDProdoucts(int Id)
        {
            int PageSize = int.Parse(HttpContext.Session.GetString("PageSize"));
            int PageNumber = int.Parse(HttpContext.Session.GetString("PageNumber"));
            IList<SsdVM> ssdVMs = null;
            if (Id != 0)
            {
                ssdVMs = _iwonder.GetSSDProductsByPrice(_iwonder.SSDPaginations(PageNumber, PageSize), Id).ToList();
            }
            return Json(ssdVMs);
        }
        [HttpGet]
        public JsonResult DefaultSSD(int PageSize = 3)
        {
            HttpContext.Session.SetString("PageSize", PageSize.ToString());
            int PNumber = int.Parse(HttpContext.Session.GetString("PageNumber"));
            int SNumber = int.Parse(HttpContext.Session.GetString("PageSize"));
            IList<SsdVM> ssds = _iwonder.SSDPaginations(PNumber, SNumber).ToList();
            return Json(ssds);
        }
        [HttpGet]
        public JsonResult ProductsOfSSDBrand(string brand)
        {
            IList<SsdVM> ssdVMs = null;
            if (!String.IsNullOrEmpty(brand))
            {
                int PNumber = int.Parse(HttpContext.Session.GetString("PageNumber")); // Session for PageNumber
                int SNumber = int.Parse(HttpContext.Session.GetString("PageSize")); // Session for PageSize
                ssdVMs = _iwonder.GetSSDProductsByBrand(brand, PNumber, SNumber).ToList();
            }
            return Json(ssdVMs);
        }
        #endregion
        // End SDD
        // Start Graphics Card
        #region
        [HttpGet]
        public IActionResult GraphicsCard(int pageNumber = 1, int PageSize = 3)
        {
            HttpContext.Session.SetString("PageSize", PageSize.ToString());
            HttpContext.Session.SetString("PageNumber", pageNumber.ToString());
            int PNumber = int.Parse(HttpContext.Session.GetString("PageNumber")); // Session for PageNumber
            int SNumber = int.Parse(HttpContext.Session.GetString("PageSize")); // Session for PageSize
            IList<GraphicsCardVM> CardVMs = _iwonder.CardPaginations(PNumber, SNumber).ToList(); // Get Records
            PagedResult<GraphicsCardVM> Data = new PagedResult<GraphicsCardVM>() // To Pagination in View
            {
                PageNumber = PNumber,
                PageSize = SNumber,
                TotalItems = _iwonder.GetAllCard().Count(),
                Data = CardVMs.ToList(),
            };
            ViewBag.BrandNamesAndNumbers = _iwonder.GetCardVMBrandNamesAndNumbers(); // Get All Brands 
            ViewData["PageSize"] = PageSize;
            return View(Data);
        }
        [HttpGet]
        public JsonResult AscendingCardProdoucts(int Id)
        {
            int PageSize = int.Parse(HttpContext.Session.GetString("PageSize"));
            int PageNumber = int.Parse(HttpContext.Session.GetString("PageNumber"));
            IList<GraphicsCardVM> CardVMs = null;
            if (Id != 0)
            {
                CardVMs = _iwonder.GetCardVMProductsByPrice(_iwonder.CardPaginations(PageNumber, PageSize), Id).ToList();
            }
            return Json(CardVMs);
        }
        [HttpGet]
        public JsonResult DefaultCard(int PageSize = 3)
        {
            HttpContext.Session.SetString("PageSize", PageSize.ToString());
            int PNumber = int.Parse(HttpContext.Session.GetString("PageNumber"));
            int SNumber = int.Parse(HttpContext.Session.GetString("PageSize"));
            IList<GraphicsCardVM> Cards = _iwonder.CardPaginations(PNumber, SNumber).ToList();
            return Json(Cards);
        }
        [HttpGet]
        public JsonResult ProductsOfCardBrand(string brand)
        {
            IList<GraphicsCardVM> CardVMs = null;
            if (!String.IsNullOrEmpty(brand))
            {
                int PNumber = int.Parse(HttpContext.Session.GetString("PageNumber")); // Session for PageNumber
                int SNumber = int.Parse(HttpContext.Session.GetString("PageSize")); // Session for PageSize
                CardVMs = _iwonder.GetCardProductsByBrand(brand, PNumber, SNumber).ToList();
            }
            return Json(CardVMs);
        }
        #endregion
        // End Graphics card
        // Start Case
        #region
        [HttpGet]
        public IActionResult Case(int pageNumber = 1, int PageSize = 3)
        {
            HttpContext.Session.SetString("PageSize", PageSize.ToString());
            HttpContext.Session.SetString("PageNumber", pageNumber.ToString());
            int PNumber = int.Parse(HttpContext.Session.GetString("PageNumber")); // Session for PageNumber
            int SNumber = int.Parse(HttpContext.Session.GetString("PageSize")); // Session for PageSize
            IList<CaseVM> caseVMs = _iwonder.CasePaginations(PNumber, SNumber).ToList(); // Get Records
            PagedResult<CaseVM> Data = new PagedResult<CaseVM>() // To Pagination in View
            {
                PageNumber = PNumber,
                PageSize = SNumber,
                TotalItems = _iwonder.GetAllCard().Count(),
                Data = caseVMs.ToList(),
            };
            ViewBag.BrandNamesAndNumbers = _iwonder.GetCaseVMBrandNamesAndNumbers(); // Get All Brands 
            ViewData["PageSize"] = PageSize;
            return View(Data);
        }
        [HttpGet]
        public JsonResult AscendingCaseProdoucts(int Id)
        {
            int PageSize = int.Parse(HttpContext.Session.GetString("PageSize"));
            int PageNumber = int.Parse(HttpContext.Session.GetString("PageNumber"));
            IList<CaseVM> caseVMs = null;
            if (Id != 0)
            {
                caseVMs = _iwonder.GetCaseVMProductsByPrice(_iwonder.CasePaginations(PageNumber, PageSize), Id).ToList();
            }
            return Json(caseVMs);
        }
        [HttpGet]
        public JsonResult DefaultCase(int PageSize = 3)
        {
            HttpContext.Session.SetString("PageSize", PageSize.ToString());
            int PNumber = int.Parse(HttpContext.Session.GetString("PageNumber"));
            int SNumber = int.Parse(HttpContext.Session.GetString("PageSize"));
            IList<CaseVM> caseVMs = _iwonder.CasePaginations(PNumber, SNumber).ToList();
            return Json(caseVMs);
        }
        [HttpGet]
        public JsonResult ProductsOfCaseBrand(string brand)
        {
            IList<CaseVM> caseVMs = null;
            if (!String.IsNullOrEmpty(brand))
            {
                int PNumber = int.Parse(HttpContext.Session.GetString("PageNumber")); // Session for PageNumber
                int SNumber = int.Parse(HttpContext.Session.GetString("PageSize")); // Session for PageSize
                caseVMs = _iwonder.GetCaseProductsByBrand(brand, PNumber, SNumber).ToList();
            }
            return Json(caseVMs);
        }
        #endregion
        // End Case
        // Start PowerSuply
        #region
        [HttpGet]
        public IActionResult PowerSuply(int pageNumber = 1, int PageSize = 3)
        {
            HttpContext.Session.SetString("PageSize", PageSize.ToString());
            HttpContext.Session.SetString("PageNumber", pageNumber.ToString());
            int PNumber = int.Parse(HttpContext.Session.GetString("PageNumber")); // Session for PageNumber
            int SNumber = int.Parse(HttpContext.Session.GetString("PageSize")); // Session for PageSize
            IList<PowerSupplyVM> psvm = _iwonder.PowerSuplyPaginations(PNumber, SNumber).ToList(); // Get Records
            PagedResult<PowerSupplyVM> Data = new PagedResult<PowerSupplyVM>() // To Pagination in View
            {
                PageNumber = PNumber,
                PageSize = SNumber,
                TotalItems = _iwonder.GetAllCard().Count(),
                Data = psvm.ToList(),
            };
            ViewBag.BrandNamesAndNumbers = _iwonder.GetPowerSupplyBrandNamesAndNumbers(); // Get All Brands 
            ViewData["PageSize"] = PageSize;
            return View(Data);
        }
        [HttpGet]
        public JsonResult AscendingPowerSuplyProdoucts(int Id)
        {
            int PageSize = int.Parse(HttpContext.Session.GetString("PageSize"));
            int PageNumber = int.Parse(HttpContext.Session.GetString("PageNumber"));
            IList<PowerSupplyVM> PSVMs = null;
            if (Id != 0)
            {
                PSVMs = _iwonder.GetPowerSupplyProductsByPrice(_iwonder.PowerSuplyPaginations(PageNumber, PageSize), Id).ToList();
            }
            return Json(PSVMs);
        }
        [HttpGet]
        public JsonResult DefaultPowerSuply(int PageSize = 3)
        {
            HttpContext.Session.SetString("PageSize", PageSize.ToString());
            int PNumber = int.Parse(HttpContext.Session.GetString("PageNumber"));
            int SNumber = int.Parse(HttpContext.Session.GetString("PageSize"));
            IList<PowerSupplyVM> psVMs = _iwonder.PowerSuplyPaginations(PNumber, SNumber).ToList();
            return Json(psVMs);
        }
        [HttpGet]
        public JsonResult ProductsOfPowerSuplyBrand(string brand)
        {
            IList<PowerSupplyVM> PsVMs = null;
            if (!String.IsNullOrEmpty(brand))
            {
                int PNumber = int.Parse(HttpContext.Session.GetString("PageNumber")); // Session for PageNumber
                int SNumber = int.Parse(HttpContext.Session.GetString("PageSize")); // Session for PageSize
                PsVMs = _iwonder.GetPowerSupplyVMsProductsByBrand(brand, PNumber, SNumber).ToList();
            }
            return Json(PsVMs);
        }
        #endregion
        // End PowerSuply
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




