using ShiNengShiHui.Entities.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Abp.EntityFramework;
using Abp.Domain.Repositories;
using Abp.Domain.Entities;
using Abp.Runtime.Session;

namespace ShiNengShiHui.EntityFramework.Repositories
{
    public class SqlRepositoryBase<TEntity> : SqlRepositoryBase<TEntity, int>
        where TEntity : class, IEntity<int>
    {
        public SqlRepositoryBase(IDbContextProvider<ShiNengShiHuiDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
    /// <summary>
    /// 基于EFRepositoryBase重写出SqlRepositoryBase，需要重写 GetAll Insert Update Delete方法
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TPrimaryKey"></typeparam>
    public class SqlRepositoryBase<TEntity, TPrimaryKey> : ShiNengShiHuiRepositoryBase<TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        public IAbpSession AbpSession{get;set;}

        public SqlRepositoryBase(IDbContextProvider<ShiNengShiHuiDbContext> dbContextProvider) : base(dbContextProvider)
        {
            AbpSession = NullAbpSession.Instance;
        }

        #region Get table name
        /// <summary>
        /// Get table option
        /// </summary>
        public enum TableType
        {
            Student,
            Grade,
            Prize
        }

        /// <summary>
        /// Get table name
        /// </summary>
        /// <param name="tableType"></param>
        /// <returns>如果查询结果成立，返回表格名，否则返回null</returns>
        public string GetTable(TableType tableType)
        {
            string result = null;
            if (AbpSession != null)
            {
                var user = Context.Users.FirstOrDefault(u => u.Id == AbpSession.UserId);
                if (user != null)
                {
                    var teacher = user.Teacher;
                    if (teacher != null)
                    {
                        var Class = teacher.Class;
                        switch (tableType)
                        {
                            case TableType.Student:
                                result = Class.StudentsTable;
                                break;
                            case TableType.Grade:
                                result = Class.GradesTable;
                                break;
                            case TableType.Prize:
                                result = Class.PrizesTable;
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            return result;
        }
        #endregion

        #region Override 对于基类一些方法的重写
        public override TPrimaryKey InsertAndGetId(TEntity entity)
        {
            return Insert(entity).Id;
        }

        public override Task<TPrimaryKey> InsertAndGetIdAsync(TEntity entity)
        {
            return base.InsertAndGetIdAsync(entity);
        }

        public override TEntity InsertOrUpdate(TEntity entity)
        {
            var result = FirstOrDefault(entity.Id);
            if (result == null)
            {
                result = Insert(entity);
            }
            else
            {
                result = Update(entity);
            }
            return result;
        }

        public override Task<TEntity> InsertOrUpdateAsync(TEntity entity)
        {
            return base.InsertOrUpdateAsync(entity);
        }

        public override TPrimaryKey InsertOrUpdateAndGetId(TEntity entity)
        {
            return InsertOrUpdate(entity).Id;
        }

        public override Task<TPrimaryKey> InsertOrUpdateAndGetIdAsync(TEntity entity)
        {
            return base.InsertOrUpdateAndGetIdAsync(entity);
        }

        public override void Delete(TPrimaryKey id)
        {
            var temp = FirstOrDefault(id);
            if (temp==null)
            {
                return;
            }
            Delete(temp);
        }

        protected override void AttachIfNot(TEntity entity)
        {
            if (FirstOrDefault(entity.Id)==null)
            {
                Insert(entity);
            }
        }
        #endregion
    }
}
