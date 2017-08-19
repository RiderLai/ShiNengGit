using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiNengShiHui.AppServices.Teacher.Dto
{
    public class UpdateGradeInput
    {
        [Required]
        public long Id { get; set; }

        [Required]
        public int StudentId { get; set; }

        [Required]
        public int[] Grades { get; set; }

        [Required]
        public DateTime Datetime { get; set; }

        [Required]
        public int SchoolYead { get; set; }

        [Required]
        public int Semester { get; set; }

        [Required]
        public int Week { get; set; }
    }
}
