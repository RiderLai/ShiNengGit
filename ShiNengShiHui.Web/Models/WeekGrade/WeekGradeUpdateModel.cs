using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShiNengShiHui.Web.Models.WeekGrade
{
    public class WeekGradeUpdateModel
    {
        public List<WeekGrade> WeekGrades { get; set; }
        public String SchoolYear { get; set; }
        public String Semester { get; set; }
        public String Week { get; set; }
    }
}