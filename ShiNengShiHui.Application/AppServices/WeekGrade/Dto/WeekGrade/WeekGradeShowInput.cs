using Abp.AutoMapper;
using ShiNengShiHui.Entities.WeekGrades;
using System;

namespace ShiNengShiHui.AppServices.WeekGradeDTO
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