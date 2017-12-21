using Abp.AutoMapper;
using ShiNengShiHui.Entities.Grades;
using ShiNengShiHui.Entities.OtherData;
using System;

namespace ShiNengShiHui.AppServices.TeacherDTO
{
    [AutoMapTo(typeof(WeekDate))]
    public class WeekGradeCreateInput
    {
        public WeekGradeCreate[] StudentWeekGrades { get; set; }

        public String SchoolYear { get; set; }
        public String Semester { get; set; }
        public String Week { get; set; }
    }

    [AutoMapTo(typeof(Grade))]
    public class WeekGradeCreate
    {
        public int StudentId { get; set; }

        public int[] Grades { get; set; }
    }
}