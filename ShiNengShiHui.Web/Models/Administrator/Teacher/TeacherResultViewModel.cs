using Abp.AutoMapper;
using ShiNengShiHui.AppServices.AdministratorDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShiNengShiHui.Web.Models.Administrator.Teacher
{
    [AutoMapFrom(typeof(TeacherShowOutput))]
    public class TeacherResultViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool Sex { get; set; }

        public DateTime? BirthDay { get; set; }

        public string ClassName { get; set; }
    }
}