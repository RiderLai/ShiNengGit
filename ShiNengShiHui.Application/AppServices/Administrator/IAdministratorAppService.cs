using Abp.Application.Services;
using ShiNengShiHui.AppServices.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiNengShiHui.AppServices
{
    public interface IAdministratorAppService:IApplicationService
    {
        UserShowOutput UserShow(UserShowInput userShowInput);

        UserShowPageOutput UserShowPage(UserShowPageInput userShowPageInput);

        UserCreateOutput UserCreate(UserCreateInput userCreateInput);

        UserUpdateOutput UserUpdate(UserUpdateInput userUpdateInput);

        void UserDelete(UserDeleteInput userDeleteInput);

        TeacherShowOutput TeacherShow(TeacherShowInput teacherShowInput);

        TeacherShowPageOutput TeacherShowPage(TeacherShowPageInput teacherShowPageInput);

        TeacherCreateOutput TeacherCreate(TeacherCreateInput teacherCreateInput);

        TeacherCreateRangeOutput TeacherCreateRange(TeacherCreateRangeInput teacherCreateRangeInput);

        TeacherUpdateOutput TeacherUpdate(TeacherUpdateInput teacherUpdateInput);

        TeacherDeleteOutput TeacherDelete(TeacherDeleteInput teacherDeleteInput);

        ClassShowOutput ClassShow(ClassShowInput classShowInput);

        ClassShowPageOutput ClassShowPage(ClassShowPageInput classShowPageInput);

        ClassCreateOutput ClassCreate(ClassCreateInput classCreateInput);

        ClassCreateOutput ClassCreateRange(ClassCreateRangeInput classCreateRangeInput);

        ClassUpdateOutput ClassUpdate(ClassUpdateInput classUpdateInput);

        ClassDeleteOutput ClassDelete(ClassDeleteInput classDeleteInput);
    }
}
