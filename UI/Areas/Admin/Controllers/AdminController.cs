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
        public JsonResult CheckReview(ReviewVM review, int Num)
        {
            Review R = _wonder.Reviews.Where(x => x.DateAndTime == review.DateAndTime && x.Comment == review.Comment).Select(x => x).FirstOrDefault();
            if (Num == 1)
            {
                R.IsAvailable = false;
                _wonder.SaveChanges();
                return Json("Refused");
            }
            else if (Num == 2)
            {
                R.IsAvailable = true;
                _wonder.SaveChanges();
                return Json("Accepted");
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
        public JsonResult CaseData()
        {
            return Json(_iwonder.GetAllCase());
        }
        public IActionResult DeleteCase(string Code)
        {
            _wonder.Cases.Remove(_wonder.Cases.Where(x => x.CaseCode == Code).FirstOrDefault());
            _wonder.SaveChanges();
            return RedirectToAction("Processor");
        }
        [HttpGet]
        public ActionResult CreateCase()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateCase(Case newcase)
        {
            _wonder.Cases.Add(newcase);
            _wonder.SaveChanges();
            return View();
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
            return RedirectToAction("Processor");
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
            return RedirectToAction("Processor");
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
            return RedirectToAction("Processor");
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
            return RedirectToAction("Processor");
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
        [HttpPost]
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








