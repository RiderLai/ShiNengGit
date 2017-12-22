using Abp.AutoMapper;
using ShiNengShiHui.AppServices.TeacherDTO;
using ShiNengShiHui.Entities.OtherData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShiNengShiHui.Web.Models.Teacher.Grade
{
    [AutoMapFrom(typeof(GroupWeekGradeShowOutput))]
    public class GroupWeekGradeShowModel
    {
        public long Id { get; set; }
        public int Group { get; set; }
        public WeekDate Date { get; set; }
        public bool IsWell { get; set; }
    }
}