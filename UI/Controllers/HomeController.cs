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
using UI.Helper;

namespace UI.Controllers
{
    public class HomeController : Controller
    {
        readonly IWonder _iwonder;

        public HomeController(IWonder iwonder, WonderHardwareContext wonder)
        {
            _iwonder = iwonder;
            _wonder = wonder;
        }
        readonly WonderHardwareContext _wonder;


        //private readonly ISession session;
        //public HomeController(IHttpContextAccessor httpContextAccessor)
        //{
        //    this.session = httpContextAccessor.HttpContext.Session;
        //}

        public IActionResult Index()
        {
            int userid = HttpContext.Session.GetInt32("UserID").GetValueOrDefault();
            ViewBag.NewMotherBoards = _iwonder.GetNewMotherBoards();
            ViewBag.NewProcessors = _iwonder.GetNewProcessors();
            ViewBag.NewRam = _iwonder.GetNewRam();
            ViewBag.NewVGA = _iwonder.GetNewVGA();
            ViewBag.NewHDD = _iwonder.GetNewHDD();
            ViewBag.NewSSD = _iwonder.GetNewSSD();
            ViewBag.NewPSU = _iwonder.GetNewPSU();
            ViewBag.NewCase = _iwonder.GetNewCase(userid);
            ViewBag.TopMotherBoards = _iwonder.GetTopMothers();

            return View();
        }

        //public IActionResult GetNewProduct(string Product)
        //{
        //    if (Product == "M")
        //    {
        //        return Json(_iwonder.GetNewMotherBoards());
        //    }
        //    else if (Product == "Pro")
        //    {
        //        return Json(_iwonder.GetNewProcessors());
        //    }
        //    else if (Product == "R")
        //    {
        //        return Json(_iwonder.GetNewRam());
        //    }
        //    else if (Product == "GC")
        //    {
        //        return Json(_iwonder.GetNewVGA());
        //    }
        //    else if (Product == "HD")
        //    {
        //        return Json(_iwonder.GetNewHDD());
        //    }
        //    else if (Product == "SSD")
        //    {
        //        return Json(_iwonder.GetNewSSD());
        //    }
        //    else if (Product == "PS")
        //    {
        //        return Json(_iwonder.GetNewPSU());
        //    }
        //    else if (Product == "C")
        //    {
        //        return Json(_iwonder.GetNewCase());
        //    }
        //    else
        //    {
        //        return View();
        //    }
        //}



        #region  Processor

