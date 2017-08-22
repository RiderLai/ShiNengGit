using Abp.AutoMapper;
using ShiNengShiHui.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiNengShiHui.AppServices.Teacher.Dto
{ 
    [AutoMapFrom(typeof(ShowPageGradeInput))]
    public class ShowPageGradeOutput:PageBaseDto
    {
        public ShowGradeOutput[] ShowGradeOutputs { get; set; }

        public int Lenth { get; set; }
    }
}
