using Abp.AutoMapper;
using ShiNengShiHui.Dto;

namespace ShiNengShiHui.AppServices.HeadmasterDTO
{
    [AutoMapFrom(typeof(TeacherShowPageInput))]
    public class TeacherShowPageOutput:PageBaseDto
    {
        public TeacherShowOutput[] Teachers { get; set; }
    }
}