        [HttpGet]
        public IActionResult Processor(int PageNumber = 1, int PageSize = 3)
        {
            HttpContext.Session.SetString("PageSize", PageSize.ToString());
            HttpContext.Session.SetString("PageNumber", PageNumber.ToString());
            var PNumber = int.Parse(HttpContext.Session.GetString("PageNumber")); // Session for PageNumber
            var SNumber = int.Parse(HttpContext.Session.GetString("PageSize")); // Session for PageSize 
            var Data = Pagination.PagedResult(_iwonder.GetAllProcessors().ToList(), PNumber, SNumber);
            ViewBag.BrandNamesAndNumbers = _iwonder.GetProcessorBrandNamesAndNumbers(); // Get All Brands
            ViewData["PageSize"] = PageSize;
            return View(Data);
        }
        [HttpGet]
        public JsonResult ProcessorAjax(int PageNumber)
        {

            HttpContext.Session.SetString("PageNumber", PageNumber.ToString());
            var SNumber = int.Parse(HttpContext.Session.GetString("PageSize"));
            var PNumber = int.Parse(HttpContext.Session.GetString("PageNumber"));
            var Sort = HttpContext.Session.GetInt32("SortPro") ?? 0;
            var max = HttpContext.Session.GetInt32("Max") ?? 0;
            var min = HttpContext.Session.GetInt32("Min") ?? 0;
            var brand = HttpContext.Session.GetString("BrandsPro") ?? null;
            string[] brands = null;
            if (brand != null)
            {

                brands = brand.Split(',');

                bool IsTrue = brands.Length > 0 && brands[0] != "";
                if (IsTrue)
                {

                    var Data = _iwonder.GetProcessorProductsByBrand(brands, PNumber, SNumber, Sort, min, max);
                    return Json(Data);
                }
            }

            var result = Pagination.PagedResult(_iwonder.GetProcessorPriceDependentOnBrand(min, max, Sort).ToList(), PNumber, SNumber);



            return Json(result.Data);
        }
        [HttpGet]
        public JsonResult AscendingProcessorProdoucts(int Id)
        {
            int PageSize = int.Parse(HttpContext.Session.GetString("PageSize"));
            int PageNumber = int.Parse(HttpContext.Session.GetString("PageNumber"));
            var max = HttpContext.Session.GetInt32("Max") ?? 0;
            var min = HttpContext.Session.GetInt32("Min") ?? 0;
            if (Id != 0)
            {
                HttpContext.Session.SetInt32("SortPro", Id);
                if (HttpContext.Session.GetString("BrandsPro") != null)
                {
                    var brands = HttpContext.Session.GetString("BrandsPro").Split(',');
                    if (brands.Length > 0 && brands[0] != "")
                    {
                        var result = Pagination.PagedResult(_iwonder.GetProcessorProductsByBrand(brands, PageNumber, PageSize, Id, min, max).ToList(), PageNumber, PageSize);
                        return Json(result.Data);

                    }
                }
            }
            var processor = Pagination.PagedResult(_iwonder.GetProcessorDependentOnSort(Id).ToList(), PageNumber, PageSize);
            return Json(processor.Data);
        }
        [HttpGet]
        public JsonResult DefaultProcessor(int PageSize = 3)
        {
            HttpContext.Session.SetString("PageSize", PageSize.ToString());
            int PNumber = int.Parse(HttpContext.Session.GetString("PageNumber"));
            int SNumber = int.Parse(HttpContext.Session.GetString("PageSize"));
            var Sort = HttpContext.Session.GetInt32("SortPro") ?? 0;
            var max = HttpContext.Session.GetInt32("Max") ?? 0;
            var min = HttpContext.Session.GetInt32("Min") ?? 0;
            if (HttpContext.Session.GetString("BrandsPro") != null)
            {
                var brands = HttpContext.Session.GetString("BrandsPro").Split(',');
                if (brands.Length != 0 && brands[0] != "")
                {
                    var result = Pagination.PagedResult(_iwonder.GetProcessorProductsByBrand(brands, PNumber, SNumber, Sort, min, max).ToList(), PNumber, SNumber);
                    return Json(result.Data);

                }
            }
            var processorVMs = Pagination.PagedResult(_iwonder.GetProcessorDependentOnSort(Sort).ToList(), PNumber, PageSize);
            return Json(processorVMs.Data);
        }

        [HttpPost]
        public JsonResult ProductsOfProcessorBrand(string[] brand)
        {
            int PageSize = int.Parse(HttpContext.Session.GetString("PageSize"));
            int PageNumber = int.Parse(HttpContext.Session.GetString("PageNumber"));
            HttpContext.Session.SetString("BrandsPro", string.Join(",", brand));
            var Sort = HttpContext.Session.GetInt32("SortPro") ?? 0;
            var brands = HttpContext.Session.GetString("BrandsPro").Split(',');
            var max = HttpContext.Session.GetInt32("Max") ?? 0;
            var min = HttpContext.Session.GetInt32("Min") ?? 0;
            if (brands.Length <= 0 || brands[0] == "")
            {
                return Json(_iwonder.ProcessorPaginations(PageNumber, PageSize));
            }
            else
            {
                return Json(_iwonder.GetProcessorProductsByBrand(brands, PageNumber, PageSize, Sort, min, max));
            }

        }
        [HttpGet]
        public JsonResult GetProcessorPrice(int min, int max)
        {
            int PageSize = int.Parse(HttpContext.Session.GetString("PageSize"));
            int PageNumber = int.Parse(HttpContext.Session.GetString("PageNumber"));
            var IsNull = HttpContext.Session.GetString("BrandsPro") ?? null;
            var Sort = HttpContext.Session.GetInt32("SortPro") ?? 0;
            HttpContext.Session.SetInt32("Max", max);
            HttpContext.Session.SetInt32("Min", min);
            if ((IsNull == null || Sort <= 0))
            {
                return Json(_iwonder.ProcessorPrice(min, max, PageSize, PageNumber));
            }
            var brands = HttpContext.Session.GetString("BrandsPro").Split(',');
            var result = Pagination.PagedResult(_iwonder.GetProcessorProductsByBrand(brands, PageNumber, PageSize, Sort, min, max).ToList(), PageNumber, PageSize);



            return Json(result.Data);
        }
        #endregion

