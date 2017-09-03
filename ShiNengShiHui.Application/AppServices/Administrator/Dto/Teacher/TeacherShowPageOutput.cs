using Abp.AutoMapper;
using ShiNengShiHui.Dto;

namespace ShiNengShiHui.AppServices.AdministratorDTO
{
    [AutoMapFrom(typeof(TeacherShowPageInput))]
    public class TeacherShowPageOutput:PageBaseDto
    {
        public TeacherShowOutput[] Teachers { get; set; }
    }
}