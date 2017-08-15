using ShiNengShiHui.Entities.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework;
using System.Data.Entity;
using System.Data.SqlClient;
using Abp.Timing;

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
            using (Context.Database.Connection)
            {
                var students = Context.Database.SqlQuery<Student>("Select * From " + TableName);
                list.AddRange(students);
            }
            return list.AsQueryable<Student>();
        }

        public override Student Insert(Student entity)
        {
            using (Context.Database.Connection)
            {
                Context.Database.ExecuteSqlCommand($@"INSERT INTO [dbo].[{TableName}]
                                                            ([Name]
                                                            ,[sex]
                                                            ,[IsDeleted]
                                                            ,[DeleterUserId]
                                                            ,[DeletionTime]
                                                            ,[LastModificationTime]
                                                            ,[LastModifierUserId]
                                                            ,[CreationTime]
                                                            ,[CreatorUserId]
                                                            ,[Class_Id])
                                                      VALUES
                                                            (< @Name, nvarchar(10),>
                                                            ,< @sex, bit,>
                                                            ,< @IsDeleted, bit,>
                                                            ,< @DeleterUserId, bigint,>
                                                            ,< @DeletionTime, datetime,>
                                                            ,< @LastModificationTime, datetime,>
                                                            ,< @LastModifierUserId, bigint,>
                                                            ,< @CreationTime, datetime,>
                                                            ,< @CreatorUserId, bigint,>
                                                            ,< @Class_Id, int,>)",
                                                            new SqlParameter("Name", entity.Name),
                                                            new SqlParameter("sex", entity.sex),
                                                            new SqlParameter("IsDeleted", entity.IsDeleted),
                                                            new SqlParameter("DeleterUserId", entity.DeleterUserId),
                                                            new SqlParameter("DeletionTime", entity.DeletionTime),
                                                            new SqlParameter("LastModificationTime", entity.LastModificationTime),
                                                            new SqlParameter("LastModifierUserId", entity.LastModifierUserId),
                                                            new SqlParameter("CreationTime", entity.CreationTime),
                                                            new SqlParameter("CreatorUserId", entity.CreatorUserId),
                                                            new SqlParameter("Class_Id", entity.Class.Id));
            }
            return FirstOrDefault(s=>s.Name.Equals(entity.Name)&&s.sex==entity.sex&&s.CreationTime==entity.CreationTime);
        }

        public override Task<Student> InsertAsync(Student entity)
        {
            return base.InsertAsync(entity);
        }

        public override int InsertAndGetId(Student entity)
        {
            return Insert(entity).Id;
        }

        public override Task<int> InsertAndGetIdAsync(Student entity)
        {
            return base.InsertAndGetIdAsync(entity);
        }

        public override Student InsertOrUpdate(Student entity)
        {
            var student = FirstOrDefault(entity.Id);
            if (student==null)
            {
                student = Insert(entity);
            }
            else
            {
                Update(entity);
            }
            return student;
        }

        public override Task<Student> InsertOrUpdateAsync(Student entity)
        {
            return base.InsertOrUpdateAsync(entity);
        }

        public override Student Update(Student entity)
        {
            using (Context.Database.Connection)
            {
                Context.Database.ExecuteSqlCommand($@"UPDATE [dbo].[{TableName}]
                                                       SET[Name] = < @Name, nvarchar(10),>
                                                          ,[sex] = < @sex, bit,>
                                                          ,[IsDeleted] = < @IsDeleted, bit,>
                                                          ,[DeleterUserId] = < @DeleterUserId, bigint,>
                                                          ,[DeletionTime] = < @DeletionTime, datetime,>
                                                          ,[LastModificationTime] = < @LastModificationTime, datetime,>
                                                          ,[LastModifierUserId] = < @LastModifierUserId, bigint,>
                                                          ,[CreationTime] = < @CreationTime, datetime,>
                                                          ,[CreatorUserId] = < @CreatorUserId, bigint,>
                                                          ,[Class_Id] = < @Class_Id, int,>
                                                     WHERE Id=@Id ",
                                                     new SqlParameter("Name", entity.Name),
                                                     new SqlParameter("sex", entity.sex),
                                                     new SqlParameter("IsDeleted", entity.IsDeleted),
                                                     new SqlParameter("DeleterUserId", entity.DeleterUserId),
                                                     new SqlParameter("DeletionTime", entity.DeletionTime),
                                                     new SqlParameter("LastModificationTime", entity.LastModificationTime),
                                                     new SqlParameter("LastModifierUserId", entity.LastModifierUserId),
                                                     new SqlParameter("CreationTime", entity.CreationTime),
                                                     new SqlParameter("CreatorUserId", entity.CreatorUserId),
                                                     new SqlParameter("Class_Id", entity.Class.Id),
                                                     new SqlParameter("Id", entity.Id));
            }
            return entity;
        }

        public override Task<Student> UpdateAsync(Student entity)
        {
            return base.UpdateAsync(entity);
        }

        public override void Delete(int id)
        {
            var student = FirstOrDefault(id);
            if (student==null)
            {
                return;
            }
            Delete(student);
        }

        public override void Delete(Student entity)
        {
            entity.IsDeleted = true;
            entity.DeleterUserId = AbpSession.UserId;
            entity.DeletionTime = Clock.Now;
            Update(entity);
        }

        protected override void AttachIfNot(Student entity)
        {
            if (FirstOrDefault(entity.Id) == null)
            {
                Update(entity);
            }
        }
    }
}
