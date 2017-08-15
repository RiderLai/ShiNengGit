using ShiNengShiHui.Entities.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework;
using System.Data.Entity;

namespace ShiNengShiHui.EntityFramework.Repositories
{
    public class StudentRepository : SqlRepositoryBase<Student>, IStudentRepository
    {
        private string TableName { get => GetTable(TableType.Student) == null ? "testStudents" : GetTable(TableType.Student); }

        public StudentRepository(IDbContextProvider<ShiNengShiHuiDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        //public override int Count()
        //{
        //    return Context.Database.ExecuteSqlCommand("Select Count(*) From " + TableName);
        //}

        public override IQueryable<Student> GetAll()
        {
            var list = new List<Student>();
            using (GetDbContext())
            {
                var students = Context.Database.SqlQuery<Student>("Select * From " + TableName);
                list.AddRange(students);
            }
            return list.AsQueryable<Student>();
        }

        public override Student Insert(Student entity)
        {
            return base.Insert(entity);
        }

        public override Task<Student> InsertAsync(Student entity)
        {
            return base.InsertAsync(entity);
        }

        public override int InsertAndGetId(Student entity)
        {
            return base.InsertAndGetId(entity);
        }

        public override Task<int> InsertAndGetIdAsync(Student entity)
        {
            return base.InsertAndGetIdAsync(entity);
        }

        public override Student InsertOrUpdate(Student entity)
        {
            return base.InsertOrUpdate(entity);
        }

        public override Task<Student> InsertOrUpdateAsync(Student entity)
        {
            return base.InsertOrUpdateAsync(entity);
        }

        public override Student Update(Student entity)
        {
            return base.Update(entity);
        }

        public override Task<Student> UpdateAsync(Student entity)
        {
            return base.UpdateAsync(entity);
        }

        public override void Delete(int id)
        {
            base.Delete(id);
        }

        public override void Delete(Student entity)
        {
            base.Delete(entity);
        }

        protected override void AttachIfNot(Student entity)
        {
            base.AttachIfNot(entity);
        }
    }
}
