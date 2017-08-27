using Abp.AutoMapper;
using ShiNengShiHui.AppServices.TeacherDTO;
using System.ComponentModel.DataAnnotations;

namespace ShiNengShiHui.Web.Models.Teacher.Student
{
    [AutoMapFrom(typeof(ShowStudentOutput))]
    [AutoMapTo(typeof(UpdateStudentInput))]
    public class StudentEditViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public bool Sex { get; set; }

        public int? Group { get; set; }
    }
}