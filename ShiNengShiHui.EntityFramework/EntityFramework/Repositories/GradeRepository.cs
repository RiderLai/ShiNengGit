using ShiNengShiHui.Entities.Grades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework;
using System.Data.SqlClient;
using Abp.Timing;

namespace ShiNengShiHui.EntityFramework.Repositories
{
    public class GradeRepository : SqlRepositoryBase<Grade, long>, IGradeRepository
    {
        private string TableName { get => GetTable(SqlRepositoryBase<Grade, long>.TableType.Grade) == null ? "testGrades" : GetTable(SqlRepositoryBase<Grade, long>.TableType.Grade); }

        public GradeRepository(IDbContextProvider<ShiNengShiHuiDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        //TODO:代码重复 可以重做到SqlRepositoryBase当中 --2017-8-16 13:54:52
        //TODO:可以做出重载 查找出softdelete == true
        public override IQueryable<Grade> GetAll()
        {
            List<Grade> list = new List<Grade>();
            //using (var connection = Context.Database.Connection)
            //{
            //    connection.Open();
                var grades = Context.Database.SqlQuery<Grade>($@"Select * From {TableName} Where IsDeleted=0");
                list.AddRange(grades);
            //}
            return list.AsQueryable<Grade>();
        }

        public override Grade Insert(Grade entity)
        {
            entity.CreationTime = Clock.Now;
            entity.CreatorUserId = AbpSession.UserId;
            //using (var connection = Context.Database.Connection)
            //{
            //    connection.Open();
                Context.Database.ExecuteSqlCommand($@"INSERT INTO [dbo].[{TableName}]
                                                            ([DateJson]
                                                            ,[GradeStringJson]
                                                            ,[IsDeleted]
                                                            ,[CreationTime]
                                                            ,[CreatorUserId]
                                                            ,[StudentId])
                                                      VALUES
                                                            (@DateJson
                                                            ,@GradeStringJson
                                                            ,@IsDeleted
                                                            ,@CreationTime
                                                            ,@CreatorUserId
                                                            ,@StudentId)",
                                                            new SqlParameter("DateJson", entity.DateJson),
                                                            new SqlParameter("GradeStringJson", entity.GradeStringJson),
                                                            new SqlParameter("IsDeleted", false),
                                                            new SqlParameter("CreationTime", entity.CreationTime),
                                                            new SqlParameter("CreatorUserId", entity.CreatorUserId),
                                                            new SqlParameter("StudentId", entity.StudentId));
            //}
            //Context.SaveChanges();
            return FirstOrDefault(g => g.StudentId == entity.StudentId && g.CreationTime == entity.CreationTime);
        }

        public override Task<Grade> InsertAsync(Grade entity)
        {
            return base.InsertAsync(entity);
        }

        public override Grade Update(Grade entity)
        {
            entity.LastModifierUserId = AbpSession.UserId;
            entity.LastModificationTime = Clock.Now;
            //using (var connection = Context.Database.Connection)
            //{
            //    connection.Open();
                Context.Database.ExecuteSqlCommand($@"UPDATE [dbo].[{TableName}]
                                                        SET [DateJson] = @DateJson
                                                           ,[GradeStringJson] = @GradeStringJson
                                                           ,[LastModificationTime] = @LastModificationTime
                                                           ,[LastModifierUserId] = @LastModifierUserId
                                                           ,[StudentId] = @StudentId
                                                      WHERE Id=@Id",
                                                      new SqlParameter("DateJson", entity.DateJson),
                                                      new SqlParameter("GradeStringJson", entity.GradeStringJson),
                                                      new SqlParameter("LastModificationTime", entity.LastModificationTime),
                                                      new SqlParameter("LastModifierUserId", entity.LastModifierUserId),
                                                      new SqlParameter("StudentId", entity.StudentId),
                                                      new SqlParameter("Id", entity.Id));
            //}
            return entity;
        }

        public override Task<Grade> UpdateAsync(Grade entity)
        {
            return base.UpdateAsync(entity);
        }

        public override void Delete(Grade entity)
        {
            entity.IsDeleted = true;
            entity.DeleterUserId = AbpSession.UserId;
            entity.DeletionTime = Clock.Now;
            Context.Database.ExecuteSqlCommand($@"UPDATE [dbo].[{TableName}]
                                                        SET [DateJson] = @DateJson
                                                           ,[GradeStringJson] = @GradeStringJson
                                                           ,[IsDeleted] = @IsDeleted
                                                           ,[DeleterUserId] = @DeleterUserId
                                                           ,[DeletionTime] = @DeletionTime
                                                           ,[StudentId] = @StudentId
                                                      WHERE Id=@Id",
                                                      new SqlParameter("DateJson", entity.DateJson),
                                                      new SqlParameter("GradeStringJson", entity.GradeStringJson),
                                                      new SqlParameter("IsDeleted", entity.IsDeleted),
                                                      new SqlParameter("DeleterUserId", entity.DeleterUserId),
                                                      new SqlParameter("DeletionTime", entity.DeletionTime),
                                                      new SqlParameter("StudentId", entity.StudentId),
                                                      new SqlParameter("Id", entity.Id));
        }
    }
}
