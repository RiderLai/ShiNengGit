using Abp.Domain.Entities;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;

namespace ShiNengShiHui.EntityFramework.Repositories
{
    public abstract class ShiNengShiHuiRepositoryBase<TEntity, TPrimaryKey> : EfRepositoryBase<ShiNengShiHuiDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected ShiNengShiHuiRepositoryBase(IDbContextProvider<ShiNengShiHuiDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //add common methods for all repositories
    }

    public abstract class ShiNengShiHuiRepositoryBase<TEntity> : ShiNengShiHuiRepositoryBase<TEntity, int>
        where TEntity : class, IEntity<int>
    {
        protected ShiNengShiHuiRepositoryBase(IDbContextProvider<ShiNengShiHuiDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //do not add any method here, add to the class above (since this inherits it)
    }
}
