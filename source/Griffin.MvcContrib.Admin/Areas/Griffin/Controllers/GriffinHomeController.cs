﻿using System.Web.Mvc;

namespace Griffin.MvcContrib.Areas.Griffin.Controllers
{
   
    public class GriffinHomeController : Controller
    {
        //
        // GET: /Griffin/Home/

        public ActionResult Index()
        {
            return View();
        }
    }
}