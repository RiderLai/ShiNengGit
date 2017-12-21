using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiNengShiHui.Entities.Grades
{
    public class GroupWeekGrade:Entity<long>
    {
        [Required]
        public virtual String DateJson { get; set; }

        public virtual int Group { get; set; }

        public virtual bool IsWell { get; set; }
    }
}
