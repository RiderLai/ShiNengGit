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
        private string TableName { get => GetTable(SqlRepositoryBase<Student, int>.TableType.Student) == null ? "testStudents" : GetTable(SqlRepositoryBase<Student, int>.TableType.Student); }

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
            using (var connection = Context.Database.Connection)
            {
                connection.Open();
                var students = Context.Database.SqlQuery<Student>($@"Select * From {TableName} Where IsDeleted=0");
                list.AddRange(students);
            }
            return list.AsQueryable<Student>();
        }

        public override Student Insert(Student entity)
        {
            using (var connection = Context.Database.Connection)
            {
                connection.Open();
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
                                                            (< Name, nvarchar(10),@Name>
                                                            ,< sex, bit,@sex>
                                                            ,< IsDeleted, bit,@IsDeleted>
                                                            ,< DeleterUserId, bigint,@DeleterUserId>
                                                            ,< DeletionTime, datetime,@DeletionTime>
                                                            ,< LastModificationTime, datetime,@LastModificationTime>
                                                            ,< LastModifierUserId, bigint,@LastModifierUserId>
                                                            ,< CreationTime, datetime,@CreationTime>
                                                            ,< CreatorUserId, bigint,@CreatorUserId>
                                                            ,< Class_Id, int,@Class_Id>)",
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
            return FirstOrDefault(s => s.Name.Equals(entity.Name) && s.sex == entity.sex && s.CreationTime == entity.CreationTime);
        }

        public override Task<Student> InsertAsync(Student entity)
        {
            return base.InsertAsync(entity);
        }

        public override Student Update(Student entity)
        {
            entity.LastModifierUserId = AbpSession.UserId;
            entity.LastModificationTime = Clock.Now;
            using (var connection = Context.Database.Connection)
            {
                connection.Open();
                Context.Database.ExecuteSqlCommand($@"UPDATE [dbo].[{TableName}]
                                                       SET[Name] = < Name, nvarchar(10),@Name>
                                                          ,[sex] = < sex, bit,@sex>
                                                          ,[IsDeleted] = < IsDeleted, bit,@IsDeleted>
                                                          ,[DeleterUserId] = < DeleterUserId, bigint,@DeleterUserId>
                                                          ,[DeletionTime] = < DeletionTime, datetime,@DeletionTime>
                                                          ,[LastModificationTime] = < LastModificationTime, datetime,@LastModificationTime>
                                                          ,[LastModifierUserId] = < LastModifierUserId, bigint,@LastModifierUserId>
                                                          ,[CreationTime] = < CreationTime, datetime,@CreationTime>
                                                          ,[CreatorUserId] = < CreatorUserId, bigint,@CreatorUserId>
                                                          ,[Class_Id] = < Class_Id, int,@Class_Id>
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

        public override void Delete(Student entity)
        {
            entity.IsDeleted = true;
            entity.DeleterUserId = AbpSession.UserId;
            entity.DeletionTime = Clock.Now;
            Update(entity);
        }
    }
}
