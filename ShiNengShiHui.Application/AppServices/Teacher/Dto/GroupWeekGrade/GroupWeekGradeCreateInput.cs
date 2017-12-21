using Abp.AutoMapper;
using ShiNengShiHui.Entities.WeekGrades;
using System;

namespace ShiNengShiHui.AppServices.TeacherDTO
{
    public class GroupWeekGradeCreateInput
    {
        public String SchoolYear { get; set; }
        public String Semester { get; set; }
        public String Week { get; set; }

        public bool IsWell { get; set; }

        public int Group { get; set; }
    }
}