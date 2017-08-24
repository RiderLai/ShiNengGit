using Abp.Application.Services;
using ShiNengShiHui.AppServices.Return;
using ShiNengShiHui.AppServices.Teacher.Dto;

namespace ShiNengShiHui.AppServices
{
    public interface ITeacherAppService:IApplicationService
    {
        ShowStudentOutput ShowStudent(ShowStudentInput showStudentInput);
        ShowPageStudentOutput ShowPageStudent(ShowPageStudentInput showPageStudentInput);


        ReturnVal CreateStudent(CreateStudentInput createStudentInput);
        ReturnVal CreateStudentRange(CreateStudentRangeInput createStudentRangeInput);

        ReturnVal UpdateStudent(UpdateStudentInput updateStudentInput);
        ReturnVal UpdateStudentRange(UpdateStudentRangeInput updateStudentRangeInput);

        ReturnVal DeleteStudent(DeleteStudentInput deleteStudentInput);
        ReturnVal DeleteStudentRange(DeleteStudentRangeInput deleteStudentRangeInput);

        ShowGradeOutput ShowGrade(ShowGradeInput showGradeInput);
        ShowPageGradeOutput ShowPageGrade(ShowPageGradeInput showPageGradeInput);

        ReturnVal CreateGrade(CreateGradeInput createGradeInput);
        ReturnVal CreateGradeRange(CreateGradeRangeInput createGradeRangeInput);

        ReturnVal UpdateGrade(UpdateGradeInput updateGradeInput);
        ReturnVal UpdateGradeRange(UpdateGradeRangeInput updateGradeRangeInput);

        ReturnVal DeleteGrade(DeleteGradeInput deleteGradeInput);
        ReturnVal DeleteGradeRange(DeleteGradeRangeInput deleteGradeRangeInput);

        ShowPrizeOutput ShowPrize(ShowPrizeInput showPrizeInput);
        ShowPagePrizeOutput ShowPagePrize(ShowPagePrizeInput showPagePrizeInput);

        void PrizeComput(PrizeComputInput prizeComputInput);
    }
}
