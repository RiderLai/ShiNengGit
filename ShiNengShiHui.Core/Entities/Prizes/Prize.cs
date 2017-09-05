using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using ShiNengShiHui.Entities.Students;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShiNengShiHui.Entities.Prizes
{
    public class Prize:Entity<long>
    {
        public virtual int StudentId { get; set; }

        [Required]
        public virtual string DateJosn { get; set; }

        public virtual Guid PrizeItemId { get; set; }

        [Required]
        [ForeignKey("PrizeItemId")]
        public virtual PrizeItem PrizeItem { get; set; }

        [Required]
        [ForeignKey("StudentId")]
        public virtual Student Student { get; set; }
    }
}
