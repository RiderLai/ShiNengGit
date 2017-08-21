using Abp.AutoMapper;
using ShiNengShiHui.AppServices.Teacher.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShiNengShiHui.Web.Models.Teacher.Student
{
    [AutoMapFrom(typeof(ShowStudentOutput))]
    public class StudentResultViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool Sex { get; set; }

        public int? Group { get; set; }
    }
}