﻿using Abp.Specifications;
using ShiNengShiHui.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiNengShiHui.AppServices.Teacher.Dto
{
    public class ShowPageStudentInput : PageBaseDto
    {
        public ScreenEnum ScreenCondition { get; set; }

        public DateTime DateTime { get; set; }
    }
}
