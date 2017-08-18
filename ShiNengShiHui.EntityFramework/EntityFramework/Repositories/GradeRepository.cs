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
            using (var connection = Context.Database.Connection)
            {
                connection.Open();
                var grades = Context.Database.SqlQuery<Grade>($@"Select * From {TableName} Where IsDeleted=0");
                list.AddRange(grades);
            }
            return list.AsQueryable<Grade>();
        }

        public override Grade Insert(Grade entity)
        {
            using (var connection = Context.Database.Connection)
            {
                connection.Open();
                Context.Database.ExecuteSqlCommand($@"INSERT INTO [dbo].[{TableName}]
                                                            ([DateJson]
                                                            ,[GradeStringJson]
                                                            ,[IsDeleted]
                                                            ,[DeleterUserId]
                                                            ,[DeletionTime]
                                                            ,[LastModificationTime]
                                                            ,[LastModifierUserId]
                                                            ,[CreationTime]
                                                            ,[CreatorUserId]
                                                            ,[StudentId])
                                                      VALUES
                                                            (<DateJson, nvarchar(max),@DateJson>
                                                            ,<GradeStringJson, nvarchar(max),@GradeStringJson>
                                                            ,<IsDeleted, bit,@IsDeleted>
                                                            ,<DeleterUserId, bigint,@DeleterUserId>
                                                            ,<DeletionTime, datetime,@DeletionTime>
                                                            ,<LastModificationTime, datetime,@LastModificationTime>
                                                            ,<LastModifierUserId, bigint,@LastModifierUserId>
                                                            ,<CreationTime, datetime,@CreationTime>
                                                            ,<CreatorUserId, bigint,@CreatorUserId>
                                                            ,<StudentId, int,@StudentId>)",
                                                            new SqlParameter("DateJson", entity.DateJson),
                                                            new SqlParameter("GradeStringJson", entity.GradeStringJson),
                                                            new SqlParameter("IsDeleted", entity.IsDeleted),
                                                            new SqlParameter("DeleterUserId", entity.DeleterUserId),
                                                            new SqlParameter("DeletionTime", entity.DeletionTime),
                                                            new SqlParameter("LastModificationTime", entity.LastModificationTime),
                                                            new SqlParameter("LastModifierUserId", entity.LastModifierUserId),
                                                            new SqlParameter("CreationTime", entity.CreationTime),
                                                            new SqlParameter("CreatorUserId", entity.CreatorUserId),
                                                            new SqlParameter("StudentId", entity.StudentId));
            }
            return FirstOrDefault(g => g.Student.Id == entity.Student.Id && g.CreationTime == entity.CreationTime);
        }

        public override Task<Grade> InsertAsync(Grade entity)
        {
            return base.InsertAsync(entity);
        }

        public override Grade Update(Grade entity)
        {
            entity.LastModifierUserId = AbpSession.UserId;
            entity.LastModificationTime = Clock.Now;
            using (var connection = Context.Database.Connection)
            {
                connection.Open();
                Context.Database.ExecuteSqlCommand($@"UPDATE [dbo].[{TableName}]
                                                        SET [DateJson] = <DateJson, nvarchar(max),@DateJson>
                                                           ,[GradeStringJson] = <GradeStringJson, nvarchar(max),@GradeStringJson>
                                                           ,[IsDeleted] = <IsDeleted, bit,@IsDeleted>
                                                           ,[DeleterUserId] = <DeleterUserId, bigint,@DeleterUserId>
                                                           ,[DeletionTime] = <DeletionTime, datetime,@DeletionTime>
                                                           ,[LastModificationTime] = <LastModificationTime, datetime,@LastModificationTime>
                                                           ,[LastModifierUserId] = <LastModifierUserId, bigint,@LastModifierUserId>
                                                           ,[CreationTime] = <CreationTime, datetime,@CreationTime>
                                                           ,[CreatorUserId] = <CreatorUserId, bigint,@CreatorUserId>
                                                           ,[StudentId] = <StudentId, int,@StudentId>
                                                      WHERE Id=@Id",
                                                      new SqlParameter("DateJson", entity.DateJson),
                                                      new SqlParameter("GradeStringJson", entity.GradeStringJson),
                                                      new SqlParameter("IsDeleted", entity.IsDeleted),
                                                      new SqlParameter("DeleterUserId", entity.DeleterUserId),
                                                      new SqlParameter("DeletionTime", entity.DeletionTime),
                                                      new SqlParameter("LastModificationTime", entity.LastModificationTime),
                                                      new SqlParameter("LastModifierUserId", entity.LastModifierUserId),
                                                      new SqlParameter("CreationTime", entity.CreationTime),
                                                      new SqlParameter("CreatorUserId", entity.CreatorUserId),
                                                      new SqlParameter("StudentId", entity.StudentId),
                                                      new SqlParameter("Id", entity.Id));
            }
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
            Update(entity);
        }
    }
}
