using Abp.AutoMapper;
using ShiNengShiHui.AppServices.TeacherDTO;

namespace ShiNengShiHui.Web.Models.Teacher.Student
{
    [AutoMapFrom(typeof(ShowStudentOutput))]
    public class StudentResultViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool Sex { get; set; }

        public int? Group { get; set; }
    }
}