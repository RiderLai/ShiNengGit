using Abp.Web.Mvc.Authorization;
using ShiNengShiHui.AppServices;
using ShiNengShiHui.Authorization;
using ShiNengShiHui.AppServices.Teacher.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShiNengShiHui.Web.Models.Teacher.Student;

namespace ShiNengShiHui.Web.Controllers
{
    [AbpMvcAuthorize(permissions:PermissionNames.Pages_Users_Teacher)]
    public class TeacherController : ShiNengShiHuiControllerBase
    {
        private readonly ITeacherAppService _teacherAppService;

        public TeacherController(ITeacherAppService teacherAppService)
        {
            _teacherAppService = teacherAppService;
        }

        // GET: Teacher
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult StudentIndex()
        {

            ShowPageStudentOutput result = _teacherAppService.ShowPageStudent(new ShowPageStudentInput());

            var listmodel = result.ShowStudentOutputs.Select<ShowStudentOutput, StudentResultViewModel>(m => ObjectMapper.Map<StudentResultViewModel>(m));
            return View(listmodel);
        }

        #region 编辑
        [HttpGet]
        public ActionResult StudentEdit(int Id)
        {
            ShowStudentOutput result = _teacherAppService.ShowStudent(new ShowStudentInput() { Id = Id });

            List<SelectListItem> group = new List<SelectListItem>();
            for (int i = 1; i <= 4; i++)
            {
                group.Add(new SelectListItem() { Value = i.ToString(), Text = i.ToString() });
            }
            ViewBag.Group = group;

            List<SelectListItem> sex = new List<SelectListItem>();
            sex.Add(new SelectListItem() { Value = "true", Text = "男" });
            sex.Add(new SelectListItem() { Value = "false", Text = "女" });
            ViewBag.Sex = sex;

            return View(ObjectMapper.Map<StudentEditViewModel>(result));
        }

        [HttpPost]
        public ActionResult StudentEdit(StudentEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                UpdateStudentInput update = ObjectMapper.Map<UpdateStudentInput>(model);
                _teacherAppService.UpdateStudent(update);
                return RefreshParent();
            }

            List<SelectListItem> group = new List<SelectListItem>();
            for (int i = 1; i <= 4; i++)
            {
                group.Add(new SelectListItem() { Value = i.ToString(), Text = i.ToString() });
            }
            ViewBag.Group = group;

            List<SelectListItem> sex = new List<SelectListItem>();
            sex.Add(new SelectListItem() { Value = "true", Text = "男" });
            sex.Add(new SelectListItem() { Value = "false", Text = "女" });
            ViewBag.Sex = sex;

            return View(model);
        } 
        #endregion

        [HttpGet]
        public ActionResult StudentCreate()
        {
            List<SelectListItem> group = new List<SelectListItem>();
            for (int i = 1; i <= 4; i++)
            {
                group.Add(new SelectListItem() { Value = i.ToString(), Text = i.ToString() });
            }
            ViewBag.Group = group;

            List<SelectListItem> sex = new List<SelectListItem>();
            sex.Add(new SelectListItem() { Value = "true", Text = "男" });
            sex.Add(new SelectListItem() { Value = "false", Text = "女" });
            ViewBag.Sex = sex;

            return View();
        }

        public ActionResult StudentCreate(StudentCreatreViewModel model)
        {

            if (ModelState.IsValid)
            {
                _teacherAppService.CreateStudent(ObjectMapper.Map<CreateStudentInput>(model));
                return RefreshParent();
            }

            List<SelectListItem> group = new List<SelectListItem>();
            for (int i = 1; i <= 4; i++)
            {
                group.Add(new SelectListItem() { Value = i.ToString(), Text = i.ToString() });
            }
            ViewBag.Group = group;

            List<SelectListItem> sex = new List<SelectListItem>();
            sex.Add(new SelectListItem() { Value = "true", Text = "男" });
            sex.Add(new SelectListItem() { Value = "false", Text = "女" });
            ViewBag.Sex = sex;

            return View(model);
        }

        public ActionResult StudentDelete()
        {
            return View();
        }

        public ActionResult GradeIndex()
        {
            return View();
        }

        public ActionResult PrizeIndex()
        {
            return View();
        }
    }
}