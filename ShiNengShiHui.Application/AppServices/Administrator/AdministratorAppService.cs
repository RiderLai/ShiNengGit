using System;
using ShiNengShiHui.Entities.Students;
using ShiNengShiHui.Entities.Prizes;
using ShiNengShiHui.Entities.Grades;
using Abp.Domain.Repositories;
using ShiNengShiHui.Entities.Classes;
using ShiNengShiHui.AppServices.Return;
using ShiNengShiHui.Users;
using Abp.Authorization.Users;
using ShiNengShiHui.Authorization.Roles;
using ShiNengShiHui.AppServices.AdministratorDTO;
using ShiNengShiHui.Entities.Teachers;
using Microsoft.AspNet.Identity;
using System.Linq;
using ShiNengShiHui.AppServices.ExcelDTO;
using Abp.Domain.Uow;

namespace ShiNengShiHui.AppServices
{
    public class AdministratorAppService : ShiNengShiHuiAppServiceBase, IAdministratorAppService
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        private readonly IStudentRepository _studentRepository;
        private readonly IPrizeRepository _prizeRepository;
        private readonly IGradeRepository _gradeRepository;
        private readonly ITeacherRepository _teacherRepository;
        private readonly IClassRepository _classRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRepository<Role> _roleRepository;
        private readonly IRepository<UserRole, long> _userRoleRepository;
        private readonly IExcelAppService _excelAppService;

        public AdministratorAppService(IStudentRepository studentRepository,
            IPrizeRepository prizeRepository,
            IGradeRepository gradeRepository,
            ITeacherRepository teacherRepository,
            IClassRepository classRepository,
            IUserRepository userRepository,
            IRepository<Role> roleRepository,
            IRepository<UserRole, long> userRoleRepository,
            IExcelAppService excelAppService,
            IUnitOfWorkManager unitOfWorkManager)
        {
            _studentRepository = studentRepository;
            _prizeRepository = prizeRepository;
            _gradeRepository = gradeRepository;
            _teacherRepository = teacherRepository;
            _classRepository = classRepository;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _userRoleRepository = userRoleRepository;
            _excelAppService = excelAppService;

            _unitOfWorkManager = unitOfWorkManager;
        }

        #region 班级模块

        #region 创建班级
        /// <summary>
        /// 创建班级
        /// </summary>
        /// <param name="classCreateInput"></param>
        /// <returns></returns>
        public ReturnVal ClassCreate(ClassCreateInput classCreateInput)
        {
            var Class = _classRepository.FirstOrDefault(m => m.Name == classCreateInput.Name);
            if (Class != null)
            {
                return new ReturnVal(ReturnStatu.Failure);
            }
            Class = ObjectMapper.Map<Class>(classCreateInput);
            _classRepository.Insert(Class);
            _classRepository.TableCreate(Class);
            return new ReturnVal(ReturnStatu.Success);
        }
        #endregion

        #region 批量创建班级
        /// <summary>
        /// 批量创建班级
        /// </summary>
        /// <param name="classCreateRangeInput"></param>
        /// <returns></returns>
        public ReturnVal ClassCreateRange(ClassCreateRangeInput classCreateRangeInput)
        {
            var classes = _excelAppService.ClassInsertOfExcel(new ClassInsertOfExcelInput() { DataStream = classCreateRangeInput.DataStream });

            if (classes == null)
            {
                return null;
            }

            foreach (ClassCreateInput item in classes.Classes)
            {
                var Class = _classRepository.Insert(ObjectMapper.Map<Class>(item));
                _classRepository.TableCreate(Class);
            }

            return new ReturnVal(ReturnStatu.Success);
        } 
        #endregion

        #region 删除班级
        /// <summary>
        /// 删除班级
        /// </summary>
        /// <param name="classDeleteInput"></param>
        /// <returns></returns>
        public ReturnVal ClassDelete(ClassDeleteInput classDeleteInput)
        {
            var flag = _classRepository.FirstOrDefault(classDeleteInput.Id);
            if (flag == null)
            {
                return new ReturnVal(ReturnStatu.Err);
            }
            _classRepository.Delete(classDeleteInput.Id);
            return new ReturnVal(ReturnStatu.Success);
        }
        #endregion

