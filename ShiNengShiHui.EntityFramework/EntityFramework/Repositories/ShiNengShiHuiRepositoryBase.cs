using Abp.Domain.Entities;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;
using ShiNengShiHui.RepositoryExtend;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace ShiNengShiHui.EntityFramework.Repositories
{
    public abstract class ShiNengShiHuiRepositoryBase<TEntity, TPrimaryKey> : EfRepositoryBase<ShiNengShiHuiDbContext, TEntity, TPrimaryKey>,IPageRepository<TEntity,TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected ShiNengShiHuiRepositoryBase(IDbContextProvider<ShiNengShiHuiDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public virtual TEntity[] GetPage(int pageIndex, int showCount, Expression<Func<TEntity, bool>> expression)
        {
            var data = from item in GetAll()
                       orderby item.Id
                       select item;
            if (expression==null)
            {
                return data.Take(showCount * pageIndex).Skip(showCount * (pageIndex - 1)).ToArray();
            }
            else
            {
                return data.Where(expression).Take(showCount * pageIndex).Skip(showCount * (pageIndex - 1)).ToArray();
            }
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
