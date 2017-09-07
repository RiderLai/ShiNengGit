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
        public ActionResult GradeIndex(int? pageIndex, int? classId)
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

            var grades = _headmasterAppService.GradeShowPage(new GradeShowPageInput() { PageIndex = (int)pageIndex, ClassId = (int)classId });
            if (grades.Grades.Length <= 0)
            {
                return View();
            }

            List<GradeResultViewModel> models = new List<GradeResultViewModel>();
            models.AddRange(grades.Grades.Select(m => ObjectMapper.Map<GradeResultViewModel>(m)));

            ViewData["classId"] = classId;
            ViewData["pageIndex"] = grades.PageIndex;
            ViewData["pageCount"] = grades.PageCount;

            return View(models);
        } 
        #endregion

        #region 奖项模块
        public ActionResult PrizeIndex(int? pageIndex, int? classId)
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

            var prizes = _headmasterAppService.PrizeShowPage(new PrizeShowPageInput() { PageIndex = (int)pageIndex, ClassId = (int)classId });
            if (prizes.Prizes.Length <= 0)
            {
                return View();
            }

            List<PrizeResultViewModel> models = new List<PrizeResultViewModel>();
            models.AddRange(prizes.Prizes.Select(m => ObjectMapper.Map<PrizeResultViewModel>(m)));

            ViewData["classId"] = classId;
            ViewData["pageIndex"] = prizes.PageIndex;
            ViewData["pageCount"] = prizes.PageCount;

            return View(models);
        } 
        #endregion
    }
}