        #region 展示班级
        /// <summary>
        /// 展示班级
        /// </summary>
        /// <param name="classShowInput"></param>
        /// <returns></returns>
        public ClassShowOutput ClassShow(ClassShowInput classShowInput)
        {
            var Class = _classRepository.FirstOrDefault(classShowInput.Id);
            if (Class == null)
            {
                return null;
            }

            return ObjectMapper.Map<ClassShowOutput>(Class);

        }
        #endregion

        #region 分页展示班级
        /// <summary>
        /// 分页展示班级
        /// </summary>
        /// <param name="classShowPageInput"></param>
        /// <returns></returns>
        public ClassShowPageOutput ClassShowPage(ClassShowPageInput classShowPageInput)
        {
            long count = _classRepository.Count();
            classShowPageInput.PageCount = (int)(count / classShowPageInput.ShowCount);
            if (count % classShowPageInput.ShowCount > 0)
            {
                classShowPageInput.PageCount += 1;
            }
            if (classShowPageInput.PageIndex > classShowPageInput.PageCount)
            {
                classShowPageInput.PageIndex = 1;
            }

            Class[] classes = _classRepository.GetPage(classShowPageInput.PageIndex, classShowPageInput.ShowCount);
            ClassShowOutput[] classShowOutputs = classes.Select(m => ObjectMapper.Map<ClassShowOutput>(m)).ToArray();

            ClassShowPageOutput result = ObjectMapper.Map<ClassShowPageOutput>(classShowPageInput);
            result.Classes = classShowOutputs;
            return result;
        }
        #endregion

        #region 更新班级信息
        /// <summary>
        /// 更新班级信息
        /// </summary>
        /// <param name="classUpdateInput"></param>
        /// <returns></returns>
        public ReturnVal ClassUpdate(ClassUpdateInput classUpdateInput)
        {
            var Class = _classRepository.FirstOrDefault(classUpdateInput.Id);
            if (Class == null)
            {
                return new ReturnVal(ReturnStatu.Err);
            }

            ObjectMapper.Map<ClassUpdateInput, Class>(classUpdateInput, Class);
            _classRepository.Update(Class);
            return new ReturnVal(ReturnStatu.Success);
        }
        #endregion

        #endregion

        #region 教师模块

        #region 创建教师
        /// <summary>
        /// 创建教师
        /// </summary>
        /// <param name="teacherCreateInput"></param>
        /// <returns></returns>
        public ReturnVal TeacherCreate(TeacherCreateInput teacherCreateInput)
        {
            var teacher = ObjectMapper.Map<Teacher>(teacherCreateInput);
            if (teacher == null)
            {
                return new ReturnVal(ReturnStatu.Failure);
            }
            _teacherRepository.Insert(teacher);
            return new ReturnVal(ReturnStatu.Success);
        }
        #endregion

        #region 批量创建教师
        /// <summary>
        /// 批量创建教师
        /// </summary>
        /// <param name="teacherCreateRangeInput"></param>
        /// <returns></returns>
        public ReturnVal TeacherCreateRange(TeacherCreateRangeInput teacherCreateRangeInput)
        {
            var teachers = _excelAppService.TeacherInsertOfExcel(new TeacherInsertOfExcelInput() { DataStream = teacherCreateRangeInput.DataStream });

            if (teachers == null)
            {
                return null;
            }

            foreach (TeacherCreateInput item in teachers.Teachers)
            {
                _teacherRepository.Insert(ObjectMapper.Map<Teacher>(item));
            }

            return new ReturnVal(ReturnStatu.Success);
        } 
        #endregion

        #region 删除教师
        /// <summary>
        /// 删除教师
        /// </summary>
        /// <param name="teacherDeleteInput"></param>
        /// <returns></returns>
        public ReturnVal TeacherDelete(TeacherDeleteInput teacherDeleteInput)
        {
            var flag = _teacherRepository.FirstOrDefault(teacherDeleteInput.Id);
            if (flag == null)
            {
                return new ReturnVal(ReturnStatu.Err);
            }
            _teacherRepository.Delete(flag);
            return new ReturnVal(ReturnStatu.Success);
        }
        #endregion

