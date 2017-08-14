using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using ShiNengShiHui.Entities.Students;
using System.ComponentModel.DataAnnotations;

namespace ShiNengShiHui.Entities.Grades
{
    public class Grade:FullAuditedEntity<long>
    {
        [Required]
        public virtual Student Student { get; set; }

        [Required]
        public virtual string DateJson { get; set; }

        [Required]
        public virtual string GradeStringJson { get; set; }
    }
}
