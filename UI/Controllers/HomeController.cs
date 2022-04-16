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
            int PageSize = int.Parse(HttpContext.Session.GetString("PageSize"));
            int PageNumber = int.Parse(HttpContext.Session.GetString("PageNumber"));
            IList<ProcessorVM> processor = null;
            if (Id != 0)
            {
                processor = _iwonder.GetProcessorProductsByPrice(_iwonder.ProcessorPaginations(PageNumber, PageSize), Id).ToList();
            }
            return Json(processor);
        }

        [HttpGet]
        public JsonResult DefaultProcessor(int PageSize = 3)
        {
            HttpContext.Session.SetString("PageSize", PageSize.ToString());
            int PNumber = int.Parse(HttpContext.Session.GetString("PageNumber"));
            int SNumber = int.Parse(HttpContext.Session.GetString("PageSize"));
            IList<ProcessorVM> processorVMs = _iwonder.ProcessorPaginations(PNumber, SNumber).ToList();
            return Json(processorVMs);
        }

        [HttpPost]
        public JsonResult ProductsOfProcessorBrand(string[] brand)
        {
            int PageSize = int.Parse(HttpContext.Session.GetString("PageSize"));
            int PageNumber = int.Parse(HttpContext.Session.GetString("PageNumber"));
            if (!(brand.Length == 0))
            {
                return Json(_iwonder.GetProcessorProductsByBrand(brand, PageNumber, PageSize));
            }
            else
            {
                return Json(_iwonder.ProcessorPaginations(PageNumber, PageSize));
            }
        }

        [HttpGet]
        public JsonResult GetProcessorPrice(int min, int max)
        {
            int PageSize = int.Parse(HttpContext.Session.GetString("PageSize"));
            int PageNumber = int.Parse(HttpContext.Session.GetString("PageNumber"));
            return Json(_iwonder.ProcessorPrice(min, max, PageSize, PageNumber));
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

        [HttpPost]
        public JsonResult ProductsOfMotherboardBrand(string[] brand)
        {
            int PageSize = int.Parse(HttpContext.Session.GetString("PageSize"));
            int PageNumber = int.Parse(HttpContext.Session.GetString("PageNumber"));
            if (!(brand.Length == 0))
            {
                return Json(_iwonder.GetMotherboardProductsByBrand(brand, PageNumber, PageSize));
            }
            else
            {
                return Json(_iwonder.MotherboardPaginations(PageNumber, PageSize));
            }
        }

        [HttpGet]
        public JsonResult GetMotherboardPrice(int min, int max)
        {
            int PageSize = int.Parse(HttpContext.Session.GetString("PageSize"));
            int PageNumber = int.Parse(HttpContext.Session.GetString("PageNumber"));
            return Json(_iwonder.MotherboardPrice(min, max, PageSize, PageNumber));
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

        [HttpPost]
        public JsonResult ProductsOfHDDBrand(string[] brand)
        {
            int PageSize = int.Parse(HttpContext.Session.GetString("PageSize"));
            int PageNumber = int.Parse(HttpContext.Session.GetString("PageNumber"));
            if (!(brand.Length == 0))
            {
                return Json(_iwonder.GetHDDProductsByBrand(brand, PageNumber, PageSize));
            }
            else
            {
                return Json(_iwonder.HDDPaginations(PageNumber, PageSize));
            }
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
            IList<RamVM> RamVMs = _iwonder.RAMPaginations(PNumber, SNumber).ToList(); // Get Records
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

        [HttpPost]
        public JsonResult ProductsOfRAMBrand(string[] brand)
        {
            int PageSize = int.Parse(HttpContext.Session.GetString("PageSize"));
            int PageNumber = int.Parse(HttpContext.Session.GetString("PageNumber"));
            if (!(brand.Length == 0))
            {
                return Json(_iwonder.GetRAMProductsByBrand(brand, PageNumber, PageSize));
            }
            else
            {
                return Json(_iwonder.RAMPaginations(PageNumber, PageSize));
            }
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

        [HttpPost]
        public JsonResult ProductsOfSSDBrand(string[] brand)
        {
            int PageSize = int.Parse(HttpContext.Session.GetString("PageSize"));
            int PageNumber = int.Parse(HttpContext.Session.GetString("PageNumber"));
            if (!(brand.Length == 0))
            {
                return Json(_iwonder.GetSSDProductsByBrand(brand, PageNumber, PageSize));
            }
            else
            {
                return Json(_iwonder.SSDPaginations(PageNumber, PageSize));
            }
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

        [HttpPost]
        public JsonResult ProductsOfCardBrand(string[] brand)
        {
            int PageSize = int.Parse(HttpContext.Session.GetString("PageSize"));
            int PageNumber = int.Parse(HttpContext.Session.GetString("PageNumber"));
            if (!(brand.Length == 0))
            {
                return Json(_iwonder.GetCardProductsByBrand(brand, PageNumber, PageSize));
            }
            else
            {
                return Json(_iwonder.CardPaginations(PageNumber, PageSize));
            }
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
                TotalItems = _iwonder.GetAllCase().Count(),
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

        [HttpPost]
        public JsonResult ProductsOfCaseBrand(string[] brand)
        {
            int PageSize = int.Parse(HttpContext.Session.GetString("PageSize"));
            int PageNumber = int.Parse(HttpContext.Session.GetString("PageNumber"));
            if (!(brand.Length == 0))
            {
                return Json(_iwonder.GetCaseProductsByBrand(brand, PageNumber, PageSize));
            }
            else
            {
                return Json(_iwonder.CasePaginations(PageNumber, PageSize));
            }
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
                TotalItems = _iwonder.GetAllPowerSuply().Count(),
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
        public JsonResult ProductsOfPowerSuplyBrand(string[] brand)
        {
            int PageSize = int.Parse(HttpContext.Session.GetString("PageSize"));
            int PageNumber = int.Parse(HttpContext.Session.GetString("PageNumber"));
            if (!(brand.Length == 0))
            {
                return Json(_iwonder.GetProcessorProductsByBrand(brand, PageNumber, PageSize));
            }
            else
            {
                return Json(_iwonder.ProcessorPaginations(PageNumber, PageSize));
            }
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
        public JsonResult CheckOut(SalesVM[] checkOut)
        {
            string check = "dds";
            //if (checkOut.FName != null)
            //{
            //    //create Acc + checkout
            //    check = _iwonder.CheckOrderCreateAcc(checkOut, Sales);
            //}
            //else
            //{
            //    check = _iwonder.CheckOrder(checkOut, Sales);
            //}

            return Json(check);
        }

        public IActionResult CaseDetails(string code)
        {
            ViewBag.Cases = _iwonder.GetCaseExceptOne(code);
            return View(_iwonder.CaseDetails(code));
        }

        public IActionResult GraphicsCardDetails(string code)
        {
            ViewBag.GraphicsCard = _iwonder.GetVGAExceptOne(code);
            return View(_iwonder.GraphicsCardDetails(code));
        }

        public IActionResult HddDetails(string code)
        {
            ViewBag.Hdd = _iwonder.GetHDDExceptOne(code);
            return View(_iwonder.HddDetails(code));
        }

        public IActionResult MotherboardDetails(string code)
        {
            ViewBag.Motherboard = _iwonder.GetMotherBoardsExceptOne(code);
            return View(_iwonder.MotherboardDetails(code));
        }

        public IActionResult PowerSupplyDetails(string code)
        {
            ViewBag.PowerSupply = _iwonder.GetPSUExceptOne(code);
            return View(_iwonder.PowerSupplyDetails(code));
        }

        public IActionResult ProcessorDetails(string code)
        {
            ViewBag.Processor = _iwonder.GetProcessorsExceptOne(code);
            return View(_iwonder.ProcessorDetails(code));
        }

        public IActionResult RamDetails(string code)
        {
            ViewBag.Ram = _iwonder.GetRamExceptOne(code);
            return View(_iwonder.RamDetails(code));
        }

        public IActionResult SsdDetails(string code)
        {
            ViewBag.Ssd = _iwonder.GetSSDExceptOne(code);
            return View(_iwonder.SsdDetails(code));
        }

        public IActionResult Search(string src, int num)
        {
            List<Search> x = new List<Search>();
            if (num == 0)
            {
                x = _iwonder.SearchProduct(src);
            }
            else if (num == 1)
            {
                x = _iwonder.SearchMotherBoard(src);
            }
            else if (num == 2)
            {
                x = _iwonder.SearchProcessor(src);
            }
            else if (num == 3)
            {
                x = _iwonder.SearchRam(src);
            }
            else if (num == 4)
            {
                x = _iwonder.SearchSSD(src);
            }
            else if (num == 5)
            {
                x = _iwonder.SearchHDD(src);
            }
            else if (num == 6)
            {
                x = _iwonder.SearchCase(src);
            }
            else if (num == 7)
            {
                x = _iwonder.SearchPowerSupply(src);
            }
            else if (num == 8)
            {
                x = _iwonder.SearchVGA(src);
            }
            return Json(x);
        }
    }
}