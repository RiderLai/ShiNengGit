using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiNengShiHui.AppServices.TeacherDTO
{
    public class UpdateGradeInput
    {
        public long Id { get; set; }

        public int[] Grades { get; set; }

        public string PenaltyReason { get; set; }

        public DateTime Datetime { get; set; }

        public int SchoolYead { get; set; }

        public int Semester { get; set; }

        public int Week { get; set; }
    }
}
