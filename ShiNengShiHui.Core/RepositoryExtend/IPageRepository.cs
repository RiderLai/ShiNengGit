using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ShiNengShiHui.RepositoryExtend
{
    public interface IPageRepository<TEntity,TPrimarykey>
        where TEntity:class,IEntity<TPrimarykey>
    {
        TEntity[] GetPage(int pageIndex, int showCount,Expression<Func<TEntity,bool>> exception);
    }

    public interface IPageRepository<TEntity> : IPageRepository<TEntity, int>
        where TEntity:class,IEntity<int>
    {

    }
}
