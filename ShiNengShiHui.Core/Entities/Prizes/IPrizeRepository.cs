﻿using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiNengShiHui.Entities.Prizes
{
    public interface IPrizeRepository:IRepository<Prize,long>
    {
    }
}
