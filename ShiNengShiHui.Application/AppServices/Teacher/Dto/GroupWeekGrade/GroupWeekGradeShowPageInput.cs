using Abp.AutoMapper;
using ShiNengShiHui.Dto;
using ShiNengShiHui.Entities.OtherData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiNengShiHui.AppServices.TeacherDTO
{
    [AutoMapTo(typeof(WeekDate))]
    public class GroupWeekGradeShowPageInput:PageBaseDto
    {
        public String SchoolYear { get; set; }
        public String Semester { get; set; }
        public String Week { get; set; }
    }
}
