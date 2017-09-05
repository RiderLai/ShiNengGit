using Abp.AutoMapper;
using ShiNengShiHui.AppServices.AdministratorDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShiNengShiHui.Web.Models.Administrator.Teacher
{
    [AutoMapFrom(typeof(TeacherShowOutput))]
    [AutoMapTo(typeof(TeacherUpdateInput))]
    public class TeacherEditModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool Sex { get; set; }

        public DateTime? BirthDay { get; set; }

        public int ClassId { get; set; }
    }
}