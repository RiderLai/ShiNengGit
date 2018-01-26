using ShiNengShiHui.Entities.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework;

namespace ShiNengShiHui.EntityFramework.Repositories
{
    public class ClassRepository : ShiNengShiHuiRepositoryBase<Class>, IClassRepository
    {
        public ClassRepository(IDbContextProvider<ShiNengShiHuiDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public void TableCreate(Class Class)
        {
            StudentTableCreate(Class.StudentsTable);
            GradeTableCreate(Class);
            PrizeTableCreate(Class);
            GroupWeekGradeTableCreate(Class);
        }

        private void StudentTableCreate(string tablename)
        {
            Context.Database.ExecuteSqlCommand($@"
                                                 SET ANSI_NULLS ON
                                                 
                                                 SET QUOTED_IDENTIFIER ON

                                                 CREATE TABLE [dbo].[{tablename}](
                                                    	[Id] [int] IDENTITY(1,1) NOT NULL,
                                                    	[Name] [nvarchar](10) NOT NULL,
                                                    	[sex] [bit] NOT NULL,
                                                    	[IsDeleted] [bit] NOT NULL,
                                                    	[DeleterUserId] [bigint] NULL,
                                                    	[DeletionTime] [datetime] NULL,
                                                    	[LastModificationTime] [datetime] NULL,
                                                    	[LastModifierUserId] [bigint] NULL,
                                                    	[CreationTime] [datetime] NOT NULL,
                                                    	[CreatorUserId] [bigint] NULL,
                                                    	[ClassId] [int] NOT NULL,
                                                    	[Group] [int] NULL,
                                                     CONSTRAINT [PK_dbo.{tablename}] PRIMARY KEY CLUSTERED 
                                                    (
                                                    	[Id] ASC
                                                    )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
                                                    ) ON [PRIMARY]
                                                    
                                                    
                                                    ALTER TABLE [dbo].[{tablename}]  WITH CHECK ADD  CONSTRAINT [FK_dbo.{tablename}_dbo.Classes_Class_Id] FOREIGN KEY([ClassId])
                                                    REFERENCES [dbo].[Classes] ([Id])
                                                    ON DELETE CASCADE
                                                    
                                                    ALTER TABLE [dbo].[{tablename}] CHECK CONSTRAINT [FK_dbo.{tablename}_dbo.Classes_Class_Id]");
        }

        private void GradeTableCreate(Class Class)
        {
            Context.Database.ExecuteSqlCommand($@"
                                                 SET ANSI_NULLS ON
                                                 
                                                 SET QUOTED_IDENTIFIER ON
                                                 
                                                 CREATE TABLE [dbo].[{Class.GradesTable}](
                                                    	[Id] [bigint] IDENTITY(1,1) NOT NULL,
                                                    	[DateJson] [nvarchar](max) NOT NULL,
                                                    	[GradeStringJson] [nvarchar](max) NOT NULL,
                                                    	[IsDeleted] [bit] NOT NULL,
                                                    	[DeleterUserId] [bigint] NULL,
                                                    	[DeletionTime] [datetime] NULL,
                                                    	[LastModificationTime] [datetime] NULL,
                                                    	[LastModifierUserId] [bigint] NULL,
                                                    	[CreationTime] [datetime] NOT NULL,
                                                    	[CreatorUserId] [bigint] NULL,
                                                    	[StudentId] [int] NOT NULL,
                                                     CONSTRAINT [PK_dbo.{Class.GradesTable}] PRIMARY KEY CLUSTERED 
                                                    (
                                                    	[Id] ASC
                                                    )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
                                                    ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
                                                    
                                                    
                                                    ALTER TABLE [dbo].[{Class.GradesTable}]  WITH CHECK ADD  CONSTRAINT [FK_dbo.{Class.GradesTable}_dbo.{Class.StudentsTable}_Student_Id] FOREIGN KEY([StudentId])
                                                    REFERENCES [dbo].[{Class.StudentsTable}] ([Id])
                                                    ON DELETE CASCADE
                                                    
                                                    ALTER TABLE [dbo].[{Class.GradesTable}] CHECK CONSTRAINT [FK_dbo.{Class.GradesTable}_dbo.{Class.StudentsTable}_Student_Id]");
        }

        private void PrizeTableCreate(Class Class)
        {
            Context.Database.ExecuteSqlCommand($@"
                                                 SET ANSI_NULLS ON
                                                 
                                                 SET QUOTED_IDENTIFIER ON
                                                 
                                                 CREATE TABLE [dbo].[{Class.PrizesTable}](
                                                    	[Id] [bigint] IDENTITY(1,1) NOT NULL,
                                                    	[DateJosn] [nvarchar](max) NOT NULL,
                                                    	[PrizeItemId] [uniqueidentifier] NOT NULL,
                                                    	[StudentId] [int] NOT NULL,
                                                     CONSTRAINT [PK_dbo.{Class.PrizesTable}] PRIMARY KEY CLUSTERED 
                                                    (
                                                    	[Id] ASC
                                                    )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
                                                    ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
                                                    
                                                    
                                                    ALTER TABLE [dbo].[{Class.PrizesTable}]  WITH CHECK ADD  CONSTRAINT [FK_dbo.{Class.PrizesTable}_dbo.PrizeItems_PrizeItem_Id] FOREIGN KEY([PrizeItemId])
                                                    REFERENCES [dbo].[PrizeItems] ([Id])
                                                    ON DELETE CASCADE
                                                    
                                                    ALTER TABLE [dbo].[{Class.PrizesTable}] CHECK CONSTRAINT [FK_dbo.{Class.PrizesTable}_dbo.PrizeItems_PrizeItem_Id]
                                                    
                                                    ALTER TABLE [dbo].[{Class.PrizesTable}]  WITH CHECK ADD  CONSTRAINT [FK_dbo.{Class.PrizesTable}_dbo.testStudents_Student_Id] FOREIGN KEY([StudentId])
                                                    REFERENCES [dbo].[{Class.StudentsTable}] ([Id])
                                                    ON DELETE CASCADE
                                                    
                                                    ALTER TABLE [dbo].[{Class.PrizesTable}] CHECK CONSTRAINT [FK_dbo.{Class.PrizesTable}_dbo.testStudents_Student_Id]
                                                    ");
        }

        private void GroupWeekGradeTableCreate(Class Class)
        {
            Context.Database.ExecuteSqlCommand($@"
                                                 SET ANSI_NULLS ON
                                                 
                                                 SET QUOTED_IDENTIFIER ON
                                                 
                                                 CREATE TABLE [dbo].[{Class.GroupWeekGradeTable}](
                                                 	[Id] [bigint] IDENTITY(1,1) NOT NULL,
                                                 	[DateJson] [nvarchar](max) NOT NULL,
                                                 	[Group] [int] NOT NULL,
                                                 	[IsWell] [bit] NOT NULL,
                                                  CONSTRAINT [PK_dbo.{Class.GroupWeekGradeTable}] PRIMARY KEY CLUSTERED 
                                                 (
                                                 	[Id] ASC
                                                 )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
                                                 ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
                                                 ");
        }

        public void TableDelete(Class Class)
        {
            GradeTableDelete(Class);
            PrizeTableDelete(Class);
            StudentTableDelete(Class);
            GroupWeekGradeTableDelete(Class);
        }

        private void StudentTableDelete(Class Class)
        {
            Context.Database.ExecuteSqlCommand($@"
                                                DROP TABLE [dbo].[{Class.StudentsTable}]
                                                ");
        }

        private void GradeTableDelete(Class Class)
        {
            Context.Database.ExecuteSqlCommand($@"
                                                DROP TABLE [dbo].[{Class.GradesTable}]
                                                ");
        }

        private void PrizeTableDelete(Class Class)
        {
            Context.Database.ExecuteSqlCommand($@"
                                                DROP TABLE [dbo].[{Class.PrizesTable}]
                                                ");
        }

        private void GroupWeekGradeTableDelete(Class Class)
        {
            Context.Database.ExecuteSqlCommand($@"
                                                DROP TABLE [dbo].[{Class.GroupWeekGradeTable}]
                                                ");
        }
    }
}
