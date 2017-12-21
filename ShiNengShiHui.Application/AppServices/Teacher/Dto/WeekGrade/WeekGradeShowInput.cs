using Abp.AutoMapper;
using ShiNengShiHui.Entities.OtherData;
using System;

namespace ShiNengShiHui.AppServices.TeacherDTO
{
    [AutoMapTo(typeof(WeekDate))]
    public class WeekGradeShowInput
    {
        public String SchoolYear { get; set; }
        public String Semester { get; set; }
        public String Week { get; set; }

        public int GroupId { get; set; }
    }
}