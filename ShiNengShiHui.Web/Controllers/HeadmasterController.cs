//                                                          _ooOoo_
//                                                         o8888888o
//                                                         88" . "88
//                                                         (| -_- |)
//                                                          O\ = /O
//                                                      ____/`---'\____
//                                                    .   ' \\| |// `.
//                                                     / \\||| : |||// \
//                                                   / _||||| -:- |||||- \
//                                                     | | \\\ - /// | |
//                                                   | \_| ''\---/'' | |
//                                                    \ .-\__ `-` ___/-. /
//                                                 ___`. .' /--.--\ `. . __
//                                              ."" '< `.___\_<|>_/___.' >'"".
//                                             | | : `- \`.;`\ _ /`;.`/ - ` : | |
//                                               \ \ `-. \_ __\ /__ _/ .-` / /
//                                       ======`-.____`-.___\_____/___.-`____.-'======
//                                                          `=---='
//
//                                       .............................................
//                                              佛祖保佑             永无BUG
//                                      佛曰:
//                                              写字楼里写字间，写字间里程序员；
//                                              程序人员写程序，又拿程序换酒钱。
//                                              酒醒只在网上坐，酒醉还来网下眠；
//                                              酒醉酒醒日复日，网上网下年复年。
//                                              但愿老死电脑间，不愿鞠躬老板前；
//                                              奔驰宝马贵者趣，公交自行程序员。
//                                              别人笑我忒疯癫，我笑自己命太贱；
//                                              不见满街漂亮妹，哪个归得程序员？

