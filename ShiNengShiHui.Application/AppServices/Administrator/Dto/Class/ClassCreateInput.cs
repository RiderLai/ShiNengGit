using Abp.AutoMapper;
using ShiNengShiHui.Entities.Classes;
using System;

namespace ShiNengShiHui.AppServices.AdministratorDTO
{
    [AutoMapTo(typeof(Class))]
    public class ClassCreateInput
    {
        public string Name { get; set; }

        public string Display { get; set; }

        public DateTime InTime { get; set; }
    }
}