        #region Motherboard

        [HttpGet]
        public IActionResult Motherboard(int pageNumber = 1, int PageSize = 3)
        {

            HttpContext.Session.SetString("PageSize", PageSize.ToString());
            HttpContext.Session.SetString("PageNumber", pageNumber.ToString());
            var PNumber = int.Parse(HttpContext.Session.GetString("PageNumber")); // Session for PageNumber
            var SNumber = int.Parse(HttpContext.Session.GetString("PageSize")); // Session for PageSize 
            var Data = Pagination.PagedResult(_iwonder.GetAllMotherboard().ToList(), PNumber, SNumber);
            ViewBag.BrandNamesAndNumbers = _iwonder.GetMotherboardBrandNamesAndNumbers(); // Get All Brands
            ViewData["PageSize"] = PageSize;
            return View(Data);
        }
        [HttpGet]
        public JsonResult MotherboardAjax(int PageNumber)
        {

            HttpContext.Session.SetString("PageNumber", PageNumber.ToString());
            var SNumber = int.Parse(HttpContext.Session.GetString("PageSize"));
            var PNumber = int.Parse(HttpContext.Session.GetString("PageNumber"));
            var Sort = HttpContext.Session.GetInt32("SortMoth") ?? 0;
            var max = HttpContext.Session.GetInt32("MaxMoh") ?? 0;
            var min = HttpContext.Session.GetInt32("MinMoh") ?? 0;
            var brand = HttpContext.Session.GetString("MothPro") ?? null;
            string[] brands = null;
            if (brand != null)
            {

                brands = brand.Split(',');

                bool IsTrue = brands.Length > 0 && brands[0] != "";
                if (IsTrue)
                {

                    var Data = _iwonder.GetMotherboardProductsByBrand(brands, PNumber, SNumber, Sort, min, max);
                    return Json(Data);
                }
            }
            var result = Pagination.PagedResult(_iwonder.GetMotherboardPriceDependentOnBrand(min, max, Sort).ToList(), PNumber, SNumber);
            return Json(result.Data);
        }
        [HttpGet]
        public JsonResult AscendingMotherboardProdoucts(int Id)
        {
            int PageSize = int.Parse(HttpContext.Session.GetString("PageSize"));
            int PageNumber = int.Parse(HttpContext.Session.GetString("PageNumber"));
            if (Id != 0)
            {
                HttpContext.Session.SetInt32("SortMoth", Id);
                if (HttpContext.Session.GetString("MothPro") != null)
                {
                    var brands = HttpContext.Session.GetString("MothPro").Split(',');
                    if (brands.Length > 0 && brands[0] != "")
                    {
                        var result = Pagination.PagedResult(_iwonder.MotherboardPriceBrand(PageNumber, PageSize, Id, brands).ToList(), PageNumber, PageSize);
                        return Json(result.Data);

                    }
                }
            }
            var motherboards = Pagination.PagedResult(_iwonder.GetMotherboardDependentOnSort(Id).ToList(), PageNumber, PageSize);
            return Json(motherboards.Data);
        }

