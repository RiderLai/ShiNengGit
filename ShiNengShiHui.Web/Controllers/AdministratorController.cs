using Abp.Domain.Uow;
using Abp.Web.Mvc.Authorization;
using ShiNengShiHui.AppServices;
using ShiNengShiHui.AppServices.AdministratorDTO;
using ShiNengShiHui.AppServices.ExcelDTO;
using ShiNengShiHui.AppServices.Return;
using ShiNengShiHui.Authorization;
using ShiNengShiHui.Web.Models.Administrator.Class;
using ShiNengShiHui.Web.Models.Administrator.Teacher;
using ShiNengShiHui.Web.Models.Administrator.User;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShiNengShiHui.Web.Controllers
{
    [AbpMvcAuthorize(permissions: PermissionNames.Pages)]
    public class AdministratorController : ShiNengShiHuiControllerBase
    {
        private IAdministratorAppService _administratorAppService;
        private IExcelAppService _excelAppService;

        public AdministratorController(IAdministratorAppService administratorAppService,
            IExcelAppService excelAppService)
        {
            _administratorAppService = administratorAppService;
            _excelAppService = excelAppService;
        }

        // GET: Administrator
        public ActionResult Index()
        {
            return View();
        }

        #region 用户模块
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

            if (result.Users.Length <= 0)
            {
                return View();
            }

            ViewData["pageIndex"] = result.PageIndex;
            ViewData["pageCount"] = result.PageCount;

            List<UserResultViewModel> list = result.Users.Select<UserShowOutput, UserResultViewModel>(m => ObjectMapper.Map<UserResultViewModel>(m)).ToList();
            return View(list);
        }

        #region 更新数据
        [HttpGet]
        public ActionResult UserEdit(int id)
        {
            UserShowOutput userShowOutput = _administratorAppService.UserShow(new UserShowInput() { Id = id });
            if (userShowOutput == null)
            {
                return View();
            }
            UserEditModel userEditModel = ObjectMapper.Map<UserEditModel>(userShowOutput);

            #region 初始化数据
            //添加教师数据
            List<SelectListItem> teacherList = new List<SelectListItem>();
            teacherList.Add(new SelectListItem() { Text = "空", Value = "" });
            var teacherAll = _administratorAppService.TeacherShowPage(new TeacherShowPageInput() { ShowCount = 10000 });
            teacherList.AddRange(teacherAll.Teachers.Select(m =>
            {
                if (m.Id == userEditModel.TeacherId)
                {
                    return new SelectListItem() { Text = m.Name, Value = m.Id.ToString(), Selected = true };
                }
                return new SelectListItem() { Text = m.Name, Value = m.Id.ToString() };
            }).ToList());
            ViewBag.TeacherList = teacherList;
            #endregion

            return View(userEditModel);
        }

        [HttpPost]
        public ActionResult UserEdit(UserEditModel model)
        {
            if (ModelState.IsValid)
            {
                _administratorAppService.UserUpdate(ObjectMapper.Map<UserUpdateInput>(model));
                return this.RefreshParent();
            }

            #region 初始化数据
            //添加教师数据
            List<SelectListItem> teacherList = new List<SelectListItem>();
            teacherList.Add(new SelectListItem() { Text = "空", Value = "" });
            var teacherAll = _administratorAppService.TeacherShowPage(new TeacherShowPageInput() { ShowCount = 10000 });
            teacherList.AddRange(teacherAll.Teachers.Select(m =>
            {
                if (m.Id == model.TeacherId)
                {
                    return new SelectListItem() { Text = m.Name, Value = m.Id.ToString(), Selected = true };
                }
                return new SelectListItem() { Text = m.Name, Value = m.Id.ToString() };
            }).ToList());
            ViewBag.TeacherList = teacherList;
            #endregion

            return View(model);
        }
        #endregion

        #region 添加数据
        [HttpGet]
        public ActionResult UserCreate()
        {
            #region 初始化数据
            //添加教师数据
            List<SelectListItem> teacherList = new List<SelectListItem>();
            teacherList.Add(new SelectListItem() { Text = "空", Value = "" });
            var teacherAll = _administratorAppService.TeacherShowPage(new TeacherShowPageInput() { ShowCount = 10000 });
            teacherList.AddRange(teacherAll.Teachers.Select(m =>
            {
                return new SelectListItem() { Text = m.Name, Value = m.Id.ToString() };
            }).ToList());
            ViewBag.TeacherList = teacherList;
            #endregion

            return View();
        }

        [HttpPost]
        public ActionResult UserCreate(UserCreateInput model)
        {
            if (ModelState.IsValid)
            {
                _administratorAppService.UserCreate(ObjectMapper.Map<UserCreateInput>(model));
                return this.RefreshParent();
            }

            #region 初始化数据
            //添加教师数据
            List<SelectListItem> teacherList = new List<SelectListItem>();
            teacherList.Add(new SelectListItem() { Text = "空", Value = "" });
            var teacherAll = _administratorAppService.TeacherShowPage(new TeacherShowPageInput() { ShowCount = 10000 });
            teacherList.AddRange(teacherAll.Teachers.Select(m =>
            {
                if (m.Id == model.TeacherId)
                {
                    return new SelectListItem() { Text = m.Name, Value = m.Id.ToString(), Selected = true };
                }
                return new SelectListItem() { Text = m.Name, Value = m.Id.ToString() };
            }).ToList());
            ViewBag.TeacherList = teacherList;
            #endregion

            return View(model);
        }
        #endregion

        #region 更改用户密码
        [HttpGet]
        public ActionResult UserPasswordEdit(int id)
        {
            UserShowOutput userShowOutput = _administratorAppService.UserShow(new UserShowInput() { Id = id });
            if (userShowOutput == null)
            {
                return View();
            }

            UserPasswordEditModel model = ObjectMapper.Map<UserPasswordEditModel>(userShowOutput);
            return View(model);
        }

        [HttpPost]
        public ActionResult UserPasswordEdit(UserPasswordEditModel model)
        {
            if (ModelState.IsValid)
            {
                _administratorAppService.UserPasswordUpdate(ObjectMapper.Map<UserPasswordUpdateInput>(model));
                return this.RefreshParent();
            }
            return View(model);
        }
        #endregion

        #region 删除数据
        public ActionResult UserDelete(int[] sid)
        {
            for (int i = 0, length = sid.Length; i < length; i++)
            {
                _administratorAppService.UserDelete(new UserDeleteInput() { Id = sid[i] });
            }
            return RedirectToAction("UserIndex");
        }
        #endregion

        #region 表格下载
        public FileResult UserExcelDown()
        {
            UserExcelDownOutput result = _excelAppService.UserExcelDown();
            using (result.ExcelData)
            {
                return File(result.ExcelData.ToArray(), "application/octet-stream", "用户导入模板.xls");
            }
        }
        #endregion

        #region Excel批量导入
        public ActionResult UsersCereateOfExcel(HttpPostedFileBase file)
        {
            if (file == null)
            {
                return this.RedirectAjax("Failure", "没有文件", null, null);
            }
            else if (!Path.GetExtension(file.FileName).Equals(".xls"))
            {
                return this.RedirectAjax("Failure", "请上传Excel文件", null, null);
            }
            else if (file.ContentLength >= 0)
            {
                var result = _administratorAppService.UserCreateRange(new UserCreateRangeInput() { DataStream = file.InputStream });

                if (result.Statu == ReturnStatu.Success)
                {
                    return this.RedirectAjax("Success", "上传成功", null, null);
                }
                else
                {
                    return this.RedirectAjax("Failure", "上传失败", null, null);
                }
            }
            else
            {
                return this.RedirectAjax("Failure", null, null, null);
            }
        } 
        #endregion

        #endregion

        #region 班级模块
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

            if (result.Classes.Length <= 0)
            {
                return View();
            }

            ViewData["pageIndex"] = result.PageIndex;
            ViewData["pageCount"] = result.PageCount;

            List<ClassResultViewModel> list = result.Classes.Select(m => ObjectMapper.Map<ClassResultViewModel>(m)).ToList();
            return View(list);
        }

        #region 编辑数据
        [HttpGet]
        public ActionResult ClassEdit(int id)
        {
            ClassShowOutput Class = _administratorAppService.ClassShow(new ClassShowInput() { Id = id });
            if (Class == null)
            {
                return View();
            }

            ClassEditModel model = ObjectMapper.Map<ClassEditModel>(Class);
            return View(model);
        }

        [HttpPost]
        public ActionResult ClassEdit(ClassEditModel model)
        {
            if (ModelState.IsValid)
            {
                _administratorAppService.ClassUpdate(ObjectMapper.Map<ClassUpdateInput>(model));
                return this.RefreshParent();
            }

            return View(model);
        }
        #endregion

        #region 添加数据
        [HttpGet]
        public ActionResult ClassCreate()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ClassCreate(ClassCreateModel model)
        {
            if (ModelState.IsValid)
            {
                _administratorAppService.ClassCreate(ObjectMapper.Map<ClassCreateInput>(model));
                return this.RefreshParent();
            }

            return View(model);
        }
        #endregion

        #region 删除数据
        public ActionResult ClassDelete(int[] sid)
        {
            for (int i = 0, length = sid.Length; i < length; i++)
            {
                _administratorAppService.ClassDelete(new ClassDeleteInput() { Id = sid[i] });
            }
            return RedirectToAction("ClassIndex");
        }
        #endregion

        #region 表格下载
        public FileResult ClassExcelDown()
        {
            ClassExcelDownOutput result = _excelAppService.ClassExcelDown();
            using (result.ExcelData)
            {
                return File(result.ExcelData.ToArray(), "application/octet-stream", "班级导入模板.xls");
            }
        }
        #endregion

        #region Excel批量导入
        public ActionResult ClassesCereateOfExcel(HttpPostedFileBase file)
        {
            if (file == null)
            {
                return this.RedirectAjax("Failure", "没有文件", null, null);
            }
            if (!Path.GetExtension(file.FileName).Equals(".xls"))
            {
                return this.RedirectAjax("Failure", "请上传Excel文件", null, null);
            }
            else if (file.ContentLength >= 0)
            {
                var result = _administratorAppService.ClassCreateRange(new ClassCreateRangeInput() { DataStream = file.InputStream });

                if (result.Statu == ReturnStatu.Success)
                {
                    return this.RedirectAjax("Success", "上传成功", null, null);
                }
                else
                {
                    return this.RedirectAjax("Failure", "上传失败", null, null);
                }
            }
            else
            {
                return this.RedirectAjax("Failure", null, null, null);
            }
        } 
        #endregion

        #endregion

        #region 教师模块
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

            if (result.Teachers.Length <= 0)
            {
                return View();
            }

            ViewData["pageIndex"] = result.PageIndex;
            ViewData["pageCount"] = result.PageCount;

            List<TeacherResultViewModel> list = result.Teachers.Select(m => ObjectMapper.Map<TeacherResultViewModel>(m)).ToList();
            return View(list);
        }

        #region 编辑数据
        [HttpGet]
        public ActionResult TeacherEdit(int id)
        {
            TeacherShowOutput teacherShowOutput = _administratorAppService.TeacherShow(new TeacherShowInput() { Id = id });
            if (teacherShowOutput == null)
            {
                return View();
            }
            TeacherEditModel model = ObjectMapper.Map<TeacherEditModel>(teacherShowOutput);

            #region 初始化数据
            //性别
            List<SelectListItem> sexList = new List<SelectListItem>();
            sexList.Add(new SelectListItem() { Text = "男", Value = "true" });
            sexList.Add(new SelectListItem() { Text = "女", Value = "false" });
            ViewBag.SexList = sexList;

            //班级
            List<SelectListItem> classList = new List<SelectListItem>();
            var classes = _administratorAppService.ClassShowPage(new ClassShowPageInput() { ShowCount = 10000 });
            classList.AddRange(classes.Classes.Select(m =>
            {
                if (model.ClassId == m.Id)
                {
                    return new SelectListItem() { Text = m.Display, Value = m.Id.ToString(), Selected = true };
                }
                return new SelectListItem() { Text = m.Display, Value = m.Id.ToString() };
            }).ToList());
            ViewBag.ClassList = classList;
            #endregion
            return View(model);
        }

        [HttpPost]
        public ActionResult TeacherEdit(TeacherEditModel model)
        {
            if (ModelState.IsValid)
            {
                _administratorAppService.TeacherUpdate(ObjectMapper.Map<TeacherUpdateInput>(model));
                return this.RefreshParent();
            }

            #region 初始化数据
            //性别
            List<SelectListItem> sexList = new List<SelectListItem>();
            sexList.Add(new SelectListItem() { Text = "男", Value = "true" });
            sexList.Add(new SelectListItem() { Text = "女", Value = "false" });
            ViewBag.SexList = sexList;

            //班级
            List<SelectListItem> classList = new List<SelectListItem>();
            var classes = _administratorAppService.ClassShowPage(new ClassShowPageInput() { ShowCount = 10000 });
            classList.AddRange(classes.Classes.Select(m =>
            {
                if (model.ClassId == m.Id)
                {
                    return new SelectListItem() { Text = m.Display, Value = m.Id.ToString(), Selected = true };
                }
                return new SelectListItem() { Text = m.Display, Value = m.Id.ToString() };
            }).ToList());
            ViewBag.ClassList = classList;
            #endregion
            return View(model);
        }
        #endregion

        #region 添加数据
        [HttpGet]
        public ActionResult TeacherCreate()
        {
            #region 初始化数据
            //性别
            List<SelectListItem> sexList = new List<SelectListItem>();
            sexList.Add(new SelectListItem() { Text = "男", Value = "true" });
            sexList.Add(new SelectListItem() { Text = "女", Value = "false" });
            ViewBag.SexList = sexList;

            //班级
            List<SelectListItem> classList = new List<SelectListItem>();
            var classes = _administratorAppService.ClassShowPage(new ClassShowPageInput() { ShowCount = 10000 });
            classList.AddRange(classes.Classes.Select(m =>
            {
                return new SelectListItem() { Text = m.Display, Value = m.Id.ToString() };
            }).ToList());
            ViewBag.ClassList = classList;
            #endregion
            return View();
        }

        [HttpPost]
        public ActionResult TeacherCreate(TeacherCreateModel model)
        {
            if (ModelState.IsValid)
            {
                _administratorAppService.TeacherCreate(ObjectMapper.Map<TeacherCreateInput>(model));
                return this.RefreshParent();
            }

            #region 初始化数据
            //性别
            List<SelectListItem> sexList = new List<SelectListItem>();
            sexList.Add(new SelectListItem() { Text = "男", Value = "true" });
            sexList.Add(new SelectListItem() { Text = "女", Value = "false" });
            ViewBag.SexList = sexList;

            //班级
            List<SelectListItem> classList = new List<SelectListItem>();
            var classes = _administratorAppService.ClassShowPage(new ClassShowPageInput() { ShowCount = 10000 });
            classList.AddRange(classes.Classes.Select(m =>
            {
                if (model.ClassId == m.Id)
                {
                    return new SelectListItem() { Text = m.Display, Value = m.Id.ToString(), Selected = true };
                }
                return new SelectListItem() { Text = m.Display, Value = m.Id.ToString() };
            }).ToList());
            ViewBag.ClassList = classList;
            #endregion
            return View(model);
        }
        #endregion

        #region 删除数据
        public ActionResult TeacherDelete(int[] sid)
        {
            for (int i = 0, length = sid.Length; i < length; i++)
            {
                _administratorAppService.TeacherDelete(new TeacherDeleteInput() { Id = sid[i] });
            }
            return RedirectToAction("TeacherIndex");
        }
        #endregion

        #region 表格下载
        public FileResult TeacherExcelDown()
        {
            TeacherExcelDownOutput result = _excelAppService.TeacherExcelDown();
            using (result.ExcelData)
            {
                return File(result.ExcelData.ToArray(), "application/octet-stream", "教师导入模板.xls");
            }
        }
        #endregion

        #region Excel批量导入
        public ActionResult TeachersCereateOfExcel(HttpPostedFileBase file)
        {
            if (file == null)
            {
                return this.RedirectAjax("Failure", "没有文件", null, null);
            }
            else if (!Path.GetExtension(file.FileName).Equals(".xls"))
            {
                return this.RedirectAjax("Failure", "请上传Excel文件", null, null);
            }
            else if (file.ContentLength >= 0)
            {
                var result = _administratorAppService.TeacherCreateRange(new TeacherCreateRangeInput() { DataStream = file.InputStream });

                if (result.Statu == ReturnStatu.Success)
                {
                    return this.RedirectAjax("Success", "上传成功", null, null);
                }
                else
                {
                    return this.RedirectAjax("Failure", "上传失败", null, null);
                }
            }
            else
            {
                return this.RedirectAjax("Failure", null, null, null);
            }
        } 
        #endregion

        #endregion
    }
}