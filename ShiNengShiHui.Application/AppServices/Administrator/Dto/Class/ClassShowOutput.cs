using Abp.AutoMapper;
using ShiNengShiHui.Entities.Classes;
using System;

namespace ShiNengShiHui.AppServices.AdministratorDTO
{
    [AutoMapFrom(typeof(Class))]
    public class ClassShowOutput
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Display { get; set; }

        public DateTime InTime { get; set; }

        public DateTime CreationTime { get; set; }
    }
}