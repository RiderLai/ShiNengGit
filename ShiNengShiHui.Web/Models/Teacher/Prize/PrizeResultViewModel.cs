using Abp.AutoMapper;
using ShiNengShiHui.AppServices.TeacherDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShiNengShiHui.Web.Models.Teacher.Prize
{
    [AutoMapFrom(typeof(ShowPrizeOutput))]
    public class PrizeResultViewModel
    {
        public string StudentName { get; set; }

        public string PrizeName { get; set; }

        public DateTime DateTime { get; set; }

        public string SchoolYearAndMore { get; set; }
    }
}