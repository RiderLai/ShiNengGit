﻿using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;

namespace ShiNengShiHui.Web.Controllers
{
    [AbpMvcAuthorize]
    public class HomeController : ShiNengShiHuiControllerBase
    {

        public ActionResult Index()
        {
            //return View("~/App/Main/views/layout/layout.cshtml"); //Layout of the angular application.

            
            return View();
        }

    }
}