using Abp.Application.Services;
using ShiNengShiHui.AppServices.HeadmasterDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiNengShiHui.AppServices
{
    public interface IHeadmasterAppService:IApplicationService
    {
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
        /// 展示学生
        /// </summary>
        /// <param name="studentShowInput"></param>
        /// <returns></returns>
        StudentShowOutput StudentShow(StudentShowInput studentShowInput);

        /// <summary>
        /// 分页展示学生
        /// </summary>
        /// <param name="studentShowPageInput"></param>
        /// <returns></returns>
        StudentShowPageOutput StudentShowPage(StudentShowPageInput studentShowPageInput);

        /// <summary>
        /// 展示成绩
        /// </summary>
        /// <param name="gradeShowInput"></param>
        /// <returns></returns>
        GradeShowOutput GradeShow(GradeShowInput gradeShowInput);

        /// <summary>
        /// 分页展示成绩
        /// </summary>
        /// <param name="gradeShowPageInput"></param>
        /// <returns></returns>
        GradeShowPageOutput GradeShowPage(GradeShowPageInput gradeShowPageInput);

        /// <summary>
        /// 展示奖项
        /// </summary>
        /// <param name="prizeShowInput"></param>
        /// <returns></returns>
        PrizeShowOutput PrizeShow(PrizeShowInput prizeShowInput);

        /// <summary>
        /// 分页展示奖项
        /// </summary>
        /// <param name="prizeShowPageInput"></param>
        /// <returns></returns>
        PrizeShowPageOutput PrizeShowPage(PrizeShowPageInput prizeShowPageInput);
    }
}
