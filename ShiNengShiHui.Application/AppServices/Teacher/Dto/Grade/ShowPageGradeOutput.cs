using ShiNengShiHui.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiNengShiHui.AppServices.Teacher.Dto
{ 
    public class ShowPageGradeOutput:PageBaseDto
    {
        public ShowGradeOutput[] ShowGradeOutputs { get; set; }

        public int Lenth { get; set; }
    }
}
