using Abp.AutoMapper;
using ShiNengShiHui.AppServices.AdministratorDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShiNengShiHui.Web.Models.Administrator.Class
{
    [AutoMapTo(typeof(ClassCreateInput))]
    public class ClassCreateModel
    {
        public string Name { get; set; }

        public string Display { get; set; }

        public DateTime InTime { get; set; }
    }
}