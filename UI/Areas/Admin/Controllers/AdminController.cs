using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using BLL.ViewModel;
using DataModel.Models;
using Microsoft.AspNetCore.Http;


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
            if ((HttpContext.Session.GetInt32("UserID").GetValueOrDefault()) == 0)
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
                if (_wonder.Users.Where(x => x.Phone == admin.Telephone && x.Password == admin.Password ).FirstOrDefault() != null)
                {
                    var id = _wonder.Users.Where(x => x.Phone == admin.Telephone).Select(x => x.UserId).FirstOrDefault();
                    var name = _wonder.Users.Where(x => x.UserId == id).Select(x => new { x.FirstName, x.LastName }).FirstOrDefault();
                    HttpContext.Session.SetInt32("UserID", id);
                    HttpContext.Session.SetString("UserName", name.FirstName + " " + name.LastName);
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
            HttpContext.Session.Remove("UserID");
            return RedirectToAction("Login");
        }
        public ActionResult Users()
        {
            if ((HttpContext.Session.GetInt32("UserID").GetValueOrDefault()) == 0)
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
            if ((HttpContext.Session.GetInt32("UserID").GetValueOrDefault()) == 0)
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

        public ActionResult Case()
        {
            if ((HttpContext.Session.GetInt32("UserID").GetValueOrDefault()) == 0)
            {
                return RedirectToAction("Login");
            }
            ViewBag.Brand = _iwonder.GetProductBrand();
            ViewBag.Case = _iwonder.GetAllCase();
            return View();
        }
        public ActionResult Processor()
        {
            return View();
        }
        public JsonResult ProcessorData()
        {
            return Json(_iwonder.GetAllProcessors());
        }

        public ActionResult MotherBoard()
        {
            if ((HttpContext.Session.GetInt32("UserID").GetValueOrDefault()) == 0)
            {
                return RedirectToAction("Login");
            }
            ViewBag.Brand = _iwonder.GetProductBrand();
            ViewBag.MotherBoard = _iwonder.GetAllMotherboard();
            return View();
        }

        public ActionResult Ram()
        {
            if ((HttpContext.Session.GetInt32("UserID").GetValueOrDefault()) == 0)
            {
                return RedirectToAction("Login");
            }
            ViewBag.Brand = _iwonder.GetProductBrand();
            ViewBag.Ram = _iwonder.GetAllRAM();
            return View();
        }

        #endregion Tables

        public ActionResult Sales()
        {
            if ((HttpContext.Session.GetInt32("UserID").GetValueOrDefault()) == 0)
            {
                return RedirectToAction("Login");
            }
            return View();
        }
    }
}





