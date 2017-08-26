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
        private string TableName { get => GetTable(SqlRepositoryBase<Student, int>.TableType.Student) == null ? "testStudents" : GetTable(SqlRepositoryBase<Student, int>.TableType.Student);}

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
            //using (var connection = Context.Database.Connection)
            //{
            //    connection.Open();
                var students = Context.Database.SqlQuery<Student>($@"Select * From {TableName} Where IsDeleted=0");
                list.AddRange(students);
            //}
            return list.AsQueryable<Student>();
        }

        public override Student Insert(Student entity)
        {
            entity.CreationTime = Clock.Now;
            entity.CreatorUserId = AbpSession.UserId;
            //using (var connection = Context.Database.Connection)
            //{
            //    connection.Open();
                Context.Database.ExecuteSqlCommand($@"INSERT INTO [dbo].[{TableName}]
                                                            ([Name]
                                                            ,[Sex]
                                                            ,[IsDeleted]
                                                            ,[CreationTime]
                                                            ,[CreatorUserId]
                                                            ,[ClassId]
                                                            ,[Group])
                                                      VALUES
                                                            (@Name
                                                            ,@Sex
                                                            ,@IsDeleted
                                                            ,@CreationTime
                                                            ,@CreatorUserId
                                                            ,@ClassId
                                                            ,@Group)",
                                                            new SqlParameter("Name", entity.Name),
                                                            new SqlParameter("Sex", entity.Sex),
                                                            new SqlParameter("IsDeleted", false),
                                                            new SqlParameter("CreationTime", entity.CreationTime),
                                                            new SqlParameter("CreatorUserId", entity.CreatorUserId),
                                                            new SqlParameter("ClassId", entity.ClassId),
                                                            new SqlParameter("Group", entity.Group));
            //}
            return FirstOrDefault(s => s.Name.Equals(entity.Name) && s.Sex == entity.Sex && s.CreationTime == entity.CreationTime);
        }

        public override Task<Student> InsertAsync(Student entity)
        {
            return base.InsertAsync(entity);
        }

        public override Student Update(Student entity)
        {
            entity.LastModifierUserId = AbpSession.UserId;
            entity.LastModificationTime = Clock.Now;
            //using (var connection = Context.Database.Connection)
            //{
            //    connection.Open();
                Context.Database.ExecuteSqlCommand($@"UPDATE [dbo].[{TableName}]
                                                       SET [Name] = @Name
                                                          ,[Sex] = @Sex
                                                          ,[IsDeleted] = @IsDeleted
                                                          ,[LastModificationTime] = @LastModificationTime
                                                          ,[LastModifierUserId] = @LastModifierUserId
                                                          ,[ClassId] = @ClassId
                                                          ,[Group] = @Group
                                                     WHERE Id=@Id ",
                                                     new SqlParameter("Name", entity.Name),
                                                     new SqlParameter("Sex", entity.Sex),
                                                     new SqlParameter("IsDeleted", entity.IsDeleted),
                                                     new SqlParameter("LastModificationTime", entity.LastModificationTime),
                                                     new SqlParameter("LastModifierUserId", entity.LastModifierUserId),
                                                     new SqlParameter("ClassId", entity.ClassId),
                                                     new SqlParameter("Group", entity.Group),
                                                     new SqlParameter("Id", entity.Id));
            //}
            return entity;
        }

        public override Task<Student> UpdateAsync(Student entity)
        {
            return base.UpdateAsync(entity);
        }

        public override void Delete(Student entity)
        {
            entity.IsDeleted = true;
            entity.DeleterUserId = AbpSession.UserId;
            entity.DeletionTime = Clock.Now;
            Context.Database.ExecuteSqlCommand($@"UPDATE [dbo].[{TableName}]
                                                       SET [Name] = @Name
                                                          ,[Sex] = @Sex
                                                          ,[IsDeleted] = @IsDeleted
                                                          ,[DeleterUserId] = @DeleterUserId
                                                          ,[DeletionTime] = @DeletionTime
                                                          ,[ClassId] = @ClassId
                                                          ,[Group] = @Group
                                                     WHERE Id=@Id ",
                                                     new SqlParameter("Name", entity.Name),
                                                     new SqlParameter("Sex", entity.Sex),
                                                     new SqlParameter("IsDeleted", entity.IsDeleted),
                                                     new SqlParameter("DeleterUserId", entity.DeleterUserId),
                                                     new SqlParameter("DeletionTime", entity.DeletionTime),
                                                     new SqlParameter("ClassId", entity.ClassId),
                                                     new SqlParameter("Group", entity.Group),
                                                     new SqlParameter("Id", entity.Id));
        }

        public IQueryable<Student> GetAll(string tableName)
        {
            var list = new List<Student>();

            var students = Context.Database.SqlQuery<Student>($@"Select * From {tableName} Where IsDeleted=0");
            list.AddRange(students);

            return list.AsQueryable<Student>();
        }
    }
}
