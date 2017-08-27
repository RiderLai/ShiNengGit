﻿using Abp.AutoMapper;
using ShiNengShiHui.Entities.Teachers;
using System;

namespace ShiNengShiHui.AppServices.AdministratorDTO
{
    [AutoMapTo(typeof(Teacher))]
    public class TeacherCreateInput
    {
        public string Name { get; set; }

        public bool Sex { get; set; }

        public DateTime? BirthDay { get; set; }

        public int ClassId { get; set; }
    }
}