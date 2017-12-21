using ShiNengShiHui.Entities.OtherData;
using System;

namespace ShiNengShiHui.AppServices.TeacherDTO
{
    public class GroupWeekGradeShowOutput
    {
        public WeekDate Date { get; set; }

        public long Id { get; set; }

        public bool IsWell { get; set; }

        public int Group { get; set; }
    }
}