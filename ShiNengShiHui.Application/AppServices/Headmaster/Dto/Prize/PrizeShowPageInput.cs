using ShiNengShiHui.Dto;
using System;

namespace ShiNengShiHui.AppServices.HeadmasterDTO
{
    public class PrizeShowPageInput:PageBaseDto
    {
        public int ClassId { get; set; }

        public ScreenEnum ScreenCondition { get; set; }

        public DateTime DateTime { get; set; }
    }
}