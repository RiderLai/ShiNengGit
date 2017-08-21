using Abp.AutoMapper;
using ShiNengShiHui.AppServices.Teacher.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShiNengShiHui.Web.Models.Teacher.Student
{
    [AutoMapTo(typeof(CreateStudentInput))]
    public class StudentCreatreViewModel
    {
        [Required]
        public string Name { get; set; }

        public bool Sex { get; set; }

        public int? Group { get; set; }
    }
}