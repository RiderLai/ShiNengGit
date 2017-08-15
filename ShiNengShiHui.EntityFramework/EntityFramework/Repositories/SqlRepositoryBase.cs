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
    }
}
