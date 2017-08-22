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
using ShiNengShiHui.Web.Models.Teacher.Grade;
using Abp.Timing;

namespace ShiNengShiHui.Web.Controllers
{
    [AbpMvcAuthorize(permissions: PermissionNames.Pages_Users_Teacher)]
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

        #region 学生模块
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

            #region 初始化数据
            List<SelectListItem> group = new List<SelectListItem>();
            for (int i = 1; i <= 4; i++)
            {
                if (i == result.Group)
                {
                    group.Add(new SelectListItem() { Value = i.ToString(), Text = i.ToString(), Selected = true });
                }
                else
                {
                    group.Add(new SelectListItem() { Value = i.ToString(), Text = i.ToString() });
                }
            }
            ViewBag.Group = group;

            List<SelectListItem> sex = new List<SelectListItem>();
            if (result.Sex)
            {
                sex.Add(new SelectListItem() { Value = "true", Text = "男", Selected = true });
                sex.Add(new SelectListItem() { Value = "false", Text = "女" });
            }
            else
            {
                sex.Add(new SelectListItem() { Value = "true", Text = "男" });
                sex.Add(new SelectListItem() { Value = "false", Text = "女", Selected = true });
            }
            ViewBag.Sex = sex;
            #endregion

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

            #region 初始化数据
            List<SelectListItem> group = new List<SelectListItem>();
            for (int i = 1; i <= 4; i++)
            {
                if (i == model.Group)
                {
                    group.Add(new SelectListItem() { Value = i.ToString(), Text = i.ToString(), Selected = true });
                }
                else
                {
                    group.Add(new SelectListItem() { Value = i.ToString(), Text = i.ToString() });
                }
            }
            ViewBag.Group = group;

            List<SelectListItem> sex = new List<SelectListItem>();
            if (model.Sex)
            {
                sex.Add(new SelectListItem() { Value = "true", Text = "男", Selected = true });
                sex.Add(new SelectListItem() { Value = "false", Text = "女" });
            }
            else
            {
                sex.Add(new SelectListItem() { Value = "true", Text = "男" });
                sex.Add(new SelectListItem() { Value = "false", Text = "女", Selected = true });
            }
            ViewBag.Sex = sex;
            #endregion

