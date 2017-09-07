using Abp.AutoMapper;
using ShiNengShiHui.Dto;

namespace ShiNengShiHui.AppServices.HeadmasterDTO
{
    [AutoMapFrom(typeof(GradeShowPageInput))]
    public class GradeShowPageOutput:PageBaseDto
    {
        public GradeShowOutput[] Grades { get; set; }
    }
}