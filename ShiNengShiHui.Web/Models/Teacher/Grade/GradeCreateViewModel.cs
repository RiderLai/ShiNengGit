using Abp.AutoMapper;
using ShiNengShiHui.AppServices.Teacher.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShiNengShiHui.Web.Models.Teacher.Grade
{
    [AutoMapTo(typeof(CreateGradeInput))]
    public class GradeCreateViewModel
    {
        public int StudentId { get; set; }

        public int[] Grades { get; set; }

        public string PenaltyReason { get; set; }

        public DateTime Datetime { get; set; }

        public int SchoolYead { get; set; }

        public int Semester { get; set; }

        public int Week { get; set; }
    }
}