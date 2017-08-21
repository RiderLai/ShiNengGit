using Abp.AutoMapper;
using ShiNengShiHui.Entities.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiNengShiHui.AppServices.Teacher.Dto
{
    [AutoMapTo(typeof(Student))]
    public class UpdateStudentInput
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool Sex { get; set; }

        public int? Group { get; set; }

        public int ClassId { get; set; }
    }
}