        [HttpGet]
        public JsonResult DefaultMotherboard(int PageSize = 3)
        {
            HttpContext.Session.SetString("PageSize", PageSize.ToString());
            int PNumber = int.Parse(HttpContext.Session.GetString("PageNumber"));
            int SNumber = int.Parse(HttpContext.Session.GetString("PageSize"));
            var Sort = HttpContext.Session.GetInt32("SortMoth") ?? 0;
            var max = HttpContext.Session.GetInt32("MaxMoh") ?? 0;
            var min = HttpContext.Session.GetInt32("MinMoh") ?? 0;
            if (HttpContext.Session.GetString("MothPro") != null)
            {
                var brands = HttpContext.Session.GetString("MothPro").Split(',');
                if (brands.Length > 0 && brands[0] != "")
                {
                    var result = Pagination.PagedResult(_iwonder.GetMotherboardProductsByBrand(brands, PNumber, SNumber, Sort, min, max).ToList(), PNumber, SNumber);
                    return Json(result.Data);

                }
            }
            var motherboards = Pagination.PagedResult(_iwonder.GetMotherboardDependentOnSort(Sort).ToList(), PNumber, PageSize);
            return Json(motherboards.Data);
        }

        [HttpPost]
        public JsonResult ProductsOfMotherboardBrand(string[] brand)
        {
            int PageSize = int.Parse(HttpContext.Session.GetString("PageSize"));
            int PageNumber = int.Parse(HttpContext.Session.GetString("PageNumber"));
            HttpContext.Session.SetString("MothPro", string.Join(",", brand));
            var Sort = HttpContext.Session.GetInt32("SortMoth") ?? 0;
            var brands = HttpContext.Session.GetString("MothPro").Split(',');
            var max = HttpContext.Session.GetInt32("MaxMoh") ?? 0;
            var min = HttpContext.Session.GetInt32("MinMoh") ?? 0;
            if (brands.Length <= 0 || brands[0] == "")
            {
                return Json(_iwonder.MotherboardPaginations(PageNumber, PageSize));
            }
            else
            {
                return Json(_iwonder.GetMotherboardProductsByBrand(brands, PageNumber, PageSize, Sort, min, max));
            }
        }
        [HttpGet]
        public JsonResult GetMotherboardPrice(int min, int max)
        {
            int PageSize = int.Parse(HttpContext.Session.GetString("PageSize"));
            int PageNumber = int.Parse(HttpContext.Session.GetString("PageNumber"));
            var IsNull = HttpContext.Session.GetString("MothPro") ?? null;
            var Sort = HttpContext.Session.GetInt32("SortMoth") ?? 0;
            HttpContext.Session.SetInt32("MaxMoh", max);
            HttpContext.Session.SetInt32("MinMoh", min);
            if ((IsNull == null && Sort <= 0))
            {
                return Json(_iwonder.MotherboardPrice(min, max, PageSize, PageNumber));
            }
            var brands = HttpContext.Session.GetString("MothPro").Split(',');
            var result = Pagination.PagedResult(_iwonder.GetMotherboardProductsByBrand(brands, PageNumber, PageSize, Sort, min, max).ToList(), PageNumber, PageSize);
            return Json(result.Data);
        }
        #endregion

        #region HDD

