using ShiNengShiHui.AppServices;
//using ShiNengShiHui.AppServices.TeacherDTO;
using ShiNengShiHui.AppServices.WeekGradeDTO;
using ShiNengShiHui.Web.Models.WeekGrade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShiNengShiHui.Web.Controllers
{
    public class WeekGradeController : ShiNengShiHuiControllerBase
    {

        private readonly IWeekGradeAppService _weekGradeAppService;
        private readonly ITeacherAppService _teacherAppService;

        public WeekGradeController(IWeekGradeAppService weekGradeAppService,
            ITeacherAppService teacherAppService)
        {
            _weekGradeAppService = weekGradeAppService;
            _teacherAppService = teacherAppService;
        }

        #region WeekGrade
        // GET: WeekGrade
        public ActionResult Index()
        {
            #region 初始数据
            List<SelectListItem> schoolyearList = new List<SelectListItem>();
            for (int i = 2017; i < 2017 + 6; i++)
            {
                schoolyearList.Add(new SelectListItem() { Text = i + "学年", Value = i.ToString() });
            }

            List<SelectListItem> semesterList = new List<SelectListItem>();
            semesterList.Add(new SelectListItem() { Text = "第一学期", Value = 1.ToString() });
            semesterList.Add(new SelectListItem() { Text = "第二学期", Value = 2.ToString() });

            List<SelectListItem> weekList = new List<SelectListItem>();
            for (int i = 1; i < 31; i++)
            {
                weekList.Add(new SelectListItem() { Text = "第" + i + "周", Value = i.ToString() });
            }

            List<SelectListItem> groupId = new List<SelectListItem>();
            for (int i = 1; i < 5; i++)
            {
                groupId.Add(new SelectListItem() { Text = i.ToString(), Value = i.ToString() });
            }

            ViewBag.SchoolYear = schoolyearList;
            ViewBag.Semester = semesterList;
            ViewBag.Week = weekList;
            ViewBag.GroupId = groupId;
            #endregion

            return View();
        }

        #region 新增
        [HttpGet]
        public ActionResult WeekGradeCreate(int? group)
        {
            #region 初始数据
            List<SelectListItem> schoolyearList = new List<SelectListItem>();
            for (int i = 2017; i < 2017 + 6; i++)
            {
                schoolyearList.Add(new SelectListItem() { Text = i + "学年", Value = i.ToString() });
            }

            List<SelectListItem> semesterList = new List<SelectListItem>();
            semesterList.Add(new SelectListItem() { Text = "第一学期", Value = 1.ToString() });
            semesterList.Add(new SelectListItem() { Text = "第二学期", Value = 2.ToString() });

            List<SelectListItem> weekList = new List<SelectListItem>();
            for (int i = 1; i < 31; i++)
            {
                weekList.Add(new SelectListItem() { Text = "第" + i + "周", Value = i.ToString() });
            }

            ViewBag.SchoolYear = schoolyearList;
            ViewBag.Semester = semesterList;
            ViewBag.Week = weekList;
            #endregion

            if (group == null)
            {
                group = 1;
            }
            ViewData["GroupId"] = group;
            var students = _teacherAppService.ShowPageStudent(new AppServices.TeacherDTO.ShowPageStudentInput() { ShowCount = 50 });
            
            WeekGradeCreateModel result = new WeekGradeCreateModel();
            result.WeekGrades = new List<WeekGrade>();
            int[] grades = new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
            foreach (AppServices.TeacherDTO.ShowStudentOutput item in students.ShowStudentOutputs)
            {
                if (item.Group == group)
                {
                    result.WeekGrades.Add(new WeekGrade() { SID = item.Id, Name = item.Name, Grades = grades });
                }
            }
            return View(result);
        }

        [HttpPost]
        public ActionResult WeekGradeCreate(WeekGradeCreateModel model)
        {
            if (ModelState.IsValid)
            {
                var weekGradeCreateInput = new WeekGradeCreateInput() { SchoolYear = model.SchoolYear, Semester = model.Semester, Week = model.Week };
                var grades = model.WeekGrades.Select<WeekGrade, WeekGradeCreate>(m => new AppServices.WeekGradeDTO.WeekGradeCreate() { SID = m.SID, Grades = m.Grades }).ToArray();
                weekGradeCreateInput.StudentWeekGrades = grades;
                _weekGradeAppService.WeekGradeCreate(weekGradeCreateInput);
                return RedirectToAction("Index");
            }
            return this.RefreshParent();
        }
        #endregion

        #region 编辑
        [HttpGet]
        public ActionResult WeekGradeUpdate(string schoolyear, string semester, string week, int? groupId)
        {
            ViewData["GroupId"] = groupId;

            var weekGradeShowInput = new WeekGradeShowInput() { SchoolYear = schoolyear, Semester = semester, Week = week };
            List<WeekGradeShow> weekGradeShows = new List<WeekGradeShow>();
            if (groupId == null)
            { 
                for (int i = 1; i < 5; i++)
                {
                    weekGradeShowInput.GroupId = i;
                    weekGradeShows.AddRange(_weekGradeAppService.WeekGradeShow(weekGradeShowInput).WeekGradeShows);
                }
            }
            else
            {
                weekGradeShowInput.GroupId = (int)groupId;
                var weekGrades = _weekGradeAppService.WeekGradeShow(weekGradeShowInput);
                if (weekGrades!=null)
                {
                    weekGradeShows.AddRange(weekGrades.WeekGradeShows);
                    ViewData["IsWell"] = weekGrades.IsWellGroup;
                }
            }

            if (weekGradeShows.Count <= 0)
            {
                return RedirectToAction("Index");
            }

            var model = new WeekGradeUpdateModel() { SchoolYear = schoolyear, Semester = semester, Week = week };
            model.WeekGrades = weekGradeShows.Select(m => new WeekGrade() { Id = m.Id, Name = m.Name, Grades = m.Grades }).ToList();
            return View(model);
        }

        [HttpPost]
        public ActionResult WeekGradeUpdate(WeekGradeUpdateModel model)
        {
            if (ModelState.IsValid)
            {
                var weekGradeUpdateInput = new WeekGradeUpdateInput();
                weekGradeUpdateInput.StudentWeekGrades = model.WeekGrades.Select(m => new WeekGradeUpdate() { Id = m.Id, Grades = m.Grades }).ToArray();
                _weekGradeAppService.WeekGradeUpdate(weekGradeUpdateInput);
                return RedirectToAction("Index");
            }
            return this.RefreshParent();
        }
        #endregion

        #region 展示
        public ActionResult WeekGradeShow(String SchoolYear, String Semester, String Week, int? GroupId)
        {
            #region 初始数据
            List<SelectListItem> schoolyearList = new List<SelectListItem>();
            for (int i = 2017; i < 2017 + 6; i++)
            {
                schoolyearList.Add(new SelectListItem() { Text = i + "学年", Value = i.ToString() });
            }

            List<SelectListItem> semesterList = new List<SelectListItem>();
            semesterList.Add(new SelectListItem() { Text = "第一学期", Value = 1.ToString() });
            semesterList.Add(new SelectListItem() { Text = "第二学期", Value = 2.ToString() });

            List<SelectListItem> weekList = new List<SelectListItem>();
            for (int i = 1; i < 31; i++)
            {
                weekList.Add(new SelectListItem() { Text = "第" + i + "周", Value = i.ToString() });
            }

            List<SelectListItem> groupId = new List<SelectListItem>();
            for (int i = 1; i < 5; i++)
            {
                groupId.Add(new SelectListItem() { Text = "第" + i + "组", Value = i.ToString() });
            }

            ViewBag.SchoolYear = schoolyearList;
            ViewBag.Semester = semesterList;
            ViewBag.Week = weekList;
            ViewBag.GroupId = groupId;
            #endregion

            if (SchoolYear == null || Semester == null || Week == null || GroupId == null)
            {
                return View();
            }

            var grades = _weekGradeAppService.WeekGradeShow(new WeekGradeShowInput() { SchoolYear = SchoolYear, Semester = Semester, Week = Week, GroupId = (int)GroupId });
            if (grades == null || grades.WeekGradeShows.Length <= 0)
            {

            }
            else
            {
                ViewBag.Data = grades.WeekGradeShows.ToList();
                ViewData["IsWell"] = grades.IsWellGroup;
                ViewData["g"] = GroupId;
                ViewData["y"] = SchoolYear;
                ViewData["s"] = Semester;
                ViewData["w"] = Week;
            }
            return View();
        }
        #endregion
        #endregion

        #region 优秀小组
        public ActionResult GroupWeekIndex()
        {
            var result = _weekGradeAppService.GroupWeekGradeShow(new GroupWeekGradeShowInput());
            if (result.GroupWeekGradeShows.Length <= 0)
            {

            }
            else
            {
                ViewBag.Data = result.GroupWeekGradeShows.ToList();
            }
            return View();
        }

        #region 添加
        [HttpGet]
        public ActionResult GroupWeekCreate()
        {
            #region 初始数据
            List<SelectListItem> schoolyearList = new List<SelectListItem>();
            for (int i = 2017; i < 2017 + 6; i++)
            {
                schoolyearList.Add(new SelectListItem() { Text = i + "学年", Value = i.ToString() });
            }

            List<SelectListItem> semesterList = new List<SelectListItem>();
            semesterList.Add(new SelectListItem() { Text = "第一学期", Value = 1.ToString() });
            semesterList.Add(new SelectListItem() { Text = "第二学期", Value = 2.ToString() });

            List<SelectListItem> weekList = new List<SelectListItem>();
            for (int i = 1; i < 31; i++)
            {
                weekList.Add(new SelectListItem() { Text = "第" + i + "周", Value = i.ToString() });
            }

            List<SelectListItem> groupId = new List<SelectListItem>();
            for (int i = 1; i < 5; i++)
            {
                groupId.Add(new SelectListItem() { Text = "第" + i + "组", Value = i.ToString() });
            }

            ViewBag.SchoolYear = schoolyearList;
            ViewBag.Semester = semesterList;
            ViewBag.Week = weekList;
            ViewBag.GroupId = groupId;
            #endregion
            var model = new GroupWeekGradeCreateInput();
            return View(model);
        }

        [HttpPost]
        public ActionResult GroupWeekCreate(GroupWeekGradeCreateInput model)
        {
            if (ModelState.IsValid)
            {
                _weekGradeAppService.GroupWeekGradeCreate(model);
            }
            return this.RefreshParent();
        }
        #endregion

        #region 编辑
        [HttpGet]
        public ActionResult GroupWeekUpdate(long id)
        {
            var GroupGrade = _weekGradeAppService.GroupWeekGradeShow(new GroupWeekGradeShowInput() { Id = id });
            if (GroupGrade == null)
            {
                return this.RefreshParent();
            }

            var model = new GroupWeekGradeUpdate() { Id = GroupGrade.GroupWeekGradeShows[0].Id, IsWell = GroupGrade.GroupWeekGradeShows[0].IsWell };
            ViewData["SchoolYear"] = GroupGrade.GroupWeekGradeShows[0].Date.SchoolYear;
            ViewData["Semester"] = GroupGrade.GroupWeekGradeShows[0].Date.Semester;
            ViewData["Week"] = GroupGrade.GroupWeekGradeShows[0].Date.Week;
            ViewData["GroupId"] = GroupGrade.GroupWeekGradeShows[0].Group;
            return View(model);
        }

        [HttpPost]
        public ActionResult GroupWeekUpdate(GroupWeekGradeUpdate model)
        {
            if (ModelState.IsValid)
            {
                _weekGradeAppService.GroupWeekGradeUpdate(model);
            }
            return this.RefreshParent();
        }
        #endregion
        #endregion
    }
}