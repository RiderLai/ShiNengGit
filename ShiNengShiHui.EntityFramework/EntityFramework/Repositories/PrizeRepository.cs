using ShiNengShiHui.Entities.Prizes;
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
    public class PrizeRepository : SqlRepositoryBase<Prize, long>, IPrizeRepository
    {
        private string TableName { get => GetTable(SqlRepositoryBase<Prize, long>.TableType.Prize) == null ? "testPrizes" : GetTable(SqlRepositoryBase<Prize, long>.TableType.Prize); }

        public PrizeRepository(IDbContextProvider<ShiNengShiHuiDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public override IQueryable<Prize> GetAll()
        {
            List<Prize> list = new List<Prize>();
            //using (var connection=Context.Database.Connection)
            //{
            //    connection.Open();
                var prizes = Context.Database.SqlQuery<Prize>($@"Select * From {TableName} Where IsDeleted=0");
                list.AddRange(prizes);
            //}
            return list.AsQueryable<Prize>();
        }

        public override Prize Insert(Prize entity)
        {
            //using (var connection = Context.Database.Connection)
            //{
            //    connection.Open();
            //    Context.Database.ExecuteSqlCommand($@"INSERT INTO [dbo].[{TableName}]
            //                                                ([DateJosn]
            //                                                ,[IsDeleted]
            //                                                ,[DeleterUserId]
            //                                                ,[DeletionTime]
            //                                                ,[LastModificationTime]
            //                                                ,[LastModifierUserId]
            //                                                ,[CreationTime]
            //                                                ,[CreatorUserId]
            //                                                ,[PrizeItemId]
            //                                                ,[StudentId])
            //                                          VALUES
            //                                                (<DateJosn, nvarchar(max),@DateJosn>
            //                                                ,<IsDeleted, bit,@IsDeleted>
            //                                                ,<DeleterUserId, bigint,@DeleterUserId>
            //                                                ,<DeletionTime, datetime,@DeletionTime>
            //                                                ,<LastModificationTime, datetime,@LastModificationTime>
            //                                                ,<LastModifierUserId, bigint,@LastModifierUserId>
            //                                                ,<CreationTime, datetime,@CreationTime>
            //                                                ,<CreatorUserId, bigint,@CreatorUserId>
            //                                                ,<PrizeItemId, uniqueidentifier,@PrizeItemId>
            //                                                ,<StudentId, int,@StudentId>)",
            //                                                new SqlParameter("DateJosn", entity.DateJosn),
            //                                                new SqlParameter("IsDeleted", entity.IsDeleted),
            //                                                new SqlParameter("DeleterUserId", entity.DeleterUserId),
            //                                                new SqlParameter("DeletionTime", entity.DeletionTime),
            //                                                new SqlParameter("LastModificationTime", entity.LastModificationTime),
            //                                                new SqlParameter("LastModifierUserId", entity.LastModifierUserId),
            //                                                new SqlParameter("CreationTime", entity.CreationTime),
            //                                                new SqlParameter("CreatorUserId", entity.CreatorUserId),
            //                                                new SqlParameter("PrizeItemId", entity.PrizeItemId),
            //                                                new SqlParameter("StudentId", entity.StudentId));
            ////}
            //return FirstOrDefault(p => p.Student.Id == entity.Student.Id && p.PrizeItem.Id == entity.PrizeItem.Id && p.CreationTime == entity.CreationTime);
            return new Prize();
        }

        public override Task<Prize> InsertAsync(Prize entity)
        {
            return base.InsertAsync(entity);
        }

        public override Prize Update(Prize entity)
        {
            //entity.LastModifierUserId = AbpSession.UserId;
            //entity.LastModificationTime = Clock.Now;
            ////using (var connection=Context.Database.Connection)
            ////{
            ////    connection.Open();
            //    Context.Database.ExecuteSqlCommand($@"UPDATE [dbo].[{TableName}]
            //                                            SET [DateJosn] = <DateJosn, nvarchar(max),@DateJosn>
            //                                               ,[IsDeleted] = <IsDeleted, bit,@IsDeleted>
            //                                               ,[DeleterUserId] = <DeleterUserId, bigint,@DeleterUserId>
            //                                               ,[DeletionTime] = <DeletionTime, datetime,@DeletionTime>
            //                                               ,[LastModificationTime] = <LastModificationTime, datetime,@LastModificationTime>
            //                                               ,[LastModifierUserId] = <LastModifierUserId, bigint,@LastModifierUserId>
            //                                               ,[CreationTime] = <CreationTime, datetime,@CreationTime>
            //                                               ,[CreatorUserId] = <CreatorUserId, bigint,@CreatorUserId>
            //                                               ,[PrizeItem_Id] = <PrizeItem_Id, uniqueidentifier,@PrizeItem_Id>
            //                                               ,[StudentId] = <StudentId, int,@StudentId>
            //                                          WHERE Id=@Id",
            //                                          new SqlParameter("DateJosn", entity.DateJosn),
            //                                          new SqlParameter("IsDeleted", entity.IsDeleted),
            //                                          new SqlParameter("DeleterUserId", entity.DeleterUserId),
            //                                          new SqlParameter("DeletionTime", entity.DeletionTime),
            //                                          new SqlParameter("LastModificationTime", entity.LastModificationTime),
            //                                          new SqlParameter("LastModifierUserId", entity.LastModifierUserId),
            //                                          new SqlParameter("CreationTime", entity.CreationTime),
            //                                          new SqlParameter("CreatorUserId", entity.CreatorUserId),
            //                                          new SqlParameter("PrizeItemId", entity.PrizeItemId),
            //                                          new SqlParameter("StudentId", entity.StudentId),
            //                                          new SqlParameter("Id", entity.Id));
            //}
            return entity;
        }

        public override Task<Prize> UpdateAsync(Prize entity)
        {
            return base.UpdateAsync(entity);
        }

        public override void Delete(Prize entity)
        {
            //entity.IsDeleted = true;
            //entity.DeleterUserId = AbpSession.UserId;
            //entity.DeletionTime = Clock.Now;
            //Update(entity);
        }
    }
}
