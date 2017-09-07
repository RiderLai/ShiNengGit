using Abp.AutoMapper;
using ShiNengShiHui.Dto;

namespace ShiNengShiHui.AppServices.HeadmasterDTO
{
    [AutoMapFrom(typeof(StudentShowPageInput))]
    public class StudentShowPageOutput:PageBaseDto
    {
        public StudentShowOutput[] Students { get; set; }
    }
}