﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Chillisoft.LendingLibrary.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Test()
        {
            return View();
        }
        public ActionResult TestResult(FormCollection formsCollection)
        {
            return View("Test");
        }
    }
}