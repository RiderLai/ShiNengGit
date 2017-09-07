using Abp.AutoMapper;
using ShiNengShiHui.Dto;

namespace ShiNengShiHui.AppServices.HeadmasterDTO
{
    [AutoMapFrom(typeof(PrizeShowPageInput))]
    public class PrizeShowPageOutput:PageBaseDto
    {
        public PrizeShowOutput[] Prizes { get; set; }
    }
}