        #region 展示教师
        /// <summary>
        /// 展示教师
        /// </summary>
        /// <param name="teacherShowInput"></param>
        /// <returns></returns>
        public TeacherShowOutput TeacherShow(TeacherShowInput teacherShowInput)
        {
            var teacher = _teacherRepository.FirstOrDefault(teacherShowInput.Id);
            if (teacher == null)
            {
                return null;
            }

            return ObjectMapper.Map<TeacherShowOutput>(teacher);
        }
        #endregion

        #region 分页展示教师
        /// <summary>
        /// 分页展示教师
        /// </summary>
        /// <param name="teacherShowPageInput"></param>
        /// <returns></returns>
        public TeacherShowPageOutput TeacherShowPage(TeacherShowPageInput teacherShowPageInput)
        {
            long count = _teacherRepository.Count();
            teacherShowPageInput.PageCount = (int)(count / teacherShowPageInput.ShowCount);
            if (count % teacherShowPageInput.ShowCount > 0)
            {
                teacherShowPageInput.PageCount += 1;
            }
            if (teacherShowPageInput.ShowCount > teacherShowPageInput.PageCount)
            {
                teacherShowPageInput.PageIndex = 1;
            }

            Teacher[] teachers = _teacherRepository.GetPage(teacherShowPageInput.PageIndex, teacherShowPageInput.ShowCount);
            TeacherShowOutput[] teacherShowOutputs = teachers.Select(m => ObjectMapper.Map<TeacherShowOutput>(m)).ToArray();

            TeacherShowPageOutput result = ObjectMapper.Map<TeacherShowPageOutput>(teacherShowPageInput);
            result.Teachers = teacherShowOutputs;

            return result;
        }
        #endregion

        #region 更新教师信息
        /// <summary>
        /// 更新教师信息
        /// </summary>
        /// <param name="teacherUpdateInput"></param>
        /// <returns></returns>
        public ReturnVal TeacherUpdate(TeacherUpdateInput teacherUpdateInput)
        {
            var teacher = _teacherRepository.FirstOrDefault(teacherUpdateInput.Id);
            if (teacher == null)
            {
                return new ReturnVal(ReturnStatu.Err);
            }

            ObjectMapper.Map<TeacherUpdateInput, Teacher>(teacherUpdateInput, teacher);
            _teacherRepository.Update(teacher);
            return new ReturnVal(ReturnStatu.Success);
        }
        #endregion

        #endregion

        #region 用户模块

        #region 创建用户
        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="userCreateInput"></param>
        /// <returns></returns>
        public ReturnVal UserCreate(UserCreateInput userCreateInput)
        {
            var user = ObjectMapper.Map<User>(userCreateInput);

            user.TenantId = 1;
            user.Surname = user.Name;
            user.Password = new PasswordHasher().HashPassword(user.Password);
            //添加教师
            if (userCreateInput.TeacherId != null)
            {
                var teacher = _teacherRepository.FirstOrDefault((int)userCreateInput.TeacherId);
                if (teacher != null)
                {
                    user.Teacher = teacher;
                }
            }
            user = _userRepository.Insert(user);

            var teacherRole = _roleRepository.FirstOrDefault(m => m.Name.Equals(StaticRoleNames.Tenants.Teacher) && m.TenantId == 1);
            _userRoleRepository.Insert(new UserRole()
            {
                UserId = user.Id,
                RoleId = teacherRole.Id,
                TenantId = user.TenantId
            });

            return new ReturnVal(ReturnStatu.Success);
        }
        #endregion

        #region 批量创建用户
        /// <summary>
        /// 批量创建用户
        /// </summary>
        /// <param name="userCreateRangeInput"></param>
        /// <returns></returns>
        public ReturnVal UserCreateRange(UserCreateRangeInput userCreateRangeInput)
        {
            var users = _excelAppService.UserInsertOfExcel(new UserInsertOfExcelInput() { DataStream = userCreateRangeInput.DataStream });

            if (users == null)
            {
                return null;
            }

            var teacherRole = _roleRepository.FirstOrDefault(m => m.Name.Equals(StaticRoleNames.Tenants.Teacher) && m.TenantId == 1);

            foreach (UserCreateInput item in users.Users)
            {

                var user = _userRepository.FirstOrDefault(m => m.Name.Equals(item.Name));
                if (user != null)
                {
                    continue;
                }
                user = ObjectMapper.Map<User>(item);
                user.Password = new PasswordHasher().HashPassword(user.Password);
                user.TenantId = 1;
                user.Surname = user.Name;

                user = _userRepository.Insert(user);

                _userRoleRepository.Insert(new UserRole()
                {
                    UserId = user.Id,
                    RoleId = teacherRole.Id,
                    TenantId = user.TenantId
                });

                _unitOfWorkManager.Current.SaveChanges();
            }

            return new ReturnVal(ReturnStatu.Success);
        } 
        #endregion

