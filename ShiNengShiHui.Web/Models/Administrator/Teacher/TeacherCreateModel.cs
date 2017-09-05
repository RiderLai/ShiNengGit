using Abp.AutoMapper;
using ShiNengShiHui.AppServices.AdministratorDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShiNengShiHui.Web.Models.Administrator.Teacher
{
    [AutoMapTo(typeof(TeacherCreateInput))]
    public class TeacherCreateModel
    {
        public string Name { get; set; }

        public bool Sex { get; set; }

        public DateTime? BirthDay { get; set; }

        public int ClassId { get; set; }
    }
}