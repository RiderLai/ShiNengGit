using ShiNengShiHui.Entities.Teachers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework;

namespace ShiNengShiHui.EntityFramework.Repositories
{
    public class TeacherRepository : ShiNengShiHuiRepositoryBase<Teacher>, ITeacherRepository
    {
        public TeacherRepository(IDbContextProvider<ShiNengShiHuiDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
