using Abp.Application.Services;
using ShiNengShiHui.AppServices.HeadmasterDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiNengShiHui.AppServices
{
    interface IHeadmasterAppService:IApplicationService
    {
        TeacherShowOutput TeacherShow(TeacherShowInput teacherShowInput);

        TeacherShowPageOutput TeacherShowPage(TeacherShowPageInput teacherShowPageInput);

        ClassShowOutput ClassShow(ClassShowInput classShowInput);

        ClassShowPageOutput ClassShowPage(ClassShowPageInput classShowPageInput);

        StudentShowOutput StudentShow(StudentShowInput studentShowInput);

        StudentShowPageOutput StudentShowPage(StudentShowPageInput studentShowPageInput);

        GradeShowOutput GradeShow(GradeShowInput gradeShowInput);

        GradeShowPageOutput GradeShowPage(GradeShowPageInput gradeShowPageInput);

        PrizeShowOutput PrizeShow(PrizeShowInput prizeShowInput);

        PrizeShowPageOutput PrizeShowPage(PrizeShowPageInput prizeShowPageInput);
    }
}
