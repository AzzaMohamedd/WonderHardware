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
            int Uid = HttpContext.Session.GetInt32("UserID").GetValueOrDefault();
            #region NewProducts
            ViewBag.NewMotherBoards = _iwonder.GetNewMotherBoards(userid: Uid);
            ViewBag.NewProcessors = _iwonder.GetNewProcessors(userid: Uid);
            ViewBag.NewRam = _iwonder.GetNewRam(userid: Uid);
            ViewBag.NewVGA = _iwonder.GetNewVGA(userid: Uid);
            ViewBag.NewHDD = _iwonder.GetNewHDD(userid: Uid);
            ViewBag.NewSSD = _iwonder.GetNewSSD(userid: Uid);
            ViewBag.NewPSU = _iwonder.GetNewPSU(userid: Uid);
            ViewBag.NewCase = _iwonder.GetNewCase(userid: Uid);
            #endregion

            #region TopSelling
            ViewBag.GetTopCases = _iwonder.GetTopCases(userid: Uid);
            ViewBag.GetTopVgas = _iwonder.GetTopVgas(userid: Uid);
            ViewBag.GetTopHdds = _iwonder.GetTopHdds(userid: Uid);
            ViewBag.GetTopMotherboards = _iwonder.GetTopMotherboards(userid: Uid);
            ViewBag.GetTopPsus = _iwonder.GetTopPsus(userid: Uid);
            ViewBag.GetTopProcessors = _iwonder.GetTopProcessors(userid: Uid);
            ViewBag.GetTopRams = _iwonder.GetTopRams(userid: Uid);
            ViewBag.GetTopSsds = _iwonder.GetTopSsds(userid: Uid);
            #endregion

            return View();
        }

        #region  Processor

        [HttpGet]
        public IActionResult Processor(int PageNumber = 1, int PageSize = 3)
        {
            int Uid = HttpContext.Session.GetInt32("UserID").GetValueOrDefault();
            HttpContext.Session.SetString("PageSize", PageSize.ToString());
            HttpContext.Session.SetString("PageNumber", PageNumber.ToString());
            var PNumber = int.Parse(HttpContext.Session.GetString("PageNumber")); // Session for PageNumber
            var SNumber = int.Parse(HttpContext.Session.GetString("PageSize")); // Session for PageSize 
            var Data = Pagination.PagedResult(_iwonder.GetAllProcessors(userid: Uid).ToList(), PNumber, SNumber);
            ViewBag.BrandNamesAndNumbers = _iwonder.GetProcessorBrandNamesAndNumbers(); // Get All Brands
            ViewData["PageSize"] = PageSize;
            return View(Data);
        }
        [HttpGet]
        public JsonResult ProcessorAjax(int PageNumber)
        {
            int Uid = HttpContext.Session.GetInt32("UserID").GetValueOrDefault();
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
                    var processor = Pagination.PagedResult(_iwonder.GetProcessorProductsByBrand(brands, PNumber, SNumber, Sort, min, max,Uid).ToList(), PageNumber, SNumber);
                    return Json(processor);
                }
            }

            var result = Pagination.PagedResult(_iwonder.GetProcessorPriceDependentOnBrand(min, max, Sort,Uid).ToList(), PNumber, SNumber);
            return Json(result);
        }
        [HttpGet]
        public JsonResult AscendingProcessorProdoucts(int Id)
        {
            int Uid = HttpContext.Session.GetInt32("UserID").GetValueOrDefault();
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
                        var result = Pagination.PagedResult(_iwonder.GetProcessorProductsByBrand(brands, PageNumber, PageSize, Id, min, max,Uid).ToList(), PageNumber, PageSize);
                        return Json(result.Data);

                    }
                }
            }
            var processor = Pagination.PagedResult(_iwonder.GetProcessorDependentOnSort(Id,Uid).ToList(), PageNumber, PageSize);
            return Json(processor.Data);
        }
        [HttpGet]
        public JsonResult DefaultProcessor(int PageSize = 3)
        {
            int Uid = HttpContext.Session.GetInt32("UserID").GetValueOrDefault();
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
                    var result = Pagination.PagedResult(_iwonder.GetProcessorProductsByBrand(brands, PNumber, SNumber, Sort, min, max,Uid).ToList(), PNumber, SNumber);
                    return Json(result);

                }
            }
            var processorVMs = Pagination.PagedResult(_iwonder.GetProcessorDependentOnSort(Sort,Uid).ToList(), PNumber, PageSize);
            return Json(processorVMs);
        }

        [HttpPost]
        public JsonResult ProductsOfProcessorBrand(string[] brand)
        {
            int Uid = HttpContext.Session.GetInt32("UserID").GetValueOrDefault();
            int PageSize = int.Parse(HttpContext.Session.GetString("PageSize"));
            int PageNumber = int.Parse(HttpContext.Session.GetString("PageNumber"));
            HttpContext.Session.SetString("BrandsPro", string.Join(",", brand));
            var Sort = HttpContext.Session.GetInt32("SortPro") ?? 0;
            var brands = HttpContext.Session.GetString("BrandsPro").Split(',');
            var max = HttpContext.Session.GetInt32("Max") ?? 0;
            var min = HttpContext.Session.GetInt32("Min") ?? 0;
            if (brands.Length <= 0 || brands[0] == "")
            {
                var processor = Pagination.PagedResult(_iwonder.ProcessorPaginations(PageNumber, PageSize, Uid).ToList(), PageNumber, PageSize);
                return Json(processor);
               
            }
            else
            {
                var processor = Pagination.PagedResult(_iwonder.GetProcessorProductsByBrand(brands, PageNumber, PageSize, Sort, min, max, Uid).ToList(), PageNumber, PageSize);
                return Json(processor);
               
            }

        }
        [HttpGet]
        public JsonResult GetProcessorPrice(int min, int max)
        {
            int Uid = HttpContext.Session.GetInt32("UserID").GetValueOrDefault();
            int PageSize = int.Parse(HttpContext.Session.GetString("PageSize"));
            int PageNumber = int.Parse(HttpContext.Session.GetString("PageNumber"));
            var IsNull = HttpContext.Session.GetString("BrandsPro") ?? null;
            var Sort = HttpContext.Session.GetInt32("SortPro") ?? 0;
            HttpContext.Session.SetInt32("Max", max);
            HttpContext.Session.SetInt32("Min", min);
            if ((IsNull == null || Sort <= 0))
            {
                var processor = Pagination.PagedResult(_iwonder.ProcessorPrice(min, max, PageSize, PageNumber,Uid).ToList(), PageNumber, PageSize);
                return Json(processor);
            }
            var brands = HttpContext.Session.GetString("BrandsPro").Split(',');
            var result = Pagination.PagedResult(_iwonder.GetProcessorProductsByBrand(brands, PageNumber, PageSize, Sort, min, max,Uid).ToList(), PageNumber, PageSize);



            return Json(result);
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
            var result = Pagination.PagedResult(_iwonder.GetHDDProductsByBrand(brands, PageNumber, PageSize, Sort, min, max).ToList(), PageNumber, PageSize);
            return Json(result.Data);
        }
        #endregion

        #region RAM

        [HttpGet]
        public IActionResult RAM(int pageNumber = 1, int PageSize = 3)
        {
            HttpContext.Session.SetString("PageSize", PageSize.ToString());
            HttpContext.Session.SetString("PageNumber", pageNumber.ToString());
            var PNumber = int.Parse(HttpContext.Session.GetString("PageNumber")); // Session for PageNumber
            var SNumber = int.Parse(HttpContext.Session.GetString("PageSize")); // Session for PageSize 
            var Data = Pagination.PagedResult(_iwonder.GetAllRAM().ToList(), PNumber, SNumber);
            ViewBag.BrandNamesAndNumbers = _iwonder.GetRAMBrandNamesAndNumbers(); // Get All Brands
            ViewData["PageSize"] = PageSize;
            return View(Data);
        }


        [HttpGet]
        public JsonResult RAMAjax(int PageNumber)
        {

            HttpContext.Session.SetString("PageNumber", PageNumber.ToString());
            var SNumber = int.Parse(HttpContext.Session.GetString("PageSize"));
            var PNumber = int.Parse(HttpContext.Session.GetString("PageNumber"));
            var Sort = HttpContext.Session.GetInt32("SortRam") ?? 0;
            var max = HttpContext.Session.GetInt32("Maxram") ?? 0;
            var min = HttpContext.Session.GetInt32("Minram") ?? 0;
            var brand = HttpContext.Session.GetString("brandram") ?? null;
            string[] brands = null;
            if (brand != null)
            {

                brands = brand.Split(',');

                bool IsTrue = brands.Length > 0 && brands[0] != "";
                if (IsTrue)
                {

                    var Data = _iwonder.GetRAMProductsByBrand(brands, PageNumber, SNumber, Sort, min, max);
                    return Json(Data);
                }
            }
            var result = Pagination.PagedResult(_iwonder.GetRAMPriceDependentOnBrand(min, max, Sort).ToList(), PNumber, SNumber);
            return Json(result.Data);
        }

        [HttpGet]
        public JsonResult AscendingRAMProdoucts(int Id)
        {
            int PageSize = int.Parse(HttpContext.Session.GetString("PageSize"));
            int PageNumber = int.Parse(HttpContext.Session.GetString("PageNumber"));
            var max = HttpContext.Session.GetInt32("Maxram") ?? 0;
            var min = HttpContext.Session.GetInt32("Minram") ?? 0;
            if (Id != 0)
            {
                HttpContext.Session.SetInt32("SortRam", Id);
                if (HttpContext.Session.GetString("brandram") != null)
                {
                    var brands = HttpContext.Session.GetString("brandram").Split(',');
                    if (brands.Length > 0 && brands[0] != "")
                    {
                        var result = Pagination.PagedResult(_iwonder.GetRAMProductsByBrand(brands, PageNumber, PageSize, Id, min, max).ToList(), PageNumber, PageSize);
                        return Json(result.Data);

                    }
                }

            }
            var rams = Pagination.PagedResult(_iwonder.GetRAMPriceDependentOnBrand(min, max, Id).ToList(), PageNumber, PageSize);
            return Json(rams.Data);
        }

        [HttpGet]
        public JsonResult DefaultRAM(int PageSize = 3)
        {

            HttpContext.Session.SetString("PageSize", PageSize.ToString());
            int PNumber = int.Parse(HttpContext.Session.GetString("PageNumber"));
            int SNumber = int.Parse(HttpContext.Session.GetString("PageSize"));
            var Sort = HttpContext.Session.GetInt32("SortRam") ?? 0;
            var max = HttpContext.Session.GetInt32("Maxram") ?? 0;
            var min = HttpContext.Session.GetInt32("Minram") ?? 0;
            if (HttpContext.Session.GetString("brandram") != null)
            {
                var brands = HttpContext.Session.GetString("brandram").Split(',');
                if (brands.Length != 0 && brands[0] != "")
                {
                    var result = Pagination.PagedResult(_iwonder.GetRAMProductsByBrand(brands, PNumber, SNumber, Sort, min, max).ToList(), PNumber, SNumber);
                    return Json(result.Data);

                }
            }
            var rams = Pagination.PagedResult(_iwonder.GetRAMDependentOnSort(Sort).ToList(), PNumber, PageSize);
            return Json(rams.Data);
        }

        [HttpPost]
        public JsonResult ProductsOfRAMBrand(string[] brand)
        {
            int PageSize = int.Parse(HttpContext.Session.GetString("PageSize"));
            int PageNumber = int.Parse(HttpContext.Session.GetString("PageNumber"));
            HttpContext.Session.SetString("brandram", string.Join(",", brand));
            var Sort = HttpContext.Session.GetInt32("SortRam") ?? 0;
            var brands = HttpContext.Session.GetString("brandram").Split(',');
            var max = HttpContext.Session.GetInt32("Maxram") ?? 0;
            var min = HttpContext.Session.GetInt32("Minram") ?? 0;
            if (brands.Length <= 0 || brands[0] == "")
            {
                var Data = Pagination.PagedResult(_iwonder.GetRAMPriceDependentOnBrand(min, max, Sort).ToList(), PageNumber, PageSize);
                return Json(Data.Data);
            }
            else
            {
                var Data = Pagination.PagedResult(_iwonder.GetRAMProductsByBrand(brands, PageNumber, PageSize, Sort, min, max).ToList(), PageNumber, PageSize);

                return Json(Data.Data);
            }
        }
        [HttpGet]
        public JsonResult GetRAMPrice(int min, int max)
        {
            int PageSize = int.Parse(HttpContext.Session.GetString("PageSize"));
            int PageNumber = int.Parse(HttpContext.Session.GetString("PageNumber"));
            var IsNull = HttpContext.Session.GetString("brandram") ?? null;
            var Sort = HttpContext.Session.GetInt32("SortRam") ?? 0;
            HttpContext.Session.SetInt32("Maxram", max);
            HttpContext.Session.SetInt32("Minram", min);
            if ((IsNull == null && Sort <= 0))
            {
                var Data = Pagination.PagedResult(_iwonder.RAMPrice(min, max, PageSize, PageNumber).ToList(), PageNumber, PageSize);

                return Json(Data.Data);
            }
            var brands = HttpContext.Session.GetString("brandram").Split(',');
            var result = Pagination.PagedResult(_iwonder.GetRAMProductsByBrand(brands, PageNumber, PageSize, Sort, min, max).ToList(), PageNumber, PageSize);
            return Json(result.Data);
        }
        #endregion

        #region Graphics Card

        [HttpGet]
        public IActionResult GraphicsCard(int pageNumber = 1, int PageSize = 3)
        {
            HttpContext.Session.SetString("PageSize", PageSize.ToString());
            HttpContext.Session.SetString("PageNumber", pageNumber.ToString());
            var PNumber = int.Parse(HttpContext.Session.GetString("PageNumber")); // Session for PageNumber
            var SNumber = int.Parse(HttpContext.Session.GetString("PageSize")); // Session for PageSize 
            var Data = Pagination.PagedResult(_iwonder.GetAllCard().ToList(), PNumber, SNumber);
            ViewBag.BrandNamesAndNumbers = _iwonder.GetCardBrandNamesAndNumbers(); // Get All Brands
            ViewData["PageSize"] = PageSize;
            return View(Data);
        }


        [HttpGet]
        public JsonResult CardAjax(int PageNumber)
        {

            HttpContext.Session.SetString("PageNumber", PageNumber.ToString());
            var SNumber = int.Parse(HttpContext.Session.GetString("PageSize"));
            var PNumber = int.Parse(HttpContext.Session.GetString("PageNumber"));
            var Sort = HttpContext.Session.GetInt32("Sortcard") ?? 0;
            var max = HttpContext.Session.GetInt32("Maxcard") ?? 0;
            var min = HttpContext.Session.GetInt32("Mincard") ?? 0;
            var brand = HttpContext.Session.GetString("brandcard") ?? null;
            string[] brands = null;
            if (brand != null)
            {

                brands = brand.Split(',');

                bool IsTrue = brands.Length > 0 && brands[0] != "";
                if (IsTrue)
                {

                    var Data = _iwonder.GetCardProductsByBrand(brands, PageNumber, SNumber, Sort, min, max);
                    return Json(Data);
                }
            }
            var result = Pagination.PagedResult(_iwonder.GetCardPriceDependentOnBrand(min, max, Sort).ToList(), PNumber, SNumber);
            return Json(result.Data);
        }

        [HttpGet]
        public JsonResult AscendingCardProdoucts(int Id)
        {
            int PageSize = int.Parse(HttpContext.Session.GetString("PageSize"));
            int PageNumber = int.Parse(HttpContext.Session.GetString("PageNumber"));
            var max = HttpContext.Session.GetInt32("Maxcard") ?? 0;
            var min = HttpContext.Session.GetInt32("Mincard") ?? 0;
            if (Id != 0)
            {
                HttpContext.Session.SetInt32("Sortcard", Id);
                if (HttpContext.Session.GetString("brandcard") != null)
                {
                    var brands = HttpContext.Session.GetString("brandcard").Split(',');
                    if (brands.Length > 0 && brands[0] != "")
                    {
                        var result = Pagination.PagedResult(_iwonder.GetCardProductsByBrand(brands, PageNumber, PageSize, Id, min, max).ToList(), PageNumber, PageSize);
                        return Json(result.Data);

                    }
                }

            }
            var motherboards = Pagination.PagedResult(_iwonder.GetCardPriceDependentOnBrand(min, max, Id).ToList(), PageNumber, PageSize);
            return Json(motherboards.Data);
        }

        [HttpGet]
        public JsonResult DefaultCard(int PageSize = 3)
        {
            HttpContext.Session.SetString("PageSize", PageSize.ToString());
            int PNumber = int.Parse(HttpContext.Session.GetString("PageNumber"));
            int SNumber = int.Parse(HttpContext.Session.GetString("PageSize"));
            var Sort = HttpContext.Session.GetInt32("Sortcard") ?? 0;
            var max = HttpContext.Session.GetInt32("Maxcard") ?? 0;
            var min = HttpContext.Session.GetInt32("Mincard") ?? 0;
            if (HttpContext.Session.GetString("brandcard") != null)
            {
                var brands = HttpContext.Session.GetString("brandcard").Split(',');
                if (brands.Length != 0 && brands[0] != "")
                {
                    var result = Pagination.PagedResult(_iwonder.GetCardProductsByBrand(brands, PNumber, SNumber, Sort, min, max).ToList(), PNumber, SNumber);
                    return Json(result.Data);

                }
            }
            var processorVMs = Pagination.PagedResult(_iwonder.GetCardDependentOnSort(Sort).ToList(), PNumber, PageSize);
            return Json(processorVMs.Data);
        }

        [HttpPost]
        public JsonResult ProductsOfCardBrand(string[] brand)
        {
            int PageSize = int.Parse(HttpContext.Session.GetString("PageSize"));
            int PageNumber = int.Parse(HttpContext.Session.GetString("PageNumber"));
            HttpContext.Session.SetString("brandcard", string.Join(",", brand));
            var Sort = HttpContext.Session.GetInt32("Sortcard") ?? 0;
            var brands = HttpContext.Session.GetString("brandcard").Split(',');
            var max = HttpContext.Session.GetInt32("Maxcard") ?? 0;
            var min = HttpContext.Session.GetInt32("Mincard") ?? 0;
            if (brands.Length <= 0 || brands[0] == "")
            {
                var Data = Pagination.PagedResult(_iwonder.GetCardPriceDependentOnBrand(min, max, Sort).ToList(), PageNumber, PageSize);
                return Json(Data.Data);
            }
            else
            {
                var Data = Pagination.PagedResult(_iwonder.GetCardProductsByBrand(brands, PageNumber, PageSize, Sort, min, max).ToList(), PageNumber, PageSize);

                return Json(Data.Data);
            }
        }
        [HttpGet]
        public JsonResult GetCardPrice(int min, int max)
        {
            int PageSize = int.Parse(HttpContext.Session.GetString("PageSize"));
            int PageNumber = int.Parse(HttpContext.Session.GetString("PageNumber"));
            var IsNull = HttpContext.Session.GetString("brandcard") ?? null;
            var Sort = HttpContext.Session.GetInt32("Sortcard") ?? 0;
            HttpContext.Session.SetInt32("Maxcard", max);
            HttpContext.Session.SetInt32("Mincard", min);
            if (IsNull == null)
            {
                if (Sort == 0)
                {
                    var Data = Pagination.PagedResult(_iwonder.CardPrice(min, max, PageSize, PageNumber).ToList(), PageNumber, PageSize);

                    return Json(Data.Data);
                }
                else
                {

                    var Data = Pagination.PagedResult(_iwonder.GetCardPriceDependentOnBrand(min, max, Sort).ToList(), PageNumber, PageSize);
                    return Json(Data.Data);
                }
            }
            var brands = HttpContext.Session.GetString("brandcard").Split(',');
            var result = Pagination.PagedResult(_iwonder.GetCardProductsByBrand(brands, PageNumber, PageSize, Sort, min, max).ToList(), PageNumber, PageSize);
            return Json(result.Data);
        }
        #endregion

        #region SSD
        [HttpGet]
        public IActionResult SSD(int pageNumber = 1, int PageSize = 3)
        {
            HttpContext.Session.SetString("PageSize", PageSize.ToString());
            HttpContext.Session.SetString("PageNumber", pageNumber.ToString());
            var PNumber = int.Parse(HttpContext.Session.GetString("PageNumber")); // Session for PageNumber
            var SNumber = int.Parse(HttpContext.Session.GetString("PageSize")); // Session for PageSize 
            var Data = Pagination.PagedResult(_iwonder.GetAllSSD().ToList(), PNumber, SNumber);
            ViewBag.BrandNamesAndNumbers = _iwonder.GetSSDBrandNamesAndNumbers(); // Get All Brands
            ViewData["PageSize"] = PageSize;
            return View(Data);
        }
        [HttpGet]
        public JsonResult SSDAjax(int PageNumber)
        {

            HttpContext.Session.SetString("PageNumber", PageNumber.ToString());
            var SNumber = int.Parse(HttpContext.Session.GetString("PageSize"));
            var PNumber = int.Parse(HttpContext.Session.GetString("PageNumber"));
            var Sort = HttpContext.Session.GetInt32("Sortssd") ?? 0;
            var max = HttpContext.Session.GetInt32("Maxssd") ?? 0;
            var min = HttpContext.Session.GetInt32("Minssd") ?? 0;
            var brand = HttpContext.Session.GetString("brandssd") ?? null;
            string[] brands = null;
            if (brand != null)
            {

                brands = brand.Split(',');

                bool IsTrue = brands.Length > 0 && brands[0] != "";
                if (IsTrue)
                {

                    var Data = _iwonder.GetSSDProductsByBrand(brands, PageNumber, SNumber, Sort, min, max);
                    return Json(Data);
                }
            }
            var result = Pagination.PagedResult(_iwonder.GetSSDPriceDependentOnBrand(min, max, Sort).ToList(), PNumber, SNumber);
            return Json(result.Data);
        }

        [HttpGet]
        public JsonResult AscendingSSDProdoucts(int Id)
        {
            int PageSize = int.Parse(HttpContext.Session.GetString("PageSize"));
            int PageNumber = int.Parse(HttpContext.Session.GetString("PageNumber"));
            var max = HttpContext.Session.GetInt32("Maxssd") ?? 0;
            var min = HttpContext.Session.GetInt32("Minssd") ?? 0;
            if (Id != 0)
            {
                HttpContext.Session.SetInt32("Sortssd", Id);
                if (HttpContext.Session.GetString("brandssd") != null)
                {
                    var brands = HttpContext.Session.GetString("brandssd").Split(',');
                    if (brands.Length > 0 && brands[0] != "")
                    {
                        var result = Pagination.PagedResult(_iwonder.GetSSDProductsByBrand(brands, PageNumber, PageSize, Id, min, max).ToList(), PageNumber, PageSize);
                        return Json(result.Data);

                    }
                }

            }
            var motherboards = Pagination.PagedResult(_iwonder.GetSSDPriceDependentOnBrand(min, max, Id).ToList(), PageNumber, PageSize);
            return Json(motherboards.Data);
        }

        [HttpGet]
        public JsonResult DefaultSSD(int PageSize = 3)
        {
            HttpContext.Session.SetString("PageSize", PageSize.ToString());
            int PNumber = int.Parse(HttpContext.Session.GetString("PageNumber"));
            int SNumber = int.Parse(HttpContext.Session.GetString("PageSize"));
            var Sort = HttpContext.Session.GetInt32("Sortssd") ?? 0;
            var max = HttpContext.Session.GetInt32("Maxssd") ?? 0;
            var min = HttpContext.Session.GetInt32("Minssd") ?? 0;
            if (HttpContext.Session.GetString("brandssd") != null)
            {
                var brands = HttpContext.Session.GetString("brandssd").Split(',');
                if (brands.Length != 0 && brands[0] != "")
                {
                    var result = Pagination.PagedResult(_iwonder.GetSSDProductsByBrand(brands, PNumber, SNumber, Sort, min, max).ToList(), PNumber, SNumber);
                    return Json(result.Data);

                }
            }
            var processorVMs = Pagination.PagedResult(_iwonder.GetSSDDependentOnSort(Sort).ToList(), PNumber, PageSize);
            return Json(processorVMs.Data);
        }

        [HttpPost]
        public JsonResult ProductsOfSSDBrand(string[] brand)
        {
            int PageSize = int.Parse(HttpContext.Session.GetString("PageSize"));
            int PageNumber = int.Parse(HttpContext.Session.GetString("PageNumber"));
            HttpContext.Session.SetString("brandssd", string.Join(",", brand));
            var Sort = HttpContext.Session.GetInt32("Sortssd") ?? 0;
            var brands = HttpContext.Session.GetString("brandssd").Split(',');
            var max = HttpContext.Session.GetInt32("Maxssd") ?? 0;
            var min = HttpContext.Session.GetInt32("Minssd") ?? 0;
            if (brands.Length <= 0 || brands[0] == "")
            {
                var Data = Pagination.PagedResult(_iwonder.GetSSDPriceDependentOnBrand(min, max, Sort).ToList(), PageNumber, PageSize);
                return Json(Data.Data);
            }
            else
            {
                var Data = Pagination.PagedResult(_iwonder.GetSSDProductsByBrand(brands, PageNumber, PageSize, Sort, min, max).ToList(), PageNumber, PageSize);

                return Json(Data.Data);
            }
        }
        [HttpGet]
        public JsonResult GetSSDPrice(int min, int max)
        {
            int PageSize = int.Parse(HttpContext.Session.GetString("PageSize"));
            int PageNumber = int.Parse(HttpContext.Session.GetString("PageNumber"));
            var IsNull = HttpContext.Session.GetString("brandssd") ?? null;
            var Sort = HttpContext.Session.GetInt32("Sortssd") ?? 0;
            HttpContext.Session.SetInt32("Maxssd", max);
            HttpContext.Session.SetInt32("Minssd", min);
            if (IsNull == null)
            {
                if (Sort == 0)
                {
                    var Data = Pagination.PagedResult(_iwonder.SSDPrice(min, max, PageSize, PageNumber).ToList(), PageNumber, PageSize);

                    return Json(Data.Data);
                }
                else
                {

                    var Data = Pagination.PagedResult(_iwonder.GetSSDPriceDependentOnBrand(min, max, Sort).ToList(), PageNumber, PageSize);
                    return Json(Data.Data);
                }
            }
            var brands = HttpContext.Session.GetString("brandssd").Split(',');
            var result = Pagination.PagedResult(_iwonder.GetSSDProductsByBrand(brands, PageNumber, PageSize, Sort, min, max).ToList(), PageNumber, PageSize);
            return Json(result.Data);
        }
        #endregion

        #region Case

        [HttpGet]
        public IActionResult Case(int pageNumber = 1, int PageSize = 3)
        {
            HttpContext.Session.SetString("PageSize", PageSize.ToString());
            HttpContext.Session.SetString("PageNumber", pageNumber.ToString());
            var PNumber = int.Parse(HttpContext.Session.GetString("PageNumber")); // Session for PageNumber
            var SNumber = int.Parse(HttpContext.Session.GetString("PageSize")); // Session for PageSize 
            var Data = Pagination.PagedResult(_iwonder.GetAllCase().ToList(), PNumber, SNumber);
            ViewBag.BrandNamesAndNumbers = _iwonder.GetCaseBrandNamesAndNumbers(); // Get All Brands
            ViewData["PageSize"] = PageSize;
            return View(Data);
        }
        [HttpGet]
        public JsonResult CaseAjax(int PageNumber)
        {

            HttpContext.Session.SetString("PageNumber", PageNumber.ToString());
            var SNumber = int.Parse(HttpContext.Session.GetString("PageSize"));
            var PNumber = int.Parse(HttpContext.Session.GetString("PageNumber"));
            var Sort = HttpContext.Session.GetInt32("Sortcase") ?? 0;
            var max = HttpContext.Session.GetInt32("Maxcase") ?? 0;
            var min = HttpContext.Session.GetInt32("Mincase") ?? 0;
            var brand = HttpContext.Session.GetString("brandcase") ?? null;
            string[] brands = null;
            if (brand != null)
            {

                brands = brand.Split(',');

                bool IsTrue = brands.Length > 0 && brands[0] != "";
                if (IsTrue)
                {

                    var Data = _iwonder.GetCaseProductsByBrand(brands, PageNumber, SNumber, Sort, min, max);
                    return Json(Data);
                }
            }
            var result = Pagination.PagedResult(_iwonder.GetCasePriceDependentOnBrand(min, max, Sort).ToList(), PNumber, SNumber);
            return Json(result.Data);
        }


        [HttpGet]
        public JsonResult AscendingCaseProdoucts(int Id)
        {
            int PageSize = int.Parse(HttpContext.Session.GetString("PageSize"));
            int PageNumber = int.Parse(HttpContext.Session.GetString("PageNumber"));
            var max = HttpContext.Session.GetInt32("Maxcase") ?? 0;
            var min = HttpContext.Session.GetInt32("Mincase") ?? 0;
            if (Id != 0)
            {
                HttpContext.Session.SetInt32("Sortcase", Id);
                if (HttpContext.Session.GetString("brandcase") != null)
                {
                    var brands = HttpContext.Session.GetString("brandcase").Split(',');
                    if (brands.Length > 0 && brands[0] != "")
                    {
                        var result = Pagination.PagedResult(_iwonder.GetCaseProductsByBrand(brands, PageNumber, PageSize, Id, min, max).ToList(), PageNumber, PageSize);
                        return Json(result.Data);

                    }
                }

            }
            var motherboards = Pagination.PagedResult(_iwonder.GetCasePriceDependentOnBrand(min, max, Id).ToList(), PageNumber, PageSize);
            return Json(motherboards.Data);
        }

        [HttpGet]
        public JsonResult DefaultCase(int PageSize = 3)
        {
            HttpContext.Session.SetString("PageSize", PageSize.ToString());
            int PNumber = int.Parse(HttpContext.Session.GetString("PageNumber"));
            int SNumber = int.Parse(HttpContext.Session.GetString("PageSize"));
            var Sort = HttpContext.Session.GetInt32("Sortcase") ?? 0;
            var max = HttpContext.Session.GetInt32("Maxcase") ?? 0;
            var min = HttpContext.Session.GetInt32("Mincase") ?? 0;
            if (HttpContext.Session.GetString("brandcase") != null)
            {
                var brands = HttpContext.Session.GetString("brandcase").Split(',');
                if (brands.Length != 0 && brands[0] != "")
                {
                    var result = Pagination.PagedResult(_iwonder.GetCaseProductsByBrand(brands, PNumber, SNumber, Sort, min, max).ToList(), PNumber, SNumber);
                    return Json(result.Data);

                }
            }
            var processorVMs = Pagination.PagedResult(_iwonder.GetCaseDependentOnSort(Sort).ToList(), PNumber, PageSize);
            return Json(processorVMs.Data);
        }

        [HttpPost]
        public JsonResult ProductsOfCaseBrand(string[] brand)
        {
            int PageSize = int.Parse(HttpContext.Session.GetString("PageSize"));
            int PageNumber = int.Parse(HttpContext.Session.GetString("PageNumber"));
            HttpContext.Session.SetString("brandcase", string.Join(",", brand));
            var Sort = HttpContext.Session.GetInt32("Sortcase") ?? 0;
            var brands = HttpContext.Session.GetString("brandcase").Split(',');
            var max = HttpContext.Session.GetInt32("Maxcase") ?? 0;
            var min = HttpContext.Session.GetInt32("Mincase") ?? 0;
            if (brands.Length <= 0 || brands[0] == "")
            {
                var Data = Pagination.PagedResult(_iwonder.GetCasePriceDependentOnBrand(min, max, Sort).ToList(), PageNumber, PageSize);
                return Json(Data.Data);
            }
            else
            {
                var Data = Pagination.PagedResult(_iwonder.GetCaseProductsByBrand(brands, PageNumber, PageSize, Sort, min, max).ToList(), PageNumber, PageSize);

                return Json(Data.Data);
            }
        }
        [HttpGet]
        public JsonResult GetCasePrice(int min, int max)
        {
            int PageSize = int.Parse(HttpContext.Session.GetString("PageSize"));
            int PageNumber = int.Parse(HttpContext.Session.GetString("PageNumber"));
            var IsNull = HttpContext.Session.GetString("brandcase") ?? null;
            var Sort = HttpContext.Session.GetInt32("Sortcase") ?? 0;
            HttpContext.Session.SetInt32("Maxcase", max);
            HttpContext.Session.SetInt32("Mincase", min);
            if (IsNull == null)
            {
                if (Sort == 0)
                {
                    var Data = Pagination.PagedResult(_iwonder.CasePrice(min, max, PageSize, PageNumber).ToList(), PageNumber, PageSize);

                    return Json(Data.Data);
                }
                else
                {

                    var Data = Pagination.PagedResult(_iwonder.GetCasePriceDependentOnBrand(min, max, Sort).ToList(), PageNumber, PageSize);
                    return Json(Data.Data);
                }
            }
            var brands = HttpContext.Session.GetString("brandcase").Split(',');
            var result = Pagination.PagedResult(_iwonder.GetCaseProductsByBrand(brands, PageNumber, PageSize, Sort, min, max).ToList(), PageNumber, PageSize);
            return Json(result.Data);
        }
        #endregion Case

        #region PowerSuply

        [HttpGet]
        public IActionResult PowerSuply(int pageNumber = 1, int PageSize = 3)
        {
            HttpContext.Session.SetString("PageSize", PageSize.ToString());
            HttpContext.Session.SetString("PageNumber", pageNumber.ToString());
            var PNumber = int.Parse(HttpContext.Session.GetString("PageNumber")); // Session for PageNumber
            var SNumber = int.Parse(HttpContext.Session.GetString("PageSize")); // Session for PageSize 
            var Data = Pagination.PagedResult(_iwonder.GetAllPowerSuply().ToList(), PNumber, SNumber);
            ViewBag.BrandNamesAndNumbers = _iwonder.GetPowerSuplyBrandNamesAndNumbers(); // Get All Brands
            ViewData["PageSize"] = PageSize;
            return View(Data);
        }


        public JsonResult PowerSuplyAjax(int PageNumber)
        {

            HttpContext.Session.SetString("PageNumber", PageNumber.ToString());
            var SNumber = int.Parse(HttpContext.Session.GetString("PageSize"));
            var PNumber = int.Parse(HttpContext.Session.GetString("PageNumber"));
            var Sort = HttpContext.Session.GetInt32("SortPS") ?? 0;
            var max = HttpContext.Session.GetInt32("MaxPS") ?? 0;
            var min = HttpContext.Session.GetInt32("MinPS") ?? 0;
            var brand = HttpContext.Session.GetString("brandPS") ?? null;
            string[] brands = null;
            if (brand != null)
            {

                brands = brand.Split(',');

                bool IsTrue = brands.Length > 0 && brands[0] != "";
                if (IsTrue)
                {

                    var Data = _iwonder.GetPowerSuplyProductsByBrand(brands, PageNumber, SNumber, Sort, min, max);
                    return Json(Data);
                }
            }
            var result = Pagination.PagedResult(_iwonder.GetPowerSuplyPriceDependentOnBrand(min, max, Sort).ToList(), PNumber, SNumber);
            return Json(result.Data);
        }

        [HttpGet]
        public JsonResult AscendingPowerSuplyProdoucts(int Id)
        {
            int PageSize = int.Parse(HttpContext.Session.GetString("PageSize"));
            int PageNumber = int.Parse(HttpContext.Session.GetString("PageNumber"));
            var max = HttpContext.Session.GetInt32("MaxPS") ?? 0;
            var min = HttpContext.Session.GetInt32("MinPS") ?? 0;
            if (Id != 0)
            {
                HttpContext.Session.SetInt32("SortPS", Id);
                if (HttpContext.Session.GetString("brandPS") != null)
                {
                    var brands = HttpContext.Session.GetString("brandPS").Split(',');
                    if (brands.Length > 0 && brands[0] != "")
                    {
                        var result = Pagination.PagedResult(_iwonder.GetPowerSuplyProductsByBrand(brands, PageNumber, PageSize, Id, min, max).ToList(), PageNumber, PageSize);
                        return Json(result.Data);

                    }
                }

            }
            var motherboards = Pagination.PagedResult(_iwonder.GetPowerSuplyPriceDependentOnBrand(min, max, Id).ToList(), PageNumber, PageSize);
            return Json(motherboards.Data);
        }

        [HttpGet]
        public JsonResult DefaultPowerSuply(int PageSize = 3)
        {
            HttpContext.Session.SetString("PageSize", PageSize.ToString());
            int PNumber = int.Parse(HttpContext.Session.GetString("PageNumber"));
            int SNumber = int.Parse(HttpContext.Session.GetString("PageSize"));
            var Sort = HttpContext.Session.GetInt32("SortPS") ?? 0;
            var max = HttpContext.Session.GetInt32("MaxPS") ?? 0;
            var min = HttpContext.Session.GetInt32("MinPS") ?? 0;
            if (HttpContext.Session.GetString("brandPS") != null)
            {
                var brands = HttpContext.Session.GetString("brandPS").Split(',');
                if (brands.Length != 0 && brands[0] != "")
                {
                    var result = Pagination.PagedResult(_iwonder.GetPowerSuplyProductsByBrand(brands, PNumber, SNumber, Sort, min, max).ToList(), PNumber, SNumber);
                    return Json(result.Data);

                }
            }
            var processorVMs = Pagination.PagedResult(_iwonder.GetPowerSuplyDependentOnSort(Sort).ToList(), PNumber, PageSize);
            return Json(processorVMs.Data);
        }

        [HttpPost]
        public JsonResult ProductsOfPowerSuplyBrand(string[] brand)
        {
            int PageSize = int.Parse(HttpContext.Session.GetString("PageSize"));
            int PageNumber = int.Parse(HttpContext.Session.GetString("PageNumber"));
            HttpContext.Session.SetString("brandPS", string.Join(",", brand));
            var Sort = HttpContext.Session.GetInt32("SortPS") ?? 0;
            var brands = HttpContext.Session.GetString("brandPS").Split(',');
            var max = HttpContext.Session.GetInt32("MaxPS") ?? 0;
            var min = HttpContext.Session.GetInt32("MinPS") ?? 0;
            if (brands.Length <= 0 || brands[0] == "")
            {
                var Data = Pagination.PagedResult(_iwonder.GetPowerSuplyPriceDependentOnBrand(min, max, Sort).ToList(), PageNumber, PageSize);
                return Json(Data.Data);
            }
            else
            {
                var Data = Pagination.PagedResult(_iwonder.GetPowerSuplyProductsByBrand(brands, PageNumber, PageSize, Sort, min, max).ToList(), PageNumber, PageSize);

                return Json(Data.Data);
            }
        }

        [HttpGet]
        public JsonResult GetPowerSuplyPrice(int min, int max)
        {
            int PageSize = int.Parse(HttpContext.Session.GetString("PageSize"));
            int PageNumber = int.Parse(HttpContext.Session.GetString("PageNumber"));
            var IsNull = HttpContext.Session.GetString("brandPS") ?? null;
            var Sort = HttpContext.Session.GetInt32("SortPS") ?? 0;
            HttpContext.Session.SetInt32("MaxPS", max);
            HttpContext.Session.SetInt32("MinPS", min);
            if (IsNull == null)
            {
                if (Sort == 0)
                {
                    var Data = Pagination.PagedResult(_iwonder.PowerSuplyPrice(min, max, PageSize, PageNumber).ToList(), PageNumber, PageSize);

                    return Json(Data.Data);
                }
                else
                {

                    var Data = Pagination.PagedResult(_iwonder.GetPowerSuplyPriceDependentOnBrand(min, max, Sort).ToList(), PageNumber, PageSize);
                    return Json(Data.Data);
                }
            }
            var brands = HttpContext.Session.GetString("brandPS").Split(',');
            var result = Pagination.PagedResult(_iwonder.GetPowerSuplyProductsByBrand(brands, PageNumber, PageSize, Sort, min, max).ToList(), PageNumber, PageSize);
            return Json(result.Data);
        }
        #endregion



        #region Cart
        public IActionResult Cart()
        {
            return View();
        }
        public IActionResult GetAvailableProducts()
        {
            Dictionary<string, WishListVM> list = new Dictionary<string, WishListVM>();
            var Casesobj = _wonder.Cases.Where(x => x.IsAvailable == true);
            foreach (var item in Casesobj)
            {
                WishListVM obj = new WishListVM();
                obj.ProductCode = item.CaseCode;
                obj.ProductName = item.CaseName;
                obj.Quantity = item.CaseQuantity;
                obj.ProductPrice = item.CasePrice;
                list.Add(item.CaseCode, obj);
            }
            var Vgasobj = _wonder.GraphicsCards.Where(x => x.IsAvailable == true);
            foreach (var item in Vgasobj)
            {
                WishListVM obj = new WishListVM();
                obj.ProductCode = item.Vgacode;
                obj.ProductName = item.Vganame;
                obj.Quantity = item.Vgaquantity;
                obj.ProductPrice = item.Vgaprice;
                list.Add(item.Vgacode, obj);
            }
            var HDDsobj = _wonder.Hdds.Where(x => x.IsAvailable == true);
            foreach (var item in HDDsobj)
            {
                WishListVM obj = new WishListVM();
                obj.ProductCode = item.Hddcode;
                obj.ProductName = item.Hddname;
                obj.Quantity = item.Hddquantity;
                obj.ProductPrice = item.Hddprice;
                list.Add(item.Hddcode, obj);
            }
            var MBsobj = _wonder.Motherboards.Where(x => x.IsAvailable == true);
            foreach (var item in MBsobj)
            {
                WishListVM obj = new WishListVM();
                obj.ProductCode = item.MotherCode;
                obj.ProductName = item.MotherName;
                obj.Quantity = item.MotherQuantity;
                obj.ProductPrice = item.MotherPrice;
                list.Add(item.MotherCode, obj);
            }
            var PSUsobj = _wonder.PowerSupplies.Where(x => x.IsAvailable == true);
            foreach (var item in PSUsobj)
            {
                WishListVM obj = new WishListVM();
                obj.ProductCode = item.Psucode;
                obj.ProductName = item.Psuname;
                obj.Quantity = item.Psuquantity;
                obj.ProductPrice = item.Psuprice;
                list.Add(item.Psucode, obj);
            }
            var Prosobj = _wonder.Processors.Where(x => x.IsAvailable == true);
            foreach (var item in Prosobj)
            {
                WishListVM obj = new WishListVM();
                obj.ProductCode = item.ProCode;
                obj.ProductName = item.ProName;
                obj.Quantity = item.ProQuantity;
                obj.ProductPrice = item.ProPrice;
                list.Add(item.ProCode, obj);
            }
            var RAMsobj = _wonder.Rams.Where(x => x.IsAvailable == true);
            foreach (var item in RAMsobj)
            {
                WishListVM obj = new WishListVM();
                obj.ProductCode = item.RamCode;
                obj.ProductName = item.RamName;
                obj.Quantity = item.RamQuantity;
                obj.ProductPrice = item.RamPrice;
                list.Add(item.RamCode, obj);
            }
            var SSDsobj = _wonder.Ssds.Where(x => x.IsAvailable == true);
            foreach (var item in SSDsobj)
            {
                WishListVM obj = new WishListVM();
                obj.ProductCode = item.Ssdcode;
                obj.ProductName = item.Ssdname;
                obj.Quantity = item.Ssdquantity;
                obj.ProductPrice = item.Ssdprice;
                list.Add(item.Ssdcode, obj);
            }
            return Json(list);
        }

        #endregion

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
            var wishlistCounter = _iwonder.GetWishList(userid).Where(x => x.IsAvailable != "Not Available").Count();

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
                if (address == null)
                {
                    userInfo.Add("Name", _wonder.Users.Where(x => x.Phone == UserData.Telephone && x.IsAdmin == false).Select(x => x.FirstName).FirstOrDefault() + " " + _wonder.Users.Where(x => x.Phone == UserData.Telephone && x.IsAdmin == false).Select(x => x.LastName).FirstOrDefault());
                    userInfo.Add("Phone", Convert.ToString(_wonder.Users.Where(x => x.Phone == UserData.Telephone && x.IsAdmin == false).Select(x => x.Phone).FirstOrDefault()));
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
                if (check == "success" || check == "success checked new address" || check == "success checked old address")
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
            else if (wishlist== "chat")
            {
                ViewBag.Wishlist = "chat";
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
                if (_wonder.Users.Where(x => x.Phone == user.Telephone && x.Password == user.Password && x.IsAdmin == false).FirstOrDefault() != null)
                {
                    var id = _wonder.Users.Where(x => x.Phone == user.Telephone && x.IsAdmin == false).Select(x => x.UserId).FirstOrDefault();
                    var name = _wonder.Users.Where(x => x.UserId == id).Select(x => new { x.FirstName, x.LastName }).FirstOrDefault();
                    HttpContext.Session.SetInt32("UserID", id);
                    HttpContext.Session.SetString("UserName", name.FirstName + " " + name.LastName);
                    //Session.Timeout = 15;
                    if (WishList == "wishlist")
                    {
                        return Json("WishList");
                    }
                    else if (WishList== "chat")
                    {
                        return Json("chat");
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
                    return Json("WishList");
                }
                else if (WishList == "chat")
                {
                    return Json("chat");
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
            int Uid = HttpContext.Session.GetInt32("UserID").GetValueOrDefault();
            //Cases Except One
            ViewBag.Case = _iwonder.GetAllCase(Uid).Where(x=>x.CaseCode != code);
            if (currentPageIndex == 0 && NextOrPreviousPage == 0)
            {
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

        //public IActionResult GraphicsCardDetails(string code, int currentPageIndex, int NextOrPreviousPage)
        //{
        //    int Uid = HttpContext.Session.GetInt32("UserID").GetValueOrDefault();
        //    //GraphicsCards Except One
        //    ViewBag.GraphicsCard = _iwonder.GetAllCard(Uid).Where(x => x.Vgacode != code);
        //    if (currentPageIndex == 0 && NextOrPreviousPage == 0)
        //    {
        //        return View(_iwonder.GraphicsCardDetails(code));
        //    }
        //    else if (currentPageIndex == 0)
        //    {
        //        return Json(_iwonder.VGACommentsPagination(code, NextOrPreviousPage));
        //    }
        //    else
        //    {
        //        return Json(_iwonder.VGACommentsPagination(code, currentPageIndex));
        //    }
        //}

        //public IActionResult HddDetails(string code, int currentPageIndex, int NextOrPreviousPage)
        //{
        //    int Uid = HttpContext.Session.GetInt32("UserID").GetValueOrDefault();
        //    //Hdds Except One
        //    ViewBag.Hdd = _iwonder.GetAllHDD(Uid).Where(x => x.Hddcode != code);
        //    if (currentPageIndex == 0 && NextOrPreviousPage == 0)
        //    {
        //        return View(_iwonder.HddDetails(code));
        //    }
        //    else if (currentPageIndex == 0)
        //    {
        //        return Json(_iwonder.HDDCommentsPagination(code, NextOrPreviousPage));
        //    }
        //    else
        //    {
        //        return Json(_iwonder.HDDCommentsPagination(code, currentPageIndex));
        //    }
        //}

        //public IActionResult MotherboardDetails(string code, int currentPageIndex, int NextOrPreviousPage)
        //{
        //    int Uid = HttpContext.Session.GetInt32("UserID").GetValueOrDefault();
        //    //Motherboards Except One
        //    ViewBag.Motherboard = _iwonder.GetAllMotherboard(Uid).Where(x => x.MotherCode != code);
        //    if (currentPageIndex == 0 && NextOrPreviousPage == 0)
        //    {
        //        return View(_iwonder.MotherboardDetails(code));
        //    }
        //    else if (currentPageIndex == 0)
        //    {
        //        return Json(_iwonder.MBCommentsPagination(code, NextOrPreviousPage));
        //    }
        //    else
        //    {
        //        return Json(_iwonder.MBCommentsPagination(code, currentPageIndex));
        //    }
        //}

        //public IActionResult PowerSupplyDetails(string code, int currentPageIndex, int NextOrPreviousPage)
        //{
        //    int Uid = HttpContext.Session.GetInt32("UserID").GetValueOrDefault();
        //    //PowerSupplies Except One
        //    ViewBag.PowerSupply = _iwonder.GetAllPowerSuply(Uid).Where(x => x.Psucode != code);
        //    if (currentPageIndex == 0 && NextOrPreviousPage == 0)
        //    {
        //        return View(_iwonder.PowerSupplyDetails(code));
        //    }
        //    else if (currentPageIndex == 0)
        //    {
        //        return Json(_iwonder.PSCommentsPagination(code, NextOrPreviousPage));
        //    }
        //    else
        //    {
        //        return Json(_iwonder.PSCommentsPagination(code, currentPageIndex));
        //    }
        //}

        //public IActionResult ProcessorDetails(string code, int currentPageIndex, int NextOrPreviousPage)
        //{
        //    int Uid = HttpContext.Session.GetInt32("UserID").GetValueOrDefault();
        //    //Processors Except One
        //    ViewBag.Processor = _iwonder.GetAllProcessors(Uid).Where(x => x.ProCode != code);
        //    if (currentPageIndex == 0 && NextOrPreviousPage == 0)
        //    {
        //        return View(_iwonder.ProcessorDetails(code));
        //    }
        //    else if (currentPageIndex == 0)
        //    {
        //        return Json(_iwonder.ProCommentsPagination(code, NextOrPreviousPage));
        //    }
        //    else
        //    {
        //        return Json(_iwonder.ProCommentsPagination(code, currentPageIndex));
        //    }
        //}

        //public IActionResult RamDetails(string code, int currentPageIndex, int NextOrPreviousPage)
        //{
        //    int Uid = HttpContext.Session.GetInt32("UserID").GetValueOrDefault();
        //    //Rams Except One
        //    ViewBag.Ram = _iwonder.GetAllRAM(Uid).Where(x => x.RamCode != code);
        //    if (currentPageIndex == 0 && NextOrPreviousPage == 0)
        //    {
        //        return View(_iwonder.RamDetails(code));
        //    }
        //    else if (currentPageIndex == 0)
        //    {
        //        return Json(_iwonder.RAMCommentsPagination(code, NextOrPreviousPage));
        //    }
        //    else
        //    {
        //        return Json(_iwonder.RAMCommentsPagination(code, currentPageIndex));
        //    }
        //}

        //public IActionResult SsdDetails(string code, int currentPageIndex, int NextOrPreviousPage)
        //{
        //    int Uid = HttpContext.Session.GetInt32("UserID").GetValueOrDefault();
        //    //Ssds Except One
        //    ViewBag.Ssd = _iwonder.GetAllSSD(Uid).Where(x => x.Ssdcode != code);
        //    if (currentPageIndex == 0 && NextOrPreviousPage == 0)
        //    {
        //        return View(_iwonder.SsdDetails(code));
        //    }
        //    else if (currentPageIndex == 0)
        //    {
        //        return Json(_iwonder.SSDCommentsPagination(code, NextOrPreviousPage));
        //    }
        //    else
        //    {
        //        return Json(_iwonder.SSDCommentsPagination(code, currentPageIndex));
        //    }
        //}

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


        public IActionResult Chat()
        {
            var userid = HttpContext.Session.GetInt32("UserID").GetValueOrDefault();
            if (userid != 0)
            {
                var messages = _iwonder.GetAllMessages(userid);
                ViewBag.userid = userid;
                return View(messages);
            }
            else
            {
                return RedirectToAction("Login_Register", new { wishlist = "chat" });
            }
            
        }
    }
}