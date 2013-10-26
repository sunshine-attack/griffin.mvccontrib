﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Griffin.MvcContrib.Localization;

namespace Griffin.MvcContrib.Admin.TestProject.Areas.TestArea.Controllers
{
    [Localized]
    public class HomeController : Controller
    {
        //
        // GET: /TestArea/Home/

        public ActionResult Index()
        {
            return View();
        }

    }
}
