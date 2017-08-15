using Abp.Application.Services;
using ShiNengShiHui.AppServices.Teacher.Dto;

namespace ShiNengShiHui.AppServices.Teacher
{
    public interface ITeacherAppService:IApplicationService
    {
        ShowStudentOutput ShowStudent(ShowStudentInput showStudentInput);
        ShowPageStudentOutput ShowPageStudent(ShowPageStudentInput showPageStudentInput);


        CreateStudentOutput CreateStudent(CreateStudentInput createStudentInput);
        CreateStudentRangeOutput CreateStudentRange(CreateStudentRangeInput createStudentRangeInput);

        UpdateStudentOutput UpdateStudent(UpdateStudentInput updateStudentInput);
        UpdateStudentRangeOutput UpdateStudentRange(UpdateStudentRangeInput updateStudentRangeInput);

        DeleteStudentOutput DeleteStudent(DeleteStudentInput deleteStudentInput);
        DeleteStudentRangeOutput DeleteStudentRange(DeleteStudentRangeInput deleteStudentRangeInput);

        ShowGradeOutput ShowGrade(ShowGradeInput showGradeInput);
        ShowPageGradeOutput ShowPageGrade(ShowPageGradeInput showPageGradeInput);

        CreateGradeOutput CreateGrade(CreateGradeInput createGradeInput);
        CreateGradeRangeOutput CreateGradeRange(CreateGradeRangeInput createGradeRangeInput);

        UpdateGradeOutput UpdateGrade(UpdateGradeInput updateGradeInput);
        UpdateGradeRangeOutput UpdateGradeRange(UpdateGradeRangeInput updateGradeRangeInput);

        DeleteGradeOutput DeleteGrade(DeleteGradeInput deleteGradeInput);
        DeleteGradeRangeOutput DeleteGradeRange(DeleteGradeRangeInput deleteGradeRangeInput);

        ShowPrizeOutput ShowPrize(ShowPrizeInput showPrizeInput);
        ShowPagePrizeOutput ShowPagePrize(ShowPagePrizeInput showPagePrizeInput);
    }
}