using Abp.Web.Mvc.Authorization;
using ShiNengShiHui.AppServices;
using ShiNengShiHui.AppServices.Headmaster;
using ShiNengShiHui.AppServices.HeadmasterDTO;
using ShiNengShiHui.Authorization;
using ShiNengShiHui.Web.Models.Headmaster.Grade;
using ShiNengShiHui.Web.Models.Headmaster.Prize;
using ShiNengShiHui.Web.Models.Headmaster.Student;
using ShiNengShiHui.Web.Models.Headmaster.Teacher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShiNengShiHui.Web.Controllers
{
    [AbpMvcAuthorize(permissions: PermissionNames.Pages_Users_Headmaster)]
    public class HeadmasterController : ShiNengShiHuiControllerBase
    {
        private IHeadmasterAppService _headmasterAppService;

        public HeadmasterController(IHeadmasterAppService headmasterAppService)
        {
            _headmasterAppService = headmasterAppService;
        }

        // GET: Headmaster
        public ActionResult Index()
        {
            return View();
        }

        #region 教师模块
        public ActionResult TeacherIndex(int? pageIndex)
        {
            if (pageIndex == null)
            {
                pageIndex = 1;
            }

            var teachers = _headmasterAppService.TeacherShowPage(new TeacherShowPageInput() { PageIndex = (int)pageIndex });

            if (teachers.Teachers.Length <= 0)
            {
                return View();
            }

            List<TeacherResultViewModel> models = new List<TeacherResultViewModel>();
            models.AddRange(teachers.Teachers.Select(m => ObjectMapper.Map<TeacherResultViewModel>(m)));

            ViewData["pageIndex"] = teachers.PageIndex;
            ViewData["pageCount"] = teachers.PageCount;

            return View(models);
        }
        #endregion

        #region 学生模块
        public ActionResult StudentIndex(int? pageIndex, int? classId)
        {
            var classes = _headmasterAppService.ClassShowPage(new ClassShowPageInput() { ShowCount = 10000 });
            List<SelectListItem> classList = new List<SelectListItem>();

            ViewBag.Classes = classList;

            if (classId == null)
            {
                classList.AddRange(classes.Classes.Select(m => new SelectListItem() { Text = m.Name, Value = m.Id.ToString() }));
                return View();
            }
            else
            {
                classList.AddRange(classes.Classes.Select(m =>
                {
                    if (m.Id == (int)classId)
                    {
                        return new SelectListItem() { Text = m.Name, Value = m.Id.ToString(), Selected = true };
                    }
                    return new SelectListItem() { Text = m.Name, Value = m.Id.ToString() };
                }));
            }

            if (pageIndex == null)
            {
                pageIndex = 1;
            }

            var students = _headmasterAppService.StudentShowPage(new StudentShowPageInput() { PageIndex = (int)pageIndex, ClassId = (int)classId });
            if (students.Students.Length <= 0)
            {
                return View();
            }


            List<StudentResultViewModel> models = new List<StudentResultViewModel>();
            models.AddRange(students.Students.Select(m => ObjectMapper.Map<StudentResultViewModel>(m)));

            ViewData["classId"] = classId;
            ViewData["pageIndex"] = students.PageIndex;
            ViewData["pageCount"] = students.PageCount;


            return View(models);
        }
        #endregion

        #region 成绩模块
        public ActionResult GradeIndex(int? pageIndex, int? classId, string selectd, DateTime? dateTime)
        {
            #region 初始化数据
            List<SelectListItem> selectList = new List<SelectListItem>();
            selectList.Add(new SelectListItem() { Value = "NULL", Text = "不选择任何条件" });
            selectList.Add(new SelectListItem() { Value = "Month", Text = "按月查找" });
            selectList.Add(new SelectListItem() { Value = "Day", Text = "按天查找" });
            ViewBag.SelectList = selectList;

            var classes = _headmasterAppService.ClassShowPage(new ClassShowPageInput() { ShowCount = 10000 });
            List<SelectListItem> classList = new List<SelectListItem>();

            ViewBag.Classes = classList;

            if (classId == null)
            {
                classList.AddRange(classes.Classes.Select(m => new SelectListItem() { Text = m.Name, Value = m.Id.ToString() }));
                return View();
            }
            else
            {
                classList.AddRange(classes.Classes.Select(m =>
                {
                    if (m.Id == (int)classId)
                    {
                        return new SelectListItem() { Text = m.Name, Value = m.Id.ToString(), Selected = true };
                    }
                    return new SelectListItem() { Text = m.Name, Value = m.Id.ToString() };
                }));
            }
            #endregion

            if (pageIndex == null || pageIndex <= 0)
            {
                pageIndex = 1;
            }

            if (dateTime == null)
            {
                dateTime = DateTime.Now;
            }

            GradeShowPageOutput grades;
            switch (selectd)
            {
                case "NULL":
                    grades = _headmasterAppService.GradeShowPage(new GradeShowPageInput() { PageIndex = (int)pageIndex, ClassId = (int)classId, ScreenCondition = ScreenEnum.No });
                    break;
                case "Month":
                    grades = _headmasterAppService.GradeShowPage(new GradeShowPageInput() { PageIndex = (int)pageIndex, ClassId = (int)classId, ScreenCondition = ScreenEnum.Month, DateTime = (DateTime)dateTime });
                    break;
                //case "Day":
                //    grades = _headmasterAppService.GradeShowPage(new GradeShowPageInput() { PageIndex = (int)pageIndex, ClassId = (int)classId, ScreenCondition = ScreenEnum.Day, DateTime = (DateTime)dateTime });
                //    break;
                default:
                    grades = _headmasterAppService.GradeShowPage(new GradeShowPageInput() { PageIndex = (int)pageIndex, ClassId = (int)classId, ScreenCondition = ScreenEnum.No });
                    break;
            }
            if (grades.Grades.Length <= 0)
            {
                return View();
            }

            List<GradeResultViewModel> models = new List<GradeResultViewModel>();
            models.AddRange(grades.Grades.Select(m => ObjectMapper.Map<GradeResultViewModel>(m)));

            ViewData["pageIndex"] = grades.PageIndex;
            ViewData["pageCount"] = grades.PageCount;

            ViewData["classId"] = classId;
            ViewData["selectd"] = selectd == null ? "" : selectd;
            ViewData["dateTime"] = dateTime == null ? "" : dateTime.ToString();

            return View(models);
        }
        #endregion

        #region 奖项模块
        public ActionResult PrizeIndex(int? pageIndex, int? classId, string selectd)
        {
            #region 初始化数据
            List<SelectListItem> selectList = new List<SelectListItem>();
            selectList.Add(new SelectListItem() { Value = "NULL", Text = "不选择任何条件" });
            selectList.Add(new SelectListItem() { Value = "Week", Text = "查找周模范生" });
            selectList.Add(new SelectListItem() { Value = "Month", Text = "查找月模范生" });
            selectList.Add(new SelectListItem() { Value = "Xiao", Text = "查找校模范生" });
            ViewBag.SelectList = selectList;

            var classes = _headmasterAppService.ClassShowPage(new ClassShowPageInput() { ShowCount = 10000 });
            List<SelectListItem> classList = new List<SelectListItem>();

            ViewBag.Classes = classList;

            if (classId == null)
            {
                classList.AddRange(classes.Classes.Select(m => new SelectListItem() { Text = m.Name, Value = m.Id.ToString() }));
                return View();
            }
            else
            {
                classList.AddRange(classes.Classes.Select(m =>
                {
                    if (m.Id == (int)classId)
                    {
                        return new SelectListItem() { Text = m.Name, Value = m.Id.ToString(), Selected = true };
                    }
                    return new SelectListItem() { Text = m.Name, Value = m.Id.ToString() };
                }));
            }

            #endregion

            if (pageIndex == null || pageIndex <= 0)
            {
                pageIndex = 1;
            }

            PrizeShowPageOutput prizes;
            switch (selectd)
            {
                case "NULL":
                    prizes = _headmasterAppService.PrizeShowPage(new PrizeShowPageInput() { PageIndex = (int)pageIndex, ClassId = (int)classId, ScreenCondition = ScreenEnum.No });
                    break;
                case "Week":
                    prizes = _headmasterAppService.PrizeShowPage(new PrizeShowPageInput() { PageIndex = (int)pageIndex, ClassId = (int)classId, ScreenCondition = ScreenEnum.Week });
                    break;
                case "Month":
                    prizes = _headmasterAppService.PrizeShowPage(new PrizeShowPageInput() { PageIndex = (int)pageIndex, ClassId = (int)classId, ScreenCondition = ScreenEnum.Month });
                    break;
                case "Xiao":
                    prizes = _headmasterAppService.PrizeShowPage(new PrizeShowPageInput() { PageIndex = (int)pageIndex, ClassId = (int)classId, ScreenCondition = ScreenEnum.Xiao });
                    break;
                default:
                    prizes = _headmasterAppService.PrizeShowPage(new PrizeShowPageInput() { PageIndex = (int)pageIndex, ClassId = (int)classId, ScreenCondition = ScreenEnum.No });
                    break;
            }
            if (prizes.Prizes.Length <= 0)
            {
                return View();
            }

            List<PrizeResultViewModel> models = new List<PrizeResultViewModel>();
            models.AddRange(prizes.Prizes.Select(m => ObjectMapper.Map<PrizeResultViewModel>(m)));

            ViewData["pageIndex"] = prizes.PageIndex;
            ViewData["pageCount"] = prizes.PageCount;

            ViewData["classId"] = classId;
            ViewData["selectd"] = selectd == null ? "" : selectd;

            return View(models);
        }
        #endregion
    }
}