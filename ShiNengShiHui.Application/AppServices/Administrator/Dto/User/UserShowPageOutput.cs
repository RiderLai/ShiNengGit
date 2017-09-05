using Abp.AutoMapper;
using ShiNengShiHui.Dto;

namespace ShiNengShiHui.AppServices.AdministratorDTO
{
    [AutoMapFrom(typeof(UserShowPageInput))]
    public class UserShowPageOutput:PageBaseDto
    {
        public UserShowOutput[] Users { get; set; }

    }
}