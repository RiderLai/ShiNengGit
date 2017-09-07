using Abp.AutoMapper;
using ShiNengShiHui.Entities.Students;

namespace ShiNengShiHui.AppServices.HeadmasterDTO
{
    [AutoMapFrom(typeof(Student))]
    public class StudentShowOutput
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool Sex { get; set; }

        public int? Group { get; set; }
    }
}