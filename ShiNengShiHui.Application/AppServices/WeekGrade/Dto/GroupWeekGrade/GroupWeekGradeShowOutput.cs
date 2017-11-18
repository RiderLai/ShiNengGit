using ShiNengShiHui.Entities.WeekGrades;
using System;

namespace ShiNengShiHui.AppServices.WeekGradeDTO
{
    public class GroupWeekGradeShowOutput
    {
        public GroupWeekGradeShow[] GroupWeekGradeShows { get; set; }
    }

    public class GroupWeekGradeShow
    {
        public WeekDate Date { get; set; }

        public long Id { get; set; }

        public bool IsWell { get; set; }

        public int Group { get; set; }
    }
}