﻿using Abp.Web.Mvc.Authorization;
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
            if (pageIndex == null || !ModelState.IsValid)
            {
                result = _teacherAppService.ShowPageStudent(new ShowPageStudentInput());
            }
            else
            {
                result = _teacherAppService.ShowPageStudent(new ShowPageStudentInput() { PageIndex = (int)pageIndex });
            }

            if (result.ShowStudentOutputs.Length <= 0)
            {
                return View();
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

        #region 成绩模块 老版本
        //public ActionResult GradeIndex(int? pageIndex, string selectd, DateTime? dateTime)
        //{
        //    #region 初始化数据
        //    List<SelectListItem> selectList = new List<SelectListItem>();
        //    selectList.Add(new SelectListItem() { Value = "NULL", Text = "不选择任何条件" });
        //    selectList.Add(new SelectListItem() { Value = "Month", Text = "按月查找" });
        //    selectList.Add(new SelectListItem() { Value = "Day", Text = "按天查找" });

        //    ViewBag.SelectList = selectList;
        //    #endregion

        //    ShowPageGradeOutput result;
        //    if (pageIndex == null || pageIndex <= 0)
        //    {
        //        pageIndex = 1;
        //    }

        //    if (dateTime == null)
        //    {
        //        dateTime = DateTime.Now;
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        switch (selectd)
        //        {
        //            case "NULL":
        //                result = _teacherAppService.ShowPageGrade(new ShowPageGradeInput() { PageIndex = (int)pageIndex, ScreenCondition = ScreenEnum.No });
        //                break;
        //            case "Month":
        //                result = _teacherAppService.ShowPageGrade(new ShowPageGradeInput() { PageIndex = (int)pageIndex, ScreenCondition = ScreenEnum.Month, DateTime = (DateTime)dateTime });
        //                break;
        //            case "Day":
        //                result = _teacherAppService.ShowPageGrade(new ShowPageGradeInput() { PageIndex = (int)pageIndex, ScreenCondition = ScreenEnum.Day, DateTime = (DateTime)dateTime });
        //                break;
        //            default:
        //                result = _teacherAppService.ShowPageGrade(new ShowPageGradeInput() { PageIndex = (int)pageIndex, ScreenCondition = ScreenEnum.No });
        //                break;
        //        }
        //    }
        //    else
        //    {
        //        result = _teacherAppService.ShowPageGrade(new ShowPageGradeInput() { ScreenCondition = ScreenEnum.No });
        //    }

        //    if (result.ShowGradeOutputs.Length <= 0)
        //    {
        //        return View();
        //    }

        //    ViewData["pageIndex"] = result.PageIndex;
        //    ViewData["pageCount"] = result.PageCount;

        //    ViewData["selectd"] = selectd == null ? "" : selectd;
        //    ViewData["dateTime"] = dateTime == null ? "" : dateTime.ToString();

        //    var listmodel = result.ShowGradeOutputs.Select<ShowGradeOutput, GradeResultViewModel>(m => ObjectMapper.Map<GradeResultViewModel>(m));
        //    return View(listmodel);
        //}

        //#region 新增
        //[HttpGet]
        //public ActionResult GradeCreate()
        //{
        //    #region 初始化的数据
        //    ShowPageStudentOutput students = _teacherAppService.ShowPageStudent(new ShowPageStudentInput() { PageIndex = 1, ShowCount = 100 });
        //    List<SelectListItem> studentList = new List<SelectListItem>();
        //    students.ShowStudentOutputs.ToList().ForEach(m => studentList.Add(new SelectListItem { Value = m.Id.ToString(), Text = m.Name }));
        //    ViewBag.StudentList = studentList;

        //    List<SelectListItem> gradeList = new List<SelectListItem>();
        //    for (int i = 10; i > 0; i--)
        //    {
        //        gradeList.Add(new SelectListItem { Value = i.ToString(), Text = i.ToString() });
        //    }
        //    ViewBag.GradeList = gradeList;


        //    List<SelectListItem> schoolYearList = new List<SelectListItem>();
        //    schoolYearList.Add(new SelectListItem() { Value = (Clock.Now.Year - 1).ToString(), Text = (Clock.Now.Year - 1).ToString() });
        //    schoolYearList.Add(new SelectListItem() { Value = Clock.Now.Year.ToString(), Text = Clock.Now.Year.ToString() });
        //    schoolYearList.Add(new SelectListItem() { Value = (Clock.Now.Year + 1).ToString(), Text = (Clock.Now.Year + 1).ToString() });
        //    ViewBag.SchoolYearList = schoolYearList;

        //    List<SelectListItem> semesterList = new List<SelectListItem>();
        //    semesterList.Add(new SelectListItem() { Value = "1", Text = "第一学期" });
        //    semesterList.Add(new SelectListItem() { Value = "2", Text = "第二学期" });
        //    ViewBag.SemesterLiset = semesterList;

        //    List<SelectListItem> weekList = new List<SelectListItem>();
        //    for (int i = 1; i < 31; i++)
        //    {
        //        weekList.Add(new SelectListItem() { Value = i.ToString(), Text = i.ToString() });
        //    }
        //    ViewBag.WeekList = weekList;
        //    #endregion

        //    return View();
        //}

        //[HttpPost]
        //public ActionResult GradeCreate(GradeCreateViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _teacherAppService.CreateGrade(ObjectMapper.Map<CreateGradeInput>(model));
        //        return RefreshParent();
        //    }

        //    #region 初始化的数据
        //    List<List<SelectListItem>> gradeSelect = new List<List<SelectListItem>>();
        //    for (int i = 0; i < 10; i++)
        //    {
        //        List<SelectListItem> gradeSelectItem = new List<SelectListItem>();
        //        for (int j = 10; j >= 0; j--)
        //        {
        //            if (j == model.Grades[i])
        //            {
        //                gradeSelectItem.Add(new SelectListItem() { Value = j.ToString(), Text = j.ToString(), Selected = true });
        //            }
        //            else
        //            {
        //                gradeSelectItem.Add(new SelectListItem() { Value = j.ToString(), Text = j.ToString() });
        //            }
        //        }
        //        gradeSelect.Add(gradeSelectItem);
        //    }
        //    ViewBag.GradeList = gradeSelect;


        //    List<SelectListItem> schoolYearList = new List<SelectListItem>();
        //    schoolYearList.Add(new SelectListItem() { Value = (Clock.Now.Year - 1).ToString(), Text = (Clock.Now.Year - 1).ToString() });
        //    schoolYearList.Add(new SelectListItem() { Value = Clock.Now.Year.ToString(), Text = Clock.Now.Year.ToString() });
        //    schoolYearList.Add(new SelectListItem() { Value = (Clock.Now.Year + 1).ToString(), Text = (Clock.Now.Year + 1).ToString() });
        //    foreach (SelectListItem item in schoolYearList)
        //    {
        //        if (item.Value.Equals(model.SchoolYead.ToString()))
        //        {
        //            item.Selected = true;
        //        }
        //    }
        //    ViewBag.SchoolYearList = schoolYearList;

        //    List<SelectListItem> semesterList = new List<SelectListItem>();
        //    semesterList.Add(new SelectListItem() { Value = "1", Text = "第一学期" });
        //    semesterList.Add(new SelectListItem() { Value = "2", Text = "第二学期" });
        //    foreach (SelectListItem item in semesterList)
        //    {
        //        if (item.Value.Equals(model.Semester.ToString()))
        //        {
        //            item.Selected = true;
        //        }
        //    }
        //    ViewBag.SemesterLiset = semesterList;

        //    List<SelectListItem> weekList = new List<SelectListItem>();
        //    for (int i = 1; i < 31; i++)
        //    {
        //        if (i == model.Week)
        //        {
        //            weekList.Add(new SelectListItem() { Value = i.ToString(), Text = i.ToString(), Selected = true });
        //        }
        //        else
        //        {
        //            weekList.Add(new SelectListItem() { Value = i.ToString(), Text = i.ToString() });
        //        }
        //    }
        //    ViewBag.WeekList = weekList;
        //    #endregion
        //    return View(model);
        //}
        //#endregion

        //#region 编辑
        //[HttpGet]
        //public ActionResult GradeEdit(int id)
        //{
        //    ShowGradeOutput gradeOutput = _teacherAppService.ShowGrade(new ShowGradeInput() { Id = id });
        //    GradeEditViewModel result = ObjectMapper.Map<GradeEditViewModel>(gradeOutput);

        //    #region 初始化的数据
        //    List<List<SelectListItem>> gradeSelect = new List<List<SelectListItem>>();
        //    for (int i = 0; i < 10; i++)
        //    {
        //        List<SelectListItem> gradeSelectItem = new List<SelectListItem>();
        //        for (int j = 10; j >= 0; j--)
        //        {
        //            if (j == result.Grades[i])
        //            {
        //                gradeSelectItem.Add(new SelectListItem() { Value = j.ToString(), Text = j.ToString(), Selected = true });
        //            }
        //            else
        //            {
        //                gradeSelectItem.Add(new SelectListItem() { Value = j.ToString(), Text = j.ToString() });
        //            }
        //        }
        //        gradeSelect.Add(gradeSelectItem);
        //    }
        //    ViewBag.GradeList = gradeSelect;


        //    List<SelectListItem> schoolYearList = new List<SelectListItem>();
        //    schoolYearList.Add(new SelectListItem() { Value = (Clock.Now.Year - 1).ToString(), Text = (Clock.Now.Year - 1).ToString() });
        //    schoolYearList.Add(new SelectListItem() { Value = Clock.Now.Year.ToString(), Text = Clock.Now.Year.ToString() });
        //    schoolYearList.Add(new SelectListItem() { Value = (Clock.Now.Year + 1).ToString(), Text = (Clock.Now.Year + 1).ToString() });
        //    foreach (SelectListItem item in schoolYearList)
        //    {
        //        if (item.Value.Equals(result.SchoolYead.ToString()))
        //        {
        //            item.Selected = true;
        //        }
        //    }
        //    ViewBag.SchoolYearList = schoolYearList;

        //    List<SelectListItem> semesterList = new List<SelectListItem>();
        //    semesterList.Add(new SelectListItem() { Value = "1", Text = "第一学期" });
        //    semesterList.Add(new SelectListItem() { Value = "2", Text = "第二学期" });
        //    foreach (SelectListItem item in semesterList)
        //    {
        //        if (item.Value.Equals(result.Semester.ToString()))
        //        {
        //            item.Selected = true;
        //        }
        //    }
        //    ViewBag.SemesterLiset = semesterList;

        //    List<SelectListItem> weekList = new List<SelectListItem>();
        //    for (int i = 1; i < 31; i++)
        //    {
        //        if (i == result.Week)
        //        {
        //            weekList.Add(new SelectListItem() { Value = i.ToString(), Text = i.ToString(), Selected = true });
        //        }
        //        else
        //        {
        //            weekList.Add(new SelectListItem() { Value = i.ToString(), Text = i.ToString() });
        //        }
        //    }
        //    ViewBag.WeekList = weekList;
        //    #endregion

        //    return View(result);
        //}

        //[HttpPost]
        //public ActionResult GradeEdit(GradeEditViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _teacherAppService.UpdateGrade(ObjectMapper.Map<UpdateGradeInput>(model));
        //        return RefreshParent();
        //    }

        //    #region 初始化的数据
        //    List<List<SelectListItem>> gradeSelect = new List<List<SelectListItem>>();
        //    for (int i = 0; i < 10; i++)
        //    {
        //        List<SelectListItem> gradeSelectItem = new List<SelectListItem>();
        //        for (int j = 10; j >= 0; j--)
        //        {
        //            if (j == model.Grades[i])
        //            {
        //                gradeSelectItem.Add(new SelectListItem() { Value = j.ToString(), Text = j.ToString(), Selected = true });
        //            }
        //            else
        //            {
        //                gradeSelectItem.Add(new SelectListItem() { Value = j.ToString(), Text = j.ToString() });
        //            }
        //        }
        //        gradeSelect.Add(gradeSelectItem);
        //    }
        //    ViewBag.GradeList = gradeSelect;


        //    List<SelectListItem> schoolYearList = new List<SelectListItem>();
        //    schoolYearList.Add(new SelectListItem() { Value = (Clock.Now.Year - 1).ToString(), Text = (Clock.Now.Year - 1).ToString() });
        //    schoolYearList.Add(new SelectListItem() { Value = Clock.Now.Year.ToString(), Text = Clock.Now.Year.ToString() });
        //    schoolYearList.Add(new SelectListItem() { Value = (Clock.Now.Year + 1).ToString(), Text = (Clock.Now.Year + 1).ToString() });
        //    foreach (SelectListItem item in schoolYearList)
        //    {
        //        if (item.Value.Equals(model.SchoolYead.ToString()))
        //        {
        //            item.Selected = true;
        //        }
        //    }
        //    ViewBag.SchoolYearList = schoolYearList;

        //    List<SelectListItem> semesterList = new List<SelectListItem>();
        //    semesterList.Add(new SelectListItem() { Value = "1", Text = "第一学期" });
        //    semesterList.Add(new SelectListItem() { Value = "2", Text = "第二学期" });
        //    foreach (SelectListItem item in semesterList)
        //    {
        //        if (item.Value.Equals(model.Semester.ToString()))
        //        {
        //            item.Selected = true;
        //        }
        //    }
        //    ViewBag.SemesterLiset = semesterList;

        //    List<SelectListItem> weekList = new List<SelectListItem>();
        //    for (int i = 1; i < 31; i++)
        //    {
        //        if (i == model.Week)
        //        {
        //            weekList.Add(new SelectListItem() { Value = i.ToString(), Text = i.ToString(), Selected = true });
        //        }
        //        else
        //        {
        //            weekList.Add(new SelectListItem() { Value = i.ToString(), Text = i.ToString() });
        //        }
        //    }
        //    ViewBag.WeekList = weekList;
        //    #endregion

        //    return View(model);
        //}
        //#endregion

        //#region 删除
        //[HttpPost]
        //public ActionResult GradeDelete(int[] id)
        //{
        //    for (int i = 0, length = id.Length; i < length; i++)
        //    {
        //        _teacherAppService.DeleteGrade(new DeleteGradeInput() { Id = id[i] });
        //    }
        //    return RedirectToAction("GradeIndex");
        //}
        //#endregion 

        //#region 表格下载
        //public FileResult GradeExcelDown()
        //{
        //    GradeExcelDownOutput gradeExcelOutput = _excelAppService.GradeExcelDown();
        //    using (gradeExcelOutput.ExcelData)
        //    {
        //        return File(gradeExcelOutput.ExcelData.ToArray(), "application/octet-stream", "成绩导入模板.xls");
        //    }
        //}
        //#endregion

        //#region Excel批量导入
        //public ActionResult GradeCereateOfExcel(HttpPostedFileBase file)
        //{
        //    if (file == null)
        //    {
        //        return this.RedirectAjax("Failure", "没有文件", null, null);
        //    }
        //    else if (!Path.GetExtension(file.FileName).Equals(".xls"))
        //    {
        //        return this.RedirectAjax("Failure", "请选择excel", null, null);
        //    }
        //    else if (file.ContentLength > 0)
        //    {
        //        ReturnVal returnVal = _teacherAppService.CreateGradeRange(new CreateGradeRangeInput() { DataStream = file.InputStream });
        //        if (returnVal.Data != null)
        //        {
        //            StringBuilder returnDataInfo = new StringBuilder();
        //            foreach (string item in (List<string>)returnVal.Data)
        //            {
        //                returnDataInfo.Append(item);
        //                returnDataInfo.Append("  ");
        //            }
        //            returnVal.Message += returnDataInfo.ToString();
        //        }

        //        switch (returnVal.Statu)
        //        {
        //            case ReturnStatu.Err:
        //                return this.RedirectAjax("Failure", returnVal.Message, null, null);
        //            case ReturnStatu.Failure:
        //                return this.RedirectAjax("Failure", returnVal.Message, null, null);
        //            case ReturnStatu.Success:
        //                return this.RedirectAjax("Success", returnVal.Message, null, null);
        //            default:
        //                return this.RedirectAjax("Failure", "内部出错", null, null);
        //        }
        //    }
        //    else
        //    {
        //        return this.RedirectAjax("Failure", null, null, null);
        //    }
        //}
        //#endregion 
        #endregion

        #region 成绩模块 新版本
        public ActionResult GradeIndex()
        {
            #region 初始数据
            var classCurrent = _teacherAppService.GetCurrentClass();
            int classInTimeYear = classCurrent.ClassIntime.Year;

            List<SelectListItem> schoolyearList = new List<SelectListItem>();
            for (int i = classInTimeYear; i < classInTimeYear + 6; i++)
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
            var classCurrent = _teacherAppService.GetCurrentClass();
            int classInTimeYear = classCurrent.ClassIntime.Year;

            List<SelectListItem> schoolyearList = new List<SelectListItem>();
            for (int i = classInTimeYear; i < classInTimeYear + 6; i++)
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
            var students = _teacherAppService.ShowGroupStudent(new ShowGroupStudentInput() { Group = (int)group });

            WeekGradeCreateModel result = new WeekGradeCreateModel();
            result.WeekGrades = new List<WeekGrade>();
            int[] grades = new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
            foreach (ShowStudentOutput item in students.showStudentOutput)
            {
                result.WeekGrades.Add(new WeekGrade() { SID = item.Id, Name = item.Name, Grades = grades });
            }
            return View(result);
        }

        [HttpPost]
        public ActionResult WeekGradeCreate(WeekGradeCreateModel model)
        {
            if (ModelState.IsValid)
            {
                var weekGradeCreateInput = new WeekGradeCreateInput() { SchoolYear = model.SchoolYear, Semester = model.Semester, Week = model.Week };
                var grades = model.WeekGrades.Select<WeekGrade, WeekGradeCreate>(m => new WeekGradeCreate() { StudentId = m.SID, Grades = m.Grades }).ToArray();
                weekGradeCreateInput.StudentWeekGrades = grades;
                _teacherAppService.WeekGradeCreate(weekGradeCreateInput);
                return RedirectToAction("GradeIndex");
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
                    weekGradeShows.AddRange(_teacherAppService.WeekGradeShow(weekGradeShowInput).WeekGradeShows);
                }
            }
            else
            {
                weekGradeShowInput.GroupId = (int)groupId;
                var weekGrades = _teacherAppService.WeekGradeShow(weekGradeShowInput);
                if (weekGrades != null)
                {
                    weekGradeShows.AddRange(weekGrades.WeekGradeShows);
                    ViewData["IsWell"] = weekGrades.IsWellGroup;
                }
            }

            if (weekGradeShows.Count <= 0)
            {
                return RedirectToAction("GradeIndex");
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
                _teacherAppService.WeekGradeUpdate(weekGradeUpdateInput);
                return RedirectToAction("GradeIndex");
            }
            return this.RefreshParent();
        }
        #endregion

        #region 展示
        public ActionResult WeekGradeShow(String SchoolYear, String Semester, String Week, int? GroupId)
        {
            #region 初始数据
            var classCurrent = _teacherAppService.GetCurrentClass();
            int classInTimeYear = classCurrent.ClassIntime.Year;

            List<SelectListItem> schoolyearList = new List<SelectListItem>();
            for (int i = classInTimeYear; i < classInTimeYear + 6; i++)
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

            var grades = _teacherAppService.WeekGradeShow(new WeekGradeShowInput() { SchoolYear = SchoolYear, Semester = Semester, Week = Week, GroupId = (int)GroupId });
            if (grades == null || grades.WeekGradeShows.Length <= 0)
            {
                return View();
            }
            else
            {
                WeekGradeShowModel weekGradeCreateShowModel = new WeekGradeShowModel()
                {
                    WeekGrades = grades.WeekGradeShows.Select<WeekGradeShow, WeekGrade>(m =>
                    {
                        return new WeekGrade()
                        {
                            Id = m.Id,
                            Name = m.Name,
                            Grades = m.Grades
                        };
                    }).ToList()
                };
                ViewData["IsWell"] = grades.IsWellGroup;
                ViewData["g"] = GroupId;
                ViewData["y"] = SchoolYear;
                ViewData["s"] = Semester;
                ViewData["w"] = Week;
                return View(weekGradeCreateShowModel);
            }

        }
        #endregion 
        #endregion

        #endregion

        #region 优秀小组
        public ActionResult GroupWeekGradeIndex(int? pageIndex, string schoolyear, string semester)
        {
            #region 初始数据
            var classCurrent = _teacherAppService.GetCurrentClass();
            int classInTimeYear = classCurrent.ClassIntime.Year;

            List<SelectListItem> schoolyearList = new List<SelectListItem>();
            for (int i = classInTimeYear; i < classInTimeYear + 6; i++)
            {
                schoolyearList.Add(new SelectListItem() { Text = i + "学年", Value = i.ToString() });
            }

            List<SelectListItem> semesterList = new List<SelectListItem>();
            semesterList.Add(new SelectListItem() { Text = "第一学期", Value = 1.ToString() });
            semesterList.Add(new SelectListItem() { Text = "第二学期", Value = 2.ToString() });

            ViewBag.SchoolYear = schoolyearList;
            ViewBag.Semester = semesterList;
            #endregion

            if (String.IsNullOrEmpty(schoolyear) || String.IsNullOrEmpty(semester))
            {
                return View();
            }

            GroupWeekGradeShowPageOutput result;
            if (pageIndex == null || !ModelState.IsValid)
            {
                result = _teacherAppService.GroupWeekGradeShowPage(new GroupWeekGradeShowPageInput() { SchoolYear = schoolyear, Semester = semester });
            }
            else
            {
                result = _teacherAppService.GroupWeekGradeShowPage(new GroupWeekGradeShowPageInput() { PageIndex = (int)pageIndex, SchoolYear = schoolyear, Semester = semester });
            }

            if (result.groupWeekGrades.Length <= 0)
            {
                return View();
            }

            var listmodel = result.groupWeekGrades.Select<GroupWeekGradeShowOutput, GroupWeekGradeShowModel>(m => ObjectMapper.Map<GroupWeekGradeShowModel>(m)).ToList();
            ViewData["pageIndex"] = result.PageIndex;
            ViewData["pageCount"] = result.PageCount;

            return View(listmodel);
        }

        #region 添加
        [HttpGet]
        public ActionResult GroupWeekCreate()
        {
            #region 初始数据
            var classCurrent = _teacherAppService.GetCurrentClass();
            int classInTimeYear = classCurrent.ClassIntime.Year;

            List<SelectListItem> schoolyearList = new List<SelectListItem>();
            for (int i = classInTimeYear; i < classInTimeYear + 6; i++)
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
                _teacherAppService.GroupWeekGradeCreate(model);
            }
            return this.RefreshParent();
        }
        #endregion

        #region 编辑
        [HttpGet]
        public ActionResult GroupWeekUpdate(long id)
        {
            var GroupGrade = _teacherAppService.GroupWeekGradeShow(new GroupWeekGradeShowInput() { Id = id });
            if (GroupGrade == null)
            {
                return this.RefreshParent();
            }

            var model = new GroupWeekGradeUpdate() { Id = GroupGrade.Id, IsWell = GroupGrade.IsWell };
            ViewData["SchoolYear"] = GroupGrade.Date.SchoolYear;
            ViewData["Semester"] = GroupGrade.Date.Semester;
            ViewData["Week"] = GroupGrade.Date.Week;
            ViewData["GroupId"] = GroupGrade.Group;
            return View(model);
        }

        [HttpPost]
        public ActionResult GroupWeekUpdate(GroupWeekGradeUpdate model)
        {
            if (ModelState.IsValid)
            {
                _teacherAppService.GroupWeekGradeUpdate(model);
            }
            return this.RefreshParent();
        }
        #endregion

        #endregion

        #region 奖项模块
        public ActionResult PrizeIndex(int? pageIndex, string selectd, DateTime? dateTime)
        {
            #region 初始化数据
            List<SelectListItem> computSelectList = new List<SelectListItem>();
            computSelectList.Add(new SelectListItem() { Value = "TianMoFanSheng", Text = "计算天模范生", Selected = true });
            computSelectList.Add(new SelectListItem() { Value = "ZhouMoFanSheng", Text = "计算周模范生" });
            computSelectList.Add(new SelectListItem() { Value = "YueMoFanSheng", Text = "计算月模范生" });
            computSelectList.Add(new SelectListItem() { Value = "XiaoMoFanSheng", Text = "计算校模范生" });
            ViewBag.ComputSelect = computSelectList;

            List<SelectListItem> selectList = new List<SelectListItem>();
            selectList.Add(new SelectListItem() { Value = "NULL", Text = "不选择任何条件" });
            selectList.Add(new SelectListItem() { Value = "Month", Text = "按月查找" });
            selectList.Add(new SelectListItem() { Value = "Day", Text = "按天查找" });
            ViewBag.SelectList = selectList;
            #endregion

            ShowPagePrizeOutput prizes;
            if (pageIndex == null || pageIndex <= 0)
            {
                pageIndex = 1;
            }

            if (dateTime == null)
            {
                dateTime = DateTime.Now;
            }

            switch (selectd)
            {
                case "NULL":
                    prizes = _teacherAppService.ShowPagePrize(new ShowPagePrizeInput() { PageIndex = (int)pageIndex, ScreenCondition = ScreenEnum.No });
                    break;
                case "Month":
                    prizes = _teacherAppService.ShowPagePrize(new ShowPagePrizeInput() { PageIndex = (int)pageIndex, ScreenCondition = ScreenEnum.Month, DateTime = (DateTime)dateTime });
                    break;
                case "Day":
                    prizes = _teacherAppService.ShowPagePrize(new ShowPagePrizeInput() { PageIndex = (int)pageIndex, ScreenCondition = ScreenEnum.Day, DateTime = (DateTime)dateTime });
                    break;
                default:
                    prizes = _teacherAppService.ShowPagePrize(new ShowPagePrizeInput() { PageIndex = (int)pageIndex, ScreenCondition = ScreenEnum.No });
                    break;
            }

            if (prizes.ShowPrizeOutputs.Length <= 0)
            {
                return View();
            }

            List<PrizeResultViewModel> models = prizes.ShowPrizeOutputs.Select(m => ObjectMapper.Map<PrizeResultViewModel>(m)).ToList();

            ViewData["pageIndex"] = prizes.PageIndex;
            ViewData["pageCount"] = prizes.PageCount;

            ViewData["selectd"] = selectd == null ? "" : selectd;
            ViewData["dateTime"] = dateTime == null ? "" : dateTime.ToString();

            return View(models);
        }

        public ActionResult PrizeComput(DateTime time, string computSelect, int? schoolYear, int? semester)
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
                    if (schoolYear == null || semester == null)
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