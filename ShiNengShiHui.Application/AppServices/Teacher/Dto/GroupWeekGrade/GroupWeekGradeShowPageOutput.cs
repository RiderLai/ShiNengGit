using Abp.AutoMapper;
using ShiNengShiHui.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiNengShiHui.AppServices.TeacherDTO
{
    [AutoMapFrom(typeof(GroupWeekGradeShowPageInput))]
    public class GroupWeekGradeShowPageOutput: PageBaseDto
    {
        public GroupWeekGradeShowOutput[] groupWeekGrades { get; set; }
    }
}
