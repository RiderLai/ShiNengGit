﻿using ShiNengShiHui.AppServices;
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

            ViewData["pageIndex"] = result.PageIndex;
            ViewData["pageCount"] = result.PageCount;

            List<TeacherResultViewModel> list = result.Teachers.Select(m => ObjectMapper.Map<TeacherResultViewModel>(m)).ToList();
            return View(list);
        } 
        #endregion
    }
}