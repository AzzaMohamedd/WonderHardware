using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using DataModel.Models;

namespace UI.Controllers
{
    public class AdminController : Controller
    {
        readonly IWonder _iwonder;

        WonderHardwareContext db = new WonderHardwareContext();

        public AdminController(IWonder iwonder)
        {
            _iwonder = iwonder;
        }

        public IActionResult Index()
        {
            //hamza or ragab
            return View();
        }

        public ActionResult Login()
        {
            return View();
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