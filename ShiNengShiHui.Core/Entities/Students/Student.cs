using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using ShiNengShiHui.Entities.Classes;
using ShiNengShiHui.Entities.Grades;
using ShiNengShiHui.Entities.Prizes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShiNengShiHui.Entities.Students
{
    public class Student:FullAuditedEntity
    {
        [Required]
        [StringLength(maximumLength: 10)]
        public virtual string Name { get; set; }

        [Required]
        public virtual bool sex { get; set; }

        [Required]
        public virtual Class Class { get; set; }

        public virtual ICollection<Grade> Grades { get; set; }

        public virtual ICollection<Prize> Prizes { get; set; }

    }
}
