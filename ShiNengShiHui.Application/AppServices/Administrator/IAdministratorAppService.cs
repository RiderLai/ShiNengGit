using Abp.Application.Services;
using ShiNengShiHui.AppServices.AdministratorDTO;
using ShiNengShiHui.AppServices.Return;

namespace ShiNengShiHui.AppServices
{
    public interface IAdministratorAppService:IApplicationService
    {
        UserShowOutput UserShow(UserShowInput userShowInput);

        UserShowPageOutput UserShowPage(UserShowPageInput userShowPageInput);

        ReturnVal UserPasswordUpdate(UserPasswordUpdateInput userPasswordUpdateInput);

        ReturnVal UserCreate(UserCreateInput userCreateInput);

        ReturnVal UserCreateRange(UserCreateRangeInput userCreateRangeInput);

        ReturnVal UserUpdate(UserUpdateInput userUpdateInput);

        ReturnVal UserDelete(UserDeleteInput userDeleteInput);

        TeacherShowOutput TeacherShow(TeacherShowInput teacherShowInput);

        TeacherShowPageOutput TeacherShowPage(TeacherShowPageInput teacherShowPageInput);

        ReturnVal TeacherCreate(TeacherCreateInput teacherCreateInput);

        ReturnVal TeacherCreateRange(TeacherCreateRangeInput teacherCreateRangeInput);

        ReturnVal TeacherUpdate(TeacherUpdateInput teacherUpdateInput);

        ReturnVal TeacherDelete(TeacherDeleteInput teacherDeleteInput);

        ClassShowOutput ClassShow(ClassShowInput classShowInput);

        ClassShowPageOutput ClassShowPage(ClassShowPageInput classShowPageInput);

        ReturnVal ClassCreate(ClassCreateInput classCreateInput);

        ReturnVal ClassCreateRange(ClassCreateRangeInput classCreateRangeInput);

        ReturnVal ClassUpdate(ClassUpdateInput classUpdateInput);

        ReturnVal ClassDelete(ClassDeleteInput classDeleteInput);
    }
}
