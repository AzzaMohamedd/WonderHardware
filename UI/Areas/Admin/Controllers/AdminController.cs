using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL;

namespace UI.Controllers
{
    public class AdminController : Controller
    {
        readonly IWonder _iwonder;

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
            var result = _iwonder.GetUsersData();
            return Json(result);
        }
        public ActionResult Admins()
        {
            return View();
        }
        public JsonResult AdminData()
        {
            return Json(_iwonder.GetAdmins());
        }
        #region Tables
        public ActionResult Case()
        {
            return View();
        }
        #endregion

        public ActionResult Sales()
        {
            return View();
        }

    }
}
