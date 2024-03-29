﻿using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiNengShiHui.RepositoryExtend
{
    public interface IPageFromTableRepository<TEntity,TPrimarykey>
        where TEntity:IEntity<TPrimarykey>
    {
        IQueryable<TEntity> GetAll(string tableName);

        TEntity[] GetPageFromTable(string tablename, int pageIndex, int showCount);
    }

    public interface IPageFromTableRepository<TEntity>:IPageFromTableRepository<TEntity,int>
        where TEntity : IEntity<int>
    {
    }
}
