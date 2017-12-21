using System;

namespace ShiNengShiHui.AppServices.TeacherDTO
{
    public class WeekGradeShowOutput
    {
        public WeekGradeShow[] WeekGradeShows { get; set; }

        public bool IsWellGroup { get; set; }
    }

    public class WeekGradeShow
    {
        public long Id { get; set; }
        public String Name { get; set; }
        public int[] Grades { get; set; }
    }
}