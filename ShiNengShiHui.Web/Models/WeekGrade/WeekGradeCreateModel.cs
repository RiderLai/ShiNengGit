using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShiNengShiHui.Web.Models.WeekGrade
{
    public class WeekGradeCreateModel
    {
        public List<WeekGrade> WeekGrades { get; set; }
        public String SchoolYear { get; set; }
        public String Semester { get; set; }
        public String Week { get; set; }
    }

    public class WeekGrade
    {
        public long Id { get; set; }
        public int SID { get; set; }
        public String Name { get; set; }
        public int[] Grades { get; set; }
    }
}