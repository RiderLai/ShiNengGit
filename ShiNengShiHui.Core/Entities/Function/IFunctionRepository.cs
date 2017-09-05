using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiNengShiHui.Entities.Function
{
    public interface IFunctionRepository:IRepository<Function>
    {
        List<Function> GetFunctionOfRoles();
    }
}