        #region 删除用户
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="userDeleteInput"></param>
        /// <returns></returns>
        public ReturnVal UserDelete(UserDeleteInput userDeleteInput)
        {
            var user = _userRepository.FirstOrDefault(m => m.Id == userDeleteInput.Id && m.TenantId == userDeleteInput.TenatId);
            if (user == null)
            {
                return new ReturnVal(ReturnStatu.Err);
            }
            _userRepository.Delete(user);
            return new ReturnVal(ReturnStatu.Success);
        }
        #endregion

        #region 修改用户密码
        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="userPasswordUpdateInput"></param>
        /// <returns></returns>
        public ReturnVal UserPasswordUpdate(UserPasswordUpdateInput userPasswordUpdateInput)
        {
            var user = _userRepository.FirstOrDefault(userPasswordUpdateInput.Id);
            if (user == null)
            {
                return new ReturnVal(ReturnStatu.Failure);
            }

            user.Password = new PasswordHasher().HashPassword(userPasswordUpdateInput.Password);
            _userRepository.Update(user);
            return new ReturnVal(ReturnStatu.Success);
        }
        #endregion

        #region 展示用户
        /// <summary>
        /// 展示用户
        /// </summary>
        /// <param name="userShowInput"></param>
        /// <returns></returns>
        public UserShowOutput UserShow(UserShowInput userShowInput)
        {
            var user = _userRepository.FirstOrDefault(userShowInput.Id);
            if (user == null)
            {
                return null;
            }

            return ObjectMapper.Map<UserShowOutput>(user);
        }
        #endregion

        #region 分页展示用户
        /// <summary>
        /// 分页展示用户
        /// </summary>
        /// <param name="userShowPageInput"></param>
        /// <returns></returns>
        public UserShowPageOutput UserShowPage(UserShowPageInput userShowPageInput)
        {
            long count = _userRepository.Count();
            userShowPageInput.PageCount = (int)(count / userShowPageInput.ShowCount);
            if (count % userShowPageInput.ShowCount > 0)
            {
                userShowPageInput.PageCount += 1;
            }
            if (userShowPageInput.PageIndex > userShowPageInput.PageCount)
            {
                userShowPageInput.PageIndex = 1;
            }

            User[] users = _userRepository.GetPage(userShowPageInput.PageIndex, userShowPageInput.ShowCount);
            UserShowOutput[] userShowOutputs = users.Select<User, UserShowOutput>(m => ObjectMapper.Map<UserShowOutput>(m)).ToArray();

            UserShowPageOutput result = ObjectMapper.Map<UserShowPageOutput>(userShowPageInput);
            result.Users = userShowOutputs;
            return result;
        }
        #endregion

        #region 更新用户信息
        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="userUpdateInput"></param>
        /// <returns></returns>
        public ReturnVal UserUpdate(UserUpdateInput userUpdateInput)
        {
            var user = _userRepository.FirstOrDefault(userUpdateInput.Id);
            if (user == null)
            {
                return new ReturnVal(ReturnStatu.Err);
            }

            ObjectMapper.Map<UserUpdateInput, User>(userUpdateInput, user);

            user.Surname = user.Name;
            //添加教师
            if (userUpdateInput.TeacherId != null)
            {
                var teacher = _teacherRepository.FirstOrDefault((int)userUpdateInput.TeacherId);
                if (teacher != null)
                {
                    user.Teacher = teacher;
                }
            }
            _userRepository.Update(user);
            return new ReturnVal(ReturnStatu.Success);
        }
        #endregion

        #endregion
    }
}
