using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiNengShiHui.AppServices.TeacherDTO
{
    public class ShowPrizeOutput
    {
        public string StudentName { get; set; }

        public string PrizeName { get; set; }

        public DateTime DateTime { get; set; }

        public string SchoolYearAndMore { get; set; }
    }
}
