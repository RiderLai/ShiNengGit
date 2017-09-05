using Abp.Application.Services.Dto;
using Abp.Runtime.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiNengShiHui.AppServices.TeacherDTO
{
    public class CreateGradeInput
    {
        public int StudentId { get; set; }

        public int[] Grades { get; set; }

        public string PenaltyReason { get; set; }

        public DateTime Datetime { get; set; }

        public int SchoolYead { get; set; }

        public int Semester { get; set; }

        public int Week { get; set; }

        //public void Normalize()
        //{
        //    if (this.PenaltyReason.Equals("")||this.PenaltyReason==null)
        //    {
        //        this.PenaltyReason = "无";
        //    }
        //}
    }
}
