using ShiNengShiHui.Entities.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework;

namespace ShiNengShiHui.EntityFramework.Repositories
{
    public class ClassRepository : ShiNengShiHuiRepositoryBase<Class>, IClassRepository
    {
        public ClassRepository(IDbContextProvider<ShiNengShiHuiDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
