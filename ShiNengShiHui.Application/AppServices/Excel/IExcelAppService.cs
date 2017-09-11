using Abp.Application.Services;
using ShiNengShiHui.AppServices.ExcelDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiNengShiHui.AppServices
{
    public interface IExcelAppService:IApplicationService
    {
        GradeExcelDownOutput GradeExcelDown();

        GradeInsertOfExcelOutput GradeInsertOfExcel(GradeInsertOfExcelInput gradeInsertOfExcelInput);

        StudentExcelDownOutput StudentExcelDown();

        StudentInsertOfExcelOutput StudentInsertOfExcel(StudentInsertOfExcelInput studentInsertOfExcelInput);

        UserExcelDownOutput UserExcelDown();

        UserInsertOfExcelOutput UserInsertOfExcel(UserInsertOfExcelInput userInsertOfExcelInput);

        TeacherExcelDownOutput TeacherExcelDown();

        TeacherInsertOfExcelOutput TeacherInsertOfExcel(TeacherInsertOfExcelInput teacherInsertOfExcelInput);

        ClassExcelDownOutput ClassExcelDown();

        ClassInsertOfExcelOutput ClassInsertOfExcel(ClassInsertOfExcelInput classInsertOfExcelInput);
    }
}