        [HttpGet]
        public IActionResult HDD(int pageNumber = 1, int PageSize = 3)
        {
            HttpContext.Session.SetString("PageSize", PageSize.ToString());
            HttpContext.Session.SetString("PageNumber", pageNumber.ToString());
            var PNumber = int.Parse(HttpContext.Session.GetString("PageNumber")); // Session for PageNumber
            var SNumber = int.Parse(HttpContext.Session.GetString("PageSize")); // Session for PageSize 
            var Data = Pagination.PagedResult(_iwonder.GetAllHDD().ToList(), PNumber, SNumber);
            ViewBag.BrandNamesAndNumbers = _iwonder.GetHDDBrandNamesAndNumbers(); // Get All Brands
            ViewData["PageSize"] = PageSize;
            return View(Data);
        }
        [HttpGet]
        public JsonResult HDDAjax(int PageNumber)
        {

            HttpContext.Session.SetString("PageNumber", PageNumber.ToString());
            var SNumber = int.Parse(HttpContext.Session.GetString("PageSize"));
            var PNumber = int.Parse(HttpContext.Session.GetString("PageNumber"));
            var Sort = HttpContext.Session.GetInt32("SortHdd") ?? 0;
            var max = HttpContext.Session.GetInt32("Maxhd") ?? 0;
            var min = HttpContext.Session.GetInt32("Minhd") ?? 0;
            var brand = HttpContext.Session.GetString("brandHdd") ?? null;
            string[] brands = null;
            if (brand != null)
            {

                brands = brand.Split(',');

                bool IsTrue = brands.Length > 0 && brands[0] != "";
                if (IsTrue)
                {

                    var Data = _iwonder.GetHDDProductsByBrand(brands, PageNumber, SNumber, Sort, min, max);
                    return Json(Data);
                }
            }
            var result = Pagination.PagedResult(_iwonder.GetHDDPriceDependentOnBrand(min, max, Sort).ToList(), PNumber, SNumber);
            return Json(result.Data);
        }
        [HttpGet]
        public JsonResult AscendingHDDProdoucts(int Id)
        {
            int PageSize = int.Parse(HttpContext.Session.GetString("PageSize"));
            int PageNumber = int.Parse(HttpContext.Session.GetString("PageNumber"));
            var max = HttpContext.Session.GetInt32("Maxhd") ?? 0;
            var min = HttpContext.Session.GetInt32("Minhd") ?? 0;
            if (Id != 0)
            {
                HttpContext.Session.SetInt32("SortHdd", Id);
                if (HttpContext.Session.GetString("brandHdd") != null)
                {
                    var brands = HttpContext.Session.GetString("brandHdd").Split(',');
                    if (brands.Length > 0 && brands[0] != "")
                    {
                        var result = Pagination.PagedResult(_iwonder.GetHDDProductsByBrand(brands, PageNumber, PageSize, Id, min, max).ToList(), PageNumber, PageSize);
                        return Json(result.Data);

                    }
                }
               
            }
            var motherboards = Pagination.PagedResult(_iwonder.GetHDDPriceDependentOnBrand(min, max, Id).ToList(), PageNumber, PageSize);
            return Json(motherboards.Data);
        }

        [HttpGet]
        public JsonResult DefaultHDD(int PageSize = 3)
        {
            HttpContext.Session.SetString("PageSize", PageSize.ToString());
            int PNumber = int.Parse(HttpContext.Session.GetString("PageNumber"));
            int SNumber = int.Parse(HttpContext.Session.GetString("PageSize"));
            var Sort = HttpContext.Session.GetInt32("SortHdd") ?? 0;
            var max = HttpContext.Session.GetInt32("Maxhd") ?? 0;
            var min = HttpContext.Session.GetInt32("Minhd") ?? 0;
            if (HttpContext.Session.GetString("brandHdd") != null)
            {
                var brands = HttpContext.Session.GetString("brandHdd").Split(',');
                if (brands.Length != 0 && brands[0] != "")
                {
                    var result = Pagination.PagedResult(_iwonder.GetHDDProductsByBrand(brands, PNumber, SNumber, Sort, min, max).ToList(), PNumber, SNumber);
                    return Json(result.Data);

                }
            }
            var processorVMs = Pagination.PagedResult(_iwonder.GetHDDDependentOnSort(Sort).ToList(), PNumber, PageSize);
            return Json(processorVMs.Data);
        }

