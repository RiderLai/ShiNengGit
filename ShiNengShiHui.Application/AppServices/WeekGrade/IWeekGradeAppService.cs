using Abp.Application.Services;
using ShiNengShiHui.AppServices.Return;
using ShiNengShiHui.AppServices.WeekGradeDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiNengShiHui.AppServices
{
    public interface IWeekGradeAppService:IApplicationService
    {
        ReturnVal WeekGradeCreate(WeekGradeCreateInput weekGradeCreateInput);

        ReturnVal WeekGradeUpdate(WeekGradeUpdateInput weekGradeUpdateInput);

        WeekGradeShowOutput WeekGradeShow(WeekGradeShowInput weekGradeShowInput);

        ReturnVal GroupWeekGradeCreate(GroupWeekGradeCreateInput groupWeekGradeCreateInput);

        ReturnVal GroupWeekGradeUpdate(GroupWeekGradeUpdate groupWeekGradeUpdate);

        GroupWeekGradeShowOutput GroupWeekGradeShow(GroupWeekGradeShowInput groupWeekGradeShowInput);
    }
}
