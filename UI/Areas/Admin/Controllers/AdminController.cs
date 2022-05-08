﻿using Microsoft.AspNetCore.Mvc;
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








