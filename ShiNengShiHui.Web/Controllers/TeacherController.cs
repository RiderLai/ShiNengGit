using Abp.Web.Mvc.Authorization;
using ShiNengShiHui.AppServices;
using ShiNengShiHui.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShiNengShiHui.Web.Models.Teacher.Student;
using ShiNengShiHui.Web.Models.Teacher.Grade;
using Abp.Timing;
using System.IO;
using ShiNengShiHui.AppServices.Return;
using System.Text;
using ShiNengShiHui.AppServices.TeacherDTO;
using ShiNengShiHui.AppServices.ExcelDTO;
using ShiNengShiHui.Web.Models.Teacher.Prize;

namespace ShiNengShiHui.Web.Controllers
{
    [AbpMvcAuthorize(permissions: PermissionNames.Pages_Users_Teacher)]
    public class TeacherController : ShiNengShiHuiControllerBase
    {
        private readonly ITeacherAppService _teacherAppService;
        private readonly IExcelAppService _excelAppService;

        public object PrizeReusltViewModel { get; private set; }

        public TeacherController(ITeacherAppService teacherAppService,
            IExcelAppService excelAppService)
        {
            _teacherAppService = teacherAppService;
            _excelAppService = excelAppService;
        }

        // GET: Teacher
        public ActionResult Index()
        {
            return View();
        }

        #region 学生模块

