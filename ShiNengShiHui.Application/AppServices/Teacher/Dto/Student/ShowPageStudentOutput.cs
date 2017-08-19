using ShiNengShiHui.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiNengShiHui.AppServices.Teacher.Dto
{
    public class ShowPageStudentOutput:PageBaseDto
    {
        public ShowStudentOutput[] ShowStudentOutputs { get; set; }

        public int Lenth { get; set; }
    }
}
