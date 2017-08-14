using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using ShiNengShiHui.Entities.Students;
using System.ComponentModel.DataAnnotations;

namespace ShiNengShiHui.Entities.Prizes
{
    public class Prize:FullAuditedEntity<long>
    {
        [Required]
        public virtual Student Student { get; set; }

        [Required]
        public virtual string DateJosn { get; set; }

        [Required]
        public virtual PrizeItem PrizeItem { get; set; }
    }
}
