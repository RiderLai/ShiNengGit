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
                var prizes = Context.Database.SqlQuery<Prize>($@"Select * From {TableName}");
                list.AddRange(prizes);
            //}
            return list.AsQueryable<Prize>();
        }

        public override Prize Insert(Prize entity)
        {
            Context.Database.ExecuteSqlCommand($@"INSERT INTO [dbo].[{TableName}]
                                                        ([DateJosn]
                                                        ,[PrizeItemId]
                                                        ,[StudentId])
                                                  VALUES
                                                        (@DateJosn
                                                        ,@PrizeItemId
                                                        ,@StudentId)",
                                                        new SqlParameter("DateJosn", entity.DateJosn),
                                                        new SqlParameter("PrizeItemId", entity.PrizeItemId),
                                                        new SqlParameter("StudentId", entity.StudentId));

            return FirstOrDefault(p => p.StudentId == entity.StudentId && p.PrizeItemId == entity.PrizeItemId && p.DateJosn == entity.DateJosn);
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
            Context.Database.ExecuteSqlCommand($@"UPDATE [dbo].[{TableName}]
                                                    SET [DateJosn] = @DateJosn
                                                       ,[PrizeItemId] = @PrizeItemId
                                                       ,[StudentId] = @StudentId
                                                  WHERE Id=@Id",
                                                  new SqlParameter("DateJosn", entity.DateJosn),
                                                  new SqlParameter("PrizeItemId", entity.PrizeItemId),
                                                  new SqlParameter("StudentId", entity.StudentId),
                                                  new SqlParameter("Id", entity.Id));
            //}
            return entity;
        }

        public override Task<Prize> UpdateAsync(Prize entity)
        {
            return base.UpdateAsync(entity);
        }

        public override void Delete(Prize entity)
        {
            Context.Database.ExecuteSqlCommand($@"DELETE FROM [dbo].[testPrizes]
                                                  WHERE Id=@Id",
                                                  new SqlParameter("Id", entity.Id));
        }

        public override IQueryable<Prize> GetAll(string tableName)
        {
            List<Prize> list = new List<Prize>();

            var prizes = Context.Database.SqlQuery<Prize>($@"Select * From {TableName}");
            list.AddRange(prizes);

            return list.AsQueryable<Prize>();
        }
    }
}
