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
        GradeExcelOutput GradeExcelDown();

        StudentExcelDownOutput StudentExcelDown();

        GradeInsertOfExcelOutput GradeInsertOfExcel(GradeInsertOfExcelInput gradeInsertOfExcelInput);

        StudentInsertOfExcelOutput StudentInsertOfExcel(StudentInsertOfExcelInput studentInsertOfExcelInput);
    }
}
