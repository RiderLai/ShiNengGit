using Abp.AutoMapper;
using ShiNengShiHui.AppServices.TeacherDTO;
using System.ComponentModel.DataAnnotations;

namespace ShiNengShiHui.Web.Models.Teacher.Student
{
    [AutoMapTo(typeof(CreateStudentInput))]
    public class StudentCreatreViewModel
    {
        [Required]
        public string Name { get; set; }

        public bool Sex { get; set; }

        public int? Group { get; set; }
    }
}