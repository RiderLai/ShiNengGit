using Abp.AutoMapper;
using ShiNengShiHui.Dto;

namespace ShiNengShiHui.AppServices.HeadmasterDTO
{
    [AutoMapFrom(typeof(ClassShowPageInput))]
    public class ClassShowPageOutput:PageBaseDto
    {
        public ClassShowOutput[] Classes { get; set; }
    }
}