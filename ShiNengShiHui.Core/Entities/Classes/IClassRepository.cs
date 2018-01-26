using Abp.Domain.Repositories;
using ShiNengShiHui.RepositoryExtend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiNengShiHui.Entities.Classes
{
    public interface IClassRepository:IRepository<Class>,IPageRepository<Class>
    {
        void TableCreate(Class Class);

        void TableDelete(Class Class);
    }
}
