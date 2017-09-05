using Abp.AutoMapper;
using ShiNengShiHui.AppServices.AdministratorDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShiNengShiHui.Web.Models.Administrator.Class
{
    [AutoMapFrom(typeof(ClassShowOutput))]
    public class ClassResultViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Display { get; set; }

        public DateTime InTime { get; set; }

        public DateTime CreationTime { get; set; }
    }
}