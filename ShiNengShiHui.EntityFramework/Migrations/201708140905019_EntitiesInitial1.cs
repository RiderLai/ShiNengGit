namespace ShiNengShiHui.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class EntitiesInitial1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Classes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                        InTime = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Class_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.testGrades",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        DateJson = c.String(nullable: false),
                        GradeStringJson = c.String(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                        Student_Id = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Grade_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.testStudents", t => t.Student_Id, cascadeDelete: true)
                .Index(t => t.Student_Id);
            
            CreateTable(
                "dbo.testStudents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 10),
                        sex = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                        Class_Id = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Student_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Classes", t => t.Class_Id, cascadeDelete: true)
                .Index(t => t.Class_Id);
            
            CreateTable(
                "dbo.testPrizes",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        DateJosn = c.String(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                        PrizeItem_Id = c.Guid(nullable: false),
                        Student_Id = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Prize_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PrizeItems", t => t.PrizeItem_Id, cascadeDelete: true)
                .ForeignKey("dbo.testStudents", t => t.Student_Id, cascadeDelete: true)
                .Index(t => t.PrizeItem_Id)
                .Index(t => t.Student_Id);
            
            CreateTable(
                "dbo.PrizeItems",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Teachers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 10),
                        Sex = c.Boolean(nullable: false),
                        BirthDay = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                        Class_Id = c.Int(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Teacher_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Classes", t => t.Class_Id)
                .Index(t => t.Class_Id);
            
            AddColumn("dbo.AbpUsers", "Teacher_Id", c => c.Int());
            CreateIndex("dbo.AbpUsers", "Teacher_Id");
            AddForeignKey("dbo.AbpUsers", "Teacher_Id", "dbo.Teachers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AbpUsers", "Teacher_Id", "dbo.Teachers");
            DropForeignKey("dbo.Teachers", "Class_Id", "dbo.Classes");
            DropForeignKey("dbo.Grades", "Student_Id", "dbo.Students");
            DropForeignKey("dbo.Prizes", "Student_Id", "dbo.Students");
            DropForeignKey("dbo.Prizes", "PrizeItem_Id", "dbo.PrizeItems");
            DropForeignKey("dbo.Students", "Class_Id", "dbo.Classes");
            DropIndex("dbo.Teachers", new[] { "Class_Id" });
            DropIndex("dbo.AbpUsers", new[] { "Teacher_Id" });
            DropIndex("dbo.testPrizes", new[] { "Student_Id" });
            DropIndex("dbo.testPrizes", new[] { "PrizeItem_Id" });
            DropIndex("dbo.testStudents", new[] { "Class_Id" });
            DropIndex("dbo.testGrades", new[] { "Student_Id" });
            DropColumn("dbo.AbpUsers", "Teacher_Id");
            DropTable("dbo.Teachers",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Teacher_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.PrizeItems");
            DropTable("dbo.testPrizes",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Prize_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.testStudents",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Student_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.testGrades",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Grade_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.Classes",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Class_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