            return View(model);
        }
        #endregion

        #region 新增
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

        [HttpPost]
        public ActionResult StudentCreate(StudentCreatreViewModel model)
        {

            if (ModelState.IsValid)
            {
                _teacherAppService.CreateStudent(ObjectMapper.Map<CreateStudentInput>(model));
                return RefreshParent();
            }

            #region 初始化数据
            List<SelectListItem> group = new List<SelectListItem>();
            for (int i = 1; i <= 4; i++)
            {
                if (i == model.Group)
                {
                    group.Add(new SelectListItem() { Value = i.ToString(), Text = i.ToString(), Selected = true });
                }
                else
                {
                    group.Add(new SelectListItem() { Value = i.ToString(), Text = i.ToString() });
                }
            }
            ViewBag.Group = group;

            List<SelectListItem> sex = new List<SelectListItem>();
            if (model.Sex)
            {
                sex.Add(new SelectListItem() { Value = "true", Text = "男", Selected = true });
                sex.Add(new SelectListItem() { Value = "false", Text = "女" });
            }
            else
            {
                sex.Add(new SelectListItem() { Value = "true", Text = "男" });
                sex.Add(new SelectListItem() { Value = "false", Text = "女", Selected = true });
            }
            ViewBag.Sex = sex;
            #endregion

            return View(model);
        }
        #endregion

        #region 删除
        [HttpPost]
        public ActionResult StudentDelete(int[] sid)
        {
            for (int i = 0, length = sid.Length; i < length; i++)
            {
                _teacherAppService.DeleteStudent(new DeleteStudentInput() { Id = sid[i] });
            }
            return RedirectToAction("StudentIndex");
        }
        #endregion
        #endregion

        #region 成绩模块
        public ActionResult GradeIndex()
        {
            ShowPageGradeOutput result = _teacherAppService.ShowPageGrade(new ShowPageGradeInput());

            var listmodel = result.ShowGradeOutputs.Select<ShowGradeOutput, GradeResultViewModel>(m => ObjectMapper.Map<GradeResultViewModel>(m));
            return View(listmodel);
        }

        #region 新增
        [HttpGet]
        public ActionResult GradeCreate()
        {
            #region 初始化的数据
            ShowPageStudentOutput students = _teacherAppService.ShowPageStudent(new ShowPageStudentInput() { PageIndex = 1, ShowCount = 100 });
            List<SelectListItem> studentList = new List<SelectListItem>();
            students.ShowStudentOutputs.ToList().ForEach(m => studentList.Add(new SelectListItem { Value = m.Id.ToString(), Text = m.Name }));
            ViewBag.StudentList = studentList;

            List<SelectListItem> gradeList = new List<SelectListItem>();
            for (int i = 10; i > 0; i--)
            {
                gradeList.Add(new SelectListItem { Value = i.ToString(), Text = i.ToString() });
            }
            ViewBag.GradeList = gradeList;


            List<SelectListItem> schoolYearList = new List<SelectListItem>();
            schoolYearList.Add(new SelectListItem() { Value = (Clock.Now.Year - 1).ToString(), Text = (Clock.Now.Year - 1).ToString() });
            schoolYearList.Add(new SelectListItem() { Value = Clock.Now.Year.ToString(), Text = Clock.Now.Year.ToString() });
            schoolYearList.Add(new SelectListItem() { Value = (Clock.Now.Year + 1).ToString(), Text = (Clock.Now.Year + 1).ToString() });
            ViewBag.SchoolYearList = schoolYearList;

            List<SelectListItem> semesterList = new List<SelectListItem>();
            semesterList.Add(new SelectListItem() { Value = "1", Text = "第一学期" });
            semesterList.Add(new SelectListItem() { Value = "2", Text = "第二学期" });
            ViewBag.SemesterLiset = semesterList;

            List<SelectListItem> weekList = new List<SelectListItem>();
            for (int i = 1; i < 31; i++)
            {
                weekList.Add(new SelectListItem() { Value = i.ToString(), Text = i.ToString() });
            }
            ViewBag.WeekList = weekList;
            #endregion

            return View();
        }

        [HttpPost]
        public ActionResult GradeCreate(GradeCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                _teacherAppService.CreateGrade(ObjectMapper.Map<CreateGradeInput>(model));
                return RefreshParent();
            }

            #region 初始化的数据
            List<List<SelectListItem>> gradeSelect = new List<List<SelectListItem>>();
            for (int i = 0; i < 10; i++)
            {
                List<SelectListItem> gradeSelectItem = new List<SelectListItem>();
                for (int j = 10; j >= 0; j--)
                {
                    if (j == model.Grades[i])
                    {
                        gradeSelectItem.Add(new SelectListItem() { Value = j.ToString(), Text = j.ToString(), Selected = true });
                    }
                    else
                    {
                        gradeSelectItem.Add(new SelectListItem() { Value = j.ToString(), Text = j.ToString() });
                    }
                }
                gradeSelect.Add(gradeSelectItem);
            }
            ViewBag.GradeList = gradeSelect;


            List<SelectListItem> schoolYearList = new List<SelectListItem>();
            schoolYearList.Add(new SelectListItem() { Value = (Clock.Now.Year - 1).ToString(), Text = (Clock.Now.Year - 1).ToString() });
            schoolYearList.Add(new SelectListItem() { Value = Clock.Now.Year.ToString(), Text = Clock.Now.Year.ToString() });
            schoolYearList.Add(new SelectListItem() { Value = (Clock.Now.Year + 1).ToString(), Text = (Clock.Now.Year + 1).ToString() });
            foreach (SelectListItem item in schoolYearList)
            {
                if (item.Value.Equals(model.SchoolYead.ToString()))
                {
                    item.Selected = true;
                }
            }
            ViewBag.SchoolYearList = schoolYearList;

            List<SelectListItem> semesterList = new List<SelectListItem>();
            semesterList.Add(new SelectListItem() { Value = "1", Text = "第一学期" });
            semesterList.Add(new SelectListItem() { Value = "2", Text = "第二学期" });
            foreach (SelectListItem item in semesterList)
            {
                if (item.Value.Equals(model.Semester.ToString()))
                {
                    item.Selected = true;
                }
            }
            ViewBag.SemesterLiset = semesterList;

            List<SelectListItem> weekList = new List<SelectListItem>();
            for (int i = 1; i < 31; i++)
            {
                if (i == model.Week)
                {
                    weekList.Add(new SelectListItem() { Value = i.ToString(), Text = i.ToString(), Selected = true });
                }
                else
                {
                    weekList.Add(new SelectListItem() { Value = i.ToString(), Text = i.ToString() });
                }
            }
            ViewBag.WeekList = weekList;
            #endregion
            return View(model);
        }
        #endregion

        #region 编辑
        [HttpGet]
        public ActionResult GradeEdit(int id)
        {
            ShowGradeOutput gradeOutput = _teacherAppService.ShowGrade(new ShowGradeInput() { Id = id });
            GradeEditViewModel result = ObjectMapper.Map<GradeEditViewModel>(gradeOutput);

            #region 初始化的数据
            List<List<SelectListItem>> gradeSelect = new List<List<SelectListItem>>();
            for (int i = 0; i < 10; i++)
            {
                List<SelectListItem> gradeSelectItem = new List<SelectListItem>();
                for (int j = 10; j >= 0; j--)
                {
                    if (j == result.Grades[i])
                    {
                        gradeSelectItem.Add(new SelectListItem() { Value = j.ToString(), Text = j.ToString(), Selected = true });
                    }
                    else
                    {
                        gradeSelectItem.Add(new SelectListItem() { Value = j.ToString(), Text = j.ToString() });
                    }
                }
                gradeSelect.Add(gradeSelectItem);
            }
            ViewBag.GradeList = gradeSelect;


            List<SelectListItem> schoolYearList = new List<SelectListItem>();
            schoolYearList.Add(new SelectListItem() { Value = (Clock.Now.Year - 1).ToString(), Text = (Clock.Now.Year - 1).ToString() });
            schoolYearList.Add(new SelectListItem() { Value = Clock.Now.Year.ToString(), Text = Clock.Now.Year.ToString() });
            schoolYearList.Add(new SelectListItem() { Value = (Clock.Now.Year + 1).ToString(), Text = (Clock.Now.Year + 1).ToString() });
            foreach (SelectListItem item in schoolYearList)
            {
                if (item.Value.Equals(result.SchoolYead.ToString()))
                {
                    item.Selected = true;
                }
            }
            ViewBag.SchoolYearList = schoolYearList;

            List<SelectListItem> semesterList = new List<SelectListItem>();
            semesterList.Add(new SelectListItem() { Value = "1", Text = "第一学期" });
            semesterList.Add(new SelectListItem() { Value = "2", Text = "第二学期" });
            foreach (SelectListItem item in semesterList)
            {
                if (item.Value.Equals(result.Semester.ToString()))
                {
                    item.Selected = true;
                }
            }
            ViewBag.SemesterLiset = semesterList;

            List<SelectListItem> weekList = new List<SelectListItem>();
            for (int i = 1; i < 31; i++)
            {
                if (i == result.Week)
                {
                    weekList.Add(new SelectListItem() { Value = i.ToString(), Text = i.ToString(), Selected = true });
                }
                else
                {
                    weekList.Add(new SelectListItem() { Value = i.ToString(), Text = i.ToString() });
                }
            }
            ViewBag.WeekList = weekList;
            #endregion

            return View(result);
        }

        [HttpPost]
        public ActionResult GradeEdit(GradeEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                _teacherAppService.UpdateGrade(ObjectMapper.Map<UpdateGradeInput>(model));
                return RefreshParent();
            }

            #region 初始化的数据
            List<List<SelectListItem>> gradeSelect = new List<List<SelectListItem>>();
            for (int i = 0; i < 10; i++)
            {
                List<SelectListItem> gradeSelectItem = new List<SelectListItem>();
                for (int j = 10; j >= 0; j--)
                {
                    if (j == model.Grades[i])
                    {
                        gradeSelectItem.Add(new SelectListItem() { Value = j.ToString(), Text = j.ToString(), Selected = true });
                    }
                    else
                    {
                        gradeSelectItem.Add(new SelectListItem() { Value = j.ToString(), Text = j.ToString() });
                    }
                }
                gradeSelect.Add(gradeSelectItem);
            }
            ViewBag.GradeList = gradeSelect;


            List<SelectListItem> schoolYearList = new List<SelectListItem>();
            schoolYearList.Add(new SelectListItem() { Value = (Clock.Now.Year - 1).ToString(), Text = (Clock.Now.Year - 1).ToString() });
            schoolYearList.Add(new SelectListItem() { Value = Clock.Now.Year.ToString(), Text = Clock.Now.Year.ToString() });
            schoolYearList.Add(new SelectListItem() { Value = (Clock.Now.Year + 1).ToString(), Text = (Clock.Now.Year + 1).ToString() });
            foreach (SelectListItem item in schoolYearList)
            {
                if (item.Value.Equals(model.SchoolYead.ToString()))
                {
                    item.Selected = true;
                }
            }
            ViewBag.SchoolYearList = schoolYearList;

            List<SelectListItem> semesterList = new List<SelectListItem>();
            semesterList.Add(new SelectListItem() { Value = "1", Text = "第一学期" });
            semesterList.Add(new SelectListItem() { Value = "2", Text = "第二学期" });
            foreach (SelectListItem item in semesterList)
            {
                if (item.Value.Equals(model.Semester.ToString()))
                {
                    item.Selected = true;
                }
            }
            ViewBag.SemesterLiset = semesterList;

            List<SelectListItem> weekList = new List<SelectListItem>();
            for (int i = 1; i < 31; i++)
            {
                if (i == model.Week)
                {
                    weekList.Add(new SelectListItem() { Value = i.ToString(), Text = i.ToString(), Selected = true });
                }
                else
                {
                    weekList.Add(new SelectListItem() { Value = i.ToString(), Text = i.ToString() });
                }
            }
            ViewBag.WeekList = weekList;
            #endregion

            return View(model);
        }
        #endregion

        #region 删除
        [HttpPost]
        public ActionResult GradeDelete(int[] id)
        {
            for (int i = 0, length = id.Length; i < length; i++)
            {
                _teacherAppService.DeleteGrade(new DeleteGradeInput() { Id = id[i] });
            }
            return RedirectToAction("GradeIndex");
        }
        #endregion 
        #endregion


        public ActionResult PrizeIndex()
        {
            return View();
        }
    }
}