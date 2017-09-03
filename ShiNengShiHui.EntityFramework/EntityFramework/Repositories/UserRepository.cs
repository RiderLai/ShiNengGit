using ShiNengShiHui.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework;

namespace ShiNengShiHui.EntityFramework.Repositories
{
    public class UserRepository : ShiNengShiHuiRepositoryBase<User, long>,IUserRepository
    {
        public UserRepository(IDbContextProvider<ShiNengShiHuiDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
