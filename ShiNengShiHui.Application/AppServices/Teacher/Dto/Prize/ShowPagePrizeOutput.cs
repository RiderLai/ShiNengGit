﻿using ShiNengShiHui.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiNengShiHui.AppServices.TeacherDTO
{
    public class ShowPagePrizeOutput:PageBaseDto
    {
        public ShowPrizeOutput[] ShowPrizeOutputs { get; set; }

        public int Lenth;
    }
}
