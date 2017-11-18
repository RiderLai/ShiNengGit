using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiNengShiHui.Entities.WeekGrades
{
    public class WeekGrade:Entity<long>
    {
        [Required]
        public virtual String GradeDataJson { get; set; }

        [Required]
        public virtual String DateJson { get; set; }

        public virtual int SID { get; set; }
    }
}
