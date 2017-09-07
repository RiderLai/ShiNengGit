using Abp.AutoMapper;
using ShiNengShiHui.AppServices.HeadmasterDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShiNengShiHui.Web.Models.Headmaster.Prize
{
    [AutoMapFrom(typeof(PrizeShowOutput))]
    public class PrizeResultViewModel
    {
        public string StudentName { get; set; }

        public string PrizeName { get; set; }

        public DateTime DateTime { get; set; }

        public string SchoolYearAndMore { get; set; }
    }
}