using ShiNengShiHui.AppServices;
using ShiNengShiHui.AppServices.AdministratorDTO;
using ShiNengShiHui.Web.Models.Administrator.Class;
using ShiNengShiHui.Web.Models.Administrator.Teacher;
using ShiNengShiHui.Web.Models.Administrator.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShiNengShiHui.Web.Controllers
{
    public class AdministratorController : ShiNengShiHuiControllerBase
    {
        private IAdministratorAppService _administratorAppService;

        public AdministratorController(IAdministratorAppService administratorAppService)
        {
            _administratorAppService = administratorAppService;
        }

        // GET: Administrator
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UserIndex(int? pageIndex)
        {
            UserShowPageOutput result;
            if (pageIndex == null)
            {
                result = _administratorAppService.UserShowPage(new UserShowPageInput());
            }
            else
            {
                result = _administratorAppService.UserShowPage(new UserShowPageInput() { PageIndex = (int)pageIndex });
            }

            ViewData["pageIndex"] = result.PageIndex;
            ViewData["pageCount"] = result.PageCount;

            List<UserResultViewModel> list = result.Users.Select<UserShowOutput, UserResultViewModel>(m => ObjectMapper.Map<UserResultViewModel>(m)).ToList();
            return View(list);
        }

        public ActionResult ClassIndex(int? pageIndex)
        {
            ClassShowPageOutput result;
            if (pageIndex == null)
            {
                result = _administratorAppService.ClassShowPage(new ClassShowPageInput());
            }
            else
            {
                result = _administratorAppService.ClassShowPage(new ClassShowPageInput() { PageIndex = (int)pageIndex });
            }

            ViewData["pageIndex"] = result.PageIndex;
            ViewData["pageCount"] = result.PageCount;

            List<ClassResultViewModel> list = result.Classes.Select(m => ObjectMapper.Map<ClassResultViewModel>(m)).ToList();
            return View(list);
        }

        public ActionResult TeacherIndex(int? pageIndex)
        {
            TeacherShowPageOutput result;
            if (pageIndex == null)
            {
                result = _administratorAppService.TeacherShowPage(new TeacherShowPageInput());
            }
            else
            {
                result = _administratorAppService.TeacherShowPage(new TeacherShowPageInput() { PageIndex = (int)pageIndex });
            }

            ViewData["pageIndex"] = result.PageIndex;
            ViewData["pageCount"] = result.PageCount;

            List<TeacherResultViewModel> list = result.Teachers.Select(m => ObjectMapper.Map<TeacherResultViewModel>(m)).ToList();
            return View(list);
        }
    }
}