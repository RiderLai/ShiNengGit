using ShiNengShiHui.Entities.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework;

namespace ShiNengShiHui.EntityFramework.Repositories
{
    public class StudentRepository : SqlRepositoryBase<Student>, IStudentRepository
    {
        private string TableName { get => GetTable(TableType.Student) == null ? "testStudents" : GetTable(TableType.Student); }

        public StudentRepository(IDbContextProvider<ShiNengShiHuiDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public override int Count()
        {
            return Context.Database.ExecuteSqlCommand("Select Count(*) From " + TableName);
        }
    }
}
