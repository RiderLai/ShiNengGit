using Abp.AutoMapper;
using ShiNengShiHui.AppServices.HeadmasterDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShiNengShiHui.Web.Models.Headmaster.Student
{
    [AutoMapFrom(typeof(StudentShowOutput))]
    public class StudentResultViewModel
    {

        public string Name { get; set; }

        public bool Sex { get; set; }

        public int? Group { get; set; }
    }
}