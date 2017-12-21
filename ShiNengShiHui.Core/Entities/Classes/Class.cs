using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;

namespace ShiNengShiHui.Entities.Classes
{
    public class Class : FullAuditedEntity
    {
        [StringLength(maximumLength: 20)]
        [Required]
        public virtual string Name { get; set; }

        public virtual string Display { get; set; }

        public virtual DateTime InTime { get; set; }

        public string StudentsTable { get => Name + "Students"; }

        public string GradesTable { get => Name + "Grades"; }

        public string PrizesTable { get => Name + "Prizes"; }

        public string GroupWeekGradeTable { get => Name + "GroupWeekGrades"; }

    }
}
