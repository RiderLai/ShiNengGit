using Abp.Domain.Repositories;
using ShiNengShiHui.RepositoryExtend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiNengShiHui.Entities.Students
{
    public interface IStudentRepository:IRepository<Student>,IPageRepository<Student>,IPageFromTableRepository<Student>
    {
        
    }
}
