using ShiNengShiHui.Entities.Grades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework;
using System.Data.SqlClient;

namespace ShiNengShiHui.EntityFramework.Repositories
{
    public class GroupWeekGradeRepository : SqlRepositoryBase<GroupWeekGrade, long>, IGroupWeekGradeRepository
    {
        private string TableName { get => GetTable(SqlRepositoryBase<GroupWeekGrade, long>.TableType.GroupWeekGrade) == null ? "testGroupWeekGrades" : GetTable(SqlRepositoryBase<GroupWeekGrade, long>.TableType.GroupWeekGrade); }

        public GroupWeekGradeRepository(IDbContextProvider<ShiNengShiHuiDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public override GroupWeekGrade Insert(GroupWeekGrade entity)
        {
            Context.Database.ExecuteSqlCommand($@"INSERT INTO [dbo].[{TableName}]
                                                            ([DateJson]
                                                            ,[Group]
                                                            ,[IsWell])
                                                      VALUES
                                                            (@DateJson
                                                            ,@Group
                                                            ,@IsWell)",
                                                            new SqlParameter("DateJson", entity.DateJson),
                                                            new SqlParameter("Group", entity.Group),
                                                            new SqlParameter("IsWell", entity.IsWell));
            return FirstOrDefault(m => m.DateJson == entity.DateJson && m.Group == entity.Group);
        }

        public override Task<GroupWeekGrade> InsertAsync(GroupWeekGrade entity)
        {
            return base.InsertAsync(entity);
        }

        public override GroupWeekGrade Update(GroupWeekGrade entity)
        {
            Context.Database.ExecuteSqlCommand($@"UPDATE [dbo].[{TableName}]
                                                       SET [DateJson] = @Name
                                                          ,[Group] = @Sex
                                                          ,[IsWell] = @IsDeleted
                                                     WHERE Id=@Id ",
                                                     new SqlParameter("DateJson", entity.DateJson),
                                                     new SqlParameter("Group", entity.Group),
                                                     new SqlParameter("IsWell", entity.IsWell),
                                                     new SqlParameter("Id", entity.Id));
            return entity;
        }

        public override Task<GroupWeekGrade> UpdateAsync(GroupWeekGrade entity)
        {
            return base.UpdateAsync(entity);
        }

        public override void Delete(GroupWeekGrade entity)
        {
            Context.Database.ExecuteSqlCommand($@"DELETE FROM [dbo].[{TableName}]
                                                     WHERE Id=@Id ",
                                                     new SqlParameter("Id", entity.Id));
        }

        public override IQueryable<GroupWeekGrade> GetAll()
        {
            var list = new List<GroupWeekGrade>();
            var groupWeekGrades = Context.Database.SqlQuery<GroupWeekGrade>($@"Select * From {TableName}");
            list.AddRange(groupWeekGrades);

            return list.AsQueryable<GroupWeekGrade>();
        }

        public override IQueryable<GroupWeekGrade> GetAll(string tableName)
        {
            var list = new List<GroupWeekGrade>();
            var groupWeekGrades = Context.Database.SqlQuery<GroupWeekGrade>($@"Select * From {TableName} ");
            list.AddRange(groupWeekGrades);

            return list.AsQueryable<GroupWeekGrade>();
        }
    }
}
