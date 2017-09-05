using Abp.AutoMapper;
using ShiNengShiHui.AppServices.AdministratorDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShiNengShiHui.Web.Models.Administrator.Class
{
    [AutoMapFrom(typeof(ClassShowOutput))]
    [AutoMapTo(typeof(ClassUpdateInput))]
    public class ClassEditModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Display { get; set; }

        public DateTime InTime { get; set; }
    }
}