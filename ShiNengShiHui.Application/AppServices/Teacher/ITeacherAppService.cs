using Abp.Application.Services;
using ShiNengShiHui.AppServices.Return;
using ShiNengShiHui.AppServices.TeacherDTO;

namespace ShiNengShiHui.AppServices
{
    public interface ITeacherAppService:IApplicationService
    {
        /// <summary>
        /// 展示单个学生
        /// </summary>
        /// <param name="showStudentInput"></param>
        /// <returns></returns>
        ShowStudentOutput ShowStudent(ShowStudentInput showStudentInput);
        /// <summary>
        /// 分页展示学生
        /// </summary>
        /// <param name="showPageStudentInput"></param>
        /// <returns></returns>
        ShowPageStudentOutput ShowPageStudent(ShowPageStudentInput showPageStudentInput);

        /// <summary>
        /// 添加学生
        /// </summary>
        /// <param name="createStudentInput"></param>
        /// <returns></returns>
        ReturnVal CreateStudent(CreateStudentInput createStudentInput);
        /// <summary>
        /// 批量添加学生
        /// </summary>
        /// <param name="createStudentRangeInput"></param>
        /// <returns></returns>
        ReturnVal CreateStudentRange(CreateStudentRangeInput createStudentRangeInput);

        /// <summary>
        /// 更新学生信息
        /// </summary>
        /// <param name="updateStudentInput"></param>
        /// <returns></returns>
        ReturnVal UpdateStudent(UpdateStudentInput updateStudentInput);
        /// <summary>
        /// 批量更新学生成绩
        /// </summary>
        /// <param name="updateStudentRangeInput"></param>
        /// <returns></returns>
        ReturnVal UpdateStudentRange(UpdateStudentRangeInput updateStudentRangeInput);

        /// <summary>
        /// 删除学生
        /// </summary>
        /// <param name="deleteStudentInput"></param>
        /// <returns></returns>
        ReturnVal DeleteStudent(DeleteStudentInput deleteStudentInput);
        /// <summary>
        /// 批量删除学生
        /// </summary>
        /// <param name="deleteStudentRangeInput"></param>
        /// <returns></returns>
        ReturnVal DeleteStudentRange(DeleteStudentRangeInput deleteStudentRangeInput);

        /// <summary>
        /// 展示单个成绩
        /// </summary>
        /// <param name="showGradeInput"></param>
        /// <returns></returns>
        ShowGradeOutput ShowGrade(ShowGradeInput showGradeInput);
        /// <summary>
        /// 分页展示成绩
        /// </summary>
        /// <param name="showPageGradeInput"></param>
        /// <returns></returns>
        ShowPageGradeOutput ShowPageGrade(ShowPageGradeInput showPageGradeInput);

        /// <summary>
        /// 添加成绩
        /// </summary>
        /// <param name="createGradeInput"></param>
        /// <returns></returns>
        ReturnVal CreateGrade(CreateGradeInput createGradeInput);
        /// <summary>
        /// 批量添加成绩
        /// </summary>
        /// <param name="createGradeRangeInput"></param>
        /// <returns></returns>
        ReturnVal CreateGradeRange(CreateGradeRangeInput createGradeRangeInput);

        /// <summary>
        /// 更新成绩信息
        /// </summary>
        /// <param name="updateGradeInput"></param>
        /// <returns></returns>
        ReturnVal UpdateGrade(UpdateGradeInput updateGradeInput);
        /// <summary>
        /// 批量更新成绩信息
        /// </summary>
        /// <param name="updateGradeRangeInput"></param>
        /// <returns></returns>
        ReturnVal UpdateGradeRange(UpdateGradeRangeInput updateGradeRangeInput);

        /// <summary>
        /// 删除成绩
        /// </summary>
        /// <param name="deleteGradeInput"></param>
        /// <returns></returns>
        ReturnVal DeleteGrade(DeleteGradeInput deleteGradeInput);
        /// <summary>
        /// 批量删除成绩
        /// </summary>
        /// <param name="deleteGradeRangeInput"></param>
        /// <returns></returns>
        ReturnVal DeleteGradeRange(DeleteGradeRangeInput deleteGradeRangeInput);

        /// <summary>
        /// 展示奖项
        /// </summary>
        /// <param name="showPrizeInput"></param>
        /// <returns></returns>
        ShowPrizeOutput ShowPrize(ShowPrizeInput showPrizeInput);
        /// <summary>
        /// 分页展示成绩
        /// </summary>
        /// <param name="showPagePrizeInput"></param>
        /// <returns></returns>
        ShowPagePrizeOutput ShowPagePrize(ShowPagePrizeInput showPagePrizeInput);

        /// <summary>
        /// 奖项计算
        /// </summary>
        /// <param name="prizeCoumputInput"></param>
        void PrizeComput(PrizeComputInput prizeCoumputInput);

        /// <summary>
        /// 计算天模范生
        /// </summary>
        /// <param name="prizeComputInput"></param>
        void PrizeTianMoFanShengComput(PrizeTianMoFanShengComputInput prizeComputInput);

        /// <summary>
        /// 计算周模范生
        /// </summary>
        /// <param name="prizeZhouMoFanShengComputInput"></param>
        void PrizeZhouMoFanShengComput(PrizeZhouMoFanShengComputInput prizeZhouMoFanShengComputInput);

        /// <summary>
        /// 计算月模范生
        /// </summary>
        /// <param name="prizeYueMoFanShengComput"></param>
        void PrizeYueMoFanShengComput(PrizeYueMoFanShengComputInput prizeYueMoFanShengComput);

        /// <summary>
        /// 计算校模范生
        /// </summary>
        /// <param name="prizeXiaoMoFanShengComput"></param>
        void PrizeXiaoMoFanShengComput(PrizeXiaoMoFanShengComputInput prizeXiaoMoFanShengComput);
    }
}