        public ActionResult StudentIndex(int? pageIndex)
        {
            ShowPageStudentOutput result;
            if (pageIndex==null||!ModelState.IsValid)
            {
                result= _teacherAppService.ShowPageStudent(new ShowPageStudentInput());
            }
            else
            {
                result = _teacherAppService.ShowPageStudent(new ShowPageStudentInput() { PageIndex=(int)pageIndex});
            }

            ViewData["pageIndex"] = result.PageIndex;
            ViewData["pageCount"] = result.PageCount;

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

        #region 表格下载
        public FileResult StudentExcelDown()
        {
            StudentExcelDownOutput studentExcelDownOutput = _excelAppService.StudentExcelDown();
            using (studentExcelDownOutput.ExcelData)
            {
                return File(studentExcelDownOutput.ExcelData.ToArray(), "application/octet-stream", "学生登记表.xls");
            }
        }
        #endregion

        #region Excel批量导入
        public ActionResult StudentsCereateOfExcel(HttpPostedFileBase file)
        {
            if (file == null)
            {
                return this.RedirectAjax("Failure", "没有文件", null, null);
            }
            else if (!Path.GetExtension(file.FileName).Equals(".xls"))
            {
                return this.RedirectAjax("Failure", "请选择excel", null, null);
            }
            else if (file.ContentLength > 0)
            {
                ReturnVal returnVal = _teacherAppService.CreateStudentRange(new CreateStudentRangeInput() { DataStream = file.InputStream });
                if (returnVal.Data != null)
                {
                    StringBuilder returnDataInfo = new StringBuilder();
                    foreach (string item in (List<string>)returnVal.Data)
                    {
                        returnDataInfo.Append(item);
                        returnDataInfo.Append("  ");
                    }
                    returnVal.Message += returnDataInfo.ToString();
                }

                switch (returnVal.Statu)
                {
                    case ReturnStatu.Err:
                        return this.RedirectAjax("Failure", returnVal.Message, null, null);
                    case ReturnStatu.Failure:
                        return this.RedirectAjax("Failure", returnVal.Message, null, null);
                    case ReturnStatu.Success:
                        return this.RedirectAjax("Success", returnVal.Message, null, null);
                    default:
                        return this.RedirectAjax("Failure", "内部出错", null, null);
                }
            }
            else
            {
                return this.RedirectAjax("Failure", null, null, null);
            }
        } 
        #endregion

        #endregion

        #region 成绩模块

        public ActionResult GradeIndex(int? pageIndex)
        {
            ShowPageGradeOutput result;
            if (pageIndex == null || !ModelState.IsValid)
            {
                result = _teacherAppService.ShowPageGrade(new ShowPageGradeInput());
            }
            else
            {
                result = _teacherAppService.ShowPageGrade(new ShowPageGradeInput() { PageIndex = (int)pageIndex });
            }

            ViewData["pageIndex"] = result.PageIndex;
            ViewData["pageCount"] = result.PageCount;
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

        #region 表格下载
        public FileResult GradeExcelDown()
        {
            GradeExcelOutput gradeExcelOutput = _excelAppService.GradeExcelDown();
            using (gradeExcelOutput.ExcelData)
            {
                return File(gradeExcelOutput.ExcelData.ToArray(), "application/octet-stream", "成绩导入模板.xls");
            }
        }
        #endregion

        #region Excel批量导入
        public ActionResult GradeCereateOfExcel(HttpPostedFileBase file)
        {
            if (file == null)
            {
                return this.RedirectAjax("Failure", "没有文件", null, null);
            }
            else if (!Path.GetExtension(file.FileName).Equals(".xls"))
            {
                return this.RedirectAjax("Failure", "请选择excel", null, null);
            }
            else if (file.ContentLength > 0)
            {
                ReturnVal returnVal = _teacherAppService.CreateGradeRange(new CreateGradeRangeInput() { DataStream = file.InputStream });
                if (returnVal.Data != null)
                {
                    StringBuilder returnDataInfo = new StringBuilder();
                    foreach (string item in (List<string>)returnVal.Data)
                    {
                        returnDataInfo.Append(item);
                        returnDataInfo.Append("  ");
                    }
                    returnVal.Message += returnDataInfo.ToString();
                }

                switch (returnVal.Statu)
                {
                    case ReturnStatu.Err:
                        return this.RedirectAjax("Failure", returnVal.Message, null, null);
                    case ReturnStatu.Failure:
                        return this.RedirectAjax("Failure", returnVal.Message, null, null);
                    case ReturnStatu.Success:
                        return this.RedirectAjax("Success", returnVal.Message, null, null);
                    default:
                        return this.RedirectAjax("Failure", "内部出错", null, null);
                }
            }
            else
            {
                return this.RedirectAjax("Failure", null, null, null);
            }
        }
        #endregion

        #endregion


        #region 奖项模块
        public ActionResult PrizeIndex(int? pageIndex)
        {
            #region 初始化数据
            List<SelectListItem> computSelectList = new List<SelectListItem>();
            computSelectList.Add(new SelectListItem() { Value = "TianMoFanSheng", Text = "计算天模范生", Selected = true });
            computSelectList.Add(new SelectListItem() { Value = "ZhouMoFanSheng", Text = "计算周模范生" });
            computSelectList.Add(new SelectListItem() { Value = "YueMoFanSheng", Text = "计算月模范生" });
            computSelectList.Add(new SelectListItem() { Value = "XiaoMoFanSheng", Text = "计算校模范生" });
            ViewBag.ComputSelect = computSelectList;
            #endregion

            if (pageIndex == null)
            {
                pageIndex = 1;
            }

            var prizes = _teacherAppService.ShowPagePrize(new ShowPagePrizeInput() { PageIndex = (int)pageIndex });
            if (prizes.ShowPrizeOutputs.Length <= 0)
            {
                return View();
            }

            List<PrizeResultViewModel> models = prizes.ShowPrizeOutputs.Select(m => ObjectMapper.Map<PrizeResultViewModel>(m)).ToList();

            ViewData["pageIndex"] = prizes.PageIndex;
            ViewData["pageCount"] = prizes.PageCount;

            return View(models);
        }

        public ActionResult PrizeComput(DateTime time, string computSelect,int? schoolYear,int? semester)
        {
            switch (computSelect)
            {
                case "TianMoFanSheng":
                    _teacherAppService.PrizeTianMoFanShengComput(new PrizeTianMoFanShengComputInput() { DateTime = time });
                    break;
                case "ZhouMoFanSheng":
                    _teacherAppService.PrizeZhouMoFanShengComput(new PrizeZhouMoFanShengComputInput() { DateTime = time });
                    break;
                case "YueMoFanSheng":
                    _teacherAppService.PrizeYueMoFanShengComput(new PrizeYueMoFanShengComputInput() { DateTime = time });
                    break;
                case "XiaoMoFanSheng":
                    if (schoolYear==null || semester==null)
                    {
                        break;
                    }
                    _teacherAppService.PrizeXiaoMoFanShengComput(new PrizeXiaoMoFanShengComputInput() { SchoolYear = (int)schoolYear, Semester = (int)semester });
                    break;
            }
            return RedirectToAction("PrizeIndex");
        } 
        #endregion
    }
}