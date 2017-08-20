using ShiNengShiHui.AppServices;
using ShiNengShiHui.AppServices.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShiNengShiHui.Web.Controllers
{
    public class HelperController : ShiNengShiHuiControllerBase
    {
        private readonly IFunctionAppService _functionAppService;

        public HelperController(IFunctionAppService functionAppService)
        {
            _functionAppService = functionAppService;
        }

        // GET: Helper
        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult FunctionMenue()
        {
            FunctionGetOfRoleOutput functionOutput = _functionAppService.FunctionOfRoleGetAll();
            return PartialView(functionOutput);
        }
    }
}