        [HttpPost]
        public JsonResult ProductsOfHDDBrand(string[] brand)
        {
            int PageSize = int.Parse(HttpContext.Session.GetString("PageSize"));
            int PageNumber = int.Parse(HttpContext.Session.GetString("PageNumber"));
            HttpContext.Session.SetString("brandHdd", string.Join(",", brand));
            var Sort = HttpContext.Session.GetInt32("SortHdd") ?? 0;
            var brands = HttpContext.Session.GetString("brandHdd").Split(',');
            var max = HttpContext.Session.GetInt32("Maxhd") ?? 0;
            var min = HttpContext.Session.GetInt32("Minhd") ?? 0;
            if (brands.Length <= 0 || brands[0] == "")
            {
                var Data = Pagination.PagedResult(_iwonder.GetHDDPriceDependentOnBrand(min, max, Sort).ToList(), PageNumber, PageSize);
                return Json(Data.Data);
            }
            else
            {
                var Data = Pagination.PagedResult(_iwonder.GetHDDProductsByBrand(brands, PageNumber, PageSize, Sort, min, max).ToList(), PageNumber, PageSize);

                return Json(Data.Data);
            }
        }
        [HttpGet]
        public JsonResult GetHDDPrice(int min, int max)
        {
            int PageSize = int.Parse(HttpContext.Session.GetString("PageSize"));
            int PageNumber = int.Parse(HttpContext.Session.GetString("PageNumber"));
            var IsNull = HttpContext.Session.GetString("brandHdd") ?? null;
            var Sort = HttpContext.Session.GetInt32("SortHdd") ?? 0;
            HttpContext.Session.SetInt32("Maxhd", max);
            HttpContext.Session.SetInt32("Minhd", min);
            if ((IsNull == null && Sort <= 0))
            {
                var Data = Pagination.PagedResult(_iwonder.HDDPrice(min, max, PageSize, PageNumber).ToList(), PageNumber, PageSize);

                return Json(Data.Data);
            }
            var brands = HttpContext.Session.GetString("brandHdd").Split(',');
            var result = Pagination.PagedResult(_iwonder.GetHDDProductsByBrand(brands, PageNumber, PageSize,Sort, min, max).ToList(), PageNumber, PageSize);
            return Json(result.Data);
        }
        #endregion

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
                return Json(_iwonder.GetPowerSupplyVMsProductsByBrand(brand, PageNumber, PageSize));
            }
            else
            {
                return Json(_iwonder.PowerSuplyPaginations(PageNumber, PageSize));
            }
        }
        #endregion

        // End PowerSuply
        public IActionResult Cart()
        {
            return View();
        }

        #region WishList
        public IActionResult WishList()
        {
            var userid = HttpContext.Session.GetInt32("UserID").GetValueOrDefault();
            if (userid != 0)
            {
                return View("WishList", _iwonder.GetWishList(userid));
            }
            else
            {
                return RedirectToAction("Login_Register", new { wishlist = "wishlist" });
            }
        }
        public ActionResult WishListCounter()
        {
            var userid = HttpContext.Session.GetInt32("UserID").GetValueOrDefault();
            var wishlistCounter = _wonder.WishLists.Where(x => x.UserId == userid && x.IsAdded == true).Count();

            return Json(wishlistCounter);
        }
        public IActionResult AddToWL(string ProductCode)
        {
            var userid = HttpContext.Session.GetInt32("UserID").GetValueOrDefault();
            if (userid != 0)
            {
                return Json(_iwonder.AddToWL(ProductCode, userid));
            }
            else
            {
                return Json("LoginRegisterPopup");

            }
        }
        public IActionResult CheckWishList(string ProductCode)
        {
            var userid = HttpContext.Session.GetInt32("UserID").GetValueOrDefault();
            if (userid != 0)
            {
                return Json(_iwonder.CheckfromWL(ProductCode, userid));
            }
            else
            {
                return Json("no user");
            }
        }
        public IActionResult DeleteFromWL(string ProductCode)
        {
            var userid = HttpContext.Session.GetInt32("UserID").GetValueOrDefault();
            return Json(_iwonder.DeletefromWL(ProductCode, userid));
        }
        #endregion

        #region Checkout
        [HttpGet]
        public IActionResult CheckOut()
        {
            var userid = HttpContext.Session.GetInt32("UserID");
            ViewBag.UserPhone = _wonder.Users.Where(x => x.UserId == userid).Select(x => x.Phone).FirstOrDefault();
            ViewBag.UserAddress = _wonder.Sales.Where(x => x.UserId == userid).OrderByDescending(x => x.DateAndTime).Take(1).Select(x => x.Address).FirstOrDefault();
            return View();
        }

        public IActionResult CheckAccount(UserVM UserData)
        {
            var userid = _wonder.Users.Where(x => x.Phone == UserData.Telephone && x.IsAdmin == false).Select(x => x.UserId).FirstOrDefault();
            var pass = _wonder.Users.Where(x => x.Password == UserData.Password && x.Phone == UserData.Telephone && x.IsAdmin == false).Select(x => x.UserId).FirstOrDefault();
            if (userid == 0)
            {
                return Json("failed phone");
            }
            else if (pass == 0)
            {
                return Json("failed password");
            }
            else if (userid != 0 && pass != 0)
            {
                //اعمل ليست ابعت فيها الاسم والتليفون و الأدرس 
                IDictionary<string, string> userInfo = new Dictionary<string, string>();
                var address = _wonder.Sales.Where(x => x.UserId == userid).OrderByDescending(x => x.DateAndTime).Take(1).Select(x => x.Address).FirstOrDefault();
                if (address==null)
                {
                    userInfo.Add("Name", _wonder.Users.Where(x => x.Phone == UserData.Telephone && x.IsAdmin == false).Select(x => x.FirstName).FirstOrDefault()+" "+ _wonder.Users.Where(x => x.Phone == UserData.Telephone && x.IsAdmin == false).Select(x => x.LastName).FirstOrDefault());
                    userInfo.Add("Phone",Convert.ToString(_wonder.Users.Where(x => x.Phone == UserData.Telephone && x.IsAdmin == false).Select(x => x.Phone).FirstOrDefault()));
                    return Json(userInfo);
                }
                else
                {
                    userInfo.Add("Address", address);
                    userInfo.Add("Name", _wonder.Users.Where(x => x.Phone == UserData.Telephone && x.IsAdmin == false).Select(x => x.FirstName).FirstOrDefault() + " " + _wonder.Users.Where(x => x.Phone == UserData.Telephone && x.IsAdmin == false).Select(x => x.LastName).FirstOrDefault());
                    userInfo.Add("Phone", Convert.ToString(_wonder.Users.Where(x => x.Phone == UserData.Telephone && x.IsAdmin == false).Select(x => x.Phone).FirstOrDefault()));
                    return Json(userInfo);
                }
                
            }
            else
            {
                //Phone or password isn't correct or both !!
                return Json("failed phone&pass");
            }
        }
        [HttpPost]
        public JsonResult CheckOut(UserVM UserData, SalesVM[] OrderData)
        {
            string check;
            if (UserData.FName == null && UserData.LName == null && UserData.ID == 0 && UserData.IsAdmin == false && UserData.LatestBuyTime == null && UserData.NumberOfTimes == 0 && UserData.Password == null && UserData.Telephone == 0)
            {
                //already signed in (add address for the first time / update address or not)
                check = _iwonder.CheckOrder(OrderData);
            }
            else
            {
                if (UserData.FName != null)
                {
                    //create Acc + checkout
                    check = _iwonder.CheckOrderCreateAcc(UserData, OrderData);
                }
                else
                {
                    //sign in + checkout
                    check = _iwonder.CheckOrderSignIn(UserData, OrderData);
                }
                if (check== "success" || check== "success checked new address" || check == "success checked old address")
                {
                    var userid = _wonder.Users.Where(x => x.Phone == UserData.Telephone).Select(x => x.UserId).FirstOrDefault();
                    var name = _wonder.Users.Where(x => x.UserId == userid).Select(x => new { x.FirstName, x.LastName }).FirstOrDefault();
                    HttpContext.Session.SetInt32("UserID", userid);
                    HttpContext.Session.SetString("UserName", name.FirstName + " " + name.LastName);
                }
               
            }

            return Json(check);
        }
        #endregion

        #region Login & Logout & Register
        public ActionResult Login_Register(string wishlist)
        {
            if (wishlist == "wishlist")
            {
                ViewBag.Wishlist = "wishlist";
            }
            return View();
        }
        public ActionResult LogOut(int? UserID)
        {
            HttpContext.Session.Remove("UserID");
            return RedirectToAction("Index");
        }

        public ActionResult Login(UserVM user, string WishList)
        {

            if (_wonder.Users.Where(x => x.Phone == user.Telephone && x.IsAdmin == false).FirstOrDefault() != null)
            {
                if (_wonder.Users.Where(x => x.Phone == user.Telephone && x.Password == user.Password).FirstOrDefault() != null)
                {
                    var id = _wonder.Users.Where(x => x.Phone == user.Telephone).Select(x => x.UserId).FirstOrDefault();
                    var name = _wonder.Users.Where(x => x.UserId == id).Select(x => new { x.FirstName, x.LastName }).FirstOrDefault();
                    HttpContext.Session.SetInt32("UserID", id);
                    HttpContext.Session.SetString("UserName", name.FirstName + " " + name.LastName);
                    //Session.Timeout = 15;
                    if (WishList == "wishlist")
                    {
                        return Json("WishList");
                    }
                    else
                    {
                        return Json(id);
                    }
                }
                else
                {
                    return Json("wrong password");
                }
            }
            else
                return Json("this phone isn't exist");

        }

        public ActionResult Register(UserVM user, string WishList)
        {
            if (_wonder.Users.Select(x => x.Phone).Contains(user.Telephone))
            {
                return Json("this phone is already exist");
            }
            else
            {
                User Uobj = new User();
                Uobj.FirstName = user.FName;
                Uobj.LastName = user.LName;
                Uobj.Phone = user.Telephone;
                Uobj.Password = user.Password;
                _wonder.Users.Add(Uobj);
                _wonder.SaveChanges();
                var id = _wonder.Users.Where(x => x.Phone == user.Telephone).Select(x => x.UserId).FirstOrDefault();
                var name = _wonder.Users.Where(x => x.UserId == id).Select(x => new { x.FirstName, x.LastName }).FirstOrDefault();
                HttpContext.Session.SetInt32("UserID", id);
                HttpContext.Session.SetString("UserName", name.FirstName + " " + name.LastName);
                if (WishList == "wishlist")
                {
                    return RedirectToAction("WishList");
                }
                else
                {
                    return Json(id);
                }
            }

        }
        #endregion

        #region ProductDetails

        public IActionResult CaseDetails(string code, int currentPageIndex, int NextOrPreviousPage)
        {
            if (currentPageIndex == 0 && NextOrPreviousPage == 0)
            {
                ViewBag.Case = _iwonder.GetCaseExceptOne(code);
                return View(_iwonder.CaseDetails(code));
            }
            else if (currentPageIndex == 0)
            {
                return Json(_iwonder.CaseCommentsPagination(code, NextOrPreviousPage));
            }
            else
            {
                return Json(_iwonder.CaseCommentsPagination(code, currentPageIndex));
            }
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

        #endregion

        #region Search

        public IActionResult SearchPage(string src, int num)
        {
            List<Search> x = new List<Search>();
            ViewBag.searchWord = src;
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
            return View(x);
        }
        public IActionResult Search(string src, int num, string txt)
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

        #endregion

        public IActionResult Review(ReviewVM review)
        {
            ReviewVM result = _iwonder.AddReview(review);
            /////////////////////////////////
            return Json(result);
        }
    }
}