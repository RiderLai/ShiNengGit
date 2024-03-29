﻿using Abp.Domain.Entities.Auditing;
using ShiNengShiHui.Entities.Classes;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShiNengShiHui.Entities.Teachers
{
    public class Teacher:FullAuditedEntity
    {
        [Required]
        [StringLength(maximumLength: 10)]
        public virtual string Name { get; set; }

        [Required]
        public virtual bool Sex { get; set; }

        public virtual DateTime? BirthDay { get; set; }

        public virtual int ClassId { get; set; }

        [ForeignKey("ClassId")]
        public virtual Class Class { get; set; }
    }
}
