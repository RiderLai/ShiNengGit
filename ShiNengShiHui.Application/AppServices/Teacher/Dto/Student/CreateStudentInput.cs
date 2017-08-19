using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiNengShiHui.AppServices.Teacher.Dto
{
    public class CreateStudentInput:FullAuditedEntityDto
    {
        public string Name { get; set; }

        public bool Sex { get; set; }

        public int? Group { get; set; }

        public int ClassId { get; set; }
    }
}
