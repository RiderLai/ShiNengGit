using Abp.AutoMapper;
using ShiNengShiHui.Entities.WeekGrades;
using System;

namespace ShiNengShiHui.AppServices.WeekGradeDTO
{
    [AutoMapTo(typeof(WeekDate))]
    public class WeekGradeCreateInput
    {
        public WeekGradeCreate[] StudentWeekGrades { get; set; }

        public String SchoolYear { get; set; }
        public String Semester { get; set; }
        public String Week { get; set; }
    }

    [AutoMapTo(typeof(WeekGrade))]
    public class WeekGradeCreate
    {
        public int SID { get; set; }

        public int[] Grades { get; set; }
    }
}