using Abp.Domain.Repositories;
using ShiNengShiHui.RepositoryExtend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiNengShiHui.Entities.Grades
{
    public interface IGroupWeekGradeRepository:IRepository<GroupWeekGrade,long>,IPageRepository<GroupWeekGrade,long>,IPageFromTableRepository<GroupWeekGrade,long>
    {
    }
}
