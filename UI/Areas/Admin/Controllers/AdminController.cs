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



        public IActionResult Index(UserVM admin)
        {
            //hamza or ragab
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
        public ActionResult EnterAccount(UserVM admin)
        {
            if (_wonder.Users.Select(x => x.Phone).Contains(admin.Telephone))
            {
                if (_wonder.Users.Where(x => x.Phone == admin.Telephone && x.Password == admin.Password).FirstOrDefault() != null)
                {
                    var id = _wonder.Users.Where(x => x.Phone == admin.Telephone).Select(x => x.UserId).FirstOrDefault();
                    var name = _wonder.Users.Where(x => x.UserId == id).Select(x => new { x.FirstName, x.LastName }).FirstOrDefault();
                    HttpContext.Session.SetInt32("UserID", id);
                    HttpContext.Session.SetString("UserName", name.FirstName + " " + name.LastName);
                    return RedirectToAction("Index", new { ID = id, Name = name.FirstName+" "+name.LastName });
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
            return View();
        }

        public JsonResult UsersData()
        {
            return Json(_iwonder.GetUsersData());
        }

        public ActionResult Admins()
        {
            return View();
        }

        public JsonResult AdminsData()
        {
            return Json(_iwonder.GetAdmins());
        }

        #region Tables

        public ActionResult Case()
        {
            ViewBag.Brand = _iwonder.GetProductBrand();
            ViewBag.Case = _iwonder.GetAllCase();
            return View();
        }

        public ActionResult Processor()
        {
            ViewBag.Brand = _iwonder.GetProductBrand();
            ViewBag.Processor = _iwonder.GetAllProcessors();
            return View();
        }

        public ActionResult MotherBoard()
        {
            ViewBag.Brand = _iwonder.GetProductBrand();
            ViewBag.MotherBoard = _iwonder.GetAllMotherboard();
            return View();
        }

        public ActionResult Ram()
        {
            ViewBag.Brand = _iwonder.GetProductBrand();
            ViewBag.Ram = _iwonder.GetAllRAM();
            return View();
        }

        #endregion Tables

        public ActionResult Sales()
        {
            return View();
        }
    }
}