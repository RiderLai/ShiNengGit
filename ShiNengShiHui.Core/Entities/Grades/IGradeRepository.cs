using Abp.Domain.Repositories;
using ShiNengShiHui.RepositoryExtend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiNengShiHui.Entities.Grades
{
    public interface IGradeRepository:IRepository<Grade,long>,IPageRepository<Grade,long>
    {
        IQueryable<Grade> GetAll(string tableName);
    }
}
