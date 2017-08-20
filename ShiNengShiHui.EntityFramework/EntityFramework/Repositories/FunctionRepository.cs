using ShiNengShiHui.Entities.Function;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework;
using Abp.Runtime.Session;

namespace ShiNengShiHui.EntityFramework.Repositories
{
    public class FunctionRepository : ShiNengShiHuiRepositoryBase<Function>, IFunctionRepository
    {
        public IAbpSession AbpSession { get; set; }

        public FunctionRepository(IDbContextProvider<ShiNengShiHuiDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
            AbpSession = NullAbpSession.Instance;
        }

        public List<Function> GetFunctionOfRoles(long? userId)
        {
            var role = Context.UserRoles.FirstOrDefault(m => m.UserId == AbpSession.UserId);
            if (role == null)
            {
                return null;
            }
            var result= GetAllList(m => m.RoleId == role.RoleId);
            return result;
        }
    }
}
