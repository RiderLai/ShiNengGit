using Abp.Domain.Repositories;
using ShiNengShiHui.RepositoryExtend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiNengShiHui.Users
{
    public interface IUserRepository:IRepository<User,long>,IPageRepository<User,long>
    {
    }
}
