using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using BLL.ViewModel;
using DataModel.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace UI.Controllers
{
    public class AdminController : Controller
    {
        readonly IWonder _iwonder;

        public AdminController(IWonder iwonder, WonderHardwareContext wonder)
        {
            _iwonder = iwonder;
            _wonder = wonder;
        }
        readonly WonderHardwareContext _wonder;



        public IActionResult Index()
        {
            if ((HttpContext.Session.GetInt32("AdminID").GetValueOrDefault()) == 0)
            {
                return RedirectToAction("Login");
            }

            #region counter
            Dictionary<string, int> Counter = new Dictionary<string, int>();

            Counter.Add("UsersCount", _iwonder.GetUsersData().Count());
            Counter.Add("SalesCount", (int)_wonder.Sales.Sum(x => x.ProductQuantity));
            Counter.Add("Revenue", (int)_wonder.Sales.Sum(x => x.TotalPrice));
            Counter.Add("ProductsCounts", _iwonder.GetAllProcessors().Count() + _iwonder.GetAllCard().Count() + _iwonder.GetAllCase().Count() + _iwonder.GetAllHDD().Count() + _iwonder.GetAllMotherboard().Count() + _iwonder.GetAllPowerSuply().Count() + _iwonder.GetAllRAM().Count() + _iwonder.GetAllSSD().Count());

            ViewBag.Counter = Counter;
            #endregion

            #region big chart

            Dictionary<string, List<int>> productsEarningsMonthly = new Dictionary<string, List<int>>();

            List<int> earningsMonthly1 = new List<int>();
            List<int> earningsMonthly2 = new List<int>();
            List<int> earningsMonthly3 = new List<int>();
            List<int> earningsMonthly4 = new List<int>();
            List<int> earningsMonthly5 = new List<int>();
            List<int> earningsMonthly6 = new List<int>();
            List<int> earningsMonthly7 = new List<int>();
            List<int> earningsMonthly8 = new List<int>();

            for (int i = 1; i <= 12; i++)
            {
                earningsMonthly1.Add((int)_wonder.Sales.Where(x => x.ProCode != null && x.DateAndTime.Value.Year == DateTime.Now.Year && x.DateAndTime.Value.Month == i).Sum(x => x.TotalPrice));
            }
            productsEarningsMonthly.Add("Pro", earningsMonthly1);

            for (int i = 1; i <= 12; i++)
            {
                earningsMonthly2.Add((int)_wonder.Sales.Where(x => x.CaseCode != null && x.DateAndTime.Value.Year == DateTime.Now.Year && x.DateAndTime.Value.Month == i).Sum(x => x.TotalPrice));
            }
            productsEarningsMonthly.Add("Case", earningsMonthly2);

            for (int i = 1; i <= 12; i++)
            {
                earningsMonthly3.Add((int)_wonder.Sales.Where(x => x.Vgacode != null && x.DateAndTime.Value.Year == DateTime.Now.Year && x.DateAndTime.Value.Month == i).Sum(x => x.TotalPrice));
            }
            productsEarningsMonthly.Add("VGA", earningsMonthly3);

            for (int i = 1; i <= 12; i++)
            {
                earningsMonthly4.Add((int)_wonder.Sales.Where(x => x.Hddcode != null && x.DateAndTime.Value.Year == DateTime.Now.Year && x.DateAndTime.Value.Month == i).Sum(x => x.TotalPrice));
            }
            productsEarningsMonthly.Add("HDD", earningsMonthly4);

            for (int i = 1; i <= 12; i++)
            {
                earningsMonthly5.Add((int)_wonder.Sales.Where(x => x.MotherCode != null && x.DateAndTime.Value.Year == DateTime.Now.Year && x.DateAndTime.Value.Month == i).Sum(x => x.TotalPrice));
            }
            productsEarningsMonthly.Add("MB", earningsMonthly5);

            for (int i = 1; i <= 12; i++)
            {
                earningsMonthly6.Add((int)_wonder.Sales.Where(x => x.Psucode != null && x.DateAndTime.Value.Year == DateTime.Now.Year && x.DateAndTime.Value.Month == i).Sum(x => x.TotalPrice));
            }
            productsEarningsMonthly.Add("PSu", earningsMonthly6);

            for (int i = 1; i <= 12; i++)
            {
                earningsMonthly7.Add((int)_wonder.Sales.Where(x => x.RamCode != null && x.DateAndTime.Value.Year == DateTime.Now.Year && x.DateAndTime.Value.Month == i).Sum(x => x.TotalPrice));
            }
            productsEarningsMonthly.Add("RAM", earningsMonthly7);

            for (int i = 1; i <= 12; i++)
            {
                earningsMonthly8.Add((int)_wonder.Sales.Where(x => x.Ssdcode != null && x.DateAndTime.Value.Year == DateTime.Now.Year && x.DateAndTime.Value.Month == i).Sum(x => x.TotalPrice));
            }
            productsEarningsMonthly.Add("SSD", earningsMonthly8);


            ViewBag.productsEarningsMonthly = productsEarningsMonthly;


            #endregion

            #region progress bar
            Dictionary<string, int> SalesSum = new Dictionary<string, int>();

            SalesSum.Add("Cases", (int)_wonder.Sales.Where(x => x.CaseCode != null).Sum(x => x.ProductQuantity));
            SalesSum.Add("VGAs", (int)_wonder.Sales.Where(x => x.Vgacode != null).Sum(x => x.ProductQuantity));
            SalesSum.Add("HDDs", (int)_wonder.Sales.Where(x => x.Hddcode != null).Sum(x => x.ProductQuantity));
            SalesSum.Add("MBs", (int)_wonder.Sales.Where(x => x.MotherCode != null).Sum(x => x.ProductQuantity));
            SalesSum.Add("PSs", (int)_wonder.Sales.Where(x => x.Psucode != null).Sum(x => x.ProductQuantity));
            SalesSum.Add("RAMs", (int)_wonder.Sales.Where(x => x.RamCode != null).Sum(x => x.ProductQuantity));
            SalesSum.Add("Pros", (int)_wonder.Sales.Where(x => x.ProCode != null).Sum(x => x.ProductQuantity));
            SalesSum.Add("SSDs", (int)_wonder.Sales.Where(x => x.Ssdcode != null).Sum(x => x.ProductQuantity));

            ViewBag.SalesSum = SalesSum;
            #endregion

            #region circle chart
            Dictionary<string, int> quantity = new Dictionary<string, int>();

            quantity.Add("Cases", _wonder.Cases.Sum(x => x.CaseQuantity));
            quantity.Add("VGAs", _wonder.GraphicsCards.Sum(x => x.Vgaquantity));
            quantity.Add("HDDs", _wonder.Hdds.Sum(x => x.Hddquantity));
            quantity.Add("MBs", _wonder.Motherboards.Sum(x => x.MotherQuantity));
            quantity.Add("PSs", _wonder.PowerSupplies.Sum(x => x.Psuquantity));
            quantity.Add("RAMs", _wonder.Rams.Sum(x => x.RamQuantity));
            quantity.Add("Pros", _wonder.Processors.Sum(x => x.ProQuantity));
            quantity.Add("SSDs", _wonder.Ssds.Sum(x => x.Ssdquantity));

            ViewBag.ProductsQuantity = quantity;
            #endregion

            #region top users 
            var topuseres = (from o in _wonder.Sales
                             group o by o.UserId into g
                             orderby g.Sum(x => x.ProductQuantity) descending
                             select new { id = g.Key, quantity = g.Sum(x => x.ProductQuantity) })
                            .Take(5).ToList();

            List<UserVM> topuseresData = new List<UserVM>();
            foreach (var item in topuseres)
            {
                UserVM obj = new UserVM();
                obj.ID = (int)item.id;
                obj.Name = _wonder.Users.Where(x => x.UserId == (int)item.id).Select(x => x.FirstName).FirstOrDefault() + " " + _wonder.Users.Where(x => x.UserId == (int)item.id).Select(x => x.LastName).FirstOrDefault();
                obj.Telephone = _wonder.Users.Where(x => x.UserId == (int)item.id).Select(x => x.Phone).FirstOrDefault();
                obj.Quantity = (int)item.quantity;
                topuseresData.Add(obj);
            }

            ViewBag.TopUsersData = topuseresData;
            #endregion
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
        public ActionResult EnterAccount(UserVM admin)
        {
            if (_wonder.Users.Where(x => x.Phone == admin.Telephone && x.IsAdmin == true).FirstOrDefault() != null)
            {
                if (_wonder.Users.Where(x => x.Phone == admin.Telephone && x.Password == admin.Password).FirstOrDefault() != null)
                {
                    var id = _wonder.Users.Where(x => x.Phone == admin.Telephone).Select(x => x.UserId).FirstOrDefault();
                    var name = _wonder.Users.Where(x => x.UserId == id).Select(x => new { x.FirstName, x.LastName }).FirstOrDefault();
                    HttpContext.Session.SetInt32("AdminID", id);
                    HttpContext.Session.SetString("AdminName", name.FirstName + " " + name.LastName);
                    return Json("return to index");
                }
                else
                {
                    return Json("wrong password");
                }
            }
            else
                return Json("this phone isn't exist");
        }

        public ActionResult LogOut()
        {
            HttpContext.Session.Remove("AdminID");
            return RedirectToAction("Login");
        }
        public ActionResult Users()
        {
            if ((HttpContext.Session.GetInt32("AdminID").GetValueOrDefault()) == 0)
            {
                return RedirectToAction("Login");
            }
            return View();
        }

        public JsonResult UsersData()
        {
            return Json(_iwonder.GetUsersData());
        }

        public ActionResult Admins()
        {
            if ((HttpContext.Session.GetInt32("AdminID").GetValueOrDefault()) == 0)
            {
                return RedirectToAction("Login");
            }
            return View();
        }

        public JsonResult AdminsData()
        {
            return Json(_iwonder.GetAdmins());
        }

        #region Reviews
        public ActionResult ProductReviews(string code)
        {
            string[] arr = new string[2];
            arr[0] = code;
            arr[1] = "";
            if (code.StartsWith("S"))
            {
                arr[1] = _wonder.Ssds.Where(x => x.Ssdcode == code).Select(x => x.Ssdname).FirstOrDefault();
            }
            else if (code.StartsWith("R"))
            {
                arr[1] = _wonder.Rams.Where(x => x.RamCode == code).Select(x => x.RamName).FirstOrDefault();
            }
            else if (code.StartsWith("C"))
            {
                arr[1] = _wonder.Cases.Where(x => x.CaseCode == code).Select(x => x.CaseName).FirstOrDefault();
            }
            else if (code.StartsWith("V"))
            {
                arr[1] = _wonder.GraphicsCards.Where(x => x.Vgacode == code).Select(x => x.Vganame).FirstOrDefault();
            }
            else if (code.StartsWith("PS"))
            {
                arr[1] = _wonder.PowerSupplies.Where(x => x.Psucode == code).Select(x => x.Psuname).FirstOrDefault();
            }
            else if (code.StartsWith("Pr"))
            {
                arr[1] = _wonder.Processors.Where(x => x.ProCode == code).Select(x => x.ProName).FirstOrDefault();
            }
            else if (code.StartsWith("M"))
            {
                arr[1] = _wonder.Motherboards.Where(x => x.MotherCode == code).Select(x => x.MotherName).FirstOrDefault();
            }
            else if (code.StartsWith("H"))
            {
                arr[1] = _wonder.Hdds.Where(x => x.Hddcode == code).Select(x => x.Hddname).FirstOrDefault();
            }
            return View(arr);
        }
        public JsonResult ReviewsData(string code)
        {
            return Json(_iwonder.Reviews(code));
        }
        public JsonResult CheckReview(int ID, int Num)
        {
            Review R = _wonder.Reviews.Where(x => x.ReviewId == ID).Select(x => x).FirstOrDefault();
            if (Num == 1)
            {
                R.IsAvailable = true;
                _wonder.Reviews.Update(R);
                _wonder.SaveChanges();
                return Json("Accepted");
            }
            else if (Num == 2)
            {
                R.IsAvailable = false;
                _wonder.Reviews.Update(R);
                _wonder.SaveChanges();
                return Json("Refused");
            }
            else
            {
                return Json("SomeThing Error");
            }
        }
        #endregion
        #region Tables

        #region Case
        public ActionResult Case()
        {
            if ((HttpContext.Session.GetInt32("AdminID").GetValueOrDefault()) == 0)
            {
                return RedirectToAction("Login");
            }
            return View();
        }
        public JsonResult CaseData(string deleteddata)
        {
            return Json(_iwonder.GetAllCase(deleteddata));
        }

        public IActionResult UpdateCase(string Code)
        {
            return View(_iwonder.CaseDetails(Code));
        }

        [HttpPost]
        public IActionResult UpdateCase(CaseVM item)
        {
            Case EditCase = _wonder.Cases.Where(x => x.CaseCode == item.CaseCode).FirstOrDefault();
            EditCase.CaseName = item.CaseName;
            EditCase.CasePrice = item.CasePrice;
            EditCase.CaseBrandId = item.CaseBrandId;
            EditCase.CaseFactorySize = item.CaseFactorySize;
            EditCase.CaseQuantity = item.CaseQuantity;
            _wonder.Cases.Update(EditCase);
            _wonder.SaveChanges();
            return RedirectToAction("Case");
        }
        public ActionResult DeleteCase(string Code)
        {
            Case obj = _wonder.Cases.Where(x => x.CaseCode == Code).FirstOrDefault();
            obj.IsAvailable = false;
            _wonder.Cases.Update(obj);
            _wonder.SaveChanges();
            return RedirectToAction("Case");
        }
        public ActionResult AddDeletedCase(string Code)
        {
            Case obj = _wonder.Cases.Where(x => x.CaseCode == Code).FirstOrDefault();
            obj.IsAvailable = true;
            _wonder.Cases.Update(obj);
            _wonder.SaveChanges();
            return RedirectToAction("Case");
        }

        public ActionResult CreateCase()
        {
            ViewBag.Brands = _iwonder.GetProductBrand();
            return View();
        }
        [HttpPost]
        public ActionResult CreateCase(Case newcase)
        {
            Case obj = newcase;
            obj.IsAvailable = true;
            _wonder.Cases.Add(obj);
            _wonder.SaveChanges();
            return RedirectToAction("Case");
        }
        #endregion Case

        #region GraphicsCard
        public ActionResult GraphicsCard()
        {
            if ((HttpContext.Session.GetInt32("AdminID").GetValueOrDefault()) == 0)
            {
                return RedirectToAction("Login");
            }
            return View();
        }
        public JsonResult GraphicsCardData()
        {
            return Json(_iwonder.GetAllCard());
        }

        public IActionResult DeleteVga(string Code)
        {
            _wonder.GraphicsCards.Remove(_wonder.GraphicsCards.Where(x => x.Vgacode == Code).FirstOrDefault());
            _wonder.SaveChanges();
            return RedirectToAction("GraphicsCard");
        }
        [HttpGet]
        public ActionResult CreateVga()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateVga(GraphicsCard newvga)
        {
            _wonder.GraphicsCards.Add(newvga);
            _wonder.SaveChanges();
            return View();
        }
        #endregion GraphicsCard

        #region HDD
        public ActionResult Hdd()
        {
            if ((HttpContext.Session.GetInt32("AdminID").GetValueOrDefault()) == 0)
            {
                return RedirectToAction("Login");
            }
            return View();
        }
        public JsonResult HddData()
        {
            return Json(_iwonder.GetAllHDD());
        }

        public IActionResult DeleteHdd(string Code)
        {
            _wonder.Hdds.Remove(_wonder.Hdds.Where(x => x.Hddcode == Code).FirstOrDefault());
            _wonder.SaveChanges();
            return RedirectToAction("Hdd");
        }
        [HttpGet]
        public ActionResult CreateHdd()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateHdd(Hdd newHdd)
        {
            _wonder.Hdds.Add(newHdd);
            _wonder.SaveChanges();
            return View();
        }
        #endregion HDD

        #region MotherBoard
        public ActionResult Motherboard()
        {
            if ((HttpContext.Session.GetInt32("AdminID").GetValueOrDefault()) == 0)
            {
                return RedirectToAction("Login");
            }
            return View();
        }

        public JsonResult MotherboardData()
        {
            return Json(_iwonder.GetAllMotherboard());
        }

        public IActionResult DeleteMotherboard(string Code)
        {
            _wonder.Motherboards.Remove(_wonder.Motherboards.Where(x => x.MotherCode == Code).FirstOrDefault());
            _wonder.SaveChanges();
            return RedirectToAction("Motherboard");
        }
        [HttpGet]
        public ActionResult CreateMotherboard()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateMotherboard(Motherboard newmotherboard)
        {
            _wonder.Motherboards.Add(newmotherboard);
            _wonder.SaveChanges();
            return View();
        }

        #endregion MotherBoard

        #region PSU
        public ActionResult PowerSupply()
        {
            if ((HttpContext.Session.GetInt32("AdminID").GetValueOrDefault()) == 0)
            {
                return RedirectToAction("Login");
            }
            return View();
        }
        public JsonResult PowerSupplyData()
        {
            return Json(_iwonder.GetAllPowerSuply());
        }

        public IActionResult DeletePowersupply(string Code)
        {
            _wonder.PowerSupplies.Remove(_wonder.PowerSupplies.Where(x => x.Psucode == Code).FirstOrDefault());
            _wonder.SaveChanges();
            return RedirectToAction("PowerSupply");
        }
        [HttpGet]
        public ActionResult CreatePowersupply()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreatePowersupply(PowerSupply newpsu)
        {
            _wonder.PowerSupplies.Add(newpsu);
            _wonder.SaveChanges();
            return View();
        }
        #endregion PSU

        #region Processor
        public ActionResult Processor()
        {
            if ((HttpContext.Session.GetInt32("AdminID").GetValueOrDefault()) == 0)
            {
                return RedirectToAction("Login");
            }
            return View();
        }
        public JsonResult ProcessorData()
        {
            return Json(_iwonder.GetAllProcessors());
        }

        public IActionResult DeleteProcessor(string Code)
        {
            _wonder.Processors.Remove(_wonder.Processors.Where(x => x.ProCode == Code).FirstOrDefault());
            _wonder.SaveChanges();
            return RedirectToAction("Processor");
        }
        [HttpGet]
        public ActionResult CreateProcessor()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateProcessor(Processor processor)
        {
            _wonder.Processors.Add(processor);
            _wonder.SaveChanges();
            return View();
        }

        #endregion Processor

        #region Ram
        public ActionResult Ram()
        {
            if ((HttpContext.Session.GetInt32("AdminID").GetValueOrDefault()) == 0)
            {
                return RedirectToAction("Login");
            }
            return View();
        }

        public JsonResult RamData()
        {
            return Json(_iwonder.GetAllRAM());
        }

        public IActionResult DeleteRam(string Code)
        {
            _wonder.Rams.Remove(_wonder.Rams.Where(x => x.RamCode == Code).FirstOrDefault());
            _wonder.SaveChanges();
            return RedirectToAction("Ram");
        }
        [HttpGet]
        public ActionResult CreateRam()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateRam(Ram newram)
        {
            _wonder.Rams.Add(newram);
            _wonder.SaveChanges();
            return View();
        }
        #endregion Ram

        #region SSD
        public ActionResult Ssd()
        {
            if ((HttpContext.Session.GetInt32("AdminID").GetValueOrDefault()) == 0)
            {
                return RedirectToAction("Login");
            }
            return View();
        }

        public JsonResult SsdData()
        {
            return Json(_iwonder.GetAllSSD());
        }

        public IActionResult DeleteSsd(string Code)
        {
            _wonder.Ssds.Remove(_wonder.Ssds.Where(x => x.Ssdcode == Code).FirstOrDefault());
            _wonder.SaveChanges();
            return RedirectToAction("Ssd");
        }
        [HttpGet]
        public ActionResult CreateSsd()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateSsd(Ssd newssd)
        {
            _wonder.Ssds.Add(newssd);
            _wonder.SaveChanges();
            return View();
        }
        #endregion SSD


        #endregion Tables

        public ActionResult Sales()
        {
            if ((HttpContext.Session.GetInt32("AdminID").GetValueOrDefault()) == 0)
            {
                return RedirectToAction("Login");
            }
            return View();
        }
        #region Sales
        [HttpPost]
        public IActionResult SalesData(string code)
        {
            List<SalesVM> Sales = new List<SalesVM>();
            if (code != null)
            {
                var salesTable = code.Split(',');

                foreach (var item in salesTable)
                {
                    switch (item.ToUpper())
                    {
                        case "CAS":
                            var CAS = _iwonder.GetCases();
                            Sales.AddRange(CAS);
                            break;
                        case "V":
                            var V = _iwonder.GetGraphicsCard();
                            Sales.AddRange(V);
                            break;
                        case "HD":
                            var HD = _iwonder.GetHDD();
                            Sales.AddRange(HD);
                            break;
                        case "MO":
                            var MO = _iwonder.GetMotherboard();
                            Sales.AddRange(MO);
                            break;
                        case "PS":
                            var PS = _iwonder.GetPowerSupplies();
                            Sales.AddRange(PS);
                            break;
                        case "Pr":
                            var Pr = _iwonder.GetProcessor();
                            Sales.AddRange(Pr);
                            break;
                        case "R":
                            var R = _iwonder.GetRam();
                            Sales.AddRange(R);
                            break;
                        case "SSD":
                            var SSD = _iwonder.GetSDD();
                            Sales.AddRange(SSD);
                            break;
                    }
                }
                return Json(Sales.OrderBy(s => s.UserID).Distinct());
            }
            return Json(new SalesVM());
        }

        #endregion
    }
}








