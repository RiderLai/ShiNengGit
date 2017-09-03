using Abp.AutoMapper;
using ShiNengShiHui.Dto;

namespace ShiNengShiHui.AppServices.AdministratorDTO
{
    [AutoMapFrom(typeof(ClassShowPageInput))]
    public class ClassShowPageOutput:PageBaseDto
    {
        public ClassShowOutput[] Classes { get; set; }
    }
}