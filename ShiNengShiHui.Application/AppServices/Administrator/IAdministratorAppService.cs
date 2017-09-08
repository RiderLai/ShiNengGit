using Abp.Application.Services;
using ShiNengShiHui.AppServices.AdministratorDTO;
using ShiNengShiHui.AppServices.Return;

namespace ShiNengShiHui.AppServices
{
    public interface IAdministratorAppService:IApplicationService
    {
        /// <summary>
        /// 展示用户
        /// </summary>
        /// <param name="userShowInput"></param>
        /// <returns></returns>
        UserShowOutput UserShow(UserShowInput userShowInput);

        /// <summary>
        /// 分页展示用户
        /// </summary>
        /// <param name="userShowPageInput"></param>
        /// <returns></returns>
        UserShowPageOutput UserShowPage(UserShowPageInput userShowPageInput);

        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="userPasswordUpdateInput"></param>
        /// <returns></returns>
        ReturnVal UserPasswordUpdate(UserPasswordUpdateInput userPasswordUpdateInput);

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="userCreateInput"></param>
        /// <returns></returns>
        ReturnVal UserCreate(UserCreateInput userCreateInput);

        /// <summary>
        /// 批量创建用户
        /// </summary>
        /// <param name="userCreateRangeInput"></param>
        /// <returns></returns>
        ReturnVal UserCreateRange(UserCreateRangeInput userCreateRangeInput);

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="userUpdateInput"></param>
        /// <returns></returns>
        ReturnVal UserUpdate(UserUpdateInput userUpdateInput);

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="userDeleteInput"></param>
        /// <returns></returns>
        ReturnVal UserDelete(UserDeleteInput userDeleteInput);

        /// <summary>
        /// 展示教师
        /// </summary>
        /// <param name="teacherShowInput"></param>
        /// <returns></returns>
        TeacherShowOutput TeacherShow(TeacherShowInput teacherShowInput);

        /// <summary>
        /// 分页展示教师
        /// </summary>
        /// <param name="teacherShowPageInput"></param>
        /// <returns></returns>
        TeacherShowPageOutput TeacherShowPage(TeacherShowPageInput teacherShowPageInput);

        /// <summary>
        /// 创建教师
        /// </summary>
        /// <param name="teacherCreateInput"></param>
        /// <returns></returns>
        ReturnVal TeacherCreate(TeacherCreateInput teacherCreateInput);

        /// <summary>
        /// 批量创建教师
        /// </summary>
        /// <param name="teacherCreateRangeInput"></param>
        /// <returns></returns>
        ReturnVal TeacherCreateRange(TeacherCreateRangeInput teacherCreateRangeInput);

        /// <summary>
        /// 更新教师信息
        /// </summary>
        /// <param name="teacherUpdateInput"></param>
        /// <returns></returns>
        ReturnVal TeacherUpdate(TeacherUpdateInput teacherUpdateInput);

        /// <summary>
        /// 删除教师
        /// </summary>
        /// <param name="teacherDeleteInput"></param>
        /// <returns></returns>
        ReturnVal TeacherDelete(TeacherDeleteInput teacherDeleteInput);

        /// <summary>
        /// 展示班级
        /// </summary>
        /// <param name="classShowInput"></param>
        /// <returns></returns>
        ClassShowOutput ClassShow(ClassShowInput classShowInput);

        /// <summary>
        /// 分页展示班级
        /// </summary>
        /// <param name="classShowPageInput"></param>
        /// <returns></returns>
        ClassShowPageOutput ClassShowPage(ClassShowPageInput classShowPageInput);

        /// <summary>
        /// 创建班级
        /// </summary>
        /// <param name="classCreateInput"></param>
        /// <returns></returns>
        ReturnVal ClassCreate(ClassCreateInput classCreateInput);

        /// <summary>
        /// 批量创建班级
        /// </summary>
        /// <param name="classCreateRangeInput"></param>
        /// <returns></returns>
        ReturnVal ClassCreateRange(ClassCreateRangeInput classCreateRangeInput);

        /// <summary>
        /// 更新班级信息
        /// </summary>
        /// <param name="classUpdateInput"></param>
        /// <returns></returns>
        ReturnVal ClassUpdate(ClassUpdateInput classUpdateInput);

        /// <summary>
        /// 删除班级
        /// </summary>
        /// <param name="classDeleteInput"></param>
        /// <returns></returns>
        ReturnVal ClassDelete(ClassDeleteInput classDeleteInput);
    }
}
