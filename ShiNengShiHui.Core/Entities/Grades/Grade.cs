using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using ShiNengShiHui.Entities.Students;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShiNengShiHui.Entities.Grades
{
    public class Grade:FullAuditedEntity<long>
    {
        public virtual int StudentId { get; set; }

        [Required]
        public virtual string DateJson { get; set; }

        [Required]
        public virtual string GradeStringJson { get; set; }

        [Required]
        [ForeignKey("StudentId")]
        public virtual Student Student { get; set; }
    }
}
