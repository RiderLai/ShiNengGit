﻿using Abp.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace ShiNengShiHui.Entities.Prizes
{
    public class PrizeItem:Entity<Guid>
    {
        #region PrizeItemConst
        public const string TianMoFanSheng = "天模范生";
        public const string ZhouMoFanSheng = "周模范生";
        public const string YueMoFanSheng = "月模范生";
        #endregion

        [Required]
        public virtual string Name { get; set; }
    }
}
