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

namespace ShiNengShiHui.AppServices
{
    public class AdministratorAppService : ShiNengShiHuiAppServiceBase, IAdministratorAppService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IPrizeRepository _prizeRepository;
        private readonly IGradeRepository _gradeRepository;
        private readonly IRepository<ShiNengShiHui.Entities.Teachers.Teacher> _teacherRepository;
        private readonly IRepository<Class> _classRepository;
        private readonly IRepository<User, long> _userRepository;
        private readonly IRepository<Role> _roleRepository;
        private readonly IRepository<UserRole, long> _userRoleRepository;

        public AdministratorAppService(IStudentRepository studentRepository,
            IPrizeRepository prizeRepository,
            IGradeRepository gradeRepository,
            IRepository<ShiNengShiHui.Entities.Teachers.Teacher> teacherRepository,
            IRepository<Class> classRepository,
            IRepository<User, long> userRepository,
            IRepository<Role> roleRepository,
            IRepository<UserRole, long> userRoleRepository)
        {
            _studentRepository = studentRepository;
            _prizeRepository = prizeRepository;
            _gradeRepository = gradeRepository;
            _teacherRepository = teacherRepository;
            _classRepository = classRepository;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _userRoleRepository = userRoleRepository;
        }

        public ReturnVal ClassCreate(ClassCreateInput classCreateInput)
        {
            var Class = ObjectMapper.Map<Class>(classCreateInput);
            if (Class == null)
            {
                return new ReturnVal(ReturnStatu.Failure);
            }
            _classRepository.Insert(Class);
            return new ReturnVal(ReturnStatu.Success);
        }

        public ReturnVal ClassCreateRange(ClassCreateRangeInput classCreateRangeInput)
        {
            throw new NotImplementedException();
        }

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

        public ClassShowOutput ClassShow(ClassShowInput classShowInput)
        {
            throw new NotImplementedException();
        }

        public ClassShowPageOutput ClassShowPage(ClassShowPageInput classShowPageInput)
        {
            throw new NotImplementedException();
        }

        public ReturnVal ClassUpdate(ClassUpdateInput classUpdateInput)
        {
            if (_classRepository.FirstOrDefault(classUpdateInput.Id) == null)
            {
                return new ReturnVal(ReturnStatu.Err);
            }

            var Class = ObjectMapper.Map<Class>(classUpdateInput);
            if (Class == null)
            {
                return new ReturnVal(ReturnStatu.Failure);
            }

            _classRepository.Update(Class);
            return new ReturnVal(ReturnStatu.Success);
        }

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

        public ReturnVal TeacherCreateRange(TeacherCreateRangeInput teacherCreateRangeInput)
        {
            throw new NotImplementedException();
        }

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

        public TeacherShowOutput TeacherShow(TeacherShowInput teacherShowInput)
        {
            throw new NotImplementedException();
        }

        public TeacherShowPageOutput TeacherShowPage(TeacherShowPageInput teacherShowPageInput)
        {
            throw new NotImplementedException();
        }

        public ReturnVal TeacherUpdate(TeacherUpdateInput teacherUpdateInput)
        {
            if (_teacherRepository.FirstOrDefault(teacherUpdateInput.Id) == null)
            {
                return new ReturnVal(ReturnStatu.Err);
            }

            var teacher = ObjectMapper.Map<Teacher>(teacherUpdateInput);
            if (teacher == null)
            {
                return new ReturnVal(ReturnStatu.Failure);
            }

            _teacherRepository.Insert(teacher);
            return new ReturnVal(ReturnStatu.Success);
        }

        public ReturnVal UserCreate(UserCreateInput userCreateInput)
        {
            var user = ObjectMapper.Map<User>(userCreateInput);
            if (user == null)
            {
                return new ReturnVal(ReturnStatu.Failure);
            }

            user.TenantId = 1;
            user.Surname = user.Name;
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

        public ReturnVal UserCreateRange(UserCreateRangeInput userCreateRangeInput)
        {
            throw new NotImplementedException();
        }

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

        public UserShowOutput UserShow(UserShowInput userShowInput)
        {
            throw new NotImplementedException();
        }

        public UserShowPageOutput UserShowPage(UserShowPageInput userShowPageInput)
        {
            throw new NotImplementedException();
        }

        public ReturnVal UserUpdate(UserUpdateInput userUpdateInput)
        {

            if (_userRepository.FirstOrDefault(m => m.Id == userUpdateInput.Id) == null)
            {
                return new ReturnVal(ReturnStatu.Err);
            }

            var user = ObjectMapper.Map<User>(userUpdateInput);
            if (user==null)
            {
                return new ReturnVal(ReturnStatu.Failure);
            }

            user.Surname = user.Name;
            _userRepository.Update(user);
            return new ReturnVal(ReturnStatu.Success);
        }
